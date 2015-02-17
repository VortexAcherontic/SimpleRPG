using UnityEngine;
using System.Collections;

public class registrierung : MonoBehaviour {
		public string loginname;
		public string passwort;
		public string error_message;
		public int step = 0;
		CreatureOriginData newPlayer = new CreatureOriginData ();
		PlayerBehaviour p001;

		string Datenbank_URL = "http://www.cards-of-destruction.com/SimpleRpg/";

		// Use this for initialization
		void Start () {
				p001 = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerBehaviour> ();
		}
		void OnGUI () {
				if (step > 0) {
						Rect Anzeigebereich = new Rect (5, 5, Screen.width - 5, Screen.height - 5);
						Rect ErsteZeile = new Rect (0, 20, Anzeigebereich.width, 20);
						Rect Spalte = ErsteZeile;
						GUI.Box (Anzeigebereich, "Char Erstellen");
						GUILayout.BeginArea (Anzeigebereich);
						if (step == 1) {
								GUI.Label (ErsteZeile, error_message);
								ErsteZeile.position = new Vector2 (ErsteZeile.position.x, ErsteZeile.position.y + ErsteZeile.height);
								Spalte = ErsteZeile;
								Spalte.width = ErsteZeile.width / 2;
								GUI.Label (Spalte, "Username: ");
								Spalte.position = new Vector2 (Spalte.position.x + Spalte.width, Spalte.position.y);
								loginname = GUI.TextField (Spalte, loginname);
								ErsteZeile.position = new Vector2 (ErsteZeile.position.x, ErsteZeile.position.y + ErsteZeile.height);
								Spalte = ErsteZeile;
								Spalte.width = ErsteZeile.width / 2;
								GUI.Label (Spalte, "Password: ");
								Spalte.position = new Vector2 (Spalte.position.x + Spalte.width, Spalte.position.y);
								passwort = GUI.TextField (Spalte, passwort);
								ErsteZeile.position = new Vector2 (ErsteZeile.position.x, ErsteZeile.position.y + ErsteZeile.height);
								if (GUI.Button (ErsteZeile, "Next")) {
										step = -1;
										StartCoroutine (account_generieren (loginname, passwort));
								}
						}
						if (step == 2) {
								difficulty (ErsteZeile); //zuerst anzeigen
						}
						if (step == 3) {
								charaktererstellung (ErsteZeile); // als zweiten anzeigen
						}
						if (step == 4) {
								p001.me.Create (newPlayer);
								StartCoroutine (GameObject.FindGameObjectWithTag ("Player").GetComponent<Player_Save> ().Login (loginname, passwort));
								//StartCoroutine (GameObject.FindGameObjectWithTag ("Player").GetComponent<Player_Save> ().Save ());
								//StartCoroutine (GameObject.FindGameObjectWithTag ("Player").GetComponent<Player_Save> ().Load ());
								step = -1;
						}
						GUILayout.EndArea ();
				}
				
		}

		IEnumerator account_generieren (string loginname, string password) {
				// Datenbank_URL
				WWWForm LoginForm = new WWWForm ();
				LoginForm.AddField ("user_name", loginname);
				LoginForm.AddField ("user_password", password);
				WWW web = new WWW (Datenbank_URL + "login.php", LoginForm);
				yield return web;
				if (web.text != "-1") {
						// Login Gut!
						Debug.Log (web.text);
						string[] zeilen = web.text.Split ("\n" [0]);
						string[] tmp_spalten = new string[0];
						foreach (string zeile in zeilen) {
								tmp_spalten = zeile.Split (";" [0]);
						}
						//Player_ID = int.Parse (tmp_spalten [0]);
						CreateEmptyNewPlayer ();
						step = 2;
				} else {
						// Lgoin Bad
						step = 1;
						error_message = "Bad Login";
				}
		}
		
		void CreateEmptyNewPlayer () {
				newPlayer.Name = loginname;
				newPlayer.Level = 1;
				newPlayer.Str = 1;
				newPlayer.Agi = 1;
				newPlayer.Dex = 1;
				newPlayer.Vit = 1;
				newPlayer.Int = 1;
				newPlayer.Luc = 1;
				newPlayer.Position = new Vector2 (80, 80);
				newPlayer.IsRegAble = true;
		}
		void difficulty (Rect ErsteZeile) {
				GUI.Label (ErsteZeile, "Choose your Difficulty:");
				ErsteZeile.position = new Vector2 (ErsteZeile.position.x, ErsteZeile.position.y + ErsteZeile.height);
				if (GUI.Button (new Rect (ErsteZeile), "Easy")) {
						newPlayer.Vit += 5;
						newPlayer.Int += 5;
						newPlayer.Gold += 100;
						step = 3;
				}
				ErsteZeile.position = new Vector2 (ErsteZeile.position.x, ErsteZeile.position.y + ErsteZeile.height);
				if (GUI.Button (new Rect (ErsteZeile), "Hard")) {
						newPlayer.Vit += 1;
						newPlayer.Int += 1;
						newPlayer.Gold += 0;
						step = 3;
				}
				ErsteZeile.position = new Vector2 (ErsteZeile.position.x, ErsteZeile.position.y + ErsteZeile.height);
				if (GUI.Button (new Rect (ErsteZeile), "test")) {
						newPlayer.Vit += 10;
						newPlayer.Int += 10;
						newPlayer.Gold += 10000;
						step = 3;
				}
		}

		void charaktererstellung (Rect ErsteZeile) {
				if (p001 != null) {
						p001 = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerBehaviour> ();
				}
				GUI.Label (ErsteZeile, "Choose your Class:");
				ErsteZeile.position = new Vector2 (ErsteZeile.position.x, ErsteZeile.position.y + ErsteZeile.height);
				if (GUI.Button (new Rect (ErsteZeile), "Melee")) {
						newPlayer.Str += 10;
						newPlayer.Dex += 5;
						newPlayer.Agi += 5;
						newPlayer.Vit += 5;
						step = 4;
				}
				ErsteZeile.position = new Vector2 (ErsteZeile.position.x, ErsteZeile.position.y + ErsteZeile.height);
				if (GUI.Button (new Rect (ErsteZeile), "Range")) {
						newPlayer.Str += 2;
						newPlayer.Dex += 10;
						newPlayer.Agi += 5;
						step = 4;
				}
				ErsteZeile.position = new Vector2 (ErsteZeile.position.x, ErsteZeile.position.y + ErsteZeile.height);
				if (GUI.Button (new Rect (ErsteZeile), "Mage")) {
						newPlayer.Agi += 1;
						newPlayer.Dex += 5;
						newPlayer.Int += 10;
						step = 4;
				}
				ErsteZeile.position = new Vector2 (ErsteZeile.position.x, ErsteZeile.position.y + ErsteZeile.height);
				//return p001;
		}

		// Update is called once per frame
		void Update () {
				if (p001 != null) {
						p001 = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerBehaviour> ();
				}
		}
}

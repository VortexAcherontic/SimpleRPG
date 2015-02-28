using UnityEngine;
using System.Collections;

public class registrierung : MonoBehaviour {
		GUI_Helper GUI_ZoD = new GUI_Helper ();
		public Texture bg;
		public string loginname;
		public string passwort;
		public string passwort2;
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
						Rect Anzeige_All = new Rect (0, 0, 1920, 1080);
						Rect Anzeige_Background = GUI_ZoD.BeginBackground (bg, Anzeige_All);
						{
								Rect Anzeigebereich = new Rect (40, 520, 700, 500);
								Rect ErsteZeile = new Rect (0, 20, Anzeigebereich.width, (Anzeigebereich.height - 20) / 8);
								Rect Spalte = ErsteZeile;
								GUI_ZoD.Box ("Char Erstellen", 11, Anzeigebereich);
								GUI_ZoD.BeginArea ("Char ertellen", Anzeigebereich);
								{
										if (step == 1) {
												GUI_ZoD.Label (error_message, 11, ErsteZeile);
												ErsteZeile.position = new Vector2 (ErsteZeile.position.x, ErsteZeile.position.y + ErsteZeile.height);
												Spalte = ErsteZeile;
												Spalte.width = ErsteZeile.width / 2;
												GUI_ZoD.Label ("Username: ", 11, Spalte);
												Spalte.position = new Vector2 (Spalte.position.x + Spalte.width, Spalte.position.y);
												loginname = GUI_ZoD.TextField (loginname, 11, Spalte);
												ErsteZeile.position = new Vector2 (ErsteZeile.position.x, ErsteZeile.position.y + ErsteZeile.height);
												Spalte = ErsteZeile;
												Spalte.width = ErsteZeile.width / 2;
												GUI_ZoD.Label ("Password: ", 11, Spalte);
												Spalte.position = new Vector2 (Spalte.position.x + Spalte.width, Spalte.position.y);
												passwort = GUI_ZoD.TextField (passwort, 11, Spalte);
												ErsteZeile.position = new Vector2 (ErsteZeile.position.x, ErsteZeile.position.y + ErsteZeile.height);
												Spalte = ErsteZeile;
												Spalte.width = ErsteZeile.width / 2;
												GUI_ZoD.Label ("Confirm Password: ", 11, Spalte);
												Spalte.position = new Vector2 (Spalte.position.x + Spalte.width, Spalte.position.y);
												passwort2 = GUI_ZoD.TextField (passwort2, 11, Spalte);
												ErsteZeile.position = new Vector2 (ErsteZeile.position.x, ErsteZeile.position.y + ErsteZeile.height);
												if ((GUI_ZoD.Button_Text ("Next", 11, ErsteZeile)) && (passwort == passwort2)) {
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
												GameObject.FindGameObjectWithTag ("Player").GetComponent<Player_Save> ().StartGame ();
												step = -1;
										}
								}
								GUI_ZoD.EndArea ();
						}
						GUI_ZoD.EndBackground ();
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
				GUI_ZoD.Label ("Choose your Difficulty:", 11, ErsteZeile);
				ErsteZeile.position = new Vector2 (ErsteZeile.position.x, ErsteZeile.position.y + ErsteZeile.height);
				if (GUI_ZoD.Button_Text ("Easy", 11, new Rect (ErsteZeile))) {
						newPlayer.Vit += 5;
						newPlayer.Int += 5;
						newPlayer.Gold += 100;
						step = 3;
				}
				ErsteZeile.position = new Vector2 (ErsteZeile.position.x, ErsteZeile.position.y + ErsteZeile.height);
				if (GUI_ZoD.Button_Text ("Hard", 11, new Rect (ErsteZeile))) {
						newPlayer.Vit += 1;
						newPlayer.Int += 1;
						newPlayer.Gold += 0;
						step = 3;
				}
				ErsteZeile.position = new Vector2 (ErsteZeile.position.x, ErsteZeile.position.y + ErsteZeile.height);
				if (GUI_ZoD.Button_Text ("Test/Dev", 11, new Rect (ErsteZeile))) {
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
				GUI_ZoD.Label ("Choose your Class:", 11, ErsteZeile);
				ErsteZeile.position = new Vector2 (ErsteZeile.position.x, ErsteZeile.position.y + ErsteZeile.height);
				if (GUI_ZoD.Button_Text ("Melee", 11, new Rect (ErsteZeile))) {
						newPlayer.Str += 10;
						newPlayer.Dex += 5;
						newPlayer.Agi += 5;
						newPlayer.Vit += 5;
						step = 4;
				}
				ErsteZeile.position = new Vector2 (ErsteZeile.position.x, ErsteZeile.position.y + ErsteZeile.height);
				if (GUI_ZoD.Button_Text ("Range", 11, new Rect (ErsteZeile))) {
						newPlayer.Str += 2;
						newPlayer.Dex += 10;
						newPlayer.Agi += 5;
						step = 4;
				}
				ErsteZeile.position = new Vector2 (ErsteZeile.position.x, ErsteZeile.position.y + ErsteZeile.height);
				if (GUI_ZoD.Button_Text ("Mage", 11, new Rect (ErsteZeile))) {
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

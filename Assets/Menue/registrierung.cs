using UnityEngine;
using System.Collections;

public class registrierung : MonoBehaviour {
		int Player_ID;
		public string loginname;
		public string passwort;
		string error_message = "";
		player p001;
		int diff_HP = 1000;
		int diff_MaxHP = 1000;
		int diff_MP = 1000;
		int diff_MaxMP = 1000;
		int diff_pwr = 5;
		int diff_armor = 5;
		int diff_agility = 1;
		int start_lvl = 0;
		int diff_gold = 0;
		int diff_xp = 0;

		public int step = 0;

		string Datenbank_URL = "http://www.cards-of-destruction.com/SimpleRpg/";

		// Use this for initialization
		void Start () {
				p001 = GameObject.Find ("Main Camera").GetComponent<player> ();
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
								StartCoroutine (p001.save (Datenbank_URL, Player_ID));
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
						Player_ID = int.Parse (tmp_spalten [0]);
						step = 2;
				} else {
						// Lgoin Bad
						step = 1;
						error_message = "Bad Login";
				}
		}

		void difficulty (Rect ErsteZeile) {
				GUI.Label (ErsteZeile, "Choose your Difficulty:");
				ErsteZeile.position = new Vector2 (ErsteZeile.position.x, ErsteZeile.position.y + ErsteZeile.height);
				if (GUI.Button (new Rect (ErsteZeile), "Easy")) {
						diff_MaxHP = 1000;
						diff_HP = diff_MaxHP;
						diff_MaxMP = 1000;
						diff_MP = diff_MaxMP;
						step = 3;
				}
				ErsteZeile.position = new Vector2 (ErsteZeile.position.x, ErsteZeile.position.y + ErsteZeile.height);
				if (GUI.Button (new Rect (ErsteZeile), "Hard")) {
						diff_MaxHP = 500;
						diff_HP = diff_MaxHP;
						diff_MaxMP = 500;
						diff_MP = diff_MaxMP;
						step = 3;
				}
				ErsteZeile.position = new Vector2 (ErsteZeile.position.x, ErsteZeile.position.y + ErsteZeile.height);
				if (GUI.Button (new Rect (ErsteZeile), "test")) {
						diff_MaxHP = 1000;
						diff_HP = diff_MaxHP;
						diff_MaxMP = 1000;
						diff_MP = diff_MaxMP;
						diff_gold = 10000;
						step = 3;
				}
		}

		void charaktererstellung (Rect ErsteZeile) {
				if (p001 != null) {
						p001 = GameObject.Find ("Main Camera").GetComponent<player> ();
				}
				GUI.Label (ErsteZeile, "Choose your Class:");
				ErsteZeile.position = new Vector2 (ErsteZeile.position.x, ErsteZeile.position.y + ErsteZeile.height);
				if (GUI.Button (new Rect (ErsteZeile), "Melee")) {
						diff_armor = 7;
						diff_agility = 2;
						diff_pwr = 7;
						diff_MaxHP += 250;
						diff_HP = diff_MaxHP;
						p001.Beginn (diff_HP, diff_MaxHP, diff_MP, diff_MaxMP, diff_xp, start_lvl, diff_gold, diff_pwr, diff_armor, diff_agility, loginname, 80, 80, Player_ID);
						step = 4;
				}
				ErsteZeile.position = new Vector2 (ErsteZeile.position.x, ErsteZeile.position.y + ErsteZeile.height);
				if (GUI.Button (new Rect (ErsteZeile), "Range")) {
						diff_armor = 7;
						diff_agility = 7;
						diff_pwr = 5;
						p001.Beginn (diff_HP, diff_MaxHP, diff_MP, diff_MaxMP, diff_xp, start_lvl, diff_gold, diff_pwr, diff_armor, diff_agility, loginname, 80, 80, Player_ID);
						step = 4;
				}
				ErsteZeile.position = new Vector2 (ErsteZeile.position.x, ErsteZeile.position.y + ErsteZeile.height);
				if (GUI.Button (new Rect (ErsteZeile), "Mage")) {
						diff_armor = 2;
						diff_agility = 5;
						diff_pwr = 5;
						diff_MaxMP += 200;
						diff_MP = diff_MaxMP;
						p001.Beginn (diff_HP, diff_MaxHP, diff_MP, diff_MaxMP, diff_xp, start_lvl, diff_gold, diff_pwr, diff_armor, diff_agility, loginname, 80, 80, Player_ID);
						step = 4;
				}
				ErsteZeile.position = new Vector2 (ErsteZeile.position.x, ErsteZeile.position.y + ErsteZeile.height);
				//return p001;
		}

		// Update is called once per frame
		void Update () {
				if (p001 != null) {
						p001 = GameObject.Find ("Main Camera").GetComponent<player> ();
				}
		}
}

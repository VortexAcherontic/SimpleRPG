using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class mainmenu : MonoBehaviour {
		bool showoptions;
		public bool showsettings = false;
		public bool showinv = false;
		public bool showmap = false;
		public bool showequip = false;
		public bool showgamemenue = true;
		public bool showlogin = false;
		public bool gameloaded = false;
		public Texture bg;
		PlayerBehaviour p001;
		public bool debugmode = false;
		
		// Use this for initialization
		void Start () {
				p001 = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerBehaviour> ();
				//p001.Beginn (1000, 1000, 1000, 1000, 80, 0, 100000, 0, 0, 0, "sascha", 50, 50);
				SkillAndKeys tmp = new SkillAndKeys ();
				tmp.action = "MoveForward";
				tmp.key = forwardkey;
				KeySettings.Add (tmp);
				tmp.action = "MoveBackward";
				tmp.key = backwardskey;
				KeySettings.Add (tmp);
				tmp.action = "MoveLeft";
				tmp.key = leftkey;
				KeySettings.Add (tmp);
				tmp.action = "MoveRight";
				tmp.key = rightkey;
				KeySettings.Add (tmp);
		
				tmp.action = "Interact";
				tmp.key = interact;
				KeySettings.Add (tmp);
				tmp.action = "ShowMap";
				tmp.key = mapkey;
				KeySettings.Add (tmp);
				tmp.action = "ShowInventory";
				tmp.key = invkey;
				KeySettings.Add (tmp);
				tmp.action = "ShowJournal";
				tmp.key = journalkey;
				KeySettings.Add (tmp);
				tmp.action = "ShowCharacter";
				tmp.key = characterkey;
				KeySettings.Add (tmp);
				tmp.action = "UseHealPotion";
				tmp.key = hppotionkey;
				KeySettings.Add (tmp);
		}
	
		// Update is called once per frame
		
		void Update () {
				if (p001 != null) {
						if (Input.GetKeyDown (KeyCode.Escape)) {
								showoptions = !showoptions;
						}
				}
				EndScreen ();
		}
	
		void Startscreen () {
				if (showgamemenue) {
						GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), bg);
						//GUI.Label (new Rect (0, 0, 100, 100), "Dies ist ein Text");
						if (GUI.Button (new Rect (145, 425, 170, 45), "")) {
								showgamemenue = false;
								GameObject.Find ("Uebergabe").GetComponent<registrierung> ().step = 1;
						}
						if (GUI.Button (new Rect (145, 475, 170, 45), "")) {
								showgamemenue = false;
								showlogin = true;
						}
						if (GUI.Button (new Rect (145, 525, 170, 45), "")) {
								//Debug.Log ("funzt2");
						}
						if (GUI.Button (new Rect (1250, 0, 30, 30), "")) {
								Application.Quit ();
								Debug.Log ("quit");
						}
				}
		}

		string loginname = "";
		string passwort = "";
		void ShowLogin () {
				if (showlogin) {
						if ((Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)) {
								StartCoroutine (GameObject.FindGameObjectWithTag ("Player").GetComponent<Player_Save> ().Login (loginname, passwort));
								showlogin = false;
						}
						Rect Anzeigebereich = new Rect (5, 5, Screen.width - 5, Screen.height - 5);
						Rect ErsteZeile = new Rect (0, 20, Anzeigebereich.width, 20);
						Rect Spalte = ErsteZeile;
						GUI.Box (Anzeigebereich, "Login");
						//GUILayout.BeginArea (Anzeigebereich);
						//GUI.Label (ErsteZeile, error_message);
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
						if (GUI.Button (ErsteZeile, "Load")) {
								StartCoroutine (GameObject.FindGameObjectWithTag ("Player").GetComponent<Player_Save> ().Login (loginname, passwort));
								showlogin = false;
						}
						
				}
		}

		void options () {
				if (showoptions) {
						Time.timeScale = 0;
						Rect Pausemenue = new Rect (Screen.width / 2 - 50, Screen.height / 2 - 25, 100, 50);
						int Zeilenhoehe = 20;
						Rect ButtonZeile = new Rect (Pausemenue.position.x + 2, Pausemenue.position.y + Zeilenhoehe, Pausemenue.width - 5, Zeilenhoehe);
						GUI.Box (Pausemenue, "Pause");
						if (GUI.Button (ButtonZeile, "Quit Game")) {
								Application.Quit ();
						}
						ButtonZeile.position = new Vector2 (ButtonZeile.position.x, ButtonZeile.position.y + Zeilenhoehe);
						if (GUI.Button (ButtonZeile, "Settings")) {
								showsettings = !showsettings;
						}
				} else {
						Time.timeScale = 1;
				}
		}
	
		void EndScreen () {
				if (p001.Death) {
						Application.LoadLevel ("GameOverScreen");
				}
		}
	
		//Movement
		public string forwardkey = "w";
		public string backwardskey = "s";
		public string rightkey = "d";
		public string leftkey = "a";
		//Misc
		public string interact = "f";
		public string invkey = "i";
		public string mapkey = "m";
		public string hppotionkey = "q";
		public string journalkey = "j";
		public string characterkey = "c";
	
		public List<SkillAndKeys> KeySettings = new List<SkillAndKeys> ();
	
	
	
	
	
		void settings () {
		
				if (showsettings) {
						var centeredStyle = GUI.skin.GetStyle ("Label");
						centeredStyle.alignment = TextAnchor.UpperCenter;
						Rect Setting = new Rect (Screen.width / 2 - 500, Screen.height / 2 - 250, 1000, 500);
						Rect Zeile = new Rect (Setting.position.x + 2, Setting.position.y + 20, Setting.width / 2, 20);
						showoptions = false;
						Time.timeScale = 0;
						GUI.Box (Setting, "Settings");
						GUI.Label (Zeile, "Movement", centeredStyle);
			
						for (int i=0; i<KeySettings.Count; i++) {
								if (i == 4) {
										Zeile.position = new Vector2 (Zeile.position.x, Zeile.position.y + 25);
										GUI.Label (Zeile, "Miscellaneous", centeredStyle);
								}
				
								SkillAndKeys d = KeySettings [i];
								Zeile.position = new Vector2 (Zeile.position.x, Zeile.position.y + 25);
								GUI.Label (Zeile, KeySettings [i].action + ": " + KeySettings [i].key);
								Zeile.position = new Vector2 (Zeile.position.x + Setting.width / 2, Zeile.position.y);
								Zeile.width = 50;
								d.key = GUI.TextField (Zeile, d.key);
								/*Zeile.width = 100;
								if (GUI.Button (Zeile, "assign new key")) {
										
								}
								Zeile.width = Setting.width - 5;*/
								KeySettings [i] = d;
								Zeile.position = new Vector2 (Zeile.position.x - Setting.width / 2, Zeile.position.y);
								Zeile.width = Setting.width / 2;
						}
			
						Zeile.position = new Vector2 (Zeile.position.x, Zeile.position.y + 25);
						
						if (GUI.Button (Zeile, "back")) {
								showoptions = true;
								showsettings = false;
						}
				}
		
		}
	
		void OnGUI () {
				Startscreen ();
				ShowLogin ();
				options ();
				settings ();
		}
}

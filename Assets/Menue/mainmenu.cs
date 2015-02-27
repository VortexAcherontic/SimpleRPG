using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class mainmenu : MonoBehaviour {
		GUI_Helper GUI_ZoD = new GUI_Helper ();
		public static GameObject TheMenue;
	
		bool showoptions;
		public bool showsettings = false;
		public bool showinv = false;
		public bool showmap = false;
		public bool showequip = false;
		public bool showgamemenue = true;
		public bool showlogin = false;
		public bool gameloaded = false;
		public bool cammove = true;
		public Texture bg;
		PlayerBehaviour p001;
		public bool debugmode = false;
	
		void Awake () {
				if (TheMenue != null) {
						if (TheMenue != gameObject) {
								Destroy (gameObject);
						}
				}
				TheMenue = gameObject;
				DontDestroyOnLoad (gameObject);
		}
	
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
	
		void OnGUI () {
				Startscreen ();
				ShowLogin ();
				options ();
				settings ();
		}
	
		void Startscreen () {
				if (showgamemenue) {
						Rect Anzeige_All = new Rect (0, 0, 1920, 1080);
						Rect Anzeige_Background = GUI_ZoD.BeginBackground (bg, Anzeige_All);
						{
								//GUI.Label (new Rect (0, 0, 100, 100), "Dies ist ein Text");
								if (GUI_ZoD.Button_Text ("New Character", 11, new Rect (215, 630, 300, 70))) {
										showgamemenue = false;
										GameObject.Find ("Uebergabe").GetComponent<registrierung> ().step = 1;
										GameObject.Find ("Uebergabe").GetComponent<registrierung> ().bg = bg;
								}
								if (GUI_ZoD.Button_Text ("Load Charater", 11, new Rect (215, 715, 300, 70))) {
										showgamemenue = false;
										showlogin = true;
								}
								if (GUI_ZoD.Button_Text ("Options", 11, new Rect (215, 790, 300, 70))) {
										//
								}
								if (GUI_ZoD.Button_Text ("X", 11, new Rect (1870, 0, 50, 50))) {
										Application.Quit ();
										Debug.Log ("quit");
								}
						}
						GUI_ZoD.EndBackground ();
				}
		}

		string loginname = "";
		string passwort = "";
		void ShowLogin () {
				if (showlogin) {
						Rect Anzeige_All = new Rect (0, 0, 1920, 1080);
						Rect Anzeige_Background = GUI_ZoD.BeginBackground (bg, Anzeige_All);
						{
								if ((Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)) {
										StartCoroutine (GameObject.FindGameObjectWithTag ("Player").GetComponent<Player_Save> ().Login (loginname, passwort));
										showlogin = false;
								}
								Rect Anzeigebereich = new Rect (40, 520, 700, 500);
								Rect ErsteZeile = new Rect (0, 20, Anzeigebereich.width, (Anzeigebereich.height - 20) / 8);
								Rect Spalte = ErsteZeile;
								GUI_ZoD.Box ("Login", Anzeigebereich);
								GUI_ZoD.BeginArea ("LoginBereich", Anzeigebereich);
								{
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
										ErsteZeile.position = new Vector2 (ErsteZeile.position.x, ErsteZeile.position.y + ErsteZeile.height * 2);
										if (GUI_ZoD.Button_Text ("Load", 11, ErsteZeile)) {
												StartCoroutine (GameObject.FindGameObjectWithTag ("Player").GetComponent<Player_Save> ().Login (loginname, passwort));
												showlogin = false;
										}
								}
								GUI_ZoD.EndArea ();
						}
						GUI_ZoD.EndBackground ();
				}
		}

		void options () {
				if (showoptions) {
						cammove = !cammove;
						Time.timeScale = 0;
						Rect Pausemenue = new Rect (1920 / 2 - 50, 1080 / 2 - 25, 100, 50);
						int Zeilenhoehe = 20;
						Rect ButtonZeile = new Rect (Pausemenue.position.x + 2, Pausemenue.position.y + Zeilenhoehe, Pausemenue.width - 5, Zeilenhoehe);
						GUI_ZoD.Box ("Pause", Pausemenue);
						if (GUI_ZoD.Button_Text ("Quit Game", 11, ButtonZeile)) {
								Application.Quit ();
						}
						ButtonZeile.position = new Vector2 (ButtonZeile.position.x, ButtonZeile.position.y + Zeilenhoehe);
						if (GUI_ZoD.Button_Text ("Settings", 11, ButtonZeile)) {
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
						Rect Setting = new Rect (1080 / 2 - 500, 1920 / 2 - 250, 1000, 500);
						Rect Zeile = new Rect (Setting.position.x + 2, Setting.position.y + 20, Setting.width / 2, 20);
						showoptions = false;
						Time.timeScale = 0;
						GUI_ZoD.Box ("Settings", Setting);
						GUI_ZoD.Label ("Movement", 11, Zeile);
			
						for (int i=0; i<KeySettings.Count; i++) {
								if (i == 4) {
										Zeile.position = new Vector2 (Zeile.position.x, Zeile.position.y + 25);
										GUI_ZoD.Label ("Miscellaneous", 11, Zeile);
								}
				
								SkillAndKeys d = KeySettings [i];
								Zeile.position = new Vector2 (Zeile.position.x, Zeile.position.y + 25);
								GUI_ZoD.Label (KeySettings [i].action + ": " + KeySettings [i].key, 11, Zeile);
								Zeile.position = new Vector2 (Zeile.position.x + Setting.width / 2, Zeile.position.y);
								Zeile.width = 50;
								d.key = GUI_ZoD.TextField (d.key, 11, Zeile);
								KeySettings [i] = d;
								Zeile.position = new Vector2 (Zeile.position.x - Setting.width / 2, Zeile.position.y);
								Zeile.width = Setting.width / 2;
						}
			
						Zeile.position = new Vector2 (Zeile.position.x, Zeile.position.y + 25);
						
						if (GUI_ZoD.Button_Text ("back", 11, Zeile)) {
								showoptions = true;
								showsettings = false;
						}
				}
		
		}
}

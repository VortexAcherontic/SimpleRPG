using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class mainmenu : MonoBehaviour {
		bool showoptions;
		public bool showinv = false;
		public bool showmap = false;
		public bool showequip = false;
		public bool showgamemenue = true;
		public bool showlogin = false;
		public bool gameloaded = false;
		public Texture bg;
		player p001;
		
		bool ismoving = false;
		float movecooldown = 0.2f;
		float movetimer;
		float regcooldown = 0.5f;
		float regtimer;
		bool isregging = false;
		public bool debugmode = false;
		
		// Use this for initialization
		void Start () {
				p001 = GameObject.Find ("Main Camera").GetComponent<player> ();
				//p001.Beginn (1000, 1000, 1000, 1000, 80, 0, 100000, 0, 0, 0, "sascha", 50, 50);
		}
	
		// Update is called once per frame
		
		void Update () {
				if (p001 != null) {
						if (Input.GetKeyDown ("m")) {
								showmap = !showmap;
						}
						if (Input.GetKeyDown ("i")) {
								showinv = !showinv;
						}
						if (Input.GetKeyDown (KeyCode.Escape)) {
								showoptions = !showoptions;
						}
						/*
						if (Input.GetKeyDown ("+")) {
								debugmode = !debugmode;
						}
						*/
						if (Input.GetKey ("w") && (ismoving == false)) { // 
								p001.Move ("w");
								ismoving = true;
								//Bewegungseffekte
								int tileID = GameObject.Find ("Map").GetComponent<TileMap> ().tiles [(int)p001.pos.x, (int)p001.pos.y];
								float moveeffect = GameObject.Find ("Map").GetComponent<TileMap> ().tileTypes [tileID].walkEffect;
								movetimer = moveeffect * movecooldown + p001.movement_delay;
						}
						if (Input.GetKey ("s") && (ismoving == false)) {
								p001.Move ("s");
								ismoving = true;
								//Bewegungseffekte
								int tileID = GameObject.Find ("Map").GetComponent<TileMap> ().tiles [(int)p001.pos.x, (int)p001.pos.y];
								float moveeffect = GameObject.Find ("Map").GetComponent<TileMap> ().tileTypes [tileID].walkEffect;
								movetimer = moveeffect * movecooldown + p001.movement_delay;
						}
						if (Input.GetKey ("a") && (ismoving == false)) {
								p001.Move ("a");
								ismoving = true;
								//Bewegungseffekte
								int tileID = GameObject.Find ("Map").GetComponent<TileMap> ().tiles [(int)p001.pos.x, (int)p001.pos.y];
								float moveeffect = GameObject.Find ("Map").GetComponent<TileMap> ().tileTypes [tileID].walkEffect;
								movetimer = moveeffect * movecooldown + p001.movement_delay;
						}
						if (Input.GetKey ("d") && (ismoving == false)) {
								p001.Move ("d");
								ismoving = true;
								//Bewegungseffekte
								int tileID = GameObject.Find ("Map").GetComponent<TileMap> ().tiles [(int)p001.pos.x, (int)p001.pos.y];
								float moveeffect = GameObject.Find ("Map").GetComponent<TileMap> ().tileTypes [tileID].walkEffect;
								movetimer = moveeffect * movecooldown + p001.movement_delay;
						}
						if ((ismoving == false) && (isregging)) {
								p001.hp += p001.maxhp / 1000;
								p001.mana += p001.maxmana / 1000;
								if (p001.hp >= p001.maxhp) {
										p001.hp = p001.maxhp;
								}
								if (p001.mana >= p001.maxmana) {
										p001.mana = p001.maxmana;
								}
								isregging = false;
								regtimer = regcooldown;
						}
						movetimer -= Time.deltaTime;
						regtimer -= Time.deltaTime;
						if (movetimer <= 0) {
								ismoving = false;
						}
						if (regtimer <= 0) {
								isregging = true;
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
								GameObject.Find ("Main Camera").GetComponent<registrierung> ().step = 1;
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

		bool lvlupanzeige = false;
		void Lvlupscreen () {
				if (p001 != null) {
						if (p001.skillpoints > 0) {
								if (GUI.Button (new Rect (Screen.width / 2 - 100, Screen.height / 2 - 10, 200, 20), "LevelUp - Skillpoints available!")) {
										lvlupanzeige = true;
								}
						} else {
								lvlupanzeige = false;
						}
				}
				if (lvlupanzeige) {
						GUI.Label (new Rect (600, 460, 100, 50), "Skillpoints : " + p001.skillpoints);
						GUI.Label (new Rect (600, 260, 100, 50), "Lvl = " + p001.lvl);
						GUI.Label (new Rect (600, 310, 100, 50), "MaxHp = " + p001.maxhp);
						GUI.Label (new Rect (600, 360, 100, 50), "MaxMp = " + p001.maxmana);
						GUI.Label (new Rect (600, 410, 100, 50), "Power = " + p001.pwr);
						if (GUI.Button (new Rect (700, 310, 100, 50), "+ HP")) {
								p001.maxhp += 200;
								p001.skillpoints--;
						}
						if (GUI.Button (new Rect (700, 360, 100, 50), "+ MP")) {
								p001.maxmana += 400;
								p001.skillpoints--;
						}
						if (GUI.Button (new Rect (700, 410, 100, 50), "+ Power")) {
								p001.pwr += 5;
								p001.skillpoints--;
						}
			
				}
		}
		
		void shopanzeige () {
				// Direkt in Shop
		}
	
		string loginname = "";
		string passwort = "";
		void ShowLogin () {
				if (showlogin) {
						if ((Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)) {
								StartCoroutine (p001.load ("http://www.cards-of-destruction.com/SimpleRpg/", loginname, passwort));
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
								StartCoroutine (p001.load ("http://www.cards-of-destruction.com/SimpleRpg/", loginname, passwort));
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
				} else {
						Time.timeScale = 1;
				}
		}
	
		void EndScreen () {
				if (p001.gameover) {
						Application.LoadLevel ("GameOverScreen");
				}
		}
	
		void OnGUI () {
				Startscreen ();
				Lvlupscreen ();
				shopanzeige ();
				ShowLogin ();
				options ();
		}
}

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
		PlayerBehaviour p001;
		
		bool ismoving = false;
		float movecooldown = 0.2f;
		float movetimer;
		float regcooldown = 0.5f;
		float regtimer;
		bool isregging = false;
		public bool debugmode = false;
		
		// Use this for initialization
		void Start () {
				p001 = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerBehaviour> ();
				//p001.Beginn (1000, 1000, 1000, 1000, 80, 0, 100000, 0, 0, 0, "sascha", 50, 50);
		}
	
		// Update is called once per frame
		
		void Update () {
				if (p001 != null) {
						if (Input.GetKeyDown ("m")) {
								showmap = !showmap;
						}
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
				} else {
						Time.timeScale = 1;
				}
		}
	
		void EndScreen () {
				if (p001.Death) {
						Application.LoadLevel ("GameOverScreen");
				}
		}
	
		void OnGUI () {
				Startscreen ();
				ShowLogin ();
				options ();
		}
}

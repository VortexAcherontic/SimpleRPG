using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class mainmenu : MonoBehaviour {
		public bool showinv = false;
		public bool showmap = false;
		public bool showequip = false;
		public bool showgamemenue = true;
		public bool showlogin = false;
		public Texture bg;
		public shop s001;
		// Use this for initialization
		void Start () {
				p001 = GameObject.Find ("Main Camera").GetComponent<player> ();
				//p001.Beginn (1000, 1000, 1000, 1000, 80, 0, 100000, 0, 0, 0, "sascha", 50, 50);
				s001 = GameObject.Find ("Main Camera").GetComponent<shop> ();
				s001.Create (50, 50);
		}
	
		// Update is called once per frame
		bool ismoving = false;
		float movecooldown = 0.2f;
		float movetimer;
		void Update () {
				if (p001 != null) {
						if (Input.GetKeyDown ("m")) {
								showmap = !showmap;
						}
						if (Input.GetKeyDown ("i")) {
								showinv = !showinv;
						}
						if (Input.GetKey ("w") && (ismoving == false)) { // 
								p001.Move ("w");
								ismoving = true;
								movetimer = movecooldown;
						}
						if (Input.GetKey ("s") && (ismoving == false)) {
								p001.Move ("s");
								ismoving = true;
								movetimer = movecooldown;
						}
						if (Input.GetKey ("a") && (ismoving == false)) {
								p001.Move ("a");
								ismoving = true;
								movetimer = movecooldown;
						}
						if (Input.GetKey ("d") && (ismoving == false)) {
								p001.Move ("d");
								ismoving = true;
								movetimer = movecooldown;
						}
						if (ismoving == false) {
								p001.hp += p001.maxhp / 100;
								p001.mana += p001.maxmana / 100;
								if (p001.hp >= p001.maxhp) {
										p001.hp = p001.maxhp;
								}
								if (p001.mana >= p001.maxmana) {
										p001.mana = p001.maxmana;
								}
						}
						movetimer -= Time.deltaTime;
						if (movetimer <= 0) {
								ismoving = false;
						}
				}
		}
		player p001;
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
		bool imshop = false;
		int anzeige_kat = 0;
		List<items> item_liste = new List<items> ();
		Vector2 scroller = new Vector2 ();
		Rect Scrollbereich; // Weil ka wie sonst XD
		void shopanzeige () {
				if (p001 != null) {
						if ((p001.pos == s001.pos) && (imshop == false)) {
								if (GUI.Button (new Rect (Screen.width / 2 - 100, Screen.height / 2 - 10, 200, 20), "Click here to enter shop!")) {
										item_liste = GameObject.Find ("Main Camera").GetComponent<item> ().Item_List;
										Scrollbereich = new Rect (0, 0, 0, 0);
										imshop = true;
								}
						}
				}
				if (imshop) {
						Rect tmp_anzeige = new Rect (100, 160, 1080, 200);
						GUILayout.BeginArea (tmp_anzeige);
						{
								GUI.Box (new Rect (0, 0, tmp_anzeige.width, tmp_anzeige.height), "Shop");
								GUILayout.Space (20);								
								if (GUI.Button (new Rect (tmp_anzeige.width - 40, 0, 40, 20), "Quit")) {
										imshop = false;
										anzeige_kat = 0;
								}
								GUILayout.BeginHorizontal ();
								if (GUILayout.Button ("Meleeweapons")) {
										anzeige_kat = 1;
								}
								if (GUILayout.Button ("Rangeweapons")) {
										anzeige_kat = 2;
								}
								if (GUILayout.Button ("Armor")) {
										anzeige_kat = 3;
								}
								if (GUILayout.Button ("Potions")) {
										anzeige_kat = 4;
								}
								if (GUILayout.Button ("Ammo")) {
										anzeige_kat = 5;
								}
								if (GUILayout.Button ("Wearables")) {
										anzeige_kat = 6;
								}
								GUILayout.EndHorizontal ();
								GUILayout.FlexibleSpace ();
						}
						Rect AnzeigeScrollView = new Rect (0, 0 + 50, tmp_anzeige.width, tmp_anzeige.height - 40);
						Scrollbereich.position = new Vector2 (0, 0);
						Scrollbereich.width = AnzeigeScrollView.width - 50;
						scroller = GUI.BeginScrollView (AnzeigeScrollView, scroller, Scrollbereich);
				
						Rect Zeile1 = new Rect (0, 0, Scrollbereich.width, 20);
						Rect Spalte;
						switch (anzeige_kat) {
								case 0:
										break;
								case 1:
										foreach (items dieseitem in item_liste) {
					
												if (dieseitem.type == itemtype.Nahkampf) {
														GUILayout.BeginHorizontal ();
														Spalte = new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width / 7, Zeile1.height);
														GUI.Label (Spalte, dieseitem.name);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Price: " + dieseitem.price + " G");
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Items in stock: " + dieseitem.stock / 10);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Physical Damage: " + dieseitem.phy_dmg);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Magical Damage: " + dieseitem.mag_dmg);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Weight: " + dieseitem.gewicht + " kg");
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														if (GUI.Button (Spalte, "Buy")) {
																s001.kaufe_item (dieseitem);
														}
														Zeile1.position = new Vector2 (0, Zeile1.position.y + 20);
						
														GUILayout.EndHorizontal ();
												}
										}
										break;
								case 2:
										foreach (items dieseitem in item_liste) {
					
												if (dieseitem.type == itemtype.Fernkampf) {
														GUILayout.BeginHorizontal ();
														Spalte = new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width / 7, Zeile1.height);
														GUI.Label (Spalte, dieseitem.name);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Price: " + dieseitem.price + " G");
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Items in stock: " + dieseitem.stock / 10);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Physical Damage: " + dieseitem.phy_dmg);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Magical Damage: " + dieseitem.mag_dmg);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Weight: " + dieseitem.gewicht + " kg");
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														if (GUI.Button (Spalte, "Buy")) {
																s001.kaufe_item (dieseitem);
														}
														Zeile1.position = new Vector2 (0, Zeile1.position.y + 20);
						
														GUILayout.EndHorizontal ();
												}
										}
										break;
								case 3:
										foreach (items dieseitem in item_liste) {
					
												if ((dieseitem.type == itemtype.Kopf_Rüstung) || (dieseitem.type == itemtype.Torso_Rüstung) || (dieseitem.type == itemtype.Beine_Rüstung) || (dieseitem.type == itemtype.Stiefel_Rüstung) || (dieseitem.type == itemtype.Handschuhe_Rüstung)) {
														GUILayout.BeginHorizontal ();
														Spalte = new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width / 7, Zeile1.height);
														GUI.Label (Spalte, dieseitem.name);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Price: " + dieseitem.price + " G");
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Items in stock: " + dieseitem.stock / 10);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Physical Defense: " + dieseitem.phy_arm);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Magical Defense: " + dieseitem.mag_arm);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Weight: " + dieseitem.gewicht + " kg");
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														if (GUI.Button (Spalte, "Buy")) {
																s001.kaufe_item (dieseitem);
														}
														Zeile1.position = new Vector2 (0, Zeile1.position.y + 20);
						
														GUILayout.EndHorizontal ();
												}
										}
										break;
								case 4:
										foreach (items dieseitem in item_liste) {
					
												if (dieseitem.type == itemtype.Tränke) {
														GUILayout.BeginHorizontal ();
														Spalte = new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width / 6, Zeile1.height);
														GUI.Label (Spalte, dieseitem.name);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Price: " + dieseitem.price + " G");
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Items in stock: " + dieseitem.stock / 10);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Effect: " + dieseitem.effect + " " + dieseitem.effecttyp);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Weight: " + dieseitem.gewicht + " kg");
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														if (GUI.Button (Spalte, "Buy")) {
																s001.kaufe_item (dieseitem);
														}
														Zeile1.position = new Vector2 (0, Zeile1.position.y + 20);
						
														GUILayout.EndHorizontal ();
												}
										}
										break;
								case 5:
										foreach (items dieseitem in item_liste) {
					
												if (dieseitem.type == itemtype.Munition) {
														GUILayout.BeginHorizontal ();
														Spalte = new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width / 8, Zeile1.height);
														GUI.Label (Spalte, dieseitem.name);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Price: " + dieseitem.price + " G");
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Items in stock: " + dieseitem.stock / 10);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Physical Damage: " + dieseitem.phy_dmg);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Magical Damage: " + dieseitem.mag_dmg);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Amount: " + dieseitem.ammo_ammount);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Weight: " + dieseitem.gewicht + " kg");
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														if (GUI.Button (Spalte, "Buy")) {
																s001.kaufe_item (dieseitem);
														}
														Zeile1.position = new Vector2 (0, Zeile1.position.y + 20);
						
														GUILayout.EndHorizontal ();
												}
										}
										break;
								case 6:
										foreach (items dieseitem in item_liste) {
					
												if (dieseitem.type == itemtype.anglegbares) {
														GUILayout.BeginHorizontal ();
														Spalte = new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width / 6, Zeile1.height);
														GUI.Label (Spalte, dieseitem.name);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Price: " + dieseitem.price + " G");
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Items in stock: " + dieseitem.stock / 10);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Effect: " + dieseitem.effecttyp);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Weight: " + dieseitem.gewicht + " kg");
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														if (GUI.Button (Spalte, "Buy")) {
																s001.kaufe_item (dieseitem);
														}
														Zeile1.position = new Vector2 (0, Zeile1.position.y + 20);
						
														GUILayout.EndHorizontal ();
												}
										}
										break;

						}
						GUI.EndScrollView ();
						Scrollbereich.height = Zeile1.position.y + Zeile1.height;
						GUILayout.EndArea ();
						if (Input.GetKeyDown (KeyCode.Escape)) {
								imshop = false;
						}
				}

		}
		string loginname = "";
		string passwort = "";
		void ShowLogin () {
				if (showlogin) {
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

		void OnGUI () {
				Startscreen ();
				Lvlupscreen ();
				shopanzeige ();
				ShowLogin ();
		}
}

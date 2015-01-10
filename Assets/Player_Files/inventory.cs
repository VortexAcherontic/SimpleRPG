using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class inventory : MonoBehaviour
{

		public List<items> Inventar = new List<items> ();
		//List<items> templist = new List<items> ();
		public mainmenu gui;
		int a;
		player p001;
		string Datenbank_URL = "http://www.cards-of-destruction.com/SimpleRpg/";

		// Use this for initialization
		void Start ()
		{
				p001 = GameObject.Find ("Main Camera").GetComponent<player> ();
				gui = GameObject.Find ("Main Camera").GetComponent<mainmenu> ();
		}

		int anzeige_kat = 0;
		Vector2 scroller = new Vector2 ();
		Rect Scrollbereich; // Weil ka wie sonst XD
		/*
		private static bool FindItem (items obj)
		{
		
				if (obj.name == tmpname) {
						return true;
				} else {
						return false;
				}
		
		}
*/
		int ItemCount (items obj)
		{
				a = 0;
				//templist = Inventar.FindAll (FindItem);
				foreach (items c_obj in Inventar) {
						if (c_obj.name == obj.name) {
								a++;
						}
				}
				//a = templist.Count;
				return a;
		}	

		void OnGUI ()
		{
				if (gui.showequip == true) {
						gui.showinv = false;
				}
		   
				Rect tmp_anzeige = new Rect (Screen.width / 2 - 500, Screen.height / 2 - 200, 1000, 400);
				Rect Spalte;
				Rect Zeile1 = new Rect (0, 0, Scrollbereich.width, 20);
				Rect AnzeigeScrollView = new Rect (0, 0 + 50, tmp_anzeige.width, tmp_anzeige.height - 40);
				Scrollbereich.position = new Vector2 (0, 0);
				Scrollbereich.width = AnzeigeScrollView.width - 50;

				if (gui.showinv) {
						
						GUILayout.BeginArea (tmp_anzeige);
						{
								GUI.Box (new Rect (0, 0, tmp_anzeige.width, tmp_anzeige.height), "Inventar");
								GUILayout.Space (20);								
								if (GUI.Button (new Rect (tmp_anzeige.width - 40, 0, 40, 20), "Quit")) {
										gui.showinv = false;
								}
								if (GUI.Button (new Rect (tmp_anzeige.width - 190, 0, 40, 20), "Save")) {
										StartCoroutine (p001.save (Datenbank_URL, p001.Player_ID));
								}
								if (GUI.Button (new Rect (tmp_anzeige.width - 150, 0, 110, 20), "Swap to Equipped Items")) {
										gui.showinv = false;
										gui.showequip = true;
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
						
						scroller = GUI.BeginScrollView (AnzeigeScrollView, scroller, Scrollbereich);
						
						switch (anzeige_kat) {
						case 0:
								break;
						case 1:
								foreach (items dieseitem in Inventar) {
					
										if (dieseitem.type == itemtype.Nahkampf) {
												GUILayout.BeginHorizontal ();
												Spalte = new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width / 7, Zeile1.height);
												GUI.Label (Spalte, dieseitem.name);
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												GUI.Label (Spalte, "Items in inventory: " + ItemCount (dieseitem));
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												GUI.Label (Spalte, "Physical Damage: " + dieseitem.phy_dmg);
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												GUI.Label (Spalte, "Magical Damage: " + dieseitem.mag_dmg);
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												GUI.Label (Spalte, "Weight: " + dieseitem.gewicht + " kg");
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												if (GUI.Button (Spalte, "Drop")) {
														sub (dieseitem);
												}
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												if (GUI.Button (Spalte, "Equip")) {
														p001.equip (dieseitem);
														return;
												}
												Zeile1.position = new Vector2 (0, Zeile1.position.y + 20);
						
												GUILayout.EndHorizontal ();
										}
								}
								break;
						case 2:
								foreach (items dieseitem in Inventar) {
					
										if (dieseitem.type == itemtype.Fernkampf) {
												GUILayout.BeginHorizontal ();
												Spalte = new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width / 7, Zeile1.height);
												GUI.Label (Spalte, dieseitem.name);
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												GUI.Label (Spalte, "Items in inventory: " + ItemCount (dieseitem));
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												GUI.Label (Spalte, "Physical Damage: " + dieseitem.phy_dmg);
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												GUI.Label (Spalte, "Magical Damage: " + dieseitem.mag_dmg);
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												GUI.Label (Spalte, "Weight: " + dieseitem.gewicht + " kg");
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												if (GUI.Button (Spalte, "Drop")) {
														sub (dieseitem);
												}
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												if (GUI.Button (Spalte, "Equip")) {
														p001.equip (dieseitem);
														return;
												}
												Zeile1.position = new Vector2 (0, Zeile1.position.y + 20);
						
												GUILayout.EndHorizontal ();
										}
								}
								break;
						case 3:
								foreach (items dieseitem in Inventar) {
					
										if ((dieseitem.type == itemtype.Kopf_Rüstung) || (dieseitem.type == itemtype.Torso_Rüstung) || (dieseitem.type == itemtype.Beine_Rüstung) || (dieseitem.type == itemtype.Stiefel_Rüstung) || (dieseitem.type == itemtype.Handschuhe_Rüstung)) {
												GUILayout.BeginHorizontal ();
												Spalte = new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width / 7, Zeile1.height);
												GUI.Label (Spalte, dieseitem.name);
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												GUI.Label (Spalte, "Items in inventory: " + ItemCount (dieseitem));
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												GUI.Label (Spalte, "Physical Defense: " + dieseitem.phy_arm);
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												GUI.Label (Spalte, "Magical Defense: " + dieseitem.mag_arm);
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												GUI.Label (Spalte, "Weight: " + dieseitem.gewicht + " kg");
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												if (GUI.Button (Spalte, "Drop")) {
														sub (dieseitem);
												}
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												if (GUI.Button (Spalte, "Equip")) {
														p001.equip (dieseitem);
														return;
												}
												Zeile1.position = new Vector2 (0, Zeile1.position.y + 20);
						
												GUILayout.EndHorizontal ();
										}
								}
								break;
						case 4:
								foreach (items dieseitem in Inventar) {
					
										if (dieseitem.type == itemtype.Tränke) {
												GUILayout.BeginHorizontal ();
												Spalte = new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width / 6, Zeile1.height);
												GUI.Label (Spalte, dieseitem.name);
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												GUI.Label (Spalte, "Items in inventory: " + ItemCount (dieseitem));
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												GUI.Label (Spalte, "Effect: " + dieseitem.effect + " " + dieseitem.effecttyp);
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												GUI.Label (Spalte, "Weight: " + dieseitem.gewicht + " kg");
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												if (GUI.Button (Spalte, "Drop")) {
														sub (dieseitem);
												}
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												if (GUI.Button (Spalte, "Use")) {/*usefunktion*/
												}
												Zeile1.position = new Vector2 (0, Zeile1.position.y + 20);
						
												GUILayout.EndHorizontal ();
										}
								}
								break;
						case 5:
								foreach (items dieseitem in Inventar) {
					
										if (dieseitem.type == itemtype.utility) {
												GUILayout.BeginHorizontal ();
												Spalte = new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width / 7, Zeile1.height);
												GUI.Label (Spalte, dieseitem.name);
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												GUI.Label (Spalte, "Items in inventory: " + ItemCount (dieseitem));
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												GUI.Label (Spalte, "Capacity: " + dieseitem.capacity.Count);
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												GUI.Label (Spalte, "Weight: " + dieseitem.gewicht + " kg");
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												if (GUI.Button (Spalte, "Drop")) {
														sub (dieseitem);
												}
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												if (GUI.Button (Spalte, "Equip")) {
														p001.equip (dieseitem);
														return;
												}
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												if (GUI.Button (Spalte, "Show content.")) {
														//ShowContentfunktion
												}
												Zeile1.position = new Vector2 (0, Zeile1.position.y + 20);
						
												GUILayout.EndHorizontal ();
										}
								}
								break;
						case 6:
								foreach (items dieseitem in Inventar) {
					
										if (dieseitem.type == itemtype.anglegbares) {
												GUILayout.BeginHorizontal ();
												Spalte = new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width / 6, Zeile1.height);
												GUI.Label (Spalte, dieseitem.name);
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												GUI.Label (Spalte, "Items in inventory: " + ItemCount (dieseitem));
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												GUI.Label (Spalte, "Effect: " + dieseitem.effecttyp);
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												GUI.Label (Spalte, "Weight: " + dieseitem.gewicht + " kg");
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												if (GUI.Button (Spalte, "Drop")) {
														sub (dieseitem);
												}
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												if (GUI.Button (Spalte, "Equip")) {
														p001.equip (dieseitem);
														return;
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
				}
				if (gui.showequip) {
						GUILayout.BeginArea (tmp_anzeige);
						{

								GUI.Box (new Rect (0, 0, tmp_anzeige.width, tmp_anzeige.height), "Equip");
								GUILayout.Space (20);								
								if (GUI.Button (new Rect (tmp_anzeige.width - 40, 0, 40, 20), "Quit")) {
										gui.showequip = false;
								}
								if (GUI.Button (new Rect (tmp_anzeige.width - 150, 0, 110, 20), "Swap to Iventory")) {
										gui.showinv = true;
										gui.showequip = false;
								}
								Zeile1.position = new Vector2 (Zeile1.position.x, Zeile1.position.y + 20);

								foreach (items dieseitem in p001.Equip) {

										GUILayout.BeginHorizontal ();
										Spalte = new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width / 7, Zeile1.height);
										GUI.Label (Spalte, dieseitem.name);
										Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
										GUI.Label (Spalte, "Items in inventory: " + ItemCount (dieseitem));
										Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
										switch (dieseitem.type) {
										case itemtype.Nahkampf:
										case itemtype.Fernkampf:
												GUI.Label (Spalte, "Physical Damage: " + dieseitem.phy_dmg);
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												GUI.Label (Spalte, "Magical Damage: " + dieseitem.mag_dmg);
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												break;
										case itemtype.Kopf_Rüstung:
										case itemtype.Torso_Rüstung:
										case itemtype.Beine_Rüstung:
										case itemtype.Stiefel_Rüstung:
										case itemtype.Handschuhe_Rüstung:
												GUI.Label (Spalte, "Physical Defense: " + dieseitem.phy_arm);
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												GUI.Label (Spalte, "Magical Defense: " + dieseitem.mag_arm);
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												break;
										case itemtype.anglegbares:
										case itemtype.Tränke:
												GUI.Label (Spalte, "Effect: " + dieseitem.effect);
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												GUI.Label (Spalte, "" + dieseitem.effecttyp);
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												break;
										case itemtype.utility:
												GUI.Label (Spalte, "Capacity: " + dieseitem.capacity.Count + " / " + dieseitem.maxcapacity);
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												GUI.Label (Spalte, "");
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												break;

										}

										GUI.Label (Spalte, "Weight: " + dieseitem.gewicht + " kg");
										Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
										if (GUI.Button (Spalte, "Drop")) {
												sub (dieseitem);
										}
										Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
										if (GUI.Button (Spalte, "Unequip")) {
												p001.unequip (dieseitem);
												return;
										}
										Zeile1.position = new Vector2 (0, Zeile1.position.y + 20);
					
										GUILayout.EndHorizontal ();
								}
						}
						GUILayout.EndArea ();
				}
		}

		public void add (items diesesitem)
		{
				Inventar.Add (diesesitem);
		}

		public void sub (items diesesitem)
		{
				Inventar.Remove (diesesitem);
		}

		// Update is called once per frame
		void Update ()
		{
	
		}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct ObjShop {
		public int ShopID;
		public Vector2 pos;
}

public class shop : MonoBehaviour {
		List<ObjShop> shops = new List<ObjShop> ();
		player p001;
		List<items> item_liste = new List<items> ();
		bool imshop = false;
		int anzeige_kat = 0;
		Vector2 scroller = new Vector2 ();
		Rect Scrollbereich; // Weil ka wie sonst XD
	
		public ObjShop CreateEmptyShop (Vector2 Pos) {
				ObjShop newShop;
				newShop.ShopID = shops.Count + 1;
				newShop.pos = Pos;
				return newShop;
		}
		// Use this for initialization
		void Start () {
				p001 = GameObject.Find ("Main Camera").GetComponent<player> ();
				ObjShop newShop = CreateEmptyShop (new Vector2 (80, 80));
				shops.Add (newShop);
		}
	
		void OnGUI () {
				foreach (ObjShop s001 in shops) {
						if (p001 != null) {
								if ((p001.pos == s001.pos) && (imshop == false)) {
										if (GUI.Button (new Rect (Screen.width / 2 - 100, Screen.height / 2 - 10, 200, 20), "Click here to enter shop!")) {
												item_liste = GameObject.Find ("Main Camera").GetComponent<item> ().Item_List;
												Scrollbereich = new Rect (0, 0, 0, 0);
												imshop = true;
										}
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
																kaufe_item (dieseitem);
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
																kaufe_item (dieseitem);
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
																kaufe_item (dieseitem);
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
																kaufe_item (dieseitem);
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
																kaufe_item (dieseitem);
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
																kaufe_item (dieseitem);
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
		
		public bool kaufe_item (items diesesitem) {
				bool check = false;
				if ((p001.gold >= diesesitem.price) && (diesesitem.stock >= 10)) {
						p001.inv.add (diesesitem);
						p001.gold -= diesesitem.price;
						diesesitem.stock -= 10;
						check = true;
				}
				return check;
		}
}

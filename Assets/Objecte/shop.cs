using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct ObjShop {
		public int ShopID;
		public Vector2 pos;
}

public class shop : MonoBehaviour {
		List<ObjShop> shops = new List<ObjShop> ();
		PlayerBehaviour p001;
		List<ItemData> item_liste = new List<ItemData> ();
		List<AmmoData> ammo_liste = new List<AmmoData> ();
		bool imshop = false;
		int anzeige_kat = 0;
		Vector2 scroller = new Vector2 ();
		Rect Scrollbereich; // Weil ka wie sonst XD
		float refill_timer = 1f;
		float refill_cooldown = 10f;
		bool equipcheck = false;
		ItemData Quiver;
	
		public ObjShop CreateEmptyShop (Vector2 Pos) {
				ObjShop newShop;
				newShop.ShopID = shops.Count + 1;
				newShop.pos = Pos;
				return newShop;
		}
		// Use this for initialization
		void Start () {
				p001 = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerBehaviour> ();
				ObjShop newShop = CreateEmptyShop (new Vector2 (80, 80));
				shops.Add (newShop);
				refill_timer = refill_cooldown;
		}
	
		void Update () {
				if (GameObject.Find ("Uebergabe").GetComponent<mainmenu> ().gameloaded) {
						refill_timer -= Time.deltaTime;
						if (refill_timer <= 0) {
								shops_refill ();
								refill_timer = refill_cooldown;
						}
				}
		}
	
		bool CheckForQuiver () {
				equipcheck = false;
				foreach (ItemData c_obj in p001.me.Creat.Equipment) {
						if (c_obj.Type == ItemType.utility) {
								equipcheck = true;
						}
				}
				return equipcheck;
		}
	
		void OnGUI () {
				foreach (ObjShop s001 in shops) {
						if (p001 != null) {
								if ((p001.me.Creat.Position == s001.pos) && (imshop == false)) {
										if (GUI.Button (new Rect (Screen.width / 2 - 100, Screen.height / 2 - 10, 200, 20), "Click here to enter shop!")) {
												item_liste = GameObject.Find ("Uebergabe").GetComponent<item> ().Item_List;
												ammo_liste = GameObject.Find ("Uebergabe").GetComponent<item> ().Ammo_List;
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
								{
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
												foreach (ItemData dieseitem in item_liste) {
						
														if (dieseitem.Type == ItemType.weapon_melee) {
																GUILayout.BeginHorizontal ();
																Spalte = new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width / 7, Zeile1.height);
																GUI.Label (Spalte, dieseitem.Name);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Price: " + dieseitem.Gold + " G");
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Items in stock: " + dieseitem.Stock / 10);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Physical Damage: " + dieseitem.PhyAttack);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Magical Damage: " + dieseitem.MagAttack);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Weight: " + dieseitem.Weigth + " kg");
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
												foreach (ItemData dieseitem in item_liste) {
						
														if (dieseitem.Type == ItemType.weapon_range) {
																GUILayout.BeginHorizontal ();
																Spalte = new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width / 7, Zeile1.height);
																GUI.Label (Spalte, dieseitem.Name);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Price: " + dieseitem.Gold + " G");
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Items in stock: " + dieseitem.Stock / 10);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Physical Damage: " + dieseitem.PhyAttack);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Magical Damage: " + dieseitem.MagAttack);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Weight: " + dieseitem.Weigth + " kg");
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
												foreach (ItemData dieseitem in item_liste) {
						
														if ((dieseitem.Type == ItemType.armor_feet) || 
																(dieseitem.Type == ItemType.armor_hand) ||
																(dieseitem.Type == ItemType.armor_head) ||
																(dieseitem.Type == ItemType.armor_leg) ||
																(dieseitem.Type == ItemType.armor_torso)) {
																GUILayout.BeginHorizontal ();
																Spalte = new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width / 7, Zeile1.height);
																GUI.Label (Spalte, dieseitem.Name);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Price: " + dieseitem.Gold + " G");
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Items in stock: " + dieseitem.Stock / 10);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Physical Defense: " + dieseitem.PhyArmor);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Magical Defense: " + dieseitem.MagArmor);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Weight: " + dieseitem.Weigth + " kg");
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
												foreach (ItemData dieseitem in item_liste) {
						
														if (dieseitem.Type == ItemType.potion) {
																GUILayout.BeginHorizontal ();
																Spalte = new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width / 6, Zeile1.height);
																GUI.Label (Spalte, dieseitem.Name);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Price: " + dieseitem.Gold + " G");
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Items in stock: " + dieseitem.Stock / 10);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Effect: + " + dieseitem.Effect + " " + dieseitem.EffectType.ToString ());
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Weight: " + dieseitem.Weigth + " kg");
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
												if (CheckForQuiver ()) {
														foreach (AmmoData dieseitem in ammo_liste) {
						
																if (dieseitem.Type == ItemType.weapon_ammo) { // Sollte hier nicht utility rein?
																		GUILayout.BeginHorizontal ();
																		Spalte = new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width / 8, Zeile1.height);
																		GUI.Label (Spalte, dieseitem.Name);
																		Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																		GUI.Label (Spalte, "Price: " + dieseitem.Gold + " G");
																		Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																		GUI.Label (Spalte, "Items in stock: " + dieseitem.Stock / 10);
																		Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																		GUI.Label (Spalte, "Physical Damage: " + dieseitem.PhyAttack);
																		Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																		GUI.Label (Spalte, "Magical Damage: " + dieseitem.MagAttack);
																		Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																		GUI.Label (Spalte, "Amount: " + dieseitem.Capacity);
																		Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																		GUI.Label (Spalte, "Weight: " + dieseitem.Weigth + " kg");
																		Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																		if (GUI.Button (Spalte, "Buy")) {
																				kaufe_item_ammo (dieseitem);
																		}
																		Zeile1.position = new Vector2 (0, Zeile1.position.y + 20);
							
																		GUILayout.EndHorizontal ();
																}
														}
												} else {
														GUI.Label (new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width, Zeile1.height), "Equip a quiver before you buy ammo!");
												}
												break;
										case 6:
												foreach (ItemData dieseitem in item_liste) {
						
														if ((dieseitem.Type == ItemType.accessorie) || (dieseitem.Type == ItemType.utility)) {
																GUILayout.BeginHorizontal ();
																Spalte = new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width / 6, Zeile1.height);
																GUI.Label (Spalte, dieseitem.Name);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Price: " + dieseitem.Gold + " G");
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Items in stock: " + dieseitem.Stock / 10);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Effect: + " + dieseitem.Effect + " " + dieseitem.EffectType.ToString ());
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Weight: " + dieseitem.Weigth + " kg");
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
		}
		
		public void shops_refill () {
				item_liste = GameObject.Find ("Uebergabe").GetComponent<item> ().Item_List;
				int count_tmpitem = 0;
				foreach (ItemData otmpitem in item_liste) {
						ItemData tmpitem = otmpitem;
						tmpitem.Stock += tmpitem.RefillMod;
			
						count_tmpitem++;
				}
		}
		
		public bool kaufe_item (ItemData diesesitem) {
				bool check = false;
				if (diesesitem.Type != ItemType.weapon_ammo) {
						if ((p001.me.Creat.Gold >= diesesitem.Gold) && (diesesitem.Stock >= 10)) {
								p001.me.Creat.Inventory.Add (diesesitem);
								p001.me.Creat.Gold -= diesesitem.Gold;
								diesesitem.Stock -= 10;
								check = true;
						}
				}
				return check;
		}
		public bool kaufe_item_ammo (AmmoData diesesitem) {
				bool check = false;
				foreach (ItemData c_obj in p001.me.Creat.Equipment) {
						if (c_obj.Type == ItemType.utility) {
								Quiver = c_obj;
						}
				}
				if (Quiver.Name != "") {
						if (Quiver.Capacity < Quiver.MaxCapacity) {
								if ((p001.me.Creat.Gold >= diesesitem.Gold) && (diesesitem.Stock >= 10)) {
										Quiver.Ammo.Add (diesesitem);
										Quiver.Capacity = Quiver.Ammo.Count;
										p001.me.Creat.Gold -= diesesitem.Gold;
										diesesitem.Stock -= 10;
										check = true;
								}
						}
				}
				return check;
		}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct ObjShop {
		public int ShopID;
		public Vector2 pos;
}

public class shop : MonoBehaviour {
		GUI_Helper GUI_ZoD = new GUI_Helper ();
		List<ObjShop> shops = new List<ObjShop> ();
		PlayerBehaviour p001;
		List<ItemData> item_liste = new List<ItemData> ();
		List<AmmoData> ammo_liste = new List<AmmoData> ();
		public bool imshop = false;
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
		
				item_liste = GameObject.Find ("Uebergabe").GetComponent<item> ().Item_List;
				ammo_liste = GameObject.Find ("Uebergabe").GetComponent<item> ().Ammo_List;
				Scrollbereich = new Rect (0, 0, 0, 0);
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
		
				if (imshop) {
						float abstand = 100;
						Rect tmp_anzeige = new Rect (((1920 / 2) - (1600 / 2)), ((1080 / 2) - (1000 / 2)), 1600, 1000);
						GUI_ZoD.BeginArea ("ShopAnzeige", tmp_anzeige);
						{
								GUI_ZoD.Box ("Shop", 11, new Rect (0, 0, tmp_anzeige.width, tmp_anzeige.height));
								GUILayout.Space (abstand);								
								if (GUI_ZoD.Button_Text ("Quit", 11, new Rect (tmp_anzeige.width - 100, 0, 100, 50))) {
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
								Rect AnzeigeScrollView = new Rect (0, 0 + abstand * 2, tmp_anzeige.width, tmp_anzeige.height - 40);
								Scrollbereich.position = new Vector2 (0, 0);
								Scrollbereich.width = AnzeigeScrollView.width - 50;
								scroller = GUI_ZoD.BeginScrollView (scroller, 11, Scrollbereich, AnzeigeScrollView);
			
								Rect Zeile1 = new Rect (25, 0, Scrollbereich.width - 25, 30);
								Rect Spalte;
								int SpaltenMax = 8;
								if (anzeige_kat == 5) {
										if (CheckForQuiver ()) {
												foreach (AmmoData dieseitem in ammo_liste) {
														if (dieseitem.Type == ItemType.weapon_ammo) {
																//GUILayout.BeginHorizontal ();
																Spalte = new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width / SpaltenMax, Zeile1.height);
																GUI_ZoD.Label (dieseitem.Name, 11, Spalte);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI_ZoD.Label ("Price: " + dieseitem.Gold + " G", 11, Spalte);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI_ZoD.Label ("Items in stock: " + dieseitem.Stock / 10, 11, Spalte);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI_ZoD.Label ("Physical Damage: " + dieseitem.PhyAttack, 11, Spalte);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI_ZoD.Label ("Magical Damage: " + dieseitem.MagAttack, 11, Spalte);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI_ZoD.Label ("Amount: " + dieseitem.Capacity, 11, Spalte);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI_ZoD.Label ("Weight: " + dieseitem.Weigth + " kg", 11, Spalte);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																if (GUI_ZoD.Button_Text ("Buy", 11, Spalte)) {
																		kaufe_item_ammo (dieseitem);
																}
																Zeile1.position = new Vector2 (Zeile1.position.x, Zeile1.position.y + Zeile1.height + 5);
																//GUILayout.EndHorizontal ();
														}
												}
										} else {
												GUI_ZoD.Label ("Equip a quiver before you buy ammo!", 11, new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width, Zeile1.height));
										}
								}
								foreach (ItemData dieseitem in item_liste) {
										bool tmp_should_anzeige = false;
										switch (anzeige_kat) {
												case 0:
														break;
												case 1:
														if (dieseitem.Type == ItemType.weapon_melee) {
																tmp_should_anzeige = true;
														}
														break;
												case 2:
														if (dieseitem.Type == ItemType.weapon_range) {
																tmp_should_anzeige = true;
														}
														break;
												case 3:
														if ((dieseitem.Type == ItemType.armor_feet) || 
																(dieseitem.Type == ItemType.armor_hand) ||
																(dieseitem.Type == ItemType.armor_head) ||
																(dieseitem.Type == ItemType.armor_leg) ||
																(dieseitem.Type == ItemType.armor_torso)) {
																tmp_should_anzeige = true;
														}
														break;
												case 4:
														if (dieseitem.Type == ItemType.potion) {
																tmp_should_anzeige = true;
														}
														break;
												case 5:
														//Ammo Oben Abgefragt
														break;
												case 6:
														if ((dieseitem.Type == ItemType.accessorie) || (dieseitem.Type == ItemType.utility)) {
																tmp_should_anzeige = true;
														}
														break;
										}
										if (tmp_should_anzeige) {
												GUILayout.BeginHorizontal ();
												Spalte = new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width / SpaltenMax, Zeile1.height);
												GUI_ZoD.Label (dieseitem.Name, 11, Spalte);
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												GUI_ZoD.Label ("Price: " + dieseitem.Gold + " G", 11, Spalte);
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												GUI_ZoD.Label ("Items in stock: " + dieseitem.Stock / 10, 11, Spalte);
												switch (dieseitem.Type) {
														case ItemType.weapon_melee:
														case ItemType.weapon_range:
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI_ZoD.Label ("Physical Damage: " + dieseitem.PhyAttack, 11, Spalte);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI_ZoD.Label ("Magical Damage: " + dieseitem.MagAttack, 11, Spalte);
																break;
														case ItemType.armor_feet:
														case ItemType.armor_hand:
														case ItemType.armor_head:
														case ItemType.armor_leg:
														case ItemType.armor_torso:
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI_ZoD.Label ("Physical Defense: " + dieseitem.PhyArmor, 11, Spalte);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI_ZoD.Label ("Magical Defense: " + dieseitem.MagArmor, 11, Spalte);
																break;
														case ItemType.potion:
														case ItemType.accessorie:
														case ItemType.utility:
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI_ZoD.Label ("Effect: + " + dieseitem.Effect + " " + dieseitem.EffectType.ToString (), 11, Spalte);
																break;
												}
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												GUI_ZoD.Label ("Weight: " + dieseitem.Weigth + " kg", 11, Spalte);
												Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
												if (GUI_ZoD.Button_Text ("Buy", 11, Spalte)) {
														kaufe_item (dieseitem);
												}
												Zeile1.position = new Vector2 (Zeile1.position.x, Zeile1.position.y + Zeile1.height + 5);
												GUILayout.EndHorizontal ();
										}
								}
								GUI_ZoD.EndScrollView ();
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

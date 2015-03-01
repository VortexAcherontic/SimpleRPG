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
		item ItemScript;
		public bool imshop = false;
		int anzeige_kat = 0;
		Vector2 scroller = new Vector2 ();
		Rect Scrollbereich; // Weil ka wie sonst XD
		float refill_timer = 1f;
		float refill_cooldown = 10f;
		bool equipcheck = false;
		ItemData Quiver;
		int ausgewaehltesItem = 0;
		int ausgewaehltesTool = 0;
		int seite = 0;
		List<ItemData> ShowItems = new List<ItemData> ();
		List<AmmoData> ShowAmmo = new List<AmmoData> ();
	
	
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
				ItemScript = GameObject.Find ("Uebergabe").GetComponent<item> ();
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
						Rect tmp_anzeige = new Rect (200, 200, 1520, 780);
						GUI_ZoD.BeginArea ("ShopAnzeige", tmp_anzeige);
						{
								Rect BB = new Rect (0, 0, tmp_anzeige.width, 100);
								Rect KB = new Rect (BB.position.x, BB.position.y + BB.height, 200, tmp_anzeige.height - BB.height);
								Rect IB = new Rect (tmp_anzeige.width - 300, KB.position.y, 300, KB.height);
								Rect NB = new Rect (KB.position.x + KB.width, tmp_anzeige.height - 200, tmp_anzeige.width - KB.width - IB.width, 200);
								Rect ITB = new Rect (NB.position.x, KB.position.y, NB.width, KB.height - NB.height);
				
								int Anzahl_Zeilen = 1;
								int Anzahl_Spalten = 1;
								Rect Zeile = new Rect ();
								Rect Spalte = new Rect ();
				
								GUI_ZoD.BeginArea ("ButtonBereich", BB); // ButtonBereich
								{
										Anzahl_Zeilen = 1;
										Anzahl_Spalten = 7;
					
										Zeile = new Rect (0, 0, BB.width, BB.height / Anzahl_Zeilen);
										Spalte = new Rect (Zeile.position.x, Zeile.position.y, Zeile.width / Anzahl_Spalten, Zeile.height);
					
										if ((GUI_ZoD.Button_Text ("Back", 7, Spalte)) || (Input.GetKey (KeyCode.Escape))) {
												imshop = false;
										}
										Spalte.position = new Vector2 (Spalte.position.x + Spalte.width, Spalte.position.y);
										Spalte.position = new Vector2 (Spalte.position.x + Spalte.width, Spalte.position.y);
										Spalte.position = new Vector2 (Spalte.position.x + Spalte.width, Spalte.position.y);
										Spalte.position = new Vector2 (Spalte.position.x + Spalte.width, Spalte.position.y);
										// Platzhalter
										Spalte.position = new Vector2 (Spalte.position.x + Spalte.width, Spalte.position.y);
										GUI_ZoD.Label ("Gold: " + p001.me.Creat.Gold, 5, Spalte);
										Spalte.position = new Vector2 (Spalte.position.x + Spalte.width, Spalte.position.y);
										GUI_ZoD.Label ("Weight: " + p001.me.GGewicht + " / " + p001.me.MaxGGewicht, 5, Spalte);
								}
								GUI_ZoD.EndArea ();
				
								GUI_ZoD.BeginArea ("KategorieBereich", KB);
								{
										anzeige_kat = ItemScript.GUI_ItemKat (KB, anzeige_kat);
								}
								GUI_ZoD.EndArea ();
								GUI_ZoD.BeginArea ("ItemBereich", ITB);
								{
										ShowItems = new List<ItemData> ();
										ShowAmmo = new List<AmmoData> ();
					
										for (int i=0; i<ItemScript.Item_List.Count; i++) {
												ItemData dieseitem = ItemScript.Item_List [i];
												bool tmp_should_anzeige = ItemScript.Check_ItemTypeInKat (dieseitem.Type, anzeige_kat);
												if (tmp_should_anzeige) {
														ShowItems.Add (dieseitem);
												}
										}
										for (int i=0; i<ItemScript.Ammo_List.Count; i++) {
												AmmoData dieseitem = ItemScript.Ammo_List [i];
												bool tmp_should_anzeige = ItemScript.Check_ItemTypeInKat (dieseitem.Type, anzeige_kat);
												if (tmp_should_anzeige) {
														ShowAmmo.Add (dieseitem);
												}
										}
										if (ItemScript.Check_ItemTypeInKat (ItemType.weapon_ammo, anzeige_kat)) {
												ausgewaehltesItem = ItemScript.GUI_AnzeigeItemGrid (ShowItems, ITB, seite, ausgewaehltesItem);
										} else {
												ausgewaehltesItem = ItemScript.GUI_AnzeigeItemGrid (ShowItems, ITB, seite, ausgewaehltesItem);
										}
								}
								GUI_ZoD.EndArea ();
				
								GUI_ZoD.BeginArea ("NavigationsBereich", NB);
								{
										Anzahl_Zeilen = 1;
										Anzahl_Spalten = 2;
					
										Zeile = new Rect (0, 0, NB.width, NB.height / Anzahl_Zeilen);
										Spalte = new Rect (Zeile.position.x, Zeile.position.y, Zeile.width / Anzahl_Spalten, Zeile.height);
					
										GUI_ZoD.Button_Rahmen_weg ();
										if (GUI_ZoD.Button_Text ("DOWN", 7, Spalte)) {
												seite++;
										}
										Spalte.position = new Vector2 (Spalte.position.x + Spalte.width, Spalte.position.y);
										if (GUI_ZoD.Button_Text ("UP", 7, Spalte)) {
												seite--;
										}
										Spalte.position = new Vector2 (Spalte.position.x + Spalte.width, Spalte.position.y);
										GUI_ZoD.Button_Rahmen_hin ();
								}
								GUI_ZoD.EndArea ();
				
								GUI_ZoD.BeginArea ("InfoBereich", IB);
								{
										if (ShowItems.Count >= ausgewaehltesItem + 1) {
												ItemData Anzeige_Item;
												Anzeige_Item = ShowItems [ausgewaehltesItem];
												int FiktiveShopID = 0; // Weil ich nicht weiß wies besser geht
												for (int tmp=0; tmp<ItemScript.Item_List.Count; tmp++) {
														if (Anzeige_Item.Name == ItemScript.Item_List [tmp].Name) {
																FiktiveShopID = tmp;
														}
												}
												ItemScript.GUI_ItemDetails (Anzeige_Item, IB, p001, -1, -1, FiktiveShopID, true);
										}
								}
								GUI_ZoD.EndArea ();
						}
						GUI_ZoD.EndArea ();
				}
		}
		
		public void shops_refill () {
				for (int ct=0; ct<ItemScript.Item_List.Count; ct++) {
						ItemData tmpitem = ItemScript.Item_List [ct];
						tmpitem.Stock += tmpitem.RefillMod;
						ItemScript.Item_List [ct] = tmpitem;
				}
				for (int ct=0; ct<ItemScript.Ammo_List.Count; ct++) {
						AmmoData tmpitem = ItemScript.Ammo_List [ct];
						tmpitem.Stock += tmpitem.RefillMod;
						ItemScript.Ammo_List [ct] = tmpitem;
				}
		}
		
		public bool kaufe_item (ItemData diesesitem) {
				int auswahl = 0;
				for (int ct=0; ct<ItemScript.Item_List.Count; ct++) {
						if (ItemScript.Item_List [ct].Name == diesesitem.Name) {
								auswahl = ct;
						}
				}
				bool check = false;
				if (diesesitem.Type != ItemType.weapon_ammo) {
						if ((p001.me.Creat.Gold >= diesesitem.Gold) && (diesesitem.Stock >= 10)) {
								p001.me.Creat.Inventory.Add (diesesitem);
								p001.me.Creat.Gold -= diesesitem.Gold;
								diesesitem.Stock -= 10;
								ItemScript.Item_List [auswahl] = diesesitem;
								check = true;
						}
				}
				return check;
		}
		public bool kaufe_item_ammo (AmmoData diesesitem) {
				int auswahl = 0;
				for (int ct=0; ct<ItemScript.Ammo_List.Count; ct++) {
						if (ItemScript.Ammo_List [ct].Name == diesesitem.Name) {
								auswahl = ct;
						}
				}
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
										ItemScript.Ammo_List [auswahl] = diesesitem;
										check = true;
								}
						}
				}
				return check;
		}
}

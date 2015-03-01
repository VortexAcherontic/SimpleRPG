using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class item : MonoBehaviour {
		GUI_Helper GUI_ZoD = new GUI_Helper ();
	
		public List<ItemData> Item_List = new List<ItemData> ();
		public List<AmmoData> Ammo_List = new List<AmmoData> ();
		public List<ItemTypeShowKat> ItemKat = new List<ItemTypeShowKat> ();
		
		ItemDataList DataListObj_Item;
		KatTypes DataListObj_Kat;
		
		void Start () {
				LoadDataList ("Items");
				LoadDataList ("Gather");
				LoadKatList ("_ItemKategorieToShowList");
		}
	
		void LoadDataList (string Dateiname) {
				DataListObj_Item = (ItemDataList)Resources.Load (Dateiname);
				foreach (ItemData id in DataListObj_Item.ItemList) {
						Item_List.Add (id);
				}
				foreach (AmmoData id in DataListObj_Item.AmmoList) {
						Ammo_List.Add (id);
				}
		}
		void LoadKatList (string Dateiname) {
				DataListObj_Kat = (KatTypes)Resources.Load (Dateiname);
				foreach (ItemTypeShowKat id in DataListObj_Kat.TypeToShow) {
						ItemKat.Add (id);
				}
		}
	
		public void GUI_ItemDetails (ItemData Item, Rect Bereich, PlayerBehaviour Player, int InventarID, int EquipmentID, int ShopID, bool ShopOpen) {
				int Anzahl_Zeilen = 10;
				float offset = 5;
				Rect Button = new Rect (0, 0, Bereich.width, Bereich.height / Anzahl_Zeilen - offset);
				GUI_ZoD.Label ("<b>" + Item.Name + "</b>", 5, Button);
				Button.position = new Vector2 (Button.position.x, Button.position.y + Button.height + offset);
				switch (Item.Type) {
						case ItemType.weapon_melee:
						case ItemType.weapon_range:
								GUI_ZoD.Label ("Physical Damage: " + Item.PhyAttack, 5, Button);
								Button.position = new Vector2 (Button.position.x, Button.position.y + Button.height + offset);
								GUI_ZoD.Label ("Magical Damage: " + Item.MagAttack, 5, Button);
								Button.position = new Vector2 (Button.position.x, Button.position.y + Button.height + offset);
								GUI_ZoD.Label ("Durability: " + Item.Durability + " / " + Item.MaxDurability, 5, Button);
								Button.position = new Vector2 (Button.position.x, Button.position.y + Button.height + offset);
								break;
						case ItemType.armor_feet:
						case ItemType.armor_hand:
						case ItemType.armor_head:
						case ItemType.armor_leg:
						case ItemType.armor_torso:
								GUI_ZoD.Label ("Physical Defense: " + Item.PhyArmor, 5, Button);
								Button.position = new Vector2 (Button.position.x, Button.position.y + Button.height + offset);
								GUI_ZoD.Label ("Magical Defense: " + Item.MagArmor, 5, Button);
								Button.position = new Vector2 (Button.position.x, Button.position.y + Button.height + offset);
								GUI_ZoD.Label ("Durability: " + Item.Durability + " / " + Item.MaxDurability, 5, Button);
								Button.position = new Vector2 (Button.position.x, Button.position.y + Button.height + offset);
								break;
						case ItemType.potion:
						case ItemType.accessorie:
						case ItemType.utility:
								GUI_ZoD.Label ("Effect: + " + Item.Effect + " " + Item.EffectType.ToString (), 5, Button);
								Button.position = new Vector2 (Button.position.x, Button.position.y + Button.height + offset);
								break;
						case ItemType.tool_gardener:
						case ItemType.tool_herbalist:
						case ItemType.tool_hunter:
						case ItemType.tool_lumberjack:
						case ItemType.tool_miner:
								GUI_ZoD.Label ("Durability: " + Item.Durability + " / " + Item.MaxDurability, 5, Button);
								Button.position = new Vector2 (Button.position.x, Button.position.y + Button.height + offset);
								break;
				}
		
				GUI_ZoD.Label ("Weight: " + Item.Weigth + " kg", 5, Button);
				Button.position = new Vector2 (Button.position.x, Button.position.y + Button.height + offset);
				GUI_ZoD.Label ("Value: " + Item.Gold + " G", 5, Button);
				Button.position = new Vector2 (Button.position.x, Button.position.y + Button.height + offset);
		
				// 6 Zeilen Bisher
				
				// Buttons von unten her find ich cooler
				Button.position = new Vector2 (Button.position.x, Bereich.height - Button.height - offset);
				if (ShopOpen && InventarID >= 0) {
						if (GUI_ZoD.Button_Text ("Verkaufen", 5, Button)) {
								// Verkaufen
						}
						Button.position = new Vector2 (Button.position.x, Button.position.y - Button.height - offset);
				} else if (ShopOpen) {
						if (GUI_ZoD.Button_Text ("Kaufen", 5, Button)) {
								// Kaufen
						}
						Button.position = new Vector2 (Button.position.x, Button.position.y - Button.height - offset);
				} else {
						if (GUI_ZoD.Button_Text ("Drop", 6, Button)) {
								Player.me.Creat.Inventory.RemoveAt (InventarID);
						}
						Button.position = new Vector2 (Button.position.x, Button.position.y - Button.height - offset);
				}
				// Zeilen +1
		
				switch (Item.Type) {
						case ItemType.potion:
								if (GUI_ZoD.Button_Text ("Use", 7, Button)) {
										Player.ItemUse (Player.me.Creat.Inventory [InventarID]);
								}
								Button.position = new Vector2 (Button.position.x, Button.position.y - Button.height - offset);
								break;
				}
				switch (Item.Type) {
						case ItemType.weapon_melee:
						case ItemType.weapon_range:
						case ItemType.armor_feet:
						case ItemType.armor_hand:
						case ItemType.armor_head:
						case ItemType.armor_leg:
						case ItemType.armor_torso:
						case ItemType.utility:
						case ItemType.accessorie:
						case ItemType.tool_gardener:
						case ItemType.tool_herbalist:
						case ItemType.tool_hunter:
						case ItemType.tool_lumberjack:
						case ItemType.tool_miner:
								if (!ShopOpen && InventarID >= 0) {
										if (GUI_ZoD.Button_Text ("Equip", 6, Button)) {
												Player.me.Equip (Item);
										}
										Button.position = new Vector2 (Button.position.x, Button.position.y - Button.height - offset);
								} else if (!ShopOpen) {
										if (GUI_ZoD.Button_Text ("Unequip", 6, Button)) {
												Player.me.Unequip (Item);
										}
										Button.position = new Vector2 (Button.position.x, Button.position.y - Button.height - offset);
								}
								break;
				}
				// Zeilen +1
				
		
		}
		public int GUI_ItemKat (Rect Bereich, int Anzeige_Kat) {
				float offset = 5;
				Rect Button = new Rect (0, 0, Bereich.width, Bereich.height / (ItemKat.Count + 1) - offset);
				for (int i=0; i<ItemKat.Count; i++) {
						if (GUI_ZoD.Button_Text (ItemKat [i].Name, 7, Button)) {
								Anzeige_Kat = i;
						}
						Button.position = new Vector2 (Button.position.x, Button.position.y + Button.height + offset);
				}
				return Anzeige_Kat;
		}
		public int GUI_AnzeigeItemGrid (List<ItemData> ShowItems, Rect Bereich, int Seite, int Ausgewaehlt) {
				float offset = 5;
				Vector2 size = new Vector2 (150, 150);
				int Anzahl_Spalten = (int)(Bereich.width / (size.x + offset));
				int Anzahl_Zeilen = (int)(Bereich.height / (size.y + offset));
				int Aktuelle_Zeile = 0;
				int Aktuelle_Spalte = 0;
				Rect Button = new Rect (0, 0, size.x, size.y);
				int ReturnAusgewaehlt = Ausgewaehlt;
				for (int i=0; i<ShowItems.Count; i++) {
						ItemData TempItem = ShowItems [i];
						Texture2D Icon = TempItem.texture;
						if (i >= Seite * Anzahl_Zeilen * Anzahl_Spalten && i < (Seite + 1) * Anzahl_Zeilen * Anzahl_Spalten) {
								Aktuelle_Spalte++;
								if (GUI_ZoD.Button_Bild (Icon, Button)) {
										ReturnAusgewaehlt = i;
								}
								Button.position = new Vector2 (Button.position.x + Button.width + offset, Button.position.y);
								if (Aktuelle_Spalte >= Anzahl_Spalten) {
										Aktuelle_Spalte = 0;
										Aktuelle_Zeile++;
										Button.position = new Vector2 (0, Button.position.y + Button.height + offset);
								}
						}
				}
				return ReturnAusgewaehlt;
		}
		public bool Check_ItemTypeInKat (ItemType Type, int Anzeige_Kat) {
				bool returnval = false;
				foreach (ItemType checktype in ItemKat[Anzeige_Kat].Types) {
						if (checktype == Type) {
								returnval = true;
						}
				}
				return returnval;
		}
	
		public ItemData item_mit_name (string itembez) {
				foreach (ItemData obj in Item_List) {
						if (obj.Name == itembez) {
								return obj;
						}
				}
				return leeres_item ();
		}
		ItemData leeres_item () {
				ItemData tmp_item = new ItemData ();
				tmp_item.Name = "Dummy Item";
				return tmp_item;
		}
		
}

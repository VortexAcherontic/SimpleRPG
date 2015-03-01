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

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class item : MonoBehaviour {
		public List<ItemData> Item_List = new List<ItemData> ();
		public List<AmmoData> Ammo_List = new List<AmmoData> ();
		public ItemDataList DataListObj;
		public void Start () {
				LoadDataList ("Items");
				LoadDataList ("Gather");
		}
	
		void LoadDataList (string Dateiname) {
				DataListObj = (ItemDataList)Resources.Load (Dateiname);
				foreach (ItemData id in DataListObj.ItemList) {
						Item_List.Add (id);
				}
				foreach (AmmoData id in DataListObj.AmmoList) {
						Ammo_List.Add (id);
				}
		}
	
		ItemData leeres_item () {
				ItemData tmp_item = new ItemData ();
				tmp_item.Name = "Dummy Item";
				return tmp_item;
		}
		
		public ItemData item_mit_name (string itembez) {
				foreach (ItemData obj in Item_List) {
						if (obj.Name == itembez) {
								return obj;
						}
				}
				return leeres_item ();
		}
		
}

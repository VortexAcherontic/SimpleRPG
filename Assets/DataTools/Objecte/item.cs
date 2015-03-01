using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class item : MonoBehaviour {
		public List<ItemData> Item_List = new List<ItemData> ();
		public List<AmmoData> Ammo_List = new List<AmmoData> ();
		public ItemDataList DataListObj;
		public void Start () {
				DataListObj = (ItemDataList)Resources.Load ("Items");
				foreach (ItemData id in DataListObj.ItemList) {
						Item_List.Add (id);
				}
				foreach (AmmoData id in DataListObj.AmmoList) {
						Ammo_List.Add (id);
				}
		}
		
		public ItemData item_mit_name (string itembez) {
				return DataListObj.item_mit_name (itembez);
		}
		
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ItemDataList : ScriptableObject {
		public List<ItemData> ItemList;
		public List<AmmoData> AmmoList;
	
		ItemData leeres_item () {
				ItemData tmp_item = new ItemData ();
				tmp_item.Name = "Dummy Item";
				return tmp_item;
		}
		public ItemData item_mit_name (string itembez) {
				foreach (ItemData obj in ItemList) {
						if (obj.Name == itembez) {
								return obj;
						}
				}
				return leeres_item ();
		}
}
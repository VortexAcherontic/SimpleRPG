using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class item : MonoBehaviour {
		public List<ItemData> Item_List = new List<ItemData> ();
		// Use this for initialization
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
		void Start () {
				ItemDataList tmpobj = (ItemDataList)Resources.Load ("Items");
				Item_List = tmpobj.ItemList; //?
		}
	
		// Update is called once per frame
		void Update () {
	
		}
}

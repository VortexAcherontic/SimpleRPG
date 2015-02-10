using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class item : MonoBehaviour {
		public List<ItemData> Item_List = new List<ItemData> ();
		public ItemDataList DataListObj;
		public void Start () {
				DataListObj = (ItemDataList)Resources.Load ("Items");
				Item_List = DataListObj.ItemList; 
		}
		
		public ItemData item_mit_name (string itembez) {
				return DataListObj.item_mit_name (itembez);
		}
		
}

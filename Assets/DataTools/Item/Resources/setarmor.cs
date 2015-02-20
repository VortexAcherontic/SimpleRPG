using UnityEngine;
using System.Collections.Generic;

public class setarmor : MonoBehaviour {
		public ItemDataList DataListObj;
		public bool enabled = false;
		
		// Use this for initialization
		void Start () {
				if (enabled) {
						List<ItemData> ItemList;
						DataListObj = (ItemDataList)Resources.Load ("Items");
						ItemList = DataListObj.ItemList; 
						foreach (ItemData i in ItemList) {
								if ((i.Type == ItemType.armor_feet) ||
										(i.Type == ItemType.armor_hand) ||
										(i.Type == ItemType.armor_head) ||
										(i.Type == ItemType.armor_torso) ||
										(i.Type == ItemType.armor_leg) ||
										(i.Type == ItemType.weapon_melee) ||
										(i.Type == ItemType.weapon_range)) {
										i.MaxDurability = 100;
										i.Durability = i.MaxDurability;
								}
			    
						}
				}
		}
	
		// Update is called once per frame
		void Update () {
	
		}
}

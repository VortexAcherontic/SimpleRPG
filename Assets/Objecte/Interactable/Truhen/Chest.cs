using UnityEngine;
using System.Collections;

[System.Serializable]
public struct ChestOptions {
		public bool IsLocked;
		public int GoldLoot;
		public string[] ItemLoot;
	
}
public class Chest : MonoBehaviour {
		AudioSource sound;
		Player_Trigger triggerscript;
		Vector3 startangles;
		public ChestOptions optionen;
	
		void Start () {
				sound = GetComponent<AudioSource> ();
				triggerscript = GetComponentInChildren<Player_Trigger> ();
		}
	
		void Update () {
				Interact ();
		}
	
		void Interact () {
				if (triggerscript.Player_in_Triger) {
						if (Input.GetButtonDown ("Interact")) {
								sound.Play ();
								Loot ();
						}
				}
		}
	
		void Loot () {
				item items = GameObject.Find ("Uebergabe").GetComponent<item> ();
				GameObject PlayerObj = triggerscript.Player_Obj;
				PlayerBehaviour Player = PlayerObj.GetComponent<PlayerBehaviour> ();
				if (Player != null) {
						for (int i =0; i<optionen.ItemLoot.Length; i++) {
								string tmp_item = optionen.ItemLoot [i];
								Player.me.Creat.Inventory.Add (items.item_mit_name (tmp_item));
								optionen.ItemLoot [i].Remove (i);
								Notification not = new Notification ();
								not.time = 5;
								not.message = "Get " + tmp_item;
								Player.PickupList.Add (not);
						}
						if (optionen.GoldLoot > 0) {
								Notification not = new Notification ();
								not.time = 5;
								not.message = "Get " + optionen.GoldLoot + " Gold";
								Player.me.Creat.Gold += optionen.GoldLoot;			
								optionen.GoldLoot = 0;
						}
				}
		}
}

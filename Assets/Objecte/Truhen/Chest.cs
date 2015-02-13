using UnityEngine;
using System.Collections;

public class Chest : MonoBehaviour {
		CreatureController me;
		PlayerBehaviour p001;
		void Start () {
				p001 = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerBehaviour> ();
				me = gameObject.GetComponent<CreatureController> ();
		}
	
		void Update () {
				Loot ();
				if (looted) {
						Destroy (gameObject);
				}
		}
	
		bool Interacted () {
				if (interactable ()) {
						if (Input.GetKeyDown ("f")) {
								return true;
						} else {
								return false;
						}
				} else {
						return false;
				}
		}
	
		bool interactable () {
				if ((((me.Creat.Position.x == p001.me.Creat.Position.x + 1) || (me.Creat.Position.x == p001.me.Creat.Position.x - 1)) &&
						(me.Creat.Position.y == p001.me.Creat.Position.y)) || (((me.Creat.Position.y == p001.me.Creat.Position.y + 1) ||
						(me.Creat.Position.y == p001.me.Creat.Position.y - 1)) && (me.Creat.Position.x == p001.me.Creat.Position.x))) {
						return true;
				} else {
						return false;
				}
		}
	
		bool looted = false;
	
		void Loot () {
				if (Interacted ()) {
						foreach (ItemData tmp_item in me.Creat.Inventory) {
								p001.me.Creat.Inventory.Add (tmp_item);
								Notification not = new Notification ();
								not.time = 5;
								not.message = "Get " + tmp_item.Name;
								p001.PickupList.Add (not);
						}
						looted = true;
				}
		}
}

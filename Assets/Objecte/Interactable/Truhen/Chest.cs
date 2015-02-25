using UnityEngine;
using System.Collections;

public class Chest : MonoBehaviour {
		CreatureController me;
		PlayerBehaviour p001;
		float temp_x;
		float temp_y;
	
		int distance_manhatten;
	
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
				if (CheckDistance () <= 2) {
						if (Input.GetKeyDown ("f")) {
								return true;
						} else {
								return false;
						}
				} else {
						return false;
				}
		}
	
		public int CheckDistance () {
				temp_x = Mathf.Abs (p001.me.Creat.Position.x - transform.position.x);
				temp_y = Mathf.Abs (p001.me.Creat.Position.y - transform.position.y);
				//distance_euklid = (int)Mathf.Sqrt (temp_x * temp_x + temp_y * temp_y);
				distance_manhatten = (int)(temp_x + temp_y);
				//Debug.Log ("Distance: " + distance_manhatten);
				return distance_manhatten;
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

using UnityEngine;
using System.Collections;

public class Player_Trigger : MonoBehaviour {
		public bool Player_in_Triger;
		public GameObject Player_Obj;
	
		void OnTriggerEnter (Collider other) {
				if (other.tag == "Player") {
						Player_in_Triger = true;
						Player_Obj = other.gameObject;
				}
		}
		void OnTriggerExit (Collider other) {
				if (other.tag == "Player") {
						Player_in_Triger = false;
						Player_Obj = null;
				}
		}
}

using UnityEngine;
using System.Collections;

public class Player_Trigger : MonoBehaviour {
		public bool Player_in_Triger;
	
		void OnTriggerEnter (Collider other) {
				if (other.tag == "Player") {
						Player_in_Triger = true;
				}
		}
		void OnTriggerExit (Collider other) {
				if (other.tag == "Player") {
						Player_in_Triger = false;
				}
		}
}

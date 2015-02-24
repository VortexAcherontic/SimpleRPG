using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour {
		public mainmenu mn;
		public Transform head;


		void Update () {
				if (mn.cammove) {
						transform.RotateAround (head.position, new Vector3 (1, 0, 0), 5 * Input.GetAxis ("Mouse Y"));
						transform.RotateAround (head.position, new Vector3 (0, 1, 0), 5 * Input.GetAxis ("Mouse X"));
						transform.LookAt (head.position);
				}
		}
	
		void Start () {
				if (GetComponent<Rigidbody> ()) {
						GetComponent<Rigidbody> ().freezeRotation = true;
				}
				mn = GameObject.Find ("Uebergabe").GetComponent<mainmenu> ();
		}
}
using UnityEngine;
using System.Collections.Generic;

public class MouseLook : MonoBehaviour {
		public mainmenu mn;
		public Transform head;
		int s = 50;
		int hhalf = 0;
		int vhalf = 0;

		void Update () {
				if (mn.cammove) {
						camera ();
						if (hhalf == 1) {
								transform.RotateAround (head.position, new Vector3 (0, 1, 0), s / 10);
						} else {
								transform.RotateAround (head.position, new Vector3 (0, 1, 0), 0);
						}
						if (hhalf == -1) {
								transform.RotateAround (head.position, new Vector3 (0, 1, 0), s / ((-1) * 10));
						} else {
								transform.RotateAround (head.position, new Vector3 (0, 1, 0), 0);
						}
						if (vhalf == 1) {
								transform.RotateAround (head.position, new Vector3 (1, 0, 0), s / ((-1) * 10));
						} else {
								transform.RotateAround (head.position, new Vector3 (1, 0, 0), 0);
						}
						if (vhalf == -1) {
								transform.RotateAround (head.position, new Vector3 (1, 0, 0), s / 10);
						} else {
								transform.RotateAround (head.position, new Vector3 (1, 0, 0), 0);
						}
						/*transform.RotateAround (head.position, new Vector3 (1, 0, 0), s / ((-1) * 10) * Input.GetAxis ("Mouse Y"));
						transform.RotateAround (head.position, new Vector3 (0, 1, 0), s / 10 * Input.GetAxis ("Mouse X"));*/
						transform.LookAt (head.position);
				}
		}
	
		void Start () {
				if (GetComponent<Rigidbody> ()) {
						GetComponent<Rigidbody> ().freezeRotation = true;
				}
				mn = GameObject.Find ("Uebergabe").GetComponent<mainmenu> ();
		}
	
		void camera () {
				Vector3 tmp_cam = Input.mousePosition;
				float tmp_x = tmp_cam.x;
				float tmp_y = tmp_cam.y;
				hhalf = 0;
				vhalf = 0;
		
				if (((Screen.width / 2) + 50 < tmp_x)) {
						hhalf = 1;
				}
				if (((Screen.width / 2) - 50 > tmp_x)) {
						hhalf = -1;
				}
		
				if (((Screen.height / 2) + 50 < tmp_y)) {
						vhalf = 1;
				}
				if (((Screen.height / 2) - 50 > tmp_y)) {
						vhalf = -1;
				}
		}
}
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
						/*
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
			
						RotateHeadToMousPos ();
						transform.LookAt (head.position);
			
				}
		}
	
		void Start () {
				if (GetComponent<Rigidbody> ()) {
						GetComponent<Rigidbody> ().freezeRotation = true;
				}
				mn = GameObject.Find ("Uebergabe").GetComponent<mainmenu> ();
		}
	
		void RotateHeadToMousPos () {
				float min_left = 0;
				float max_left = Screen.width / 2 - 50;
		
				float min_right = Screen.width / 2 + 50;
				float max_right = Screen.width;
		
				float min_up = Screen.height / 2 + 50;
				float max_up = Screen.height;
		
				float min_down = 0;
				float max_down = Screen.height / 2 - 50;
		
				Vector3 tmp_cam = Input.mousePosition;
				float tmp_x = tmp_cam.x;
				float tmp_y = tmp_cam.y;
		
				Vector3 angle = Vector3.zero;
		
				//Debug.Log (tmp_cam);
				if (tmp_x > min_right) {
						angle.y = (90 * ((tmp_x - min_right) / (max_right - min_right)));
				}
				if (tmp_x < max_left) {
						angle.y = -1 * (90 * ((tmp_x - max_left) / (min_left - max_left)));
				}
		
				if (tmp_y > min_up) {
						angle.x = (90 * ((tmp_y - min_up) / (max_up - min_up)));
				}
				if (tmp_y < max_down) {
						angle.x = -1 * (90 * ((tmp_y - max_down) / (min_down - max_down)));
				}
				
				head.localEulerAngles = angle;
				Debug.Log (angle);
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
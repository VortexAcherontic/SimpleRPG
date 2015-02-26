using UnityEngine;
using System.Collections.Generic;

public class MouseLook : MonoBehaviour {
		public mainmenu mn;
		public Transform head;
		public float MoveSpeed = 1;

		void Update () {
				if (mn.cammove && Time.timeScale > 0) {
			
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
		
				if (tmp_x > min_right) {
						angle.y = (90 * ((tmp_x - min_right) / (max_right - min_right)));
				}
				if (tmp_x < max_left) {
						angle.y = -1 * (90 * ((tmp_x - max_left) / (min_left - max_left)));
				}
		
				if (tmp_y > min_up) {
						angle.x = -1 * (90 * ((tmp_y - min_up) / (max_up - min_up)));
				}
				if (tmp_y < max_down) {
						angle.x = (90 * ((tmp_y - max_down) / (min_down - max_down)));
				}
				Vector3 OldEulerAngles = head.localEulerAngles;
				angle.x = Mathf.LerpAngle (OldEulerAngles.x, angle.x, Time.deltaTime * MoveSpeed);
				angle.y = Mathf.LerpAngle (OldEulerAngles.y, angle.y, Time.deltaTime * MoveSpeed);
				head.localEulerAngles = angle;
		}
}
using UnityEngine;
using System.Collections;

[System.Serializable]
public struct DoorOptions {
		public bool IsOpen;
		public bool IsLocked;
		public bool IsTeleporter;
		public string TeleportToScene;
		public Vector3 TeleportToPosition;
		public bool TurnInvert;
}

public class Door : MonoBehaviour {
		AudioSource sound;
		Player_Trigger triggerscript;
		Vector3 startangles;
		public DoorOptions optionen;
	
		void Start () {
				startangles = transform.localEulerAngles;
				sound = GetComponent<AudioSource> ();
				triggerscript = GetComponentInChildren<Player_Trigger> ();
		}
	
		void Update () {
				Interact ();
		}
	
		void Interact () {
				if (triggerscript.Player_in_Triger) {
						if (Input.GetButtonDown ("Interact")) {
								if (optionen.IsLocked == false) {
										sound.Play ();
										optionen.IsOpen = !optionen.IsOpen;
										if (optionen.IsOpen) {
												if (optionen.TurnInvert) {
														transform.localEulerAngles = new Vector3 (startangles.x, startangles.y - 90, startangles.z);
												} else {
														transform.localEulerAngles = new Vector3 (startangles.x, startangles.y + 90, startangles.z);
												}
										} else {
												transform.localEulerAngles = new Vector3 (startangles.x, startangles.y, startangles.z);
										}
										if (optionen.IsTeleporter) {
												GameObject.FindWithTag ("Player").transform.position = optionen.TeleportToPosition;
												Application.LoadLevel (optionen.TeleportToScene);
										}
								} else {/*open lockpick minigame*/
								}
						}
				}
		}
}

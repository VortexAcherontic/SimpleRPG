using UnityEngine;
using System.Collections;

public class Player_Movement : MonoBehaviour {
		PlayerBehaviour PlayerStats;
		Animator MotionController;
	
		public float SpeedMultiplier = 1;
		public float RunSpeedMultiplier = 2;
		public float RotationSpeedMultiplier = 2;
		
		public float StaminaToAttack = 30;
		public float StaminaToRun = 20;
		public float StaminaToJump = 10;
		
	
		// Parameter die nur innerhalb des Scripts geändert werden sollten
		float speed = 0;
		float rotation = 0;
		private CollisionFlags collisionFlags; 
	
	
		void Awake () {
				// Verweise als aller erstes, deswegen nicht in Start sonder Awake
				MotionController = GetComponentInChildren<Animator> ();
				PlayerStats = GetComponent<PlayerBehaviour> ();
		}
	
		void Start () {
		
		}
	
		void Update () {
				Vector3 Bewegung = Camera.main.transform.forward;
				Bewegung.y = 0;
				float AktuelleStamina = PlayerStats.me.Creat.Stamina; // denk daran, es ist ReadOnly
				#region Animation - Werte
				// Später bei mehr Animationen halt erstmal Abfragen was getragen wird!
				MotionController.SetBool ("One Handed Weapon", true);
				MotionController.SetFloat ("Speed", speed);
				
				#endregion Animation - Werte
		
				//if (networkView.isMine) { // Schonmal vorbereiten auf Multiplayer?! Wobei ich bei den Trigger nicht sicher bin
				#region Animation - Trigger
				if (AktuelleStamina > StaminaToAttack && Input.GetButton ("Fire1")) {
						PlayerStats.me.Creat.Stamina -= StaminaToAttack;
						MotionController.SetTrigger ("Attacking");
				}
			
				if (AktuelleStamina > StaminaToJump && Input.GetButton ("Jump")) {
						PlayerStats.me.Creat.Stamina -= StaminaToJump;
						MotionController.SetTrigger ("Jumping");
				}
				#endregion Animation - Trigger
			
				#region KeyInput
				float v = Input.GetAxis ("Vertical");
				float h = Input.GetAxis ("Horizontal");
			
				if (AktuelleStamina > StaminaToRun && Input.GetButton ("Run") && v > 0) {
						PlayerStats.me.Creat.Stamina -= StaminaToRun;
						v *= RunSpeedMultiplier;
				}
			
				if (v == 0) {
						if (speed > 0.1f) {
								speed = speed - Time.deltaTime * SpeedMultiplier;
						} else if (speed < -0.1f) {
								speed = speed + Time.deltaTime * SpeedMultiplier;
						} else {
								speed = 0;
						}
				} else {
						speed = Mathf.Lerp (speed, v, Time.deltaTime * SpeedMultiplier);
				}
				rotation = h * Time.deltaTime * RotationSpeedMultiplier;
				#endregion KeyInput
						
				#region Bewegung
				Vector3 Drehung = Vector3.zero;
				Drehung.y = rotation;
				transform.Rotate (Drehung);
		
				Bewegung = Bewegung * speed;
				CharacterController controller = GetComponent<CharacterController> ();
				collisionFlags = controller.Move (Bewegung);
				#endregion Bewegung
				//}
		}
	
}
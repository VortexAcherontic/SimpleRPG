using UnityEngine;
using System.Collections;

public class SunSystem : MonoBehaviour {
		private GameTime	TimeSystem;
	
		public float GehtAufStunde = 8;
		public float GehtUnterStunde = 20;
	
		// Use this for initialization
		void Start () {
				TimeSystem = GameObject.Find ("Uebergabe").GetComponent<GameTime> ();
				GehtAufStunde *= TimeSystem.Stunde;
				GehtUnterStunde *= TimeSystem.Stunde;
		}
	
		void Update () {
				// Wenns Aufgeuhrzeit ist dann muss es wohl bei 0 Grad sein
				// Wenns Untergeuhrzeit ist dann muss es wohl bei 180 Grad sein
				// Bei Zeiten wos nicht angzeigt wird dann Light und LensFlare ausschalten
		}
}

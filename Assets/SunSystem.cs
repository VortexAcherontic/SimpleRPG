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
				bool IsVisible = false;
				if (GehtAufStunde < GehtUnterStunde) {
						//SUN
						if (TimeSystem.Zeit > GehtAufStunde && TimeSystem.Zeit < GehtUnterStunde) {
								IsVisible = true;
						}
				} else {
						//Moon
						if (TimeSystem.Zeit > GehtAufStunde || TimeSystem.Zeit < GehtUnterStunde) {
								IsVisible = true;
						}
				}
				if (IsVisible) {
						GetComponent<Light> ().enabled = true;
						GetComponent<LensFlare> ().enabled = true;
			
						// Wenns Aufgeuhrzeit ist dann muss es wohl bei 0 Grad sein
						// Wenns Untergeuhrzeit ist dann muss es wohl bei 180 Grad sein
						float DauerSichtbarkeit = ZeitDifferenz (GehtAufStunde, GehtUnterStunde);
						float StandSichtbarkeit = ZeitDifferenz (GehtAufStunde, TimeSystem.Zeit);
						float ProzentualerStand = (100 / DauerSichtbarkeit) * StandSichtbarkeit;
						float WinkelStand = (ProzentualerStand / 100) * 180;
						print ("D " + DauerSichtbarkeit);
						print ("S " + StandSichtbarkeit);			
						print ("P " + ProzentualerStand);
						transform.eulerAngles = new Vector3 (WinkelStand, 0, 0);
						
				} else {
						// Bei Zeiten wos nicht angzeigt wird dann Light und LensFlare ausschalten
						GetComponent<Light> ().enabled = false;
						GetComponent<LensFlare> ().enabled = false;
				}
				
		}
	
		float ZeitDifferenz (float von, float bis) {
				float returnValue = 0;
				if (von > bis) {
						returnValue += TimeSystem.Tag - von;
						returnValue += bis;
				} else {
						returnValue = bis - von;
				}
				return returnValue;
		}
}

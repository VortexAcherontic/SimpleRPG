using UnityEngine;
using System.Collections;

public class SunSystem : MonoBehaviour {
		private GameTime	TimeSystem;
	
		public float GehtAufStunde = 8;
		public float GehtUnterStunde = 20;
		
		float tmp_GehtAufStunde;
		float tmp_GehtUnterStunde;
		// Use this for initialization
		void Start () {
				TimeSystem = GameObject.Find ("Uebergabe").GetComponent<GameTime> ();
		}
	
		void Update () {
				tmp_GehtAufStunde = GehtAufStunde * TimeSystem.Stunde;
				tmp_GehtUnterStunde = GehtUnterStunde * TimeSystem.Stunde;
			
				bool IsVisible = false;
				if (tmp_GehtAufStunde < tmp_GehtUnterStunde) {
						//SUN
						if (TimeSystem.Zeit > tmp_GehtAufStunde && TimeSystem.Zeit < tmp_GehtUnterStunde) {
								IsVisible = true;
						}
				} else {
						//Moon
						if (TimeSystem.Zeit > tmp_GehtAufStunde || TimeSystem.Zeit < tmp_GehtUnterStunde) {
								IsVisible = true;
						}
				}
				if (IsVisible) {
						GetComponent<Light> ().enabled = true;
						GetComponent<LensFlare> ().enabled = true;
			
						// Wenns Aufgeuhrzeit ist dann muss es wohl bei 0 Grad sein
						// Wenns Untergeuhrzeit ist dann muss es wohl bei 180 Grad sein
						float DauerSichtbarkeit = ZeitDifferenz (tmp_GehtAufStunde, tmp_GehtUnterStunde);
						float StandSichtbarkeit = ZeitDifferenz (tmp_GehtAufStunde, TimeSystem.Zeit);
						float ProzentualerStand = (100 / DauerSichtbarkeit) * StandSichtbarkeit;
						float WinkelStand = (ProzentualerStand / 100) * 180;
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
						returnValue += Mathf.Abs (TimeSystem.Tag - von);
						returnValue += bis;
				} else {
						returnValue = bis - von;
				}
				return returnValue;
		}
}

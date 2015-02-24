using UnityEngine;
using System.Collections;

public class GameOverScreen : MonoBehaviour {

		// Use this for initialization
		void Start () {
	
		}
	
		// Update is called once per frame
		void Update () {
	
		}
	
		void OnGUI () {
				var centeredStyle = GUI.skin.GetStyle ("Label");
				centeredStyle.alignment = TextAnchor.UpperCenter;
				GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 30, 400, 60), "<color=white><size=50>Game Over</size></color>", centeredStyle);
		
				centeredStyle = GUI.skin.GetStyle ("Button");
				centeredStyle.alignment = TextAnchor.UpperCenter;
				if (GUI.Button (new Rect (Screen.width / 2 - 50, Screen.height / 2 - 30 + 100, 100, 30), "Restart", centeredStyle)) {
						Application.LoadLevel (0);
			

			
				}
				centeredStyle = GUI.skin.GetStyle ("Label");
				centeredStyle.alignment = TextAnchor.UpperCenter;
				GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 280, 400, 30), "<color=white>Thanks for playing SimpleRPG!</color>", centeredStyle);
				GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 250, 400, 30), "");
		
				GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 220, 400, 30), "<color=white>Istani | Programmer</color>", centeredStyle);
				GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 190, 400, 30), "<color=white>Defender833 | Concept | Programmer</color>", centeredStyle);
				GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 160, 400, 30), "<color=white>Vortex | Design | www.z-ray.de</color>", centeredStyle);
				GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 130, 400, 30), "<color=white>Craftgoll | Design</color>", centeredStyle);
				GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 100, 400, 30), "<color=white>Paliv | 3D Modeling | www.blancmiles.crevado.com/</color>", centeredStyle);
				GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 100, 400, 30), "<color=white>Tim Bartsch | Music</color>", centeredStyle);
		}
}

using UnityEngine;
using System.Collections;

public class GameOverScreen : MonoBehaviour {
		GUI_Helper GUI_ZoD = new GUI_Helper ();

		void OnGUI () {
				GUI_ZoD.Label ("<color=white><size=50>Game Over</size></color>", 11, new Rect (1920 / 2 - 200, 1080 / 2 - 30, 400, 60));
		
				if (GUI_ZoD.Button_Text ("Restart", 11, new Rect (1920 / 2 - 50, 1080 / 2 - 30 + 100, 100, 30))) {
						Application.LoadLevel (0);
				}
		
				GUI_ZoD.Label ("<color=white>Thanks for playing SimpleRPG!</color>", 11, new Rect (1920 / 2 - 200, 1080 / 2 - 280, 400, 30));
				GUI_ZoD.Label ("", 11, new Rect (1920 / 2 - 200, 1080 / 2 - 250, 400, 30));
		
				GUI_ZoD.Label ("<color=white>Istani | Programmer</color>", 11, new Rect (1920 / 2 - 200, 1080 / 2 - 220, 400, 30));
				GUI_ZoD.Label ("<color=white>Defender833 | Concept | Programmer</color>", 11, new Rect (1920 / 2 - 200, 1080 / 2 - 190, 400, 30));
				GUI_ZoD.Label ("<color=white>Vortex | Design | www.z-ray.de</color>", 11, new Rect (1920 / 2 - 200, 1080 / 2 - 160, 400, 30));
				GUI_ZoD.Label ("<color=white>Craftgoll | Design</color>", 11, new Rect (1920 / 2 - 200, 1080 / 2 - 130, 400, 30));
				GUI_ZoD.Label ("<color=white>Paliv | 3D Modeling | www.blancmiles.crevado.com/</color>", 11, new Rect (1920 / 2 - 200, 1080 / 2 - 100, 400, 30));
				GUI_ZoD.Label ("<color=white>Tim Bartsch | Music</color>", 11, new Rect (1920 / 2 - 200, 1080 / 2 - 100, 400, 30));
		}
}

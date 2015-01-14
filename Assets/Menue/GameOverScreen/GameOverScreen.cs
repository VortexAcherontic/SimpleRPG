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
		}
}

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
		}
}

using UnityEngine;
using System.Collections;

public class map : MonoBehaviour {

		public Texture2D minimap;
		public mainmenu gui;
		public player p001;

		// Use this for initialization
		void Start () {
				gui = GameObject.Find ("Main Camera").GetComponent<mainmenu> ();
				p001 = GameObject.Find ("Main Camera").GetComponent<player> ();
				LoadMap ();
		}

		void OnGUI () {
				if (gui.showmap) {
						
						//GUI.DrawTexture (new Rect (1180, 0, 100, 100), minimap);
						GUI.DrawTexture (new Rect (0, 0, 100, 100), minimap); // Koords angepass weil mein Bildschrim zu klein SK
				}
				//if(p001.pos)
		}

		public void LoadMap () {
				// Map laden... http://docs.unity3d.com/ScriptReference/Resources.Load.html
				gameObject.GetComponent<TileMap> ().GenerateMapData ();
		}
}

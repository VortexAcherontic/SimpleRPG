using UnityEngine;
using System.Collections;

public class map : MonoBehaviour {

		public Texture2D minimap;
		public mainmenu gui;
		public player p001;
		public Transform MonsterSpawner;

		// Use this for initialization
		void Start () {
				gui = GameObject.Find ("Main Camera").GetComponent<mainmenu> ();
				p001 = GameObject.Find ("Main Camera").GetComponent<player> ();
				MonsterSpawner = GameObject.Find ("MonsterSpawner").transform;
				//LoadMap (); // Nun nach Player Load
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
				foreach (Transform MonsterTrans in MonsterSpawner) {
						MonsterTrans.position = new Vector3 (p001.pos.x - 100, MonsterTrans.position.y, MonsterTrans.position.z);	
				}
				gameObject.GetComponent<TileMap> ().GenerateMapData ();
				MonsterSpawner.GetComponent<EnemySpawn> ().spawnbosses ();
		}
}

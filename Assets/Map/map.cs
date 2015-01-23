using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class map : MonoBehaviour {

		public Texture2D minimap;
		public mainmenu gui;
		public player p001;
		public Transform MonsterSpawner;
		public List<Texture2D> Maps;

		// Use this for initialization
		void Start () {
				gui = GameObject.Find ("Main Camera").GetComponent<mainmenu> ();
				p001 = GameObject.Find ("Main Camera").GetComponent<player> ();
				MonsterSpawner = GameObject.Find ("MonsterSpawner").transform;
				//LoadMap (); // Nun nach Player Load
		
				// Achtung reihenfolge muss gleich bleiben! Damit die Koordinaten beim Porter noch klappen!
				// + Natürlich darf sich im nachhinnein nicht mehr die Größe der Karten ändern
				Maps.Add ((Texture2D)Resources.Load ("Map/world001"));
				Maps.Add ((Texture2D)Resources.Load ("Map/town001"));
		}

		void OnGUI () {
				if (gui.showmap) {
						
						//GUI.DrawTexture (new Rect (1180, 0, 100, 100), minimap);
						GUI.DrawTexture (new Rect (0, 0, 100, 100), minimap); // Koords angepass weil mein Bildschrim zu klein SK
				}
				//if(p001.pos)
		}

		public void LoadMap () {
				foreach (Transform MonsterTrans in MonsterSpawner) {
						MonsterTrans.position = new Vector3 (p001.pos.x - 100, MonsterTrans.position.y, MonsterTrans.position.z);	
				}
				foreach (Transform OldTile in  GameObject.Find ("Map").transform) {
						Destroy (OldTile.gameObject);
				}
				gameObject.GetComponent<TileMap> ().GenerateMapData ();
				MonsterSpawner.GetComponent<EnemySpawn> ().spawnbosses ();
		}
}

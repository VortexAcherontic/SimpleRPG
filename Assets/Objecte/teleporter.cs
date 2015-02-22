using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct ObjTele {
		public string scene;
		//public string vonmap;
		public Vector2 vonpos;
		//public string nachmap;
		public Vector2 nachpos;
		public Texture2D minimap_to_update;
}
public class teleporter : MonoBehaviour {
		public List<ObjTele> Porter = new List<ObjTele> ();
		PlayerBehaviour p001;
		map m001;
		
	
		void Start () {
				p001 = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerBehaviour> ();
				m001 = GameObject.Find ("Map").GetComponent<map> ();
				
		}
	
		public void AddTeleporter (string Scene, Vector2 vonPos, Vector2 nachPos, Texture2D newMinimap) {
				ObjTele tmpporter;
				tmpporter.scene = Scene;
				tmpporter.vonpos = vonPos;		
				tmpporter.nachpos = nachPos;
				tmpporter.minimap_to_update = newMinimap;
				Porter.Add (tmpporter);
				
		}
	
		float temp_x;
		float temp_y;
		int distance_manhatten;
		public int CheckDistance (ObjTele vonTeleporter) {
				temp_x = Mathf.Abs (p001.me.Creat.Position.x - vonTeleporter.vonpos.x);
				temp_y = Mathf.Abs (p001.me.Creat.Position.y - vonTeleporter.vonpos.y);
				//distance_euklid = (int)Mathf.Sqrt (temp_x * temp_x + temp_y * temp_y);
				distance_manhatten = (int)(temp_x + temp_y);
				//Debug.Log ("Distance: " + distance_manhatten);
				return distance_manhatten;
		}
		// Update is called once per frame
		void Update () {
				foreach (ObjTele tmpporter in Porter) {
						if (CheckDistance (tmpporter) <= 0) {
						
								p001.me.Creat.Position = tmpporter.nachpos;
								p001.me.Creat.Position = tmpporter.nachpos;	
								m001.minimap = tmpporter.minimap_to_update;
								if (Application.loadedLevelName != tmpporter.scene) {
										Application.LoadLevel (tmpporter.scene);
								}
								//GameObject.Find ("Unit").GetComponent<PlayerToPos> ().MovePlayer ();
						}
				}
		}
		
		public void LoadPorter () {
				/*
				AddTeleporter ("Haus_Des_Spielers", new Vector2 (78, 78), new Vector2 (0, 0), m001.Maps [1]);
		
				// Links in Stadt
				AddTeleporter ("SimpleRpg", new Vector2 (75, 81), new Vector2 (161, 56), m001.Maps [1]);
				AddTeleporter ("SimpleRpg", new Vector2 (75, 80), new Vector2 (161, 55), m001.Maps [1]);
				AddTeleporter ("SimpleRpg", new Vector2 (75, 79), new Vector2 (161, 54), m001.Maps [1]);
				// Rechts in Stadt
				AddTeleporter ("SimpleRpg", new Vector2 (85, 81), new Vector2 (208, 56), m001.Maps [1]);
				AddTeleporter ("SimpleRpg", new Vector2 (85, 80), new Vector2 (208, 55), m001.Maps [1]);
				AddTeleporter ("SimpleRpg", new Vector2 (85, 79), new Vector2 (208, 54), m001.Maps [1]);
				// Oben in Stadt
				AddTeleporter ("SimpleRpg", new Vector2 (79, 85), new Vector2 (183, 78), m001.Maps [1]);
				AddTeleporter ("SimpleRpg", new Vector2 (80, 85), new Vector2 (184, 78), m001.Maps [1]);
				AddTeleporter ("SimpleRpg", new Vector2 (81, 85), new Vector2 (185, 78), m001.Maps [1]);
				// Unten in Stadt
				AddTeleporter ("SimpleRpg", new Vector2 (79, 75), new Vector2 (183, 31), m001.Maps [1]);
				AddTeleporter ("SimpleRpg", new Vector2 (80, 75), new Vector2 (184, 31), m001.Maps [1]);
				AddTeleporter ("SimpleRpg", new Vector2 (81, 75), new Vector2 (185, 31), m001.Maps [1]);
		
				
			
				// Links aus Stadt
				AddTeleporter ("SimpleRpg", new Vector2 (160, 56), new Vector2 (74, 81), m001.Maps [0]);
				AddTeleporter ("SimpleRpg", new Vector2 (160, 55), new Vector2 (74, 80), m001.Maps [0]);
				AddTeleporter ("SimpleRpg", new Vector2 (160, 54), new Vector2 (74, 79), m001.Maps [0]);
				// Rects aus Stadt
				AddTeleporter ("SimpleRpg", new Vector2 (209, 56), new Vector2 (86, 81), m001.Maps [0]);
				AddTeleporter ("SimpleRpg", new Vector2 (209, 55), new Vector2 (86, 80), m001.Maps [0]);
				AddTeleporter ("SimpleRpg", new Vector2 (209, 54), new Vector2 (86, 79), m001.Maps [0]);
				// Oben aus Stadt
				AddTeleporter ("SimpleRpg", new Vector2 (183, 79), new Vector2 (79, 86), m001.Maps [0]);
				AddTeleporter ("SimpleRpg", new Vector2 (184, 79), new Vector2 (80, 86), m001.Maps [0]);
				AddTeleporter ("SimpleRpg", new Vector2 (185, 79), new Vector2 (81, 86), m001.Maps [0]);
				// unten aus Stadt
				AddTeleporter ("SimpleRpg", new Vector2 (183, 30), new Vector2 (79, 74), m001.Maps [0]);
				AddTeleporter ("SimpleRpg", new Vector2 (184, 30), new Vector2 (80, 74), m001.Maps [0]);
				AddTeleporter ("SimpleRpg", new Vector2 (185, 30), new Vector2 (81, 74), m001.Maps [0]);
				*/
		}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct ObjTele {
		//public string vonmap;
		public Vector2 vonpos;
		//public string nachmap;
		public Vector2 nachpos;
		public Texture2D minimap_to_update;
}
public class teleporter : MonoBehaviour {
		public List<ObjTele> Porter = new List<ObjTele> ();
		player p001;
		map m001;
		
	
		void Start () {
				p001 = GameObject.Find ("Main Camera").GetComponent<player> ();
				m001 = GameObject.Find ("Map").GetComponent<map> ();
				
		}
	
		public void AddTeleporter (Vector2 vonPos, Vector2 nachPos, Texture2D newMinimap) {
				ObjTele tmpporter;
				tmpporter.vonpos = vonPos;		
				tmpporter.nachpos = nachPos;
				tmpporter.minimap_to_update = newMinimap;
				Porter.Add (tmpporter);
				
		}
		// Update is called once per frame
		void Update () {
				foreach (ObjTele tmpporter in Porter) {
						if (tmpporter.vonpos == p001.pos) {
								p001.pos = tmpporter.nachpos;
								p001.pos = tmpporter.nachpos;	
								m001.minimap = tmpporter.minimap_to_update;
								//GameObject.Find ("Unit").GetComponent<PlayerToPos> ().MovePlayer ();
						}
				}
		}
		
		public void LoadPorter () {
				// Links in Stadt
				AddTeleporter (new Vector2 (75, 81), new Vector2 (161, 56), m001.Maps [1]);
				AddTeleporter (new Vector2 (75, 80), new Vector2 (161, 55), m001.Maps [1]);
				AddTeleporter (new Vector2 (75, 79), new Vector2 (161, 54), m001.Maps [1]);
				// Rechts in Stadt
				AddTeleporter (new Vector2 (85, 81), new Vector2 (208, 56), m001.Maps [1]);
				AddTeleporter (new Vector2 (85, 80), new Vector2 (208, 55), m001.Maps [1]);
				AddTeleporter (new Vector2 (85, 79), new Vector2 (208, 54), m001.Maps [1]);
				// Oben in Stadt
				AddTeleporter (new Vector2 (79, 85), new Vector2 (183, 78), m001.Maps [1]);
				AddTeleporter (new Vector2 (80, 85), new Vector2 (184, 78), m001.Maps [1]);
				AddTeleporter (new Vector2 (81, 85), new Vector2 (185, 78), m001.Maps [1]);
				// Unten in Stadt
				AddTeleporter (new Vector2 (79, 75), new Vector2 (183, 31), m001.Maps [1]);
				AddTeleporter (new Vector2 (80, 75), new Vector2 (184, 31), m001.Maps [1]);
				AddTeleporter (new Vector2 (81, 75), new Vector2 (185, 31), m001.Maps [1]);
				
			
				// Links aus Stadt
				AddTeleporter (new Vector2 (160, 56), new Vector2 (74, 81), m001.Maps [0]);
				AddTeleporter (new Vector2 (160, 55), new Vector2 (74, 80), m001.Maps [0]);
				AddTeleporter (new Vector2 (160, 54), new Vector2 (74, 79), m001.Maps [0]);
				// Rects aus Stadt
				AddTeleporter (new Vector2 (209, 56), new Vector2 (86, 81), m001.Maps [0]);
				AddTeleporter (new Vector2 (209, 55), new Vector2 (86, 80), m001.Maps [0]);
				AddTeleporter (new Vector2 (209, 54), new Vector2 (86, 79), m001.Maps [0]);
				// Oben aus Stadt
				AddTeleporter (new Vector2 (183, 79), new Vector2 (79, 86), m001.Maps [0]);
				AddTeleporter (new Vector2 (184, 79), new Vector2 (80, 86), m001.Maps [0]);
				AddTeleporter (new Vector2 (185, 79), new Vector2 (81, 86), m001.Maps [0]);
				// unten aus Stadt
				AddTeleporter (new Vector2 (183, 30), new Vector2 (79, 74), m001.Maps [0]);
				AddTeleporter (new Vector2 (184, 30), new Vector2 (80, 74), m001.Maps [0]);
				AddTeleporter (new Vector2 (185, 30), new Vector2 (81, 74), m001.Maps [0]);
		}
}

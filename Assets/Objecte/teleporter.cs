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
								GameObject.Find ("Unit").GetComponent<PlayerToPos> ().MovePlayer ();
						}
				}
		}
		
		public void LoadPorter () {
				AddTeleporter (new Vector2 (75, 81), new Vector2 (161, 56), m001.Maps [1]);
				AddTeleporter (new Vector2 (75, 80), new Vector2 (161, 55), m001.Maps [1]);
				AddTeleporter (new Vector2 (75, 79), new Vector2 (161, 54), m001.Maps [1]);
		
				AddTeleporter (new Vector2 (160, 56), new Vector2 (74, 81), m001.Maps [0]);
				AddTeleporter (new Vector2 (160, 55), new Vector2 (74, 80), m001.Maps [0]);
				AddTeleporter (new Vector2 (160, 54), new Vector2 (74, 79), m001.Maps [0]);
		}
}

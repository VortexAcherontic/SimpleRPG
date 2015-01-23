using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct ObjTele {
	//public string vonmap;
	public Vector2 vonpos;
	//public string nachmap;
	public Vector2 nachpos;
}
public class teleporter : MonoBehaviour {
	public List<ObjTele> Porter = new List<ObjTele>();
	player p001;
	
	void Start () {
		p001 = GameObject.Find ("Main Camera").GetComponent<player> ();
		AddTeleporter (new Vector2 (75, 81), new Vector2 (161, 56));
		AddTeleporter (new Vector2 (75, 80), new Vector2 (161, 55));
		AddTeleporter (new Vector2 (75, 79), new Vector2 (161, 54));
		
		AddTeleporter (new Vector2 (160, 56), new Vector2 (74, 81));
		AddTeleporter (new Vector2 (160, 55), new Vector2 (74, 80));
		AddTeleporter (new Vector2 (160, 54), new Vector2 (74, 79));
	}
	
	public void AddTeleporter(Vector2 vonPos,Vector2 nachPos) {
		ObjTele tmpporter;
		tmpporter.vonpos = vonPos;;
		tmpporter.nachpos = nachPos;
		Porter.Add (tmpporter);
	}
	// Update is called once per frame
	void Update () {
			foreach (ObjTele tmpporter in Porter) {
				if (tmpporter.vonpos==p001.pos) {
					p001.pos=tmpporter.nachpos;
					//GameObject.Find ("Map").GetComponent<map> ().LoadMap (p001.map);
					p001.pos=tmpporter.nachpos;	
					GameObject.Find ("Unit").GetComponent<PlayerToPos> ().MovePlayer ();
				}
			}
		}
}

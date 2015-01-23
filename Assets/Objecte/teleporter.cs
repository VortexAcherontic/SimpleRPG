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
		AddTeleporter (new Vector2 (75, 81), new Vector2 (1, 25));
		AddTeleporter (new Vector2 (75, 80), new Vector2 (1, 24));
		AddTeleporter (new Vector2 (75, 79), new Vector2 (1, 23));
		
		AddTeleporter (new Vector2 (0, 25), new Vector2 (44, 51));
		AddTeleporter (new Vector2 (0, 24), new Vector2 (44, 50));
		AddTeleporter (new Vector2 (0, 23), new Vector2 (44, 49));
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

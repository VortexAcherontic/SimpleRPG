using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct ObjTele {
	public string vonmap;
	public Vector2 vonpos;
	public string nachmap;
	public Vector2 nachpos;
}
public class teleporter : MonoBehaviour {
	List<ObjTele> Porter = new List<ObjTele>();
	player p001;
	
	void Start () {
		p001 = GameObject.Find ("Main Camera").GetComponent<player> ();
		AddTeleporter ("world001", new Vector2 (45, 51), "town001", new Vector2 (1, 25));
		AddTeleporter ("world001", new Vector2 (45, 50), "town001", new Vector2 (1, 24));
		AddTeleporter ("world001", new Vector2 (45, 49), "town001", new Vector2 (1, 23));
		
		AddTeleporter ("town001", new Vector2 (0, 25), "world001", new Vector2 (44, 51));
		AddTeleporter ("town001", new Vector2 (0, 24), "world001", new Vector2 (44, 50));
		AddTeleporter ("town001", new Vector2 (0, 23), "world001", new Vector2 (44, 49));
	}
	
	public void AddTeleporter(string vonMap, Vector2 vonPos, string nachMap, Vector2 nachPos) {
		ObjTele tmpporter;
		tmpporter.vonmap = vonMap;
		tmpporter.vonpos = vonPos;
		tmpporter.nachmap = nachMap;
		tmpporter.nachpos = nachPos;
		Porter.Add (tmpporter);
	}
	// Update is called once per frame
	void Update () {
			foreach (ObjTele tmpporter in Porter) {
				if (tmpporter.vonmap==p001.map && tmpporter.vonpos==p001.pos) {
					p001.map=tmpporter.nachmap;
					p001.pos=tmpporter.nachpos;
					GameObject.Find ("Map").GetComponent<map> ().LoadMap (p001.map);
					p001.map=tmpporter.nachmap;
					p001.pos=tmpporter.nachpos;	
					GameObject.Find ("Unit").GetComponent<PlayerToPos> ().MovePlayer ();
				}
			}
		}
}

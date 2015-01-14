using UnityEngine;
using System.Collections;

public class PlayerToPos : MonoBehaviour {
		player p001;
		
		public void MovePlayer () { // Nicht in Update weil das die FPS von 60 auf 3 macht! Deswegen in p001.move()
				if (p001 == null) {
						p001 = GameObject.Find ("Main Camera").GetComponent<player> ();
				} 
				if (p001 != null) {
						Vector3 PosPlayer;
						PosPlayer.x = p001.pos.x;
						PosPlayer.y = p001.pos.y;
						PosPlayer.z = transform.position.z;
						transform.position = PosPlayer;
			
						// Camera To Player
						GameObject.Find ("Main Camera").transform.position = new Vector3 (PosPlayer.x, PosPlayer.y, -10);
				}
		}
}

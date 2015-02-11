using UnityEngine;
using System.Collections;

public class PlayerToPos : MonoBehaviour {
		PlayerBehaviour p001;
		
		public void Update () { // Nicht in Update weil das die FPS von 60 auf 3 macht! Deswegen in p001.move()
				if (p001 == null) {
						p001 = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerBehaviour> ();
				} 
				if (p001 != null) {
						Vector3 PosPlayer;
						PosPlayer.x = p001.me.Creat.Position.x;
						PosPlayer.y = p001.me.Creat.Position.y;
						PosPlayer.z = transform.position.z;
						transform.position = PosPlayer;
						PosPlayer.z = -10;
			
						// Camera To Player
						GameObject.Find ("Main Camera").transform.position = PosPlayer;
				}
		}
}

using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
		player p001;
		enemy e001;
		float temp_x;
		float temp_y;
		int distance_euklid;
		int distance_manhatten;
		EnemySpawn mobspawn;
		float loadtimer = 0.5f;
	
		// Use this for initialization
		void Start () {
				p001 = GameObject.Find ("Main Camera").GetComponent<player> ();
				e001 = gameObject.GetComponent<enemy> ();
				mobspawn = GameObject.Find ("MonsterSpawner").GetComponent<EnemySpawn> ();
		}
	
		// Update is called once per frame
		void Update () {
				loadtimer -= Time.deltaTime;
				if (CheckDistance () > 20 && loadtimer <= 0) {
						if (!e001.thismob.boss) {
								mobspawn.mobs--;			
								Destroy (gameObject);
						}			
				}
		}
		//PlayerEntfernung checken
		int CheckDistance () {
				temp_x = Mathf.Abs (p001.pos.x - transform.position.x);
				temp_y = Mathf.Abs (p001.pos.y - transform.position.y);
				//distance_euklid = (int)Mathf.Sqrt (temp_x * temp_x + temp_y * temp_y);
				distance_manhatten = (int)(temp_x + temp_y);
				//Debug.Log ("Distance: " + distance_manhatten);
				return distance_manhatten;
		}
}

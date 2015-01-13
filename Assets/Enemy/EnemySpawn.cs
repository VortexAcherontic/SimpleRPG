using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {
		public EnemyType[] enemyTypes;
		public int maxmobs = 15;
		public int mobs = 0;
		float spawncooldown = 5.0f;
		float spawntimer;
		bool isspawning = true;
		
		// Use this for initialization
		void Start () {
				//spawnboss (); // Spawnt permanente gegner die bis zur zerstörung auf der map bleiben
		}
	
		// Update is called once per frame
		void Update () {
				if (mobs < maxmobs) {
						if (isspawning) {
								spawnmob ();
								isspawning = false;
								spawntimer = spawncooldown;
								mobs++;
						}
						spawntimer -= Time.deltaTime;
						if (spawntimer <= 0) {
								isspawning = true;
						}
				}
		}
	
		void spawnmob () {
				//int mob_id=Random.Range (0,enemyTypes.Length);
				int mob_id = 0; 
				Vector3 pos = new Vector3 (Random.Range (40, 60), Random.Range (40, 60), 0);
				//Vector3 pos = new Vector3 (60, 60, 0);
		
				EnemyType tt = enemyTypes [mob_id];
				GameObject tmpobjct = (GameObject)Instantiate (tt.prefab, pos, Quaternion.identity);
				tmpobjct.transform.parent = GameObject.Find ("MonsterSpawner").transform;
				tmpobjct.GetComponent<enemy> ().SettingStats (tt);
				tmpobjct.GetComponent<enemy> ().thismob.pos = pos;
				Debug.Log ("Mob: " + pos.x + "/" + pos.y);
		}
}

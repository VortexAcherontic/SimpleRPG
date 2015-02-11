using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawn : MonoBehaviour {
		public CreatureDataList DataListObj;
		public List<CreatureOriginData> enemyTypes = new List<CreatureOriginData> ();
		CreatureOriginData random_mob;
		PlayerBehaviour p001;
	
		int maxmobs = 15;
		public int mobs = 0;
		float spawncooldown = 0.5f;
		float spawntimer;
		bool isspawning = true;
		
		TileMap	map;
		
		void Start () {
				p001 = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerBehaviour> ();
				map = GameObject.Find ("Map").GetComponent<TileMap> ();
				DataListObj = (CreatureDataList)Resources.Load ("Creatures");
				//enemyTypes = DataListObj.CreatureList;
				testmob = new CreatureOriginData[DataListObj.CreatureList.Count];
				DataListObj.CreatureList.CopyTo (testmob);
				foreach (CreatureOriginData tmp in testmob) {
						enemyTypes.Add (tmp);
				}
				//enemyTypes.();
		}
	
		// Update is called once per frame
		CreatureOriginData[] testmob;
		void Update () {
				if (mobs < maxmobs && p001.me.IsLoaded) {
						if (isspawning) {
								
								spawnmob (); // Counter und so jetzt in der Funktion
						}
						spawntimer -= Time.deltaTime;
						if (spawntimer <= 0) {
								isspawning = true;
						}
				}
		}
		bool MobInRegion (CreatureOriginData testmob, int region) {
				bool check = false;
				foreach (int mobregion in testmob.SpawnRegions) {
						if (mobregion == region) {
								check = true;	
						}
				}
				return check;
		}

		bool RandomMobGen () {
				bool return_mob = false;
				bool mob_gefunden = false;
				//int maxtry = 10;
				int maxtry = 10;
				while (!mob_gefunden && maxtry>0) {
						mob_gefunden = true;
						int mob_id = Random.Range (0, enemyTypes.Count);
						random_mob = enemyTypes [mob_id];
						Vector3 pos = new Vector3 (Random.Range (p001.me.Creat.Position.x - 30, p001.me.Creat.Position.x + 30), Random.Range (p001.me.Creat.Position.y - 30, p001.me.Creat.Position.y + 30), 0);
						if (random_mob.Prefab == null) {
								mob_gefunden = false;
						} 
						if (random_mob.IsBoss) {
								mob_gefunden = false;
						} 
						if (!MobInRegion (random_mob, map.tiles [(int)pos.x, (int)pos.y])) {
								mob_gefunden = false;
						}
						if (mob_gefunden) {
								random_mob.Position.x = (int)pos.x;
								random_mob.Position.y = (int)pos.y;
								return_mob = true;
						}
						maxtry--;
				}
		
				return return_mob;	
		}
		void spawnmob () {
				if (RandomMobGen ()) {
						Vector3 pos = new Vector3 (random_mob.Position.x, random_mob.Position.y, 0);
						GameObject tmpobjct = (GameObject)Instantiate (random_mob.Prefab, pos, Quaternion.identity);
						tmpobjct.transform.parent = GameObject.Find ("MonsterSpawner").transform;
						tmpobjct.GetComponent<CreatureController> ().Create (random_mob);
						tmpobjct.GetComponent<CreatureController> ().Creat.Position = pos;
						isspawning = false;
						spawntimer = spawncooldown;
						mobs++;
				}
		}
	
		public void spawnbosses () {
				foreach (CreatureOriginData tmpmob in enemyTypes) {
						if (tmpmob.IsBoss) {
								Vector3 pos = new Vector3 (tmpmob.Position.x, tmpmob.Position.y, 0);	
								GameObject tmpobjct = (GameObject)Instantiate (tmpmob.Prefab, pos, Quaternion.identity);
								tmpobjct.transform.parent = GameObject.Find ("MonsterSpawner").transform;
								tmpobjct.GetComponent<CreatureController> ().Create (tmpmob);
								tmpobjct.GetComponent<CreatureController> ().Creat.Position = pos;
						}
				}
		}
}

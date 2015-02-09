using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct monsters {
		//GeneralStats
		public int Monster_ID;
		public string pname;
		public Vector2 pos;
		public GameObject prefab;
		public bool boss;
	
		//Stats for Fight
		public int hp ; 
		public int maxhp ; 
		public int mana ; 
		public int maxmana ; 
		public int lvl ; 
		public int pwr ; 
		public int phy_armor ; 
		public int mag_armor ; 
		public int agility ; 
	
		//Drops
		public int golddrop;
		public int xpdrop;
		public List<ItemData> loot;
		public int[] spawnregion;
		public bool outPutDmg;
		public bool Moveable;
}

public class EnemySpawn : MonoBehaviour {
		public List<monsters> enemyTypes = new List<monsters> ();
		int maxmobs = 15;
		public int mobs = 0;
		float spawncooldown = 2.0f;
		float spawntimer;
		bool isspawning = true;
		player p001;
		TileMap	map;
		
		// Use this for initialization
		monsters CreatEmpty () {
				monsters mob = new monsters ();
				mob.Monster_ID = enemyTypes.Count + 1;
				mob.pname = "Standart Name";
				mob.pos = Vector2.zero;
				mob.prefab = (GameObject)Resources.Load ("Mob/Enemy");
				mob.boss = false;
				mob.maxhp = 1; 
				mob.hp = mob.maxhp;
				mob.maxmana = 1; 
				mob.mana = mob.maxmana; 
				mob.lvl = 1; 
				mob.pwr = 1; 
				mob.phy_armor = 0;
				mob.mag_armor = 0; 
				mob.agility = 1; 
				mob.golddrop = 0;
				mob.xpdrop = 0;
				mob.loot = new List<ItemData> ();
				mob.spawnregion = new int[1];
				mob.spawnregion [0] = map.GetRegionWithName ("Grass");
				mob.outPutDmg = false;
				mob.Moveable = true;
				return mob;
		}
		void Start () {
				p001 = GameObject.Find ("Main Camera").GetComponent<player> ();
				map = GameObject.Find ("Map").GetComponent<TileMap> ();
			
				monsters tmpmob;
				tmpmob = CreatEmpty ();
				tmpmob.pname = "DmgDummy";
				tmpmob.maxhp = 10000000;
				tmpmob.boss = true;
				tmpmob.pos = new Vector2 (80, 82);
				tmpmob.prefab = (GameObject)Resources.Load ("Mob/DmgDummy");
				tmpmob.outPutDmg = true;
				tmpmob.Moveable = false;
				enemyTypes.Add (tmpmob);
		
				tmpmob = CreatEmpty ();
				tmpmob.pname = "Stefan";
				tmpmob.maxhp = 200;
				tmpmob.maxmana = 100;
				tmpmob.pwr = 10;
				tmpmob.phy_armor = 10;
				tmpmob.mag_armor = 5; 
				tmpmob.agility = 1;
				tmpmob.golddrop = 80;
				tmpmob.xpdrop = 35;
				tmpmob.prefab = (GameObject)Resources.Load ("Mob/Gorilla");
				tmpmob.spawnregion = new int[1];
				tmpmob.spawnregion [0] = map.GetRegionWithName ("Forest");
				enemyTypes.Add (tmpmob);
		
				tmpmob = CreatEmpty ();
				tmpmob.pname = "Spider";
				tmpmob.maxhp = 100;
				tmpmob.maxmana = 100;
				tmpmob.pwr = 5;
				tmpmob.phy_armor = 10;
				tmpmob.mag_armor = 5;
				tmpmob.agility = 2;
				tmpmob.golddrop = 100;
				tmpmob.xpdrop = 25;
				tmpmob.spawnregion = new int[2];
				tmpmob.spawnregion [0] = map.GetRegionWithName ("Forest");
				tmpmob.spawnregion [1] = map.GetRegionWithName ("Grass");
				enemyTypes.Add (tmpmob);
		
				tmpmob = CreatEmpty ();
				tmpmob.pname = "Cyclop";
				tmpmob.maxhp = 5000;
				tmpmob.maxmana = 100;
				tmpmob.pwr = 75;
				tmpmob.phy_armor = 30;
				tmpmob.mag_armor = 50;
				tmpmob.agility = 1;
				tmpmob.golddrop = 1000;
				tmpmob.xpdrop = 250;
				tmpmob.prefab = (GameObject)Resources.Load ("Mob/Cyclop");
				tmpmob.boss = true;
				tmpmob.pos = new Vector2 (80, 70);
				//tmpmob.spawnregion = new int[1];
				//tmpmob.spawnregion [0] = map.GetRegionWithName ("Bei Boss egal");
				enemyTypes.Add (tmpmob);
		}
	
		// Update is called once per frame
		void Update () {
				if (mobs < maxmobs && GameObject.Find ("Main Camera").GetComponent<mainmenu> ().gameloaded) {
						if (isspawning) {
								spawnmob (); // Counter und so jetzt in der Funktion
						}
						spawntimer -= Time.deltaTime;
						if (spawntimer <= 0) {
								isspawning = true;
						}
				}
		}
		bool MobInRegion (monsters testmob, int region) {
				bool check = false;
				foreach (int mobregion in testmob.spawnregion) {
						if (mobregion == region) {
								check = true;	
						}
				}
				return check;
		}
		monsters random_mob;
		bool RandomMobGen () {
				bool return_mob = false;
				bool mob_gefunden = false;
				//int maxtry = 10;
				int maxtry = 10;
				while (!mob_gefunden && maxtry>0) {
						mob_gefunden = true;
						int mob_id = Random.Range (0, enemyTypes.Count);
						random_mob = enemyTypes [mob_id];
						Vector3 pos = new Vector3 (Random.Range (p001.pos.x - 30, p001.pos.x + 30), Random.Range (p001.pos.y - 30, p001.pos.y + 30), 0);
						if (map.tiles [(int)pos.x, (int)pos.y] == null) {
								mob_gefunden = false;
						}
						if (random_mob.prefab == null) {
								mob_gefunden = false;
						} 
						if (random_mob.boss) {
								mob_gefunden = false;
						} 
						if (!MobInRegion (random_mob, map.tiles [(int)pos.x, (int)pos.y])) {
								mob_gefunden = false;
						}
						if (mob_gefunden) {
								random_mob.pos.x = (int)pos.x;
								random_mob.pos.y = (int)pos.y;
								return_mob = true;
						}
						maxtry--;
				}
		
				return return_mob;	
		}
		void spawnmob () {
				if (RandomMobGen ()) {
						Vector3 pos = new Vector3 (random_mob.pos.x, random_mob.pos.y, 0);
						GameObject tmpobjct = (GameObject)Instantiate (random_mob.prefab, pos, Quaternion.identity);
						tmpobjct.transform.parent = GameObject.Find ("MonsterSpawner").transform;
						tmpobjct.GetComponent<enemy> ().SettingStats (random_mob);
						tmpobjct.GetComponent<enemy> ().thismob.pos = pos;
						isspawning = false;
						spawntimer = spawncooldown;
						mobs++;
				}
		}
	
		public void spawnbosses () {
				foreach (monsters tmpmob in enemyTypes) {
						if (tmpmob.boss) {
								Vector3 pos = new Vector3 (tmpmob.pos.x, tmpmob.pos.y, 0);	
								GameObject tmpobjct = (GameObject)Instantiate (tmpmob.prefab, pos, Quaternion.identity);
								tmpobjct.transform.parent = GameObject.Find ("MonsterSpawner").transform;
								tmpobjct.GetComponent<enemy> ().SettingStats (tmpmob);
								tmpobjct.GetComponent<enemy> ().thismob.pos = pos;
						}
				}
		}
}

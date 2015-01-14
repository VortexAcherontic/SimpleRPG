using UnityEngine;
using System.Collections.Generic;

public struct thismonster {
		
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
		public int armor ; 
		public int agility ; 
	
		//Drops
		public int golddrop;
		public int xpdrop;
		//public List<items> loot = new List<items> ();
		
}
public class enemy : MonoBehaviour {
		public thismonster thismob;

		public void SettingStats (EnemyType mt) {
				thismob.Monster_ID = mt.Monster_ID;
				thismob.pname = mt.pname;
				thismob.pos = mt.pos;
				thismob.prefab = mt.prefab;
				thismob.boss = mt.boss;
				thismob.hp = mt.hp;
				thismob.maxhp = mt.maxhp;
				thismob.mana = mt.mana;
				thismob.maxmana = mt.maxmana;
				thismob.lvl = mt.lvl;
				thismob.pwr = mt.pwr;
				thismob.armor = mt.armor;
				thismob.agility = mt.agility;
				thismob.xpdrop = mt.xpdrop;
				thismob.golddrop = mt.golddrop;
		}
	
		// Update is called once per frame
		void Update () {
	
		}
}

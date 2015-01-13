using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class EnemyType {
	
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


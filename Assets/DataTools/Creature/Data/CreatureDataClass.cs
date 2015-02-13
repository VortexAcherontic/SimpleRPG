using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 *  Immer daran denken auch den Inspector zu erweitern wenn hier was dazu kommt!
 *	Falls wir uns wirklich noch dazu entscheiden den Inspector zu erstellen
 */

public enum BattleStance {
		melee,
		range,
		magic
}

[System.Serializable]
public struct CreatureOriginData { // Clean Own Stats
		// Allround Stats
		public string Name;
		public Vector2 Position;
		public BattleStance Stance;
		public int Gold; // For kills: GoldDrop
		public int XP;	// For kills: XpDrop
		public GameObject Prefab;
	
		public float MoveTimer;
		public bool IsRegAble;
		public float RegTimer;
		public float RegCooldown;
		public float AttackTimer;
		public float AttackCooldown;
	
		// Changeable by Level UP (Player) or Stick to Input (Monster/NPC)
		public int Str; // Phy Dmg
		public int Dex; // Hit Chance or Phy Dmg Range?
		public int Agi;	// Avoid Chance
		public int Int; // Mag Dmg & Mana & Ressistance to Mag Dmg?
		public int Vit;	// Health
		public int Luc; // Crit Hit Chance?
	
		// Only Player
		public int Level;
		public int StatPoints;
	
		// Non for Player
		public int AggroRange;
		public bool IsMoveable;
		public bool IsBoss;
		public bool DoRespawn; // NPC/Bosses?
		public float RespawnTimer;
		
		
	
		/*
		 * Doof mit den String Array aber mir fällt nichts besseres ein
		 * 
		 */
		[HideInInspector]
		public List<int>
				SpawnRegions;
		public string[] SpawnRegions_Strings;
	
		// Item Drops & Inventory
		[HideInInspector]
		public List<ItemData>
				Inventory; // == ItemDrop
		public string[] Inventory_Strings;
		[HideInInspector]
		public List<ItemData>
				Equipment;
		public string[] Equipment_Strings;	
}


[System.Serializable]
public struct CreatureData {
		public CreatureOriginData InitalStats;
	#region doppelt von Origin
		// Allround Stats
		public string Name;
		public Vector2 Position;
		public BattleStance Stance;
		public int Gold; // For kills: GoldDrop
		public int XP;	// For kills: XpDrop
		public GameObject Prefab;
	
		public float MoveTimer;
		public bool IsRegAble;
		public float RegTimer;
		public float RegCooldown;
		public float AttackTimer;
		public float AttackCooldown;
	
		// Changeable by Level UP (Player) or Stick to Input (Monster/NPC)
		public int Str; // Phy Dmg
		public int Dex; // Hit Chance or Phy Dmg Range?
		public int Agi;	// Avoid Chance
		public int Int; // Mag Dmg & Mana & Ressistance to Mag Dmg?
		public int Vit;	// Health
		public int Luc; // Crit Hit Chance?
	
		// Only Player
		public int Level;
		public int StatPoints;
	
		// Non for Player
		public int AggroRange;
		public bool IsMoveable;
		public bool IsBoss;
		public bool DoRespawn; // NPC/Bosses?
		public float RespawnTimer;
	
		/*
		 * Doof mit den String Array aber mir fällt nichts besseres ein
		 * 
		 */
		[HideInInspector]
		public List<int>
				SpawnRegions;
		public string[] SpawnRegions_Strings;
	
		// Item Drops & Inventory
		[HideInInspector]
		public List<ItemData>
				Inventory; // == ItemDrop
		public string[] Inventory_Strings;
		[HideInInspector]
		public List<ItemData>
				Equipment;
		public string[] Equipment_Strings;	
	#endregion
	
		public Vector2 lastPos;
	
		public int HP;
		public int MP;
		public int MaxHP;
		public int MaxMP;
	
		public int PhyArmor;
		public int MagArmor;
		public int PhyAttack;
		public int MagAttack;

		public int AttackRange;
		public float Movement_Delay;
}



using UnityEngine;
using System.Collections;

/*
 *  Immer daran denken auch den Inspector zu erweitern wenn hier was dazu kommt!
 *	Falls wir uns wirklich noch dazu entscheiden den Inspector zu erstellen
 */

public enum BattleStance {
		meele,
		range,
		magic
}

[System.Serializable]
public class CreatureOriginData { // Clean Own Stats
		// Allround Stats
		public string Name;
		public Vector2 Position;
		public BattleStance Stance;
		public int Gold; // For kills: GoldDrop
		public int XP;	// For kills: XpDrop
	
		// Changeable by Level UP (Player) or Stick to Input (Monster/NPC)
		public int Str; // Phy Dmg
		public int Dex; // Hit Chance
		public int Agi;	// Avoid Chance
		public int Int; // Mag Dmg & Mana & Ressistance to Mag Dmg?
		public int Vit;	// Health
		public int Luc; // Crit Hit Chance?
	
		// Only Player
		public int Level;
		public int StatPoints;
	
		// Non for Player
		public bool IsBoss;
		public bool DoRespawn; // NPC/Bosses?
		public regions[] SpawnRegions;
}


[System.Serializable]
public class CreatureData : CreatureOriginData { // Based on Creature Stats + Equipment etc. (For Battles)
		public CreatureOriginData InitalStats;
	
		public int HP;
		public int MP;
		public int MaxHP;
		public int MaxMP;
	
		public int PhyArmor;
		public int MagArmor;
		public int PhyAttack;
		public int MagAttack;
	
		public void CalculateStats () {
				// Getting normal Stats (Auch Equip und so, eigentlich alle Werte aus InitalStats den hier entsprechenden zuweisen...)
				Str = InitalStats.Str;	
				Agi = InitalStats.Agi;
				Dex = InitalStats.Dex;
				Int = InitalStats.Int;
				Vit = InitalStats.Vit;
				Luc = InitalStats.Luc;
		
				// Adding Stats on Stuff like Equiptment (or Buffs)
				PhyArmor = 0;
				MagArmor = 0;
				PhyAttack = 0;
				MagAttack = 0;
		
				// Calculate Battle Relevant Stuff
				MaxHP = Vit * 20;
				MaxMP = Int * 20;
				PhyArmor += Vit * 3;
				MagArmor += Int * 3;
				PhyAttack += Str * 3;
				MagAttack += Int * 3;
		}
}
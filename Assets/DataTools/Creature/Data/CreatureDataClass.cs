using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
		public GameObject Prefab;
	
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

		public void Create () {
				ItemDataList DataListObj;
				DataListObj = (ItemDataList)Resources.Load ("Items");
				// Equipment richtig eintragen
				foreach (string tmpitem in Equipment_Strings) {
						Equipment.Add (DataListObj.item_mit_name (tmpitem));
				}
				// Iventory richtig eintragen
				foreach (string tmpitem in Inventory_Strings) {
						Inventory.Add (DataListObj.item_mit_name (tmpitem));
				}
				// Spawn regions richtig eintragen?
				foreach (string tmport in SpawnRegions_Strings) {
						SpawnRegions.Add (GameObject.Find ("Map").GetComponent<TileMap> ().GetRegionWithName (tmport));
			
				}
		}
	
		public CreatureOriginData Clone () {
				return (CreatureOriginData)this.MemberwiseClone ();
		}
	
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

		public int AttackRange;
	
		/*
		 * ACHTUNG!
		 * Start und Update wird hier nicht automatisch ausgeführt!
		 * 
		 */
	
		public CreatureData Clone () {
				return (CreatureData)this.MemberwiseClone ();
		}
	
		public void Start (CreatureOriginData CData) { // Bekannte Namen, damit man weiß was des machen soll XD
				InitalStats = new CreatureOriginData ();
				InitalStats = CData;
				InitalStats.Create ();
				Name = InitalStats.Name;
				Update ();
				HP = MaxHP;
				MP = MaxMP;
				// nun ist es so geladen das es los gehen kann!
		}
	
		public void Update () { // Bekannte Namen, damit man weiß was des machen soll XD
				CalculateStats ();
		}
	
		public void CalculateStats () {
				// Getting normal Stats (Auch Equip und so, eigentlich alle Werte aus InitalStats den hier entsprechenden zuweisen...)
				// also total nervig -.- Oder das geht irgendwie einfacher und ich weiß nicht wie
				// Obwohl manche sachen brauch man villeicht doch nicht so
				Str = InitalStats.Str;	
				Agi = InitalStats.Agi;
				Dex = InitalStats.Dex;
				Int = InitalStats.Int;
				Vit = InitalStats.Vit;
				Luc = InitalStats.Luc;
				AttackRange = 1;
		
				// Adding Stats on Stuff like Equiptment (or Buffs)
				PhyArmor = 0;
				MagArmor = 0;
				PhyAttack = 0;
				MagAttack = 0;
				int EffectHP = 0;
				int EffectMP = 0;
				foreach (ItemData tmp in InitalStats.Equipment) {
						if (tmp.IsStaticEffect) {
								switch (tmp.EffectType) {
										case EffectType.Health:
												EffectHP += tmp.Effect;
												break;
										case EffectType.Mana:
												EffectMP += tmp.Effect;
												break;
										case EffectType.Str:
												Str += tmp.Effect;
												break;
										case EffectType.Agi:
												Agi += tmp.Effect;
												break;
										case EffectType.Dex:
												Dex += tmp.Effect;
												break;
										case EffectType.Int:
												Int += tmp.Effect;
												break;
										case EffectType.Vit:
												Vit += tmp.Effect;
												break;
										case EffectType.Luc:
												Luc += tmp.Effect;
												break;
								}
						}
						
						// With BattleStance
						switch (tmp.Type) {
								case ItemType.accessorie:
								case ItemType.armor_feet:
								case ItemType.armor_hand:
								case ItemType.armor_head:
								case ItemType.armor_leg:
								case ItemType.armor_torso:
								case ItemType.potion:
								case ItemType.utility:
										PhyArmor += tmp.PhyArmor;
										MagArmor += tmp.MagArmor;
										PhyAttack += tmp.PhyAttack;
										MagAttack += tmp.MagAttack;
										AttackRange += tmp.Range;
										break;
								case ItemType.weapon_meele:
										if (Stance == BattleStance.meele) {
												PhyArmor += tmp.PhyArmor;
												MagArmor += tmp.MagArmor;
												PhyAttack += tmp.PhyAttack;
												MagAttack += tmp.MagAttack;
												AttackRange += tmp.Range;
										}
										break;
								case ItemType.weapon_ammo:
								case ItemType.weapon_range:
										if (Stance == BattleStance.range) {
												PhyArmor += tmp.PhyArmor;
												MagArmor += tmp.MagArmor;
												PhyAttack += tmp.PhyAttack;
												MagAttack += tmp.MagAttack;
												AttackRange += tmp.Range;
										}
										break;
						// TODO: What gives Mage Bonus Dmg? Range Weapon? Meele Weapon? Both?
						// statt bonus - verschiedene zauber zB heilen/angriff/buff/debuffs entfernen
						}
			
				}
		
				// Calculate Battle Relevant Stuff
				MaxHP = EffectHP + (Vit * 20);
				MaxMP = EffectMP + (Int * 20);
				// Armor sollte nicht durch stats mehr werden
				//PhyArmor += Vit * 3;
				//MagArmor += Int * 3;
				PhyAttack += Str * 3;
				MagAttack += Int * 3;
		}
}



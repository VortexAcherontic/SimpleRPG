using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 *  Immer daran denken auch den Inspector zu erweitern wenn hier was dazu kommt!
 *	Falls wir uns wirklich noch dazu entscheiden den Inspector zu erstellen
 */

public enum ItemType {
		junk,
	
		weapon_melee,
		weapon_range,
		weapon_ammo,
		armor_head,
		armor_torso,
		armor_leg,
		armor_feet,
		armor_hand,
	
		potion,
		accessorie,
		utility,
		
}

public enum EffectType {
		None,
		// Base Bonus
		Health,
		Mana,
		// Stats Bonus
		Str,
		Dex,
		Agi,
		Int,
		Vit,
		Luc
}

[System.Serializable]
public struct AmmoData { // erstmal aus dem alten übernommen und leicht abgeändert
		public string Name;
		public ItemType Type;
		public int Gold;
		public int Stock;
		public int RefillMod;
		public float Weigth;
		public int Durability;
		public int MaxDurability;
	
		// Base Stats
		public int MagAttack;
		public int PhyAttack;
		public int PhyArmor;
		public int MagArmor;
		public int Range;
	
		// Potions
		public EffectType EffectType;
		public int Effect;
		public bool IsStaticEffect;
	
		// Quiver
		public int Capacity;
		public int MaxCapacity;
}


[System.Serializable]
public struct ItemData { // erstmal aus dem alten übernommen und leicht abgeändert
		public string Name;
		public ItemType Type;
		public int Gold;
		public int Stock;
		public int RefillMod;
		public float Weigth;
		public int Durability;
		public int MaxDurability;
	
		// Base Stats
		public int MagAttack;
		public int PhyAttack;
		public int PhyArmor;
		public int MagArmor;
		public int Range;
	
		// Potions, + AddStats(passive)?
		public EffectType EffectType;
		public int Effect;
		public bool IsStaticEffect;
	
		// Quiver
		public int Capacity;
		public int MaxCapacity;
		[HideInInspector]
		public List<AmmoData>
				Ammo;
}

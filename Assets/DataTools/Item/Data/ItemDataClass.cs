using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 *  Immer daran denken auch den Inspector zu erweitern wenn hier was dazu kommt!
 *	Falls wir uns wirklich noch dazu entscheiden den Inspector zu erstellen
 */

public enum ItemType {
		meele,
		range,
		armor_head,
		armor_torso,
		armor_leg,
		armor_feet,
		armor_hand,
		potion,
		ammo,
		accessorie,
		utility
}

public enum EffectType {
		None,
		Health,
		Mana
}

[System.Serializable]
public class ItemData { // erstmal aus dem alten übernommen und leicht abgeändert
		public string Name;
		public ItemType Type;
		public int Gold;
		public int Stock;
		public int RefillMod;
		public float Weigth;
	
		// Base Stats
		public int MagAttack;
		public int PhyAttack;
		public int PhyArmor;
		public int MagArmor;
	
		// Potions, + AddStats(passive)?
		public EffectType EffectType;
		public int Effect;
		public bool IsStaticEffect;
	
		// Quiever
		public int Capacity;
		public int MaxCapacity;
		public ItemData Ammo;
}

[System.Serializable]
public class ItemDataList : ScriptableObject {
		public List<ItemData> ItemList;
}

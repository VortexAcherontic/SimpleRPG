using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public struct EnemyTokillStruct {
		public string Name	;
		public int Amount;
}
[System.Serializable]
public struct ItemsToCollectStruct {
		public string Name	;
		public int Amount;
}
[System.Serializable]
public struct QuestStruct {
		public string Name; // Bezeichnung
		public string NPC_Geber;
		public bool finished;
		public bool accepted;
		public bool storyrelevant;
		public bool failed;
	
	
		public List<EnemyTokillStruct> EnemyTokill;
		public List<ItemsToCollectStruct> ItemsToCollect;
		public List<string> NPCToTalk;
		public List<string> PrevQuest;
	
		public List<string> Loot;
		public int LootGold;
		public int LootXP;
	
}

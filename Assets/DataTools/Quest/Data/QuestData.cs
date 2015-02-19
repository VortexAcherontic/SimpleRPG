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
		public string NPC_Geber;
		public bool finished;
		public bool accepted;
	
		public List<EnemyTokillStruct> EnemyTokill;
		public List<ItemsToCollectStruct> ItemsToCollect;
		public List<string> NPCToTalk;
	
		public List<string> Loot;
	
}

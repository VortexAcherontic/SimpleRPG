using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Savegame :ScriptableObject {
		public CreatureData Creature;
		public List<QuestStruct> Quests;
		public List<SkillAndKeys> Keys;
}

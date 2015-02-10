using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CreatureDataList : ScriptableObject {
		public List<CreatureOriginData> CreatureList;

		public void Update () {
				foreach (CreatureOriginData Creature in CreatureList) {
						Creature.Create ();
				}
		}
}

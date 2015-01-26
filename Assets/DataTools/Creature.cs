using UnityEngine;
using System.Collections;

public class Creature : MonoBehaviour {
		public bool IsLoaded = false;
		public CreatureStats Stats;
		
		void Start () {
				// Nichts kann geladen sein bevor es Spawned,
				// Aber es kann sein das des Object Exestiert, wenn es noch nicht geladen ist
				// Also nur zur Sicherheit
				IsLoaded = false; 
		}
	
		public void Spawn (CreatureOriginStats StatsCreature) {
				Stats.InitalStats = StatsCreature;
				Stats.CalculateStats ();
				Stats.HP = Stats.MaxHP;
				Stats.MP = Stats.MaxMP;
				IsLoaded = true;
		}
	
		// Update is called once per frame
		void Update () {
				if (IsLoaded) {
						Stats.CalculateStats ();
				}
		}
}




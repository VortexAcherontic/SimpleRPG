using UnityEngine;
using System.Collections;

public class Creature : MonoBehaviour {
		public bool IsLoaded = false;
		public bool IsDead = false; // Was passiert wenns tot ist, sollte dann in Behavior?
		CreatureData Stats;
		
		void Start () {
				// Nichts kann geladen sein bevor es Spawned,
				// Aber es kann sein das des Object Exestiert, wenn es noch nicht geladen ist
				// Also nur zur Sicherheit
				IsLoaded = false; 
				IsDead = false;
		}
		void Update () { // Wenn möglich der Ordnung halber hier nur Funktionen aufrufen
				if (IsLoaded && !IsDead) {
						Stats.CalculateStats ();
						CheckingDeath ();
				}
		}
		public void Spawn (CreatureOriginData StatsCreature) {
				Stats.InitalStats = StatsCreature;
				Stats.CalculateStats ();
				Stats.HP = Stats.MaxHP;
				Stats.MP = Stats.MaxMP;
				IsLoaded = true;
		}
		void CheckingDeath () {
				if (Stats.HP <= 0) {
						IsDead = true;
				}
		}
}




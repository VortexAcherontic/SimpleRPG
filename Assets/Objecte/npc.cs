using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class npc : MonoBehaviour {

		public int hp;
		public int armor;
		public int agility;
		public int pwr;
		public string NPC_name;
		public int golddrop;
		public int xpdrop;
		public Vector2 pos;
		public List<ItemData> Drops = new List<ItemData> ();
		public bool onposition;
		player p001;

		// Use this for initialization
		void Beginn (int hp, int armor, int agility, int pwr, string NPC_name, int golddrop, int xpdrop, int posx, int posy) {
				this.hp = hp;
				this.armor = armor;
				this.agility = agility;
				this.pwr = pwr;
				this.NPC_name = NPC_name;
				this.golddrop = golddrop;
				this.xpdrop = xpdrop;
				this.pos = new Vector2 (posx, posy);
				p001 = GameObject.Find ("Main Camera").GetComponent<player> ();
		}

		/*

	void randomLoot(player){
	player.xp += xpdrop;
	player.gold += golddrop;
	
}*/



		void sprechen () {
				Debug.Log ("blablabla");
				//auswahl1 handeln (wenn npc handeln kann)
				//auswahl2 quest (wenn npc)
				//
		}

		/* kann kämpfen */

		/* kann handeln? */

		/* iwelche sonderfunktionen */


		// Update is called once per frame
		void Update () {
				if (p001 != null) {
						if (p001.pos == pos) {
								onposition = true;
						} else {
								onposition = false;
						}
						if (onposition) {
								sprechen ();
						}
				}
		}
}

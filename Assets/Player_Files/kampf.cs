using UnityEngine;
using System.Collections;

public class kampf : MonoBehaviour {
		player p001;
		player p002;
		npc n001;
		int temp_x;
		int temp_y;
		int distance_manhatten;
		string angriffsrichtung;
		string angriffstil = "melee";
		bool attack = false;
		int phy_damage;
		int mag_damage;
		
		//public bool inarena; besser in map?
		//monster m001;


		// Use this for initialization
		void Start () {
				p001 = GameObject.Find ("Main Camera").GetComponent<player> ();
				p002 = GameObject.Find ("Main Camera").GetComponent<player> ();
				n001 = GameObject.Find ("Main Camera").GetComponent<npc> ();
		}
	
		// Update is called once per frame
		void Update () {
				SelectAttackStyle ();
				Angriffsrichtung ();
		}

		void Angriffsrichtung () {
				bool attack_ausgefuert = false;
				if (Input.GetKeyDown ("left")) {
						angriffsrichtung = "l";
						attack_ausgefuert = true;
				}
				if (Input.GetKeyDown ("right")) {
						angriffsrichtung = "r";
						attack_ausgefuert = true;
				}
				if (Input.GetKeyDown ("up")) {
						angriffsrichtung = "o";
						attack_ausgefuert = true;
				}
				if (Input.GetKeyDown ("down")) {
						angriffsrichtung = "u";
						attack_ausgefuert = true;
				}
				if (attack_ausgefuert) {
						switch (angriffstil) {
								case "melee":
										Melee ();
										break;
								case "range":
										Range ();
										break;
								case "magic":
										Mage ();
										break;
						}	
				}
		}
	
		void SelectAttackStyle () {
				if (Input.GetKeyDown ("1")) {
						angriffstil = "melee";
				}
				if (Input.GetKeyDown ("2")) {
						angriffstil = "range";
				}
				if (Input.GetKeyDown ("3")) {
						angriffstil = "magic";
				}
		}
		
		void Melee () {
				foreach (Transform tmp_monster in GameObject.Find("MonsterSpawner").transform) {
						if (tmp_monster.GetComponent<EnemyBehaviour> ().CheckDistance () <= 1) {
								if ((p001.pos.x < tmp_monster.position.x) && (angriffsrichtung == "r")) {
										attack = true;
								}
								if ((p001.pos.x > tmp_monster.position.x) && (angriffsrichtung == "l")) {
										attack = true;
								}
								if ((p001.pos.y < tmp_monster.position.y) && (angriffsrichtung == "o")) {
										attack = true;
								}
								if ((p001.pos.y > tmp_monster.position.y) && (angriffsrichtung == "u")) {
										attack = true;
								}
						}
						if (attack) {
								CheckForMeleeWeapon (out phy_damage, out mag_damage);
								//physischer Schaden
								tmp_monster.GetComponent<enemy> ().thismob.hp -= ((p001.pwr * phy_damage) - tmp_monster.GetComponent<enemy> ().thismob.phy_armor);
								//magischer Schaden
								tmp_monster.GetComponent<enemy> ().thismob.hp -= ((mag_damage) - tmp_monster.GetComponent<enemy> ().thismob.mag_armor);
								attack = false;
						}
				}
		}
	
		void Range () {
				foreach (Transform tmp_monster in GameObject.Find("MonsterSpawner").transform) {
						if (tmp_monster.GetComponent<EnemyBehaviour> ().CheckDistance () <= p001.rangeweapondistance) {
								if ((p001.pos.x < tmp_monster.position.x) && (angriffsrichtung == "r")) {
										attack = true;
								}
								if ((p001.pos.x > tmp_monster.position.x) && (angriffsrichtung == "l")) {
										attack = true;
								}
								if ((p001.pos.y < tmp_monster.position.y) && (angriffsrichtung == "o")) {
										attack = true;
								}
								if ((p001.pos.y > tmp_monster.position.y) && (angriffsrichtung == "u")) {
										attack = true;
								}
						}
						if ((attack) && (CheckForArrow ())) {
								CheckForRangeWeapon (out phy_damage, out mag_damage);
								//physischer Schaden
								if (phy_damage >= tmp_monster.GetComponent<enemy> ().thismob.phy_armor) {
										tmp_monster.GetComponent<enemy> ().thismob.hp -= ((p001.pwr * phy_damage * p001.agility) - tmp_monster.GetComponent<enemy> ().thismob.phy_armor);
								}
								//magischer Schaden
								if (mag_damage >= tmp_monster.GetComponent<enemy> ().thismob.mag_armor) {
										tmp_monster.GetComponent<enemy> ().thismob.hp -= ((mag_damage) - tmp_monster.GetComponent<enemy> ().thismob.mag_armor);
								}
								attack = false;
						}
				}
		}
	
		void Mage () {
				foreach (Transform tmp_monster in GameObject.Find("MonsterSpawner").transform) {
						if (tmp_monster.GetComponent<EnemyBehaviour> ().CheckDistance () <= 2) {
								if ((p001.pos.x < tmp_monster.position.x) && (angriffsrichtung == "r")) {
										attack = true;
								}
								if ((p001.pos.x > tmp_monster.position.x) && (angriffsrichtung == "l")) {
										attack = true;
								}
								if ((p001.pos.y < tmp_monster.position.y) && (angriffsrichtung == "o")) {
										attack = true;
								}
								if ((p001.pos.y > tmp_monster.position.y) && (angriffsrichtung == "u")) {
										attack = true;
								}
						}
						if ((attack) && (p001.mana > 200)) {
								//magischer Schaden
								tmp_monster.GetComponent<enemy> ().thismob.hp -= ((p001.maxmana / p001.pwr * Mathf.Abs ((1 - (p001.mana / p001.maxmana)))) - tmp_monster.GetComponent<enemy> ().thismob.mag_armor);
								p001.mana -= 200;
								attack = false;
						}
				}
		}
	
		public void OnGUI () {
				if (!GameObject.Find ("Main Camera").GetComponent<mainmenu> ().showgamemenue) {
						GUI.Label (new Rect (5, Screen.height - 105, 170, 20), "Battlemode: " + angriffstil);
				}
		}
	
		bool CheckForArrow () {
				foreach (items tmp_item in p001.Equip) {
						if (tmp_item.type == itemtype.utility) {
								if (tmp_item.capacity.Count >= 1) {
										return true;
								}
						}
				}
				return false;
		}
	
		void CheckForMeleeWeapon (out int phy_damage, out int mag_damage) {
				foreach (items tmp_item in p001.Equip) {
						if (tmp_item.type == itemtype.Nahkampf) {
								phy_damage = tmp_item.phy_dmg;
								mag_damage = tmp_item.mag_dmg;
						}
				}
				phy_damage = 5;
				mag_damage = 5;
		}
	
		void CheckForRangeWeapon (out int phy_damage, out int mag_damage) {
				foreach (items tmp_item in p001.Equip) {
						if (tmp_item.type == itemtype.Fernkampf) {
								phy_damage = tmp_item.phy_dmg;
								mag_damage = tmp_item.mag_dmg;
						}
				}
				phy_damage = 5;
				mag_damage = 5;
		}
	
		/*magieschaden berechnen*/
		/*fernkampfschaden berechnen (ammo abziehen nicht vergessen)*/
		/*nahkampfschaden berechnen*/

		/*variable die gegnertyp festlegt?*/

		/*schaden gegenüber player berechnen durch NPC*/
		/*schaden gegenüber player berechnen durch Monster*/

		/*pvp lieber im arena modus? dann könnte man direkt ne funktion schreiben bei der beide player aufeinander einprügeln*/

}

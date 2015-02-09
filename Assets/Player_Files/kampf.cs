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
	
		float attack_cooldown = 0.5f; // Veilleicht abhänig von agi? Gewicht? Waffe!
		float attack_timer = 0;
		
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
				if (attack_timer <= 0) {
						SelectAttackStyle ();
						Angriffsrichtung ();
				}
				attack_timer -= Time.deltaTime;
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
										attack_timer = attack_cooldown;
										break;
								case "range":
										Range ();
										attack_timer = attack_cooldown;
										break;
								case "magic":
										Mage ();
										attack_timer = attack_cooldown;
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
								
								int phydmg = ((p001.pwr * phy_damage) - tmp_monster.GetComponent<enemy> ().thismob.phy_armor);
								int magdmg = ((mag_damage) - tmp_monster.GetComponent<enemy> ().thismob.mag_armor);
				
								//physischer Schaden
								tmp_monster.GetComponent<enemy> ().thismob.hp -= phydmg;
								//magischer Schaden
								tmp_monster.GetComponent<enemy> ().thismob.hp -= magdmg;
				
								if (tmp_monster.GetComponent<enemy> ().thismob.outPutDmg) {
										Debug.Log ("Phy: " + phydmg + " / Mag: " + magdmg);
								}
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
				
								int phydmg = ((p001.pwr * phy_damage * p001.agility) - tmp_monster.GetComponent<enemy> ().thismob.phy_armor);
								int magdmg = ((mag_damage) - tmp_monster.GetComponent<enemy> ().thismob.mag_armor);
				
								//physischer Schaden
								if (phy_damage >= tmp_monster.GetComponent<enemy> ().thismob.phy_armor) {
										tmp_monster.GetComponent<enemy> ().thismob.hp -= phydmg;
								}
								//magischer Schaden
								if (mag_damage >= tmp_monster.GetComponent<enemy> ().thismob.mag_armor) {
										tmp_monster.GetComponent<enemy> ().thismob.hp -= magdmg;
								}
				
								if (tmp_monster.GetComponent<enemy> ().thismob.outPutDmg) {
										Debug.Log ("Phy: " + phydmg + " / Mag: " + magdmg);
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
				
								int phydmg = 0;
								int magdmg = ((p001.maxmana / p001.pwr * Mathf.Abs ((1 - (p001.mana / p001.maxmana)))) - tmp_monster.GetComponent<enemy> ().thismob.mag_armor);
				
								//magischer Schaden
								tmp_monster.GetComponent<enemy> ().thismob.hp -= magdmg;
				
								if (tmp_monster.GetComponent<enemy> ().thismob.outPutDmg) {
										Debug.Log ("Phy: " + phydmg + " / Mag: " + magdmg);
								}
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
				foreach (ItemData tmp_item in p001.Equip) {
						if (tmp_item.Type == ItemType.utility) {
								if (tmp_item.Capacity >= 1) {
										return true;
								}
						}
				}
				return false;
		}
	
		void CheckForMeleeWeapon (out int phy_damage, out int mag_damage) {
				foreach (ItemData tmp_item in p001.Equip) {
						if (tmp_item.Type == ItemType.weapon_meele) {
								phy_damage = tmp_item.PhyAttack;
								mag_damage = tmp_item.MagAttack;
						}
				}
				phy_damage = 5;
				mag_damage = 5;
		}
	
		void CheckForRangeWeapon (out int phy_damage, out int mag_damage) {
				foreach (ItemData tmp_item in p001.Equip) {
						if (tmp_item.Type == ItemType.weapon_range) {
								phy_damage = tmp_item.PhyAttack;
								mag_damage = tmp_item.MagAttack;
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

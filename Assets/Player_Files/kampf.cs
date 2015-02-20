using UnityEngine;
using System.Collections;

public class kampf : MonoBehaviour {
		PlayerBehaviour p001;
		
		int temp_x;
		int temp_y;
		int distance_manhatten;
		string angriffsrichtung;
		BattleStance angriffstil;
		bool attack = false;
	
		float attack_cooldown = 0.5f; // Veilleicht abhänig von agi? Gewicht? Waffe!
		float attack_timer = 0;
		
		//public bool inarena; besser in map?
		//monster m001;


		// Use this for initialization
		void Start () {
				p001 = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerBehaviour> ();
		}
	
		// Update is called once per frame
		void Update () {
				if (attack_timer <= 0) {
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
						switch (p001.me.Creat.Stance) {
								case BattleStance.melee:
										Melee ();
										attack_timer = attack_cooldown;
										break;
								case BattleStance.range:
										Range ();
										attack_timer = attack_cooldown;
										break;
								case BattleStance.magic:
										Mage ();
										attack_timer = attack_cooldown;
										break;
						}	
				}
		}
	
		EnemyBehaviour EB;
		
		void Melee () {
				foreach (Transform tmp_monster in GameObject.Find("MonsterSpawner").transform) {
						EB = tmp_monster.GetComponent<EnemyBehaviour> ();
						if (EB != null) {
								if (tmp_monster.GetComponent<EnemyBehaviour> ().CheckDistance () <= 1) {
										if ((p001.me.Creat.Position.x < tmp_monster.position.x) && (angriffsrichtung == "r")) {
												attack = true;
										}
										if ((p001.me.Creat.Position.x > tmp_monster.position.x) && (angriffsrichtung == "l")) {
												attack = true;
										}
										if ((p001.me.Creat.Position.y < tmp_monster.position.y) && (angriffsrichtung == "o")) {
												attack = true;
										}
										if ((p001.me.Creat.Position.y > tmp_monster.position.y) && (angriffsrichtung == "u")) {
												attack = true;
										}
								}
								if (attack) {
										int phydmg = p001.me.Creat.PhyAttack - tmp_monster.GetComponent<CreatureController> ().Creat.PhyArmor;
										int magdmg = p001.me.Creat.MagAttack - tmp_monster.GetComponent<CreatureController> ().Creat.MagArmor;
				
										//physischer Schaden
										if (phydmg >= 0) {
												tmp_monster.GetComponent<CreatureController> ().Creat.HP -= phydmg;
										}
										//magischer Schaden
										if (magdmg >= 0) {
												tmp_monster.GetComponent<CreatureController> ().Creat.HP -= magdmg;
										}
										int count_wep = 0;
										foreach (ItemData owep in p001.me.Creat.Equipment) {
												ItemData wep = owep;
												if (wep.Type == ItemType.weapon_melee) {
														if (wep.Durability - 30 <= 0) {
																wep.Durability = 0;
														} else {
																wep.Durability -= 30;
														}
												}
												p001.me.Creat.Equipment [count_wep] = wep;
												count_wep++;
										}
										Debug.Log ("Phy: " + phydmg + " / Mag: " + magdmg);
										attack = false;
								}
						}
				}
		}
	
		void Range () {
				foreach (Transform tmp_monster in GameObject.Find("MonsterSpawner").transform) {
						EB = tmp_monster.GetComponent<EnemyBehaviour> ();
						if (EB != null) {
								if (tmp_monster.GetComponent<EnemyBehaviour> ().CheckDistance () <= p001.me.Creat.AttackRange) {
										if ((p001.me.Creat.Position.x < tmp_monster.position.x) && (angriffsrichtung == "r") && (p001.me.Creat.Position.y == tmp_monster.position.y)) {
												attack = true;
										}
										if ((p001.me.Creat.Position.x > tmp_monster.position.x) && (angriffsrichtung == "l") && (p001.me.Creat.Position.y == tmp_monster.position.y)) {
												attack = true;
										}
										if ((p001.me.Creat.Position.y < tmp_monster.position.y) && (angriffsrichtung == "o") && (p001.me.Creat.Position.x == tmp_monster.position.x)) {
												attack = true;
										}
										if ((p001.me.Creat.Position.y > tmp_monster.position.y) && (angriffsrichtung == "u") && (p001.me.Creat.Position.x == tmp_monster.position.x)) {
												attack = true;
										}
								}
								if ((attack) && CheckForArrow () && CheckForRangeWeapon ()) {
										int count_tmp = 0;
										foreach (ItemData otmp_item in p001.me.Creat.Equipment) {
												ItemData tmp_item = otmp_item;
												if (tmp_item.Type == ItemType.utility) {
														tmp_item.Ammo.RemoveAt (0);
														tmp_item.Capacity--;
												}
												p001.me.Creat.Equipment [count_tmp] = tmp_item;
												count_tmp++;
										}
										int count_wep = 0;
										foreach (ItemData owep in p001.me.Creat.Equipment) {
												ItemData wep = owep;
												if (wep.Type == ItemType.weapon_range) {
														if (wep.Durability - 30 <= 0) {
																wep.Durability = 0;
														} else {
																wep.Durability -= 30;
														}
												}
												p001.me.Creat.Equipment [count_wep] = wep;
												count_wep++;
										}
				
										int phydmg = p001.me.Creat.PhyAttack - tmp_monster.GetComponent<CreatureController> ().Creat.PhyArmor;
										int magdmg = p001.me.Creat.MagAttack - tmp_monster.GetComponent<CreatureController> ().Creat.MagArmor;
				
										//physischer Schaden
										if (phydmg >= 0) {
												tmp_monster.GetComponent<CreatureController> ().Creat.HP -= phydmg;
										}
										//magischer Schaden
										if (magdmg >= 0) {
												tmp_monster.GetComponent<CreatureController> ().Creat.HP -= magdmg;
										}
				
										Debug.Log ("Phy: " + phydmg + " / Mag: " + magdmg);
										attack = false;
								}
						}
				}
		}
	
		void Mage () {
				foreach (Transform tmp_monster in GameObject.Find("MonsterSpawner").transform) {
						EB = tmp_monster.GetComponent<EnemyBehaviour> ();
						if (EB != null) {
								if ((tmp_monster.GetComponent<EnemyBehaviour> ().CheckDistance () <= 2) && ((tmp_monster.position.x == p001.me.Creat.Position.x) || (tmp_monster.position.y == p001.me.Creat.Position.y))) {
										if ((p001.me.Creat.Position.x < tmp_monster.position.x) && (angriffsrichtung == "r")) {
												attack = true;
										}
										if ((p001.me.Creat.Position.x > tmp_monster.position.x) && (angriffsrichtung == "l")) {
												attack = true;
										}
										if ((p001.me.Creat.Position.y < tmp_monster.position.y) && (angriffsrichtung == "o")) {
												attack = true;
										}
										if ((p001.me.Creat.Position.y > tmp_monster.position.y) && (angriffsrichtung == "u")) {
												attack = true;
										}
								}
								if ((attack) && (p001.me.Creat.MP > 200)) {
				
										int phydmg = 0;
										float magdmg_tmp = 0;
										magdmg_tmp = p001.me.Creat.MagAttack - tmp_monster.GetComponent<CreatureController> ().Creat.MagArmor;
										int magdmg = (int)magdmg_tmp;			
										//magischer Schaden
										//physischer Schaden
										if (phydmg >= 0) {
												tmp_monster.GetComponent<CreatureController> ().Creat.HP -= phydmg;
										}
										//magischer Schaden
										if (magdmg >= 0) {
												tmp_monster.GetComponent<CreatureController> ().Creat.HP -= magdmg;
										}
				
										Debug.Log ("Phy: " + phydmg + " / Mag: " + magdmg);
										p001.me.Creat.MP -= 200;
										attack = false;
								}
						}
				}
		}
	
		
	
		bool CheckForArrow () {
				foreach (ItemData tmp_item in p001.me.Creat.Equipment) {
						if (tmp_item.Type == ItemType.utility) {
								if (tmp_item.Capacity >= 1) {
										return true;
								}
						}
				}
				return false;
		}
	
		bool CheckForRangeWeapon () {
				foreach (ItemData tmp_item in p001.me.Creat.Equipment) {
						if (tmp_item.Type == ItemType.weapon_range) {
								return true;
						}
				}
				return false;
		}
	
	
		/*variable die gegnertyp festlegt?*/

		/*schaden gegenüber player berechnen durch NPC*/
		/*schaden gegenüber player berechnen durch Monster*/

		/*pvp lieber im arena modus? dann könnte man direkt ne funktion schreiben bei der beide player aufeinander einprügeln*/

}

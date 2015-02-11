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
		int phy_damage;
		int mag_damage;
	
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
								case BattleStance.meele:
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
	
		
		
		void Melee () {
				foreach (Transform tmp_monster in GameObject.Find("MonsterSpawner").transform) {
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
								CheckForMeleeWeapon (out phy_damage, out mag_damage);
								
								int phydmg = p001.me.Creat.PhyAttack - tmp_monster.GetComponent<CreatureController> ().Creat.PhyArmor;
								int magdmg = p001.me.Creat.MagAttack - tmp_monster.GetComponent<CreatureController> ().Creat.MagArmor;
				
								//physischer Schaden
								tmp_monster.GetComponent<CreatureController> ().Creat.HP -= phydmg;
								//magischer Schaden
								tmp_monster.GetComponent<CreatureController> ().Creat.HP -= magdmg;
				
								Debug.Log ("Phy: " + phydmg + " / Mag: " + magdmg);
								attack = false;
						}
				}
		}
	
		void Range () {
				foreach (Transform tmp_monster in GameObject.Find("MonsterSpawner").transform) {
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
						if ((attack) && (CheckForArrow ())) {
								CheckForRangeWeapon (out phy_damage, out mag_damage);
								//Pfeil wegschmeißen XD
								foreach (ItemData tmp_item in p001.me.Creat.Equipment) {
										if (tmp_item.Type == ItemType.utility) {
												tmp_item.Ammo.RemoveAt (0);
												tmp_item.Capacity--;
										}
								}
				
								int phydmg = p001.me.Creat.PhyAttack - tmp_monster.GetComponent<CreatureController> ().Creat.PhyArmor;
								int magdmg = p001.me.Creat.MagAttack - tmp_monster.GetComponent<CreatureController> ().Creat.MagArmor;
				
								//physischer Schaden
								if (phy_damage >= tmp_monster.GetComponent<CreatureController> ().Creat.PhyArmor) {
										tmp_monster.GetComponent<CreatureController> ().Creat.HP -= phydmg;
								}
								//magischer Schaden
								if (mag_damage >= tmp_monster.GetComponent<CreatureController> ().Creat.MagArmor) {
										tmp_monster.GetComponent<CreatureController> ().Creat.HP -= magdmg;
								}
				
								Debug.Log ("Phy: " + phydmg + " / Mag: " + magdmg);
								attack = false;
						}
				}
		}
	
		void Mage () {
				foreach (Transform tmp_monster in GameObject.Find("MonsterSpawner").transform) {
						if (tmp_monster.GetComponent<EnemyBehaviour> ().CheckDistance () <= 2) {
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
								float magdmg_tmp = (p001.me.Creat.MaxMP / p001.me.Creat.Str);
								magdmg_tmp *= (1 - (((p001.me.Creat.MP + 0.001f) - 500) / (p001.me.Creat.MaxMP + 0.001f)));
								// TODO Muss noch in CalcStats angepasst werden
								magdmg_tmp = p001.me.Creat.MagAttack - tmp_monster.GetComponent<CreatureController> ().Creat.MagArmor;
								int magdmg = (int)magdmg_tmp;			
								//magischer Schaden
								tmp_monster.GetComponent<CreatureController> ().Creat.HP -= magdmg;
				
								Debug.Log ("Phy: " + phydmg + " / Mag: " + magdmg);
								p001.me.Creat.MP -= 200;
								attack = false;
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
	
		void CheckForMeleeWeapon (out int phy_damage, out int mag_damage) {
				foreach (ItemData tmp_item in p001.me.Creat.Equipment) {
						if (tmp_item.Type == ItemType.weapon_meele) {
								phy_damage = tmp_item.PhyAttack;
								mag_damage = tmp_item.MagAttack;
						}
				}
				phy_damage = 5;
				mag_damage = 5;
		}
	
		void CheckForRangeWeapon (out int phy_damage, out int mag_damage) {
				phy_damage = 0;
				mag_damage = 0;
				foreach (ItemData tmp_item in p001.me.Creat.Equipment) {
						if (tmp_item.Type == ItemType.weapon_range) {
								phy_damage += tmp_item.PhyAttack;
								mag_damage += tmp_item.MagAttack;
						}
						if (tmp_item.Type == ItemType.utility) {
								phy_damage += tmp_item.Ammo [0].PhyAttack;
								mag_damage += tmp_item.Ammo [0].MagAttack;
						}
				}
		
				phy_damage = 5;
				mag_damage = 5;
		}
	
		/*magieschaden berechnen*/
		
		/*variable die gegnertyp festlegt?*/

		/*schaden gegenüber player berechnen durch NPC*/
		/*schaden gegenüber player berechnen durch Monster*/

		/*pvp lieber im arena modus? dann könnte man direkt ne funktion schreiben bei der beide player aufeinander einprügeln*/

}

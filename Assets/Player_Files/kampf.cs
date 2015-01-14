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
				if (Input.GetKeyDown ("left")) {
						angriffsrichtung = "l";
						switch (angriffstil) {
								case "melee":
										Debug.Log ("Angriff ausgeführt!");
										Melee ();
										break;
								case "range":
					//Range();
										break;
								case "magic":
					//Mage();
										break;
						}
				}
				if (Input.GetKeyDown ("right")) {
						angriffsrichtung = "r";
						switch (angriffstil) {
								case "melee":
										Melee ();
										Debug.Log ("Angriff ausgeführt!");
										break;
								case "range":
					//Range();
										break;
								case "magic":
					//Mage();
										break;
						}
				}
				if (Input.GetKeyDown ("up")) {
						angriffsrichtung = "o";
						switch (angriffstil) {
								case "melee":
										Melee ();
										Debug.Log ("Angriff ausgeführt!");
										break;
								case "range":
					//Range();
										break;
								case "magic":
					//Mage();
										break;
						}
				}
				if (Input.GetKeyDown ("down")) {
						angriffsrichtung = "u";
						switch (angriffstil) {
								case "melee":
										Melee ();
										Debug.Log ("Angriff ausgeführt!");
										break;
								case "range":
					//Range();
										break;
								case "magic":
					//Mage();
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
						temp_x = (int)Mathf.Abs ((int)p001.pos.x - (int)tmp_monster.position.x);
						temp_y = (int)Mathf.Abs ((int)p001.pos.y - (int)tmp_monster.position.y);
						distance_manhatten = (int)(temp_x + temp_y);
						if (distance_manhatten <= 1) {
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
								tmp_monster.GetComponent<enemy> ().thismob.hp -= (p001.pwr * 10);
								attack = false;
						}
				}
		}
	
		void Range () {
				foreach (Transform tmp_monster in GameObject.Find("MonsterSpawner").transform) {
						temp_x = (int)Mathf.Abs ((int)p001.pos.x - (int)tmp_monster.position.x);
						temp_y = (int)Mathf.Abs ((int)p001.pos.y - (int)tmp_monster.position.y);
						distance_manhatten = (int)(temp_x + temp_y);
						if (distance_manhatten <= p001.rangeweapondistance) {
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
								tmp_monster.GetComponent<enemy> ().thismob.hp -= (p001.pwr * 10);
								attack = false;
						}
				}
		}
	
		public void OnGUI () {
				if (!GameObject.Find ("Main Camera").GetComponent<mainmenu> ().showgamemenue) {
						GUI.Label (new Rect (5, Screen.height - 105, 170, 20), "Battlemode: " + angriffstil);
				}
		}
	
		/*magieschaden berechnen*/
		/*fernkampfschaden berechnen (ammo abziehen nicht vergessen)*/
		/*nahkampfschaden berechnen*/

		/*variable die gegnertyp festlegt?*/

		/*schaden gegenüber player berechnen durch NPC*/
		/*schaden gegenüber player berechnen durch Monster*/

		/*pvp lieber im arena modus? dann könnte man direkt ne funktion schreiben bei der beide player aufeinander einprügeln*/

}

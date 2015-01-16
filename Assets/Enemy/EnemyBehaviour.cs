using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
		EnemySpawn mobspawn;
		float loadtimer = 0.5f;
		float attackcooldown = 1.0f;
		float attacktimer;	
		player p001;
		enemy e001;
		float temp_x;
		float temp_y;
		int distance_euklid;
		int distance_manhatten;
		int temp_dodge;
		
		// Use this for initialization
		void Start () {
				p001 = GameObject.Find ("Main Camera").GetComponent<player> ();
				e001 = gameObject.GetComponent<enemy> ();
				mobspawn = GameObject.Find ("MonsterSpawner").GetComponent<EnemySpawn> ();
		}
	
		// Update is called once per frame
		void Update () {
				loadtimer -= Time.deltaTime;
				attacktimer -= Time.deltaTime;
				if (loadtimer <= 0) {
						CheckDieDistance ();
						CheckDieHealth ();
						AttackPlayer ();
						Movement ();
				}
		}
		//PlayerEntfernung checken
		public int CheckDistance () {
				temp_x = Mathf.Abs (p001.pos.x - transform.position.x);
				temp_y = Mathf.Abs (p001.pos.y - transform.position.y);
				//distance_euklid = (int)Mathf.Sqrt (temp_x * temp_x + temp_y * temp_y);
				distance_manhatten = (int)(temp_x + temp_y);
				//Debug.Log ("Distance: " + distance_manhatten);
				return distance_manhatten;
		}
	

		void AttackPlayer () {
				if (attacktimer < 0) {
						if (CheckDistance () <= 1) {
								//Attackiert den Spieler
								Debug.Log ("PlayerHp: " + p001.hp + e001.thismob.pname + "hp:" + e001.thismob.hp);
								temp_dodge = Random.Range (0, 100);
								if (temp_dodge + p001.agility <= 90) {
										p001.hp -= (e001.thismob.pwr * 10) - p001.armor;
								}
						}
						attacktimer = attackcooldown;
				}
		} 
		
		void CheckDieDistance () {
				if (CheckDistance () > 40) {
						if (!e001.thismob.boss) {
								mobspawn.mobs--;			
								Destroy (gameObject);
						}			
				}
		}
		void CheckDieHealth () {
				if (e001.thismob.hp <= 0) {
						p001.xp += e001.thismob.xpdrop;
						p001.gold += e001.thismob.golddrop;
						mobspawn.mobs--;			
						Destroy (gameObject);
			
				}
		}
	
		void Movement () {
				e001.thismob.pos = new Vector2 (transform.position.x, transform.position.y);
				// If Moveable Mob? Manche Bosse sollen vielleicht weg versperren!
		}
}
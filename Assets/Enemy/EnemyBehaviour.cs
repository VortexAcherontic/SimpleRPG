using UnityEngine;
using System.Collections.Generic;

public class EnemyBehaviour : MonoBehaviour {
		EnemySpawn mobspawn;
		CreatureController me;
		
		float attackcooldown = 1.0f;
		float attacktimer;	
		player p001;
		
		float temp_x;
		float temp_y;
		int temp_wpx;
		int temp_wpy;
		int distance_euklid;
		int distance_manhatten;
		int temp_dodge;
		List<Vector2> Wegpunktliste = new List<Vector2> ();
		
		// Use this for initialization
		void Start () {
				p001 = GameObject.Find ("Main Camera").GetComponent<player> ();
				me = gameObject.GetComponent<CreatureController> ();
				mobspawn = GameObject.Find ("MonsterSpawner").GetComponent<EnemySpawn> ();
				WaypointGeneration ();
		}
	
		// Update is called once per frame
		void Update () {
				if (me.IsLoaded) {
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
						if (CheckDistance () <= me.Creat.AttackRange) {
								//Attackiert den Spieler
								//Debug.Log ("PlayerHp: " + p001.hp + me.Creat.pname + "hp:" + me.Creat.hp);
								temp_dodge = Random.Range (0, 100);
								if (temp_dodge + p001.agility <= 90) {
										p001.hp -= me.Creat.PhyAttack - p001.armor;
										p001.hp -= me.Creat.MagAttack - p001.armor;
								}
						}
						attacktimer = attackcooldown;
				}
		} 
		
		void CheckDieDistance () {
				if (CheckDistance () > 40) {
						if (!me.Creat.InitalStats.IsBoss) {
								mobspawn.mobs--;			
								Destroy (gameObject);
						}			
				}
		}
		void CheckDieHealth () {
				if (me.Creat.HP <= 0) {
						p001.xp += me.Creat.XP;
						p001.gold += me.Creat.Gold;
						mobspawn.mobs--;			
						Destroy (gameObject);
			
				}
		}
	
		float movetimer = 1.0f;
		float movecooldown = 1.0f;
		bool ismoving = false;
		int idlevariante;
	
		void WaypointGeneration () {
				idlevariante = Random.Range (0, 4);
				switch (idlevariante) {
						case 0:
								Wegpunktliste .Add (new Vector2 (0, 1));
								Wegpunktliste .Add (new Vector2 (1, 0));
								Wegpunktliste .Add (new Vector2 (-1, 0));
								Wegpunktliste .Add (new Vector2 (0, 1));
								Wegpunktliste .Add (new Vector2 (0, -1));
								Wegpunktliste .Add (new Vector2 (1, 0));
								Wegpunktliste .Add (new Vector2 (-1, 0));
								Wegpunktliste .Add (new Vector2 (0, -1));
								ismoving = true;
								break;
						case 1:
								Wegpunktliste .Add (new Vector2 (1, 0));
								Wegpunktliste .Add (new Vector2 (-1, 0));
								Wegpunktliste .Add (new Vector2 (0, -1));
								Wegpunktliste .Add (new Vector2 (0, 1));
								Wegpunktliste .Add (new Vector2 (0, 1));
								Wegpunktliste .Add (new Vector2 (0, -1));
								Wegpunktliste .Add (new Vector2 (0, -1));
								Wegpunktliste .Add (new Vector2 (0, 1));
								ismoving = true;
								break;
						case 2:
								Wegpunktliste .Add (new Vector2 (1, 0));
								Wegpunktliste .Add (new Vector2 (-1, 0));
								Wegpunktliste .Add (new Vector2 (0, -1));
								Wegpunktliste .Add (new Vector2 (0, 1));
								Wegpunktliste .Add (new Vector2 (1, 0));
								Wegpunktliste .Add (new Vector2 (-1, 0));
								Wegpunktliste .Add (new Vector2 (0, -1));
								Wegpunktliste .Add (new Vector2 (0, 1));
								ismoving = true;
								break;
						case 3:
								Wegpunktliste .Add (new Vector2 (1, 0));
								Wegpunktliste .Add (new Vector2 (-1, 0));
								Wegpunktliste .Add (new Vector2 (1, 0));
								Wegpunktliste .Add (new Vector2 (-1, 0));
								Wegpunktliste .Add (new Vector2 (0, 1));
								Wegpunktliste .Add (new Vector2 (0, -1));
								Wegpunktliste .Add (new Vector2 (0, -1));
								Wegpunktliste .Add (new Vector2 (0, 1));
								ismoving = true;
								break;
						case 4:
								Wegpunktliste .Add (new Vector2 (1, 0));
								Wegpunktliste .Add (new Vector2 (1, 0));
								Wegpunktliste .Add (new Vector2 (1, 0));
								Wegpunktliste .Add (new Vector2 (-1, 0));
								Wegpunktliste .Add (new Vector2 (0, 1));
								Wegpunktliste .Add (new Vector2 (0, -1));
								Wegpunktliste .Add (new Vector2 (0, -1));
								Wegpunktliste .Add (new Vector2 (-1, 0));
								Wegpunktliste .Add (new Vector2 (0, 1));
								ismoving = true;
								break;
				}		
		}
	
		Vector2 temp_wp = new Vector2 (0, 0);
		int i = 0;
	
		void IdleMovement () {
				if ((CheckDistance () <= 20) && (ismoving == false)) {
						me.Creat.Position += temp_wp;
						ismoving = true;
						transform.position = new Vector3 (me.Creat.Position.x, me.Creat.Position.y, transform.position.z);
				}
				if ((movetimer <= 0) && (ismoving)) {
						temp_wp = Wegpunktliste [i];
						movetimer = movecooldown;
						ismoving = false;
						i++;
						if (i == Wegpunktliste.Count) {
								i = 0;
						}
				}
		}
	
		bool aggro = false;
		bool moved = false;
		string zuerst;
	
		bool CheckAggro () {
				aggro = false;
				if (CheckDistance () <= me.Creat.AggroRange) {
						aggro = true;
				}
		
				if (me.Creat.HP < me.Creat.MaxHP) {	// wenn ich schaden habe, bin ich aggro XD
						aggro = true;
				}
				return aggro;
		}
	
		float a_1;
		float a_2;
	
		string zuerstfragezeichen () {
				a_1 = Mathf.Abs (p001.pos.x - transform.position.x);
				a_2 = Mathf.Abs (p001.pos.y - transform.position.y);
				if (a_1 < a_2) {
						zuerst = "y";
				}
				if (a_2 < a_1) {
						zuerst = "x";
				}
				return zuerst;
		}
	
	
		void PlayerAggro () {
				if ((ismoving == false) && (CheckDistance () >= 2)) { // Da bei CheckAggro die Distanz eine Rolle Spielt, ist diese hier egal ;)
						me.Creat.Position += temp_wp;
						ismoving = true;
						moved = false;
						transform.position = new Vector3 (me.Creat.Position.x, me.Creat.Position.y, transform.position.z);
				}
				if ((movetimer <= 0) && (ismoving)) {
						temp_wp = new Vector2 (0, 0);
						zuerstfragezeichen ();
						if ((p001.pos.x != me.Creat.Position.x) && (zuerst == "x")) {
								if (p001.pos.x > me.Creat.Position.x) {
										temp_wpx = 1;
								}
								if (p001.pos.x < me.Creat.Position.x) {
										temp_wpx = -1;
								}
						}
						if ((p001.pos.y != me.Creat.Position.y) && (zuerst == "y")) {
								if (p001.pos.y > me.Creat.Position.y) {
										temp_wpy = 1;
								}
								if (p001.pos.y < me.Creat.Position.y) {
										temp_wpy = -1;
								}
						}
						/*if (temp_wpx != 0) {
								temp_wpy = 0;
						}*/
						temp_wp = new Vector2 (temp_wpx, temp_wpy);
						temp_wpx = 0;
						temp_wpy = 0;
						movetimer = movecooldown;
						ismoving = false;
				}
		}
	
		void Movement () {
				me.Creat.Position = new Vector2 (transform.position.x, transform.position.y);
				me.Creat.InitalStats.Position = me.Creat.Position;
				if (me.Creat.InitalStats.IsMoveable) {
						if (CheckAggro ()) {
								PlayerAggro ();
						} else {
								if (!me.Creat.InitalStats.IsBoss) { // Damit der Boss nicht seinen punkt verlässt
										IdleMovement ();
								}
						}
				}
				movetimer -= Time.deltaTime;
				// If Moveable Mob? Manche Bosse sollen vielleicht weg versperren!
				
		}
}
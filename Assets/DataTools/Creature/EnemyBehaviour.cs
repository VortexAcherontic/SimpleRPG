using UnityEngine;
using System.Collections.Generic;

public class EnemyBehaviour : MonoBehaviour {
		CreatureController me;
		PlayerBehaviour p001;
		QuestController QuestObj;
		
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
				p001 = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerBehaviour> ();
				QuestObj = GameObject.FindGameObjectWithTag ("Player").GetComponent<QuestController> ();
				me = gameObject.GetComponent<CreatureController> ();
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
				temp_x = Mathf.Abs (p001.me.Creat.Position.x - transform.position.x);
				temp_y = Mathf.Abs (p001.me.Creat.Position.y - transform.position.y);
				//distance_euklid = (int)Mathf.Sqrt (temp_x * temp_x + temp_y * temp_y);
				distance_manhatten = (int)(temp_x + temp_y);
				//Debug.Log ("Distance: " + distance_manhatten);
				return distance_manhatten;
		}
	

		void AttackPlayer () {
				if (me.Creat.AttackTimer < 0) {
						if (CheckDistance () <= me.Creat.AttackRange) {
								//Attackiert den Spieler
								//Debug.Log ("PlayerHp: " + p001.hp + me.Creat.pname + "hp:" + me.Creat.hp);
								temp_dodge = Random.Range (0, 100);
								if (temp_dodge + p001.me.Creat.Agi <= 90) {
										if (me.Creat.PhyAttack > p001.me.Creat.PhyArmor) {
												p001.me.Creat.HP -= me.Creat.PhyAttack - p001.me.Creat.PhyArmor;
										}
										if (me.Creat.MagAttack > p001.me.Creat.MagArmor) {
												p001.me.Creat.HP -= me.Creat.MagAttack - p001.me.Creat.MagArmor;
										}
										int count_wep = 0;
										foreach (ItemData owep in p001.me.Creat.Equipment) {
												ItemData wep = owep;
												if ((wep.Type == ItemType.armor_feet) ||
														(wep.Type == ItemType.armor_hand) ||
														(wep.Type == ItemType.armor_head) ||
														(wep.Type == ItemType.armor_leg) ||
														(wep.Type == ItemType.armor_torso) 
						  						  ) {
														if (wep.Durability - 1 <= 0) {
																wep.Durability = 0;
														} else {
																wep.Durability -= 1;
														}
												}
												p001.me.Creat.Equipment [count_wep] = wep;
												count_wep++;
										}
								}
						}
						me.Creat.AttackTimer = me.Creat.AttackCooldown;
				}
		} 
		
		void CheckDieDistance () {
				if (CheckDistance () > 40) {
						if (!me.Creat.InitalStats.IsBoss) {		
								Destroy (gameObject);
						}			
				}
		}
		void CheckDieHealth () {
				if (me.Creat.HP <= 0) {	
						Notification not = new Notification ();
						not.time = 5;
			
						p001.me.Creat.Gold += me.Creat.Gold;
						not.message = "Get " + me.Creat.Gold + " Gold";
						p001.PickupList.Add (not);
			
						p001.me.Creat.XP += me.Creat.XP;
						not.message = "Get " + me.Creat.XP + " XP";
						p001.PickupList.Add (not);
						
						QuestObj.EnemyKilled (me.Creat.Name);	
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
	
		Vector3 temp_wp = new Vector3 (0, 0);
		int i = 0;
	
		void IdleMovement () {
				if ((CheckDistance () <= 20) && (ismoving == false)) {
						ChangeAnimation (temp_wp);
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
				a_1 = Mathf.Abs (p001.me.Creat.Position.x - transform.position.x);
				a_2 = Mathf.Abs (p001.me.Creat.Position.y - transform.position.y);
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
						ChangeAnimation (temp_wp);			
						me.Creat.Position += temp_wp;
						ismoving = true;
						transform.position = new Vector3 (me.Creat.Position.x, me.Creat.Position.y, transform.position.z);
				}
				if ((movetimer <= 0) && (ismoving)) {
						temp_wp = new Vector2 (0, 0);
						zuerstfragezeichen ();
						if ((p001.me.Creat.Position.x != me.Creat.Position.x) && (zuerst == "x")) {
								if (p001.me.Creat.Position.x > me.Creat.Position.x) {
										temp_wpx = 1;
								}
								if (p001.me.Creat.Position.x < me.Creat.Position.x) {
										temp_wpx = -1;
								}
						}
						if ((p001.me.Creat.Position.y != me.Creat.Position.y) && (zuerst == "y")) {
								if (p001.me.Creat.Position.y > me.Creat.Position.y) {
										temp_wpy = 1;
								}
								if (p001.me.Creat.Position.y < me.Creat.Position.y) {
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
		void ChangeAnimation (Vector2 MoveTo) {

		}
}
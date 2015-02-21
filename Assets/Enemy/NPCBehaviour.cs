using UnityEngine;
using System.Collections.Generic;

public class NPCBehaviour : MonoBehaviour {
	
		QuestController QuestObj;
		CreatureController me;
		PlayerBehaviour p001;
		float temp_x;
		float temp_y;
		int distance_manhatten;
	
	
		public List<string> ReperaturNPC = new List<string> ();
		public List<string> ShopNPC = new List<string> ();
	
	
		void Start () {
				p001 = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerBehaviour> ();
				QuestObj = GameObject.FindGameObjectWithTag ("Player").GetComponent<QuestController> ();
				me = gameObject.GetComponent<CreatureController> ();
		
				ReperaturNPC.Add ("Hans Peter");
				ShopNPC.Add ("Jeremy Pascal");
		}
	
		void Update () {
				Quest ();				
		}
	
		bool Interacted () {
				if (interactable ()) {
						if (Input.GetKeyDown ("f")) {
								return true;
						} else {
								return false;
						}
				} else {
						return false;
				}
		}
	
		bool interactable () {
				if (CheckDistance () <= 2) {
						return true;
				} else {
						return false;
				}
		}
	
		public int CheckDistance () {
				temp_x = Mathf.Abs (p001.me.Creat.Position.x - transform.position.x);
				temp_y = Mathf.Abs (p001.me.Creat.Position.y - transform.position.y);
				//distance_euklid = (int)Mathf.Sqrt (temp_x * temp_x + temp_y * temp_y);
				distance_manhatten = (int)(temp_x + temp_y);
				//Debug.Log ("Distance: " + distance_manhatten);
				return distance_manhatten;
		}
	
		void repair () {
				if (GUI_Repair) {
						Rect tmp_anzeige = new Rect (Screen.width / 2 - 500, Screen.height / 2 - 200, 1000, 400);
						Rect zeile = new Rect (tmp_anzeige.position.x, tmp_anzeige.position.y, tmp_anzeige.width - 500, 20);
						GUI.Box (tmp_anzeige, "Repair you stuff!");
						int count_i = 0;
						foreach (ItemData oi in p001.me.Creat.Equipment) {
								ItemData i = oi;
								if (i.Durability < i.MaxDurability) {
										zeile.position = new Vector2 (tmp_anzeige.position.x, zeile.position.y + zeile.height);
										GUI.Label (zeile, i.Name + " (" + i.Durability + "/" + i.MaxDurability + ")");
										if (GUI.Button (new Rect (zeile.position.x + 350, zeile.position.y, 200, zeile.height), "Rep 20")) {
												i.Durability += 20;
												if (i.Durability > i.MaxDurability) {
														i.Durability = i.MaxDurability;
												}
												p001.me.Creat.Gold -= 50;
										}
										if (GUI.Button (new Rect (zeile.position.x + 600, zeile.position.y, 200, zeile.height), "Rep ALL")) {
												i.Durability = i.MaxDurability;
												p001.me.Creat.Gold -= 75;
										}
								}
								p001.me.Creat.Equipment [count_i] = i;
								count_i++;
						}
				}
		}
	
		bool GUI_Repair;
	
		void OnGUI () {
				repair ();
		}
	
		void Quest () {
				if (Interacted ()) {
						/*Debug.Log (me.Creat.Name);*/
						Debug.Log (ReperaturNPC);
						foreach (string npcname in ReperaturNPC) {
								if (npcname == me.Creat.Name) {
										GUI_Repair = !GUI_Repair;		
								}
						}
						foreach (string npcname in ShopNPC) {
								if (npcname == me.Creat.Name) {
										Shop ();
								}
						}
			
						QuestObj.NPCTalk (me.Creat.Name);
						//Debug.Log ("Quest angenommen!");
				}	
		}
	
		void Shop () {
				GameObject.Find ("Uebergabe").GetComponent<shop> ().imshop = !GameObject.Find ("Uebergabe").GetComponent<shop> ().imshop;
		}
	
}

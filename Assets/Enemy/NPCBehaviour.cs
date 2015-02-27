using UnityEngine;
using System.Collections.Generic;

public class NPCBehaviour : MonoBehaviour {
		GUI_Helper GUI_ZoD = new GUI_Helper ();
		QuestController QuestObj;
		CreatureController me;
		PlayerBehaviour p001;
		float temp_x;
		float temp_y;
		int distance_manhatten;
	
	
		public List<string> ReperaturNPC = new List<string> ();
		public List<string> ShopNPC = new List<string> ();
		public List<string> AusbilderNPC = new List<string> (); //zu ersetzen durch verschiedene listen falls verschiedene npc's verschiedene sachen beibringen
	
	
		void Start () {
				p001 = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerBehaviour> ();
				QuestObj = GameObject.FindGameObjectWithTag ("Player").GetComponent<QuestController> ();
				me = gameObject.GetComponent<CreatureController> ();
		
				ReperaturNPC.Add ("Hans Peter");
				ShopNPC.Add ("Jeremy Pascal");
				AusbilderNPC.Add ("Juergen");
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
						Rect tmp_anzeige = new Rect (1920 / 2 - 500, 1080 / 2 - 200, 1000, 400);
						Rect zeile = new Rect (tmp_anzeige.position.x, tmp_anzeige.position.y, tmp_anzeige.width - 500, 20);
						GUI_ZoD.Box ("Repair you stuff!", tmp_anzeige);
						int count_i = 0;
						foreach (ItemData oi in p001.me.Creat.Equipment) {
								ItemData i = oi;
								if (i.Durability < i.MaxDurability) {
										zeile.position = new Vector2 (tmp_anzeige.position.x, zeile.position.y + zeile.height);
										GUI_ZoD.Label (i.Name + " (" + i.Durability + "/" + i.MaxDurability + ")", 11, zeile);
										if (GUI_ZoD.Button_Text ("Rep 20", 11, new Rect (zeile.position.x + 350, zeile.position.y, 200, zeile.height))) {
												i.Durability += 20;
												if (i.Durability > i.MaxDurability) {
														i.Durability = i.MaxDurability;
												}
												p001.me.Creat.Gold -= 50;
										}
										if (GUI_ZoD.Button_Text ("Rep ALL", 11, new Rect (zeile.position.x + 600, zeile.position.y, 200, zeile.height))) {
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
				Ausbilder ();
		}
	
		void Quest () {
				if (Interacted ()) {
						/*Debug.Log (me.Creat.Name);*/
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
						foreach (string npcname in AusbilderNPC) {
								if (npcname == me.Creat.Name) {
										GUI_Ausbilder = ! GUI_Ausbilder;
								}
						}
			
						QuestObj.NPCTalk (me.Creat.Name);
						//Debug.Log ("Quest angenommen!");
				}	
		}
	
		void Shop () {
				GameObject.Find ("Uebergabe").GetComponent<shop> ().imshop = !GameObject.Find ("Uebergabe").GetComponent<shop> ().imshop;
		}
	
		bool GUI_Ausbilder = false;
		public SkillsDataList DataListObj;
		List<skill> SkillsToLearn = new List<skill> ();
	
		void Ausbilder () {
				if (GUI_Ausbilder) {
						Rect tmp_anzeige = new Rect (1920 / 2 - 500, 1080 / 2 - 200, 1000, 400);
						Rect zeile = new Rect (tmp_anzeige.position.x, tmp_anzeige.position.y, tmp_anzeige.width - 500, 20);
						GUI_ZoD.Box ("Take Lessons!", tmp_anzeige);
						DataListObj = (SkillsDataList)Resources.Load ("Skill");
						SkillsToLearn.Clear ();
						foreach (skill id in DataListObj.SkillList) {
								SkillsToLearn.Add (id);
						}
						foreach (skill name in SkillsToLearn) {
								zeile.position = new Vector2 (tmp_anzeige.position.x, zeile.position.y + zeile.height);
								GUI_ZoD.Label (name.name + " | increases: " + name.Effect [0] + " | cost: " + name.cost, 11, zeile);
								zeile.position = new Vector2 (zeile.position.x + 500, zeile.position.y);
								zeile.width = 200;
								if (GUI_ZoD.Button_Text ("learn skill for " + name.spcost + " skillpoints.", 11, zeile)) {
										if (p001.me.Creat.SkillPoints >= name.spcost) {
												p001.SkillLearn (name);
												p001.me.Creat.SkillPoints -= name.spcost;
										}
								}
								zeile.width = tmp_anzeige.width - 500;
						}	
						zeile.position = new Vector2 (tmp_anzeige.position.x, zeile.position.y + zeile.height);
						zeile.width = 200;
						if (GUI_ZoD.Button_Text ("Train your skills for 20G for 1 skillpoint.", 11, zeile)) {
								if (p001.me.Creat.Gold >= 20) {
										p001.me.Creat.SkillPoints += 1;
										p001.me.Creat.Gold -= 20;
								}
						}
						zeile.width = tmp_anzeige.width - 500;
				}
		}
	
}

using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct NPCOptions {
		public string Name;
		//public string[] ItemsToSell;
		public bool IsShop;
		//public string[] SkillsToLearn;
		public bool IsTrainer;
		public bool CanRepair;
		public string Dialog;
}

public class NPCBehaviour : MonoBehaviour {
		GUI_Helper GUI_ZoD = new GUI_Helper ();
		
		public NPCOptions NPC;
		Player_Trigger triggerscript;
		QuestController QuestObj;
		shop ShopObj;
		CreatureController me;
		
		void Start () {
				triggerscript = GetComponentInChildren<Player_Trigger> ();
				QuestObj = GameObject.FindGameObjectWithTag ("Player").GetComponent<QuestController> ();
				ShopObj = GameObject.Find ("Uebergabe").GetComponent<shop> ();
				me = gameObject.GetComponent<CreatureController> ();
		}
	
		void Update () {
				Interact ();				
		}
	
		void Interact () {
				if (triggerscript.Player_in_Triger) {
						if (Input.GetButtonDown ("Interact")) {
								if (NPC.IsShop) {
										ShopObj.imshop = true;
								}
								if (NPC.IsTrainer) {
										GUI_Repair = !GUI_Repair;
								}
								if (NPC.IsShop) {
										GUI_Ausbilder = !GUI_Ausbilder;
								}
								Quest ();
						}
				} else {
						GUI_Repair = false;
						GUI_Ausbilder = false;
				}
		}
	
		void OnGUI () {
				repair ();
				Ausbilder ();
		}
	
		void repair () {
				if (GUI_Repair) {
						Rect tmp_anzeige = new Rect (1920 / 2 - 500, 1080 / 2 - 200, 1000, 400);
						Rect zeile = new Rect (tmp_anzeige.position.x, tmp_anzeige.position.y, tmp_anzeige.width - 500, 20);
						GUI_ZoD.Box ("Repair you stuff!", tmp_anzeige);
						int count_i = 0;
						foreach (ItemData oi in triggerscript.Player_Obj.GetComponent<PlayerBehaviour>().me.Creat.Equipment) {
								ItemData i = oi;
								if (i.Durability < i.MaxDurability) {
										zeile.position = new Vector2 (tmp_anzeige.position.x, zeile.position.y + zeile.height);
										GUI_ZoD.Label (i.Name + " (" + i.Durability + "/" + i.MaxDurability + ")", 11, zeile);
										if (GUI_ZoD.Button_Text ("Rep 20", 11, new Rect (zeile.position.x + 350, zeile.position.y, 200, zeile.height))) {
												i.Durability += 20;
												if (i.Durability > i.MaxDurability) {
														i.Durability = i.MaxDurability;
												}
												triggerscript.Player_Obj.GetComponent<PlayerBehaviour> ().me.Creat.Gold -= 50;
										}
										if (GUI_ZoD.Button_Text ("Rep ALL", 11, new Rect (zeile.position.x + 600, zeile.position.y, 200, zeile.height))) {
												i.Durability = i.MaxDurability;
												triggerscript.Player_Obj.GetComponent<PlayerBehaviour> ().me.Creat.Gold -= 75;
										}
								}
								triggerscript.Player_Obj.GetComponent<PlayerBehaviour> ().me.Creat.Equipment [count_i] = i;
								count_i++;
						}
				}
		}
	
		bool GUI_Repair;
	
		void Quest () {
				QuestObj.NPCTalk (me.Creat.Name);
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
										if (triggerscript.Player_Obj.GetComponent<PlayerBehaviour> ().me.Creat.SkillPoints >= name.spcost) {
												triggerscript.Player_Obj.GetComponent<PlayerBehaviour> ().SkillLearn (name);
												triggerscript.Player_Obj.GetComponent<PlayerBehaviour> ().me.Creat.SkillPoints -= name.spcost;
										}
								}
								zeile.width = tmp_anzeige.width - 500;
						}	
						zeile.position = new Vector2 (tmp_anzeige.position.x, zeile.position.y + zeile.height);
						zeile.width = 200;
						if (GUI_ZoD.Button_Text ("Train your skills for 20G for 1 skillpoint.", 11, zeile)) {
								if (triggerscript.Player_Obj.GetComponent<PlayerBehaviour> ().me.Creat.Gold >= 20) {
										triggerscript.Player_Obj.GetComponent<PlayerBehaviour> ().me.Creat.SkillPoints += 1;
										triggerscript.Player_Obj.GetComponent<PlayerBehaviour> ().me.Creat.Gold -= 20;
								}
						}
						zeile.width = tmp_anzeige.width - 500;
				}
		}
	
}

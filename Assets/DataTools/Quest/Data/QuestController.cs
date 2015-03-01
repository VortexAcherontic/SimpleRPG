using UnityEngine;
using System.Collections.Generic;

public class QuestController : MonoBehaviour {
		public List<QuestStruct> AlleQuests = new List<QuestStruct> ();
		PlayerBehaviour p001;
		Notification not = new Notification ();

	
		void Start () {
				not.time = 5;
				QuestDataList DataListObj;
				DataListObj = (QuestDataList)Resources.Load ("Quest");
				foreach (QuestStruct lq in DataListObj.QuestList) {
						AlleQuests.Add (lq);
				}
				p001 = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerBehaviour> ();
		}
	
		public void EnemyKilled (string EnemyName) {
				int count_quest = 0;
				foreach (QuestStruct Quest in AlleQuests) {
						if (Quest.accepted) {
								int count_enemy = 0;
								foreach (EnemyTokillStruct tmpenemey in Quest.EnemyTokill) {
										if (tmpenemey.Name == EnemyName) {
												EnemyTokillStruct test = AlleQuests [count_quest].EnemyTokill [count_enemy];
												test.Amount--;
												AlleQuests [count_quest].EnemyTokill [count_enemy] = test;
												if (test.Amount <= 0) {
														AlleQuests [count_quest].EnemyTokill.RemoveAt (count_enemy);
												}
										}
										count_enemy++;
								}
						}
						count_quest++;
				}
		}
	
		public void ItemsCollected () {
				int count_quest = 0;
				foreach (QuestStruct Quest in AlleQuests) {
						if (Quest.accepted) {
								int count_item = 0;
								foreach (ItemsToCollectStruct tmpitem in Quest.ItemsToCollect) {
										foreach (ItemData invcontent in p001.me.Creat.Inventory) {
												if (invcontent.Name == tmpitem.Name) {
														p001.me.Creat.Inventory.Remove (invcontent);
														not.message = "Lose " + invcontent.Name;
														p001.PickupList.Add (not);
														ItemsToCollectStruct test = AlleQuests [count_quest].ItemsToCollect [count_item];
														test.Amount--;
														AlleQuests [count_quest].ItemsToCollect [count_item] = test;
														if (test.Amount <= 0) {
																AlleQuests [count_quest].ItemsToCollect.RemoveAt (count_item);
																ItemsCollected ();
																return;
														}
												}	
										}
										count_item++;
								}
						}
						count_quest++;		
				}
		}
	
		public void NPCTalk (string NPCName) {
				int count_quest = 0;
				foreach (QuestStruct Quest in AlleQuests) {
						if (Quest.accepted) {
								int count_npc = 0;
								foreach (string targetname in Quest.NPCToTalk) {
										if (targetname == NPCName) {
												AlleQuests [count_quest].NPCToTalk.RemoveAt (count_npc);
												return;
										}
										count_npc++;
								}
				
								if (Quest.NPC_Geber == NPCName && !Quest.finished) {
										ItemsCollected ();
										bool fertigmit = true;
										if (Quest.EnemyTokill.Count > 0) {
												fertigmit = false;
										}
										if (Quest.NPCToTalk.Count > 0) {
												fertigmit = false;
										}
										if (Quest.ItemsToCollect.Count > 0) {
												fertigmit = false;
										}
										if (fertigmit) {
												
						
												QuestStruct tmpquest = AlleQuests [count_quest];
												tmpquest.finished = true;
												AlleQuests [count_quest] = tmpquest;
												
												item DataListObj;
												DataListObj = GameObject.Find ("Uebergabe").GetComponent<item> ();
												foreach (string tmploot in Quest.Loot) {
														not.message = "Get " + tmploot;
														p001.PickupList.Add (not);
														p001.me.Creat.Inventory.Add (DataListObj.item_mit_name (tmploot));
												}
												
												not.message = "Get " + Quest.LootGold + " Gold";
												p001.PickupList.Add (not);
												p001.me.Creat.Gold += Quest.LootGold;
						
												not.message = "Get " + Quest.LootXP + " XP";
												p001.PickupList.Add (not);
												p001.me.Creat.Gold += Quest.LootXP;
						
										}
					
								}
						} else {
								bool canhavequest = true;
								if (Quest.PrevQuest.Count >= 0) {
										foreach (string prevq in Quest.PrevQuest) {
												foreach (QuestStruct checkq in AlleQuests) {
														if (prevq == checkq.Name) {
																if (checkq.finished == false) {
																		canhavequest = false;
																}
														}
												}
										}
								}
								if ((Quest.NPC_Geber == NPCName) && canhavequest) {
										QuestStruct tmpquest = AlleQuests [count_quest];
										tmpquest.accepted = true;
										AlleQuests [count_quest] = tmpquest;
								}
						}
						count_quest++;
				}
		}	

		public void UpdateOnLoad (List<QuestStruct> loadedQuests) {
				int ql_count = 0;
				int qo_count = 0;
				foreach (QuestStruct ql in loadedQuests) {
						qo_count = 0;
						foreach (QuestStruct qo in AlleQuests) {
								if (ql.Name == qo.Name) {
										// Sind die gleichen Quests!
										QuestStruct q_override = new QuestStruct ();
										q_override = qo;
										q_override.EnemyTokill.Clear ();
										q_override.ItemsToCollect.Clear ();
										q_override.NPCToTalk.Clear ();
										q_override.accepted = ql.accepted;
										if (!ql.finished) {
												q_override.NPCToTalk = ql.NPCToTalk;
												q_override.EnemyTokill = ql.EnemyTokill;
												q_override.ItemsToCollect = ql.ItemsToCollect;					
												q_override.finished = ql.finished;
										}
										AlleQuests [qo_count] = q_override;
								}
								qo_count++;
						}
						ql_count++;
				}
		}
}

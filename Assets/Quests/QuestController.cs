﻿using UnityEngine;
using System.Collections.Generic;

public class QuestController : MonoBehaviour {
		public List<QuestStruct> AlleQuests = new List<QuestStruct> ();
		PlayerBehaviour p001;
	
		void Start () {
				QuestDataList DataListObj;
				DataListObj = (QuestDataList)Resources.Load ("Quest");
				AlleQuests = DataListObj.QuestList;
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
														return;
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
				int tmpcounter = 0;
				foreach (QuestStruct Quest in AlleQuests) {
						if (Quest.accepted) {
								int count_item = 0;
								foreach (ItemsToCollectStruct tmpitem in Quest.ItemsToCollect) {
										foreach (ItemData invcontent in p001.me.Creat.Inventory) {
												if (invcontent.Name == tmpitem.Name) {
														tmpcounter++;
														p001.me.Creat.Inventory.Remove (invcontent);
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
												ItemDataList DataListObj;
												DataListObj = (ItemDataList)Resources.Load ("Items");
												foreach (string tmploot in Quest.Loot) {
														p001.me.Creat.Inventory.Add (DataListObj.item_mit_name (tmploot));
												}
										}
					
								}
						} else {
								if (Quest.NPC_Geber == NPCName) {
										QuestStruct tmpquest = AlleQuests [count_quest];
										tmpquest.accepted = true;
										AlleQuests [count_quest] = tmpquest;
								}
						}
						count_quest++;
				}
		}	
}

using UnityEngine;
using System.Collections;

[System.Serializable]
public struct GatherOptions {
		public string GatherSkill;
		public ItemType GatherToolType;
		public GameObject GatherObject;
		
		public int RequiredSkillLevel;
		public string[] PossibleMats;
		
		public bool IsGathered;
}

public class Gathering : MonoBehaviour {
		AudioSource sound;
		Player_Trigger triggerscript;
		
		item ItemScript;
		public GatherOptions optionen;
		float GatherMatsRespawnTimer_Max = 50;
		float GatherMatsRespawnTimer = 0;
	
		void Start () {
				sound = GetComponent<AudioSource> ();
				triggerscript = GetComponentInChildren<Player_Trigger> ();
				ItemScript = GameObject.Find ("Uebergabe").GetComponent<item> ();
		}
	
		void Update () {
				Interact ();
				if (GatherMatsRespawnTimer > 0) {
						GatherMatsRespawnTimer -= Time.deltaTime;
				} else {
						if (optionen.IsGathered) {
								optionen.IsGathered = false;
								optionen.GatherObject.SetActive (true);
						}
				}
		}
	
		void Interact () {
				if (triggerscript.Player_in_Triger) {
						if (Input.GetButtonDown ("Interact")) {
								if (optionen.IsGathered == false) {
										
										PlayerBehaviour Player = triggerscript.Player_Obj.GetComponent<PlayerBehaviour> ();
										#region Skill Suche
										for (int ct_skill=0; ct_skill<Player.me.Creat.Skills.Count; ct_skill++) {
												skill tmp_skill = Player.me.Creat.Skills [ct_skill];
												if (tmp_skill.Name == optionen.GatherSkill && tmp_skill.Level > optionen.RequiredSkillLevel) {
														#region Equip Suche
														for (int ct_equip=0; ct_equip<Player.me.Creat.Equipment.Count; ct_equip++) {
																ItemData tmp_item = Player.me.Creat.Equipment [ct_equip];
																if ((tmp_item.Type == optionen.GatherToolType) && (tmp_item.Durability > 0)) {
																		sound.Play ();
																		tmp_item.Durability--;
																		optionen.IsGathered = true;
																		optionen.GatherObject.SetActive (false);
																		#region Loot
																		for (int ct_loot=0; ct_loot<optionen.PossibleMats.Length; ct_loot++) {
																				ItemData tmp_loot = ItemScript.item_mit_name (optionen.PossibleMats [ct_loot]);
																				Player.me.Creat.Inventory.Add (tmp_loot);
																				Notification not = new Notification ();
																				not.time = 5;
																				not.message = "Get " + tmp_loot.Name;
																				Player.PickupList.Add (not);
																		}
																		#endregion Loot
																		Player.me.Creat.Equipment [ct_equip] = tmp_item;
																}
														}
														#endregion Equip Suche
												}
										}
										#endregion Skill Suche
								}
						}
				}
		}
}

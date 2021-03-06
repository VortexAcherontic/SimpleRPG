﻿using UnityEngine;
using System.Collections.Generic;

public class CreatureController : MonoBehaviour {
		// Nur für die Berechnung! 
		// Damit nicht immer Clone exestieren?
	
		public float GGewicht = 0.0f;
		public float MaxGGewicht = 3000.0f;
		public List<Status> alleStatus = new List<Status> ();
		public CreatureData Creat;
		public bool IsLoaded = false;
		Vector2 newPos = new Vector2 (0, 0);
	
		Transform healthbar;
		Transform healthbarFilled;
	
		void Awake () {
				IsLoaded = false; // Nur zur Absicherung
		}
		void Update () {
				if (IsLoaded == true) {
						CalculateStats ();
						UpdatePosition ();
						UpdateTimer ();
						UpdateReg ();
						DrawHP ();
				}
		}
	
		public void Create (CreatureOriginData InitStat) {
		
				Creat.Skills = new List<skill> ();
				Creat.StatusEffects = new List<Status> ();
		
				Creat.lastPos = new Vector2 (0, 0);
				Creat.InitalStats = InitStat;
				Creat.StatPoints = Creat.InitalStats.StatPoints;
				Creat.Stance = Creat.InitalStats.Stance;
				Creat.Position = Creat.InitalStats.Position;
				if ((Creat.Position.y > 0) && (Creat.Position.x > 0)) {
						transform.position = Creat.Position;
				}
				CalculateStats ();
				Creat.HP = Creat.MaxHP;
				Creat.MP = Creat.MaxMP;
				Creat.Gold = Creat.InitalStats.Gold;
				Creat.XP = Creat.InitalStats.XP;
				Creat.Level = Creat.InitalStats.Level;
		
				item DataListObj;
				DataListObj = GameObject.Find ("Uebergabe").GetComponent<item> ();
				// Equipment richtig eintragen
				Creat.InitalStats.Equipment = new List<ItemData> ();
				if (Creat.InitalStats.Equipment_Strings != null) {
						foreach (string tmpitem in Creat.InitalStats.Equipment_Strings) {
								Creat.InitalStats.Equipment.Add (DataListObj.item_mit_name (tmpitem));
						}
				}
				// Iventory richtig eintragen
				Creat.InitalStats.Inventory = new List<ItemData> ();
				if (Creat.InitalStats.Inventory_Strings != null) {
						foreach (string tmpitem in Creat.InitalStats.Inventory_Strings) {
								Creat.InitalStats.Inventory.Add (DataListObj.item_mit_name (tmpitem));
						}
				}
				StatusDataList DataListObj_status;
				DataListObj_status = (StatusDataList)Resources.Load ("Status");
				alleStatus.Clear ();
				foreach (Status id in DataListObj_status.StatusList) {
						alleStatus.Add (id);
				}
		
		
				Creat.Equipment = Creat.InitalStats.Equipment;
				Creat.Inventory = Creat.InitalStats.Inventory;
				IsLoaded = true;
		}
	
		public void UpdateTimer () {
				Creat.MoveTimer -= Time.deltaTime;
				Creat.RegTimer -= Time.deltaTime;
				Creat.AttackTimer -= Time.deltaTime;
				Creat.HPPotionTimer -= Time.deltaTime;
				if (Creat.MoveTimer < -20) {
						Creat.MoveTimer = 0;
				}
				if (Creat.RegTimer < -20) {
						Creat.RegTimer = 0;
				}
				if (Creat.AttackTimer < -20) {
						Creat.AttackTimer = 0;
				}
				if (Creat.HPPotionTimer < -20) {
						Creat.HPPotionTimer = 0;
				}
		}
	
		public void Equip (ItemData obj) {
				bool equipcheck = false;
				foreach (ItemData c_obj in Creat.Equipment) {
						if (c_obj.Type == obj.Type) {
								equipcheck = true;
						}
				}
				if (equipcheck == false) {
						Creat.Equipment.Add (obj);
						Creat.Inventory.Remove (obj);
				}
				Creat.InitalStats.Equipment = Creat.Equipment;
				Creat.InitalStats.Inventory = Creat.Inventory;
		}
	
		public void Unequip (ItemData obj) {
				Creat.Inventory.Add (obj);
				Creat.Equipment.Remove (obj);
				Creat.InitalStats.Equipment = Creat.Equipment;
				Creat.InitalStats.Inventory = Creat.Inventory;
		}
	
		public Vector3 MoveTo (Vector3 Pos) {
				
				if (Creat.MoveTimer <= 0) {
						bool MovementCheck = true;
						/*			
						TileMap MapDaten = GameObject.Find ("Map").GetComponent<TileMap> ();
						int MoveToTileType = MapDaten.tiles [(int)Pos.x, (int)Pos.y];
						regions TileToMove = MapDaten.tileTypes [MoveToTileType];
						if (!TileToMove.isWalkable) {
								MovementCheck = false;
						}
						GameObject[] tmpmob;
						tmpmob = GameObject.FindGameObjectsWithTag ("Mob");
						foreach (GameObject tmpobj in tmpmob) {
								if (tmpobj.transform.position.x == Pos.x && tmpobj.transform.position.y == Pos.y) {
										MovementCheck = false;
								}
						}
						tmpmob = GameObject.FindGameObjectsWithTag ("Player");
						foreach (GameObject tmpobj in tmpmob) {
								if (tmpobj.transform.position.x == Pos.x && tmpobj.transform.position.y == Pos.y) {
										MovementCheck = false;
								}
						}*/
			
						if (MovementCheck) {
								Creat.lastPos = Creat.Position;
								BerechneMovmentDelay ();
								newPos = Pos/* - Creat.Position*/;
								//Creat.Position = Pos;
								//Creat.InitalStats.Position = Pos;
								transform.Translate (newPos * 10 * Time.deltaTime);
								newPos = transform.position;
								Creat.MoveTimer = (/*TileToMove.walkEffect + */Creat.Movement_Delay) * 0.25f; // *0 zum deven
						}
				}
				return newPos;
		}
	
		void UpdateReg () {
				if ((Creat.IsRegAble) && (Creat.RegTimer <= 0)) {
						Creat.HP += Creat.MaxHP / 100;
						if (Creat.HP >= Creat.MaxHP) {
								Creat.HP = Creat.MaxHP;
						}
						Creat.MP += Creat.MaxMP / 100;
						if (Creat.MP >= Creat.MaxMP) {
								Creat.MP = Creat.MaxMP;
						}
						Creat.Stamina += Creat.MaxStamina / 100;
						if (Creat.Stamina >= Creat.MaxStamina) {
								Creat.Stamina = Creat.MaxStamina;
						}
						Creat.RegTimer = Creat.RegCooldown;
				}
		}
	
		void UpdatePosition () {
				Creat.Position = transform.position;
		}
	
		void DrawHP () {
				if (healthbar == null) {
						if (transform.FindChild ("Health")) {
								healthbar = transform.FindChild ("Health").transform;
								healthbarFilled = transform.FindChild ("HealthBar").transform;
						}
				} else {
						if (Creat.MaxHP > 0) {
								float HpProzent = Creat.HP * 100 / Creat.MaxHP;
								// Bars Position
								Vector3 Pos_Healthbar = Vector3.up;
								Pos_Healthbar.y = 0.25f;
								healthbar.position = Camera.main.WorldToViewportPoint (transform.position + Pos_Healthbar);
								Pos_Healthbar.z -= 1;
								healthbarFilled.position = Camera.main.WorldToViewportPoint (transform.position + Pos_Healthbar);
								// Bar Fill Status
								Rect hpstatus = healthbar.GetComponent<GUITexture> ().pixelInset;
								hpstatus.width = (HpProzent * healthbarFilled.GetComponent<GUITexture> ().pixelInset.width) / 100;
								healthbar.GetComponent<GUITexture> ().pixelInset = hpstatus;
						}
				}
		}

		
		void BerechneMovmentDelay () {
				GGewicht = 0.0f;
				if (GameObject.Find ("Uebergabe").GetComponent<mainmenu> ().gameloaded) {
						foreach (ItemData tmpitem in Creat.Inventory) {
								GGewicht += tmpitem.Weigth;
						}
						foreach (ItemData tmpitem in Creat.Equipment) {
								GGewicht += tmpitem.Weigth;
						}
				}
				Creat.Movement_Delay = GGewicht / 100; // Movement Langsamer durch gewicht XD Selbst ausgetrickst beim testen
		}
	
		public void CalculateStats () {
				// Getting normal Stats (Auch Equip und so, eigentlich alle Werte aus InitalStats den hier entsprechenden zuweisen...)
				// also total nervig -.- Oder das geht irgendwie einfacher und ich weiß nicht wie
				// Obwohl manche sachen brauch man villeicht doch nicht so
				Creat.Name = Creat.InitalStats.Name;
				Creat.AggroRange = Creat.InitalStats.AggroRange;
				Creat.IsBoss = Creat.InitalStats.IsBoss;
				Creat.IsMoveable = Creat.InitalStats.IsMoveable;
				
				Creat.Str = Creat.InitalStats.Str;	
				Creat.Agi = Creat.InitalStats.Agi;
				Creat.Dex = Creat.InitalStats.Dex;
				Creat.Int = Creat.InitalStats.Int;
				Creat.Vit = Creat.InitalStats.Vit;
				Creat.Luc = Creat.InitalStats.Luc;
				Creat.MaxStamina = (Creat.InitalStats.Stamina + (int)Mathf.Sqrt (Creat.InitalStats.Agi * Creat.InitalStats.Agi + Creat.InitalStats.Dex * Creat.InitalStats.Dex)) * 1000;

				Creat.AttackRange = 1;
		
				// Adding Stats on Stuff like Equiptment (or Buffs)
				Creat.PhyArmor = 0;
				Creat.MagArmor = 0;
				Creat.PhyAttack = 0;
				Creat.MagAttack = 0;
				int EffectHP = 0;
				int EffectMP = 0;
				if (Creat.Equipment != null) {
						int tmp_count = 0;
						foreach (ItemData tmp in Creat.Equipment) {
								float equip_dura_faktor = 1;
								if (tmp.Durability == 0) {
										equip_dura_faktor = 0;
								} else if (tmp.Durability == -1) {
										equip_dura_faktor = 1;
								} else if (tmp.Durability <= tmp.MaxDurability * 0.5f) {
										equip_dura_faktor = 0.5f;
								}
								if (tmp.IsStaticEffect) {
										switch (tmp.EffectType) {
												case EffectType.Health:
														EffectHP += (int)(tmp.Effect * equip_dura_faktor);
														break;
												case EffectType.Mana:
														EffectMP += (int)(tmp.Effect * equip_dura_faktor);
														break;
												case EffectType.Str:
														Creat.Str += (int)(tmp.Effect * equip_dura_faktor);
														break;
												case EffectType.Agi:
														Creat.Agi += (int)(tmp.Effect * equip_dura_faktor);
														break;
												case EffectType.Dex:
														Creat.Dex += (int)(tmp.Effect * equip_dura_faktor);
														break;
												case EffectType.Int:
														Creat.Int += (int)(tmp.Effect * equip_dura_faktor);
														break;
												case EffectType.Vit:
														Creat.Vit += (int)(tmp.Effect * equip_dura_faktor);
														break;
												case EffectType.Luc:
														Creat.Luc += (int)(tmp.Effect * equip_dura_faktor);
														break;
										}
								}
			
								// With BattleStance
								switch (tmp.Type) {
										case ItemType.accessorie:
										case ItemType.armor_feet:
										case ItemType.armor_hand:
										case ItemType.armor_head:
										case ItemType.armor_leg:
										case ItemType.armor_torso:
										case ItemType.potion:
										case ItemType.utility:
												ItemData x = Creat.Equipment [tmp_count];
												x.Capacity = x.Ammo.Count;
												Creat.Equipment [tmp_count] = x;
												Creat.PhyArmor += (int)(tmp.PhyArmor * equip_dura_faktor);
												Creat.MagArmor += (int)(tmp.MagArmor * equip_dura_faktor);
												Creat.PhyAttack += (int)(tmp.PhyAttack * equip_dura_faktor);
												Creat.MagAttack += (int)(tmp.MagAttack * equip_dura_faktor);
												break;
										case ItemType.weapon_melee:
												if (Creat.Stance == BattleStance.melee) {
														Creat.PhyArmor += (int)(tmp.PhyArmor * equip_dura_faktor);
														Creat.MagArmor += (int)(tmp.MagArmor * equip_dura_faktor);
														Creat.PhyAttack += (int)(tmp.PhyAttack * equip_dura_faktor);
														Creat.MagAttack += (int)(tmp.MagAttack * equip_dura_faktor);
														Creat.AttackRange += (int)(tmp.Range * equip_dura_faktor);
												}
												break;
										case ItemType.weapon_ammo:
										case ItemType.weapon_range:
												if (Creat.Stance == BattleStance.range) {
														Creat.PhyArmor += (int)(tmp.PhyArmor * equip_dura_faktor);
														Creat.MagArmor += (int)(tmp.MagArmor * equip_dura_faktor);
														Creat.PhyAttack += (int)(tmp.PhyAttack * equip_dura_faktor);
														Creat.MagAttack += (int)(tmp.MagAttack * equip_dura_faktor);
														Creat.AttackRange += tmp.Range;
												}
												break;
								// TODO: What gives Mage Bonus Dmg? Range Weapon? melee Weapon? Both?
								// statt bonus - verschiedene zauber zB heilen/angriff/buff/debuffs entfernen
								}
								tmp_count++;
						}
				}
		
				for (int i = 0; i<Creat.StatusEffects.Count; i++) {
						Status tmpst = Creat.StatusEffects [i];
						tmpst.Duration -= Time.deltaTime;
						Creat.StatusEffects [i] = tmpst;
						if (Creat.StatusEffects [i].Duration <= 0) {
								Creat.StatusEffects.RemoveAt (i);
						} else {
								switch (Creat.StatusEffects [i].EffectType) {
										case EffectType.Health:
												EffectHP += (int)(Creat.StatusEffects [i].Effect * Creat.StatusEffects [i].Level);
												break;
										case EffectType.Mana:
												EffectMP += (int)(Creat.StatusEffects [i].Effect * Creat.StatusEffects [i].Level);
												break;
										case EffectType.Str:
												Creat.Str += (int)(Creat.StatusEffects [i].Effect * Creat.StatusEffects [i].Level);
												break;
										case EffectType.Agi:
												Creat.Agi += (int)(Creat.StatusEffects [i].Effect * Creat.StatusEffects [i].Level);
												break;
										case EffectType.Dex:
												Creat.Dex += (int)(Creat.StatusEffects [i].Effect * Creat.StatusEffects [i].Level);
												break;
										case EffectType.Int:
												Creat.Int += (int)(Creat.StatusEffects [i].Effect * Creat.StatusEffects [i].Level);
												break;
										case EffectType.Vit:
												Creat.Vit += (int)(Creat.StatusEffects [i].Effect * Creat.StatusEffects [i].Level);
												break;
										case EffectType.Luc:
												Creat.Luc += (int)(Creat.StatusEffects [i].Effect * Creat.StatusEffects [i].Level);
												break;
								}
						}
				}
		
		
		
				// Calculate Battle Relevant Stuff
				Creat.MaxHP = EffectHP + (Creat.Vit * 20);
				Creat.MaxMP = EffectMP + (Creat.Int * 20);
				// Armor sollte nicht durch stats mehr werden
				//PhyArmor += Vit * 3;
				//MagArmor += Int * 3;
				switch (Creat.Stance) {
						case BattleStance.melee:
								Creat.PhyAttack += Creat.Str * 3;
								break;
						case BattleStance.range:
								Creat.PhyAttack += Creat.Dex * 3;
								break;
						case BattleStance.magic:
								float magdmg_tmp = (Creat.MaxMP / (Creat.Str + 1));
								magdmg_tmp *= (1 - (((Creat.MP + 0.001f) - 500) / (Creat.MaxMP + 0.001f)));
								Creat.MagAttack += (int)magdmg_tmp;
								Creat.MagAttack += Creat.Int * 5;
								break;
				}
		
				//Creat.PhyAttack += Creat.Str * 3;
				//Creat.MagAttack += Creat.Int * 3;
		
			
				if (Creat.AttackCooldown == 0) {
						Creat.AttackCooldown = 0.5f;
				}
				if (Creat.RegCooldown == 0) {
						Creat.RegCooldown = 1.0f;
				}
				if (Creat.HPPotionCooldown == 0) {
						Creat.HPPotionCooldown = 5.0f;
				}
		}
	
		//Statusveränderunsbereich
	
		void BoostPhyDmg () {
		
		}
		void BoostMagDmg () {
		}
	
}

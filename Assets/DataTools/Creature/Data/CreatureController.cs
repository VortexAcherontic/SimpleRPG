using UnityEngine;
using System.Collections.Generic;

public class CreatureController : MonoBehaviour {
		// Nur für die Berechnung! 
		// Damit nicht immer Clone exestieren?
	
		public CreatureData Creat;
		public bool IsLoaded = false;
	
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
		
				ItemDataList DataListObj;
				DataListObj = (ItemDataList)Resources.Load ("Items");
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
				Creat.Equipment = Creat.InitalStats.Equipment;
				Creat.Inventory = Creat.InitalStats.Inventory;
		
				IsLoaded = true;
		}
	
		public void UpdateTimer () {
				Creat.MoveTimer -= Time.deltaTime;
				Creat.RegTimer -= Time.deltaTime;
				Creat.AttackTimer -= Time.deltaTime;
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
	
		public void MoveTo (Vector2 Pos) {
				
				if (Creat.MoveTimer <= 0) {
						
						bool MovementCheck = true;
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
						}
			
						if (MovementCheck) {
								Creat.lastPos = Creat.Position;
								BerechneMovmentDelay ();
								Vector3 newPos = new Vector3 (Pos.x, Pos.y, transform.position.z);
								Creat.Position = Pos;
								Creat.InitalStats.Position = Pos;
								transform.position = newPos;
								Creat.MoveTimer = TileToMove.walkEffect + Creat.Movement_Delay;
						}
				}
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
				float GGewicht = 0.0f;
				if (GameObject.Find ("Main Camera").GetComponent<mainmenu> ().gameloaded) {
						foreach (ItemData tmpitem in Creat.Inventory) {
								GGewicht += tmpitem.Weigth;
						}
						foreach (ItemData tmpitem in Creat.Equipment) {
								GGewicht += tmpitem.Weigth;
						}
				}
				Creat.Movement_Delay = GGewicht / 100; // Movement Langsamer durch gewicht XD Selbst ausgetrickst beim testen
		}
	
		void CalculateStats () {
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
				Creat.AttackRange = 1;
		
				// Adding Stats on Stuff like Equiptment (or Buffs)
				Creat.PhyArmor = 0;
				Creat.MagArmor = 0;
				Creat.PhyAttack = 0;
				Creat.MagAttack = 0;
				int EffectHP = 0;
				int EffectMP = 0;
				if (Creat.InitalStats.Equipment != null) {
						foreach (ItemData tmp in Creat.InitalStats.Equipment) {
								if (tmp.IsStaticEffect) {
										switch (tmp.EffectType) {
												case EffectType.Health:
														EffectHP += tmp.Effect;
														break;
												case EffectType.Mana:
														EffectMP += tmp.Effect;
														break;
												case EffectType.Str:
														Creat.Str += tmp.Effect;
														break;
												case EffectType.Agi:
														Creat.Agi += tmp.Effect;
														break;
												case EffectType.Dex:
														Creat.Dex += tmp.Effect;
														break;
												case EffectType.Int:
														Creat.Int += tmp.Effect;
														break;
												case EffectType.Vit:
														Creat.Vit += tmp.Effect;
														break;
												case EffectType.Luc:
														Creat.Luc += tmp.Effect;
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
												Creat.PhyArmor += tmp.PhyArmor;
												Creat.MagArmor += tmp.MagArmor;
												Creat.PhyAttack += tmp.PhyAttack;
												Creat.MagAttack += tmp.MagAttack;
												Creat.AttackRange += tmp.Range;
												break;
										case ItemType.weapon_melee:
												if (Creat.Stance == BattleStance.melee) {
														Creat.PhyArmor += tmp.PhyArmor;
														Creat.MagArmor += tmp.MagArmor;
														Creat.PhyAttack += tmp.PhyAttack;
														Creat.MagAttack += tmp.MagAttack;
														Creat.AttackRange += tmp.Range;
												}
												break;
										case ItemType.weapon_ammo:
										case ItemType.weapon_range:
												if (Creat.Stance == BattleStance.range) {
														Creat.PhyArmor += tmp.PhyArmor;
														Creat.MagArmor += tmp.MagArmor;
														Creat.PhyAttack += tmp.PhyAttack;
														Creat.MagAttack += tmp.MagAttack;
														Creat.AttackRange += tmp.Range;
												}
												break;
								// TODO: What gives Mage Bonus Dmg? Range Weapon? melee Weapon? Both?
								// statt bonus - verschiedene zauber zB heilen/angriff/buff/debuffs entfernen
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
		}
}

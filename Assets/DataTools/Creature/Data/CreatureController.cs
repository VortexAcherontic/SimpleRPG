using UnityEngine;
using System.Collections;

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
				Creat.InitalStats = InitStat;
				CalculateStats ();
				Creat.HP = Creat.MaxHP;
				Creat.MP = Creat.MaxMP;
				Creat.Gold = Creat.InitalStats.Gold;
				Creat.XP = Creat.InitalStats.XP;
				
				ItemDataList DataListObj;
				DataListObj = (ItemDataList)Resources.Load ("Items");
				// Equipment richtig eintragen
				foreach (string tmpitem in Creat.InitalStats.Equipment_Strings) {
						Creat.InitalStats.Equipment.Add (DataListObj.item_mit_name (tmpitem));
				}
				// Iventory richtig eintragen
				foreach (string tmpitem in Creat.InitalStats.Inventory_Strings) {
						Creat.InitalStats.Inventory.Add (DataListObj.item_mit_name (tmpitem));
				}
				Creat.Equipment = Creat.InitalStats.Equipment;
				Creat.Inventory = Creat.InitalStats.Inventory;
		
				IsLoaded = true;
		}
	
		public void UpdateTimer () {
				Creat.MoveTimer -= Time.deltaTime;
				Creat.RegTimer -= Time.deltaTime;
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
		}
	
		public void Unequip (ItemData obj) {
				Creat.Inventory.Add (obj);
				Creat.Equipment.Remove (obj);
		}
	
		public void MoveTo (Vector2 Pos) {
				if (Creat.MoveTimer <= 0) {
						bool MovementCheck = true;
						TileMap MapDaten = GameObject.Find ("Map").GetComponent<TileMap> ();
						int MoveToTileType = MapDaten.tiles [(int)Pos.x, (int)Pos.y];
						regions TileToMove = MapDaten.tileTypes [MoveToTileType];
						if (TileToMove.isWalkable) {
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
								Vector3 newPos = new Vector3 (Pos.x, Pos.y, transform.position.z);
								Creat.Position = Pos;
								Creat.MoveTimer = TileToMove.walkEffect;
						}
				}
		}
	
		void UpdateReg () {
				if ((Creat.IsRegAble) && (Creat.RegTimer <= 0)) {
						Creat.HP += Creat.MaxHP / 1000;
						if (Creat.HP >= Creat.MaxHP) {
								Creat.HP = Creat.MaxHP;
						}
						Creat.MP += Creat.MaxMP / 1000;
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
						healthbar = transform.FindChild ("Health").transform;
						healthbarFilled = transform.FindChild ("HealthBar").transform;
				}
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
								case ItemType.weapon_meele:
										if (Creat.Stance == BattleStance.meele) {
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
						// TODO: What gives Mage Bonus Dmg? Range Weapon? Meele Weapon? Both?
						// statt bonus - verschiedene zauber zB heilen/angriff/buff/debuffs entfernen
						}
			
				}
		
				// Calculate Battle Relevant Stuff
				Creat.MaxHP = EffectHP + (Creat.Vit * 20);
				Creat.MaxMP = EffectMP + (Creat.Int * 20);
				// Armor sollte nicht durch stats mehr werden
				//PhyArmor += Vit * 3;
				//MagArmor += Int * 3;
				Creat.PhyAttack += Creat.Str * 3;
				Creat.MagAttack += Creat.Int * 3;
		}
}

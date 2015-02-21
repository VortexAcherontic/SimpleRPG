using UnityEngine;
using System.Collections.Generic;

public struct Notification {
		public string message;
		public float time;
}

public class PlayerBehaviour : MonoBehaviour {
		public CreatureController me;
		public Sprite_CharController SC;
		public QuestController qc;
	
		public List<Notification> PickupList = new List<Notification> ();
		public bool Death = false;
		
		bool GUI_Statverteilen = false;
		bool GUI_Inventory = false;
		bool GUI_Equipment = false;
		bool GUI_journal = false;
	
		int GUI_Anzeige_Kat = 0;
		int GUI_journal_kat = 0;
		Vector2 GUI_Scroller = new Vector2 ();
		Rect GUI_Scrollbereich;
	
		List<QuestStruct> tmp_quests = new List<QuestStruct> ();
	
		// Use this for initialization
		void Start () {
				me = gameObject.GetComponent<CreatureController> ();
				SC = gameObject.GetComponentInChildren<Sprite_CharController> ();
				qc = gameObject.GetComponent<QuestController> ();
		}
	
		// Update is called once per frame
		void Update () {
				if (me.IsLoaded) {
						CheckKeyInput ();
						CheckLevelUp ();
						CheckDeath ();
				}
		}
	
		void OnGUI () {
				if (me.IsLoaded) {
						GUILevelUP ();
						GUIInventory ();
						GUIEquipment ();
						GUIStatsOverview ();
						GUINotification ();
						GUIJournal ();
				}
		}
	
		void CheckKeyInput () {
				if (Input.GetKey ("w")) {
						SC.Action = AnimationTyp.MoveUp;
						me.Creat.Position = me.MoveTo (new Vector2 (0, 1));
				}
				if (Input.GetKey ("a")) {
						SC.Action = AnimationTyp.MoveLeft;
						me.Creat.Position = me.MoveTo (new Vector2 (- 1, 0));
				}
				if (Input.GetKey ("s")) {
						SC.Action = AnimationTyp.MoveDown;
						me.Creat.Position = me.MoveTo (new Vector2 (0, - 1));
				}
				if (Input.GetKey ("d")) {
						SC.Action = AnimationTyp.MoveRight;
						me.Creat.Position = me.MoveTo (new Vector2 (1, 0));
				}
				if (Input.GetKeyDown ("i")) {
						GUI_Inventory = !GUI_Inventory;
				}
				if (Input.GetKeyDown ("j")) {
						GUI_journal = !GUI_journal;
				}
				if (Input.GetKey ("1")) {
						me.Creat.Stance = BattleStance.melee;
				}
				if (Input.GetKey ("2")) {
						me.Creat.Stance = BattleStance.range;
				}
				if (Input.GetKey ("3")) {
						me.Creat.Stance = BattleStance.magic;
				}
				if (Input.GetKey ("q")) {
						ItemData tmppot = new ItemData ();
						bool potionistda = false;
						foreach (ItemData tmp_item in me.Creat.Inventory) {
								if (tmp_item.Type == ItemType.potion && tmp_item.EffectType == EffectType.Health) {
										tmppot = tmp_item;
										potionistda = true;
								}
						}
						if ((potionistda) && (me.Creat.HPPotionTimer <= 0)) {
								ItemUse (tmppot);
								me.Creat.HPPotionTimer = me.Creat.HPPotionCooldown;
						}
				}
		}
	
		void CheckLevelUp () {
				if (me.Creat.XP >= 100 * (me.Creat.Level + 1)) {
						me.Creat.Level++;
						me.Creat.XP -= 100 * (me.Creat.Level);
						if (me.Creat.Level < 10) {
								me.Creat.StatPoints = 10;
						} else {
								me.Creat.StatPoints = 5;
						}
				
				}
		}
		void CheckDeath () {
				if (me.Creat.HP <= 0) {
						Application.LoadLevel ("GameOverScreen");
				}
		}
	
		void ItemUse (ItemData UsedItem) {
				if (UsedItem.EffectType == EffectType.Mana) {
						me.Creat.MP += UsedItem.Effect;
				}
				if (UsedItem.EffectType == EffectType.Health) {
						me.Creat.HP += UsedItem.Effect;
				}
				if (me.Creat.HP >= me.Creat.MaxHP) {
						me.Creat.HP = me.Creat.MaxHP;
				}
				if (me.Creat.MP >= me.Creat.MaxMP) {
						me.Creat.MP = me.Creat.MaxMP;
				}
				me.Creat.Inventory.Remove (UsedItem);
				me.Creat.InitalStats.Inventory = me.Creat.Inventory;
		}
	
		void GUILevelUP () {
				if (me.Creat.StatPoints > 0) {
						if (GUI.Button (new Rect (Screen.width / 2 - 100, Screen.height / 2 - 10, 200, 20), "LevelUp - Skillpoints available!")) {
								GUI_Statverteilen = true;
						}
				} else {
						GUI_Statverteilen = false;
				}
				if (GUI_Statverteilen) {
						Rect Anzeigebereich = new Rect (Screen.width / 3, Screen.height / 3, Screen.width / 3, Screen.height / 3);
						GUI.Box (Anzeigebereich, "Statpoint");
			
						// Verteilbare Punkte
						Rect Zeile = new Rect (Anzeigebereich.position.x, Anzeigebereich.position.y, Anzeigebereich.width / 3, Anzeigebereich.height / 9);
						Rect Spalte1 = new Rect (Zeile.position.x, Zeile.position.y + Zeile.height, Zeile.width, Zeile.height);
						GUI.Label (Spalte1, "Points : ");
						Rect Spalte2 = new Rect (Spalte1.position.x + Spalte1.width, Spalte1.position.y, Spalte1.width, Spalte1.height);
						GUI.Label (Spalte2, "" + me.Creat.StatPoints);
						Rect Spalte3 = new Rect (Spalte2.position.x + Spalte2.width, Spalte2.position.y, Spalte2.width, Spalte2.height);
						GUI.Label (Spalte3, "");
			
						// Atkuelles Level
						Zeile = new Rect (Zeile.position.x, Zeile.position.y + Zeile.height, Zeile.width, Zeile.height);
						Spalte1 = new Rect (Zeile.position.x, Zeile.position.y + Zeile.height, Zeile.width, Zeile.height);
						GUI.Label (Spalte1, "Level : ");
						Spalte2 = new Rect (Spalte1.position.x + Spalte1.width, Spalte1.position.y, Spalte1.width, Spalte1.height);
						GUI.Label (Spalte2, "" + me.Creat.Level);
						Spalte3 = new Rect (Spalte2.position.x + Spalte2.width, Spalte2.position.y, Spalte2.width, Spalte2.height);
						GUI.Label (Spalte3, "");
			
						// Atkuelle STR
						Zeile = new Rect (Zeile.position.x, Zeile.position.y + Zeile.height, Zeile.width, Zeile.height);
						Spalte1 = new Rect (Zeile.position.x, Zeile.position.y + Zeile.height, Zeile.width, Zeile.height);
						GUI.Label (Spalte1, "Strength : ");
						Spalte2 = new Rect (Spalte1.position.x + Spalte1.width, Spalte1.position.y, Spalte1.width, Spalte1.height);
						GUI.Label (Spalte2, "" + me.Creat.InitalStats.Str);
						Spalte3 = new Rect (Spalte2.position.x + Spalte2.width, Spalte2.position.y, Spalte2.width, Spalte2.height);
						if (GUI.Button (Spalte3, "+")) {
								me.Creat.InitalStats.Str += 5;
								me.Creat.StatPoints--;
						}
			
						// Atkuelle AGI
						Zeile = new Rect (Zeile.position.x, Zeile.position.y + Zeile.height, Zeile.width, Zeile.height);
						Spalte1 = new Rect (Zeile.position.x, Zeile.position.y + Zeile.height, Zeile.width, Zeile.height);
						GUI.Label (Spalte1, "Agility : ");
						Spalte2 = new Rect (Spalte1.position.x + Spalte1.width, Spalte1.position.y, Spalte1.width, Spalte1.height);
						GUI.Label (Spalte2, "" + me.Creat.InitalStats.Agi);
						Spalte3 = new Rect (Spalte2.position.x + Spalte2.width, Spalte2.position.y, Spalte2.width, Spalte2.height);
						if (GUI.Button (Spalte3, "+")) {
								me.Creat.InitalStats.Agi += 5;
								me.Creat.StatPoints--;
						}
			
						// Atkuelle Dex
						Zeile = new Rect (Zeile.position.x, Zeile.position.y + Zeile.height, Zeile.width, Zeile.height);
						Spalte1 = new Rect (Zeile.position.x, Zeile.position.y + Zeile.height, Zeile.width, Zeile.height);
						GUI.Label (Spalte1, "Dextery : ");
						Spalte2 = new Rect (Spalte1.position.x + Spalte1.width, Spalte1.position.y, Spalte1.width, Spalte1.height);
						GUI.Label (Spalte2, "" + me.Creat.InitalStats.Dex);
						Spalte3 = new Rect (Spalte2.position.x + Spalte2.width, Spalte2.position.y, Spalte2.width, Spalte2.height);
						if (GUI.Button (Spalte3, "+")) {
								me.Creat.InitalStats.Dex += 5;
								me.Creat.StatPoints--;
						}
			
						// Atkuelle VIT
						Zeile = new Rect (Zeile.position.x, Zeile.position.y + Zeile.height, Zeile.width, Zeile.height);
						Spalte1 = new Rect (Zeile.position.x, Zeile.position.y + Zeile.height, Zeile.width, Zeile.height);
						GUI.Label (Spalte1, "Vitality : ");
						Spalte2 = new Rect (Spalte1.position.x + Spalte1.width, Spalte1.position.y, Spalte1.width, Spalte1.height);
						GUI.Label (Spalte2, "" + me.Creat.InitalStats.Vit);
						Spalte3 = new Rect (Spalte2.position.x + Spalte2.width, Spalte2.position.y, Spalte2.width, Spalte2.height);
						if (GUI.Button (Spalte3, "+")) {
								me.Creat.InitalStats.Vit += 5;
								me.Creat.StatPoints--;
						}
			
						// Atkuelle INT
						Zeile = new Rect (Zeile.position.x, Zeile.position.y + Zeile.height, Zeile.width, Zeile.height);
						Spalte1 = new Rect (Zeile.position.x, Zeile.position.y + Zeile.height, Zeile.width, Zeile.height);
						GUI.Label (Spalte1, "Int : ");
						Spalte2 = new Rect (Spalte1.position.x + Spalte1.width, Spalte1.position.y, Spalte1.width, Spalte1.height);
						GUI.Label (Spalte2, "" + me.Creat.InitalStats.Int);
						Spalte3 = new Rect (Spalte2.position.x + Spalte2.width, Spalte2.position.y, Spalte2.width, Spalte2.height);
						if (GUI.Button (Spalte3, "+")) {
								me.Creat.InitalStats.Int += 5;
								me.Creat.StatPoints--;
						}
			
						// Atkuelle LUC
						Zeile = new Rect (Zeile.position.x, Zeile.position.y + Zeile.height, Zeile.width, Zeile.height);
						Spalte1 = new Rect (Zeile.position.x, Zeile.position.y + Zeile.height, Zeile.width, Zeile.height);
						GUI.Label (Spalte1, "Luck : ");
						Spalte2 = new Rect (Spalte1.position.x + Spalte1.width, Spalte1.position.y, Spalte1.width, Spalte1.height);
						GUI.Label (Spalte2, "" + me.Creat.InitalStats.Luc);
						Spalte3 = new Rect (Spalte2.position.x + Spalte2.width, Spalte2.position.y, Spalte2.width, Spalte2.height);
						if (GUI.Button (Spalte3, "+")) {
								me.Creat.InitalStats.Luc += 5;
								me.Creat.StatPoints--;
						}		
				}
		}
	
		void GUIEquipment () {
				Rect tmp_anzeige = new Rect (Screen.width / 2 - 500, Screen.height / 2 - 200, 1000, 400);
				Rect Spalte;
				Rect Zeile1 = new Rect (0, 0, GUI_Scrollbereich.width, 20);
				Rect AnzeigeScrollView = new Rect (0, 0 + 50, tmp_anzeige.width, tmp_anzeige.height - 40);
				GUI_Scrollbereich.position = new Vector2 (0, 0);
				GUI_Scrollbereich.width = AnzeigeScrollView.width - 50;
		
				if (GUI_Equipment) {
						GUILayout.BeginArea (tmp_anzeige);
						{
				
								GUI.Box (new Rect (0, 0, tmp_anzeige.width, tmp_anzeige.height), "Equip");
								GUILayout.Space (20);								
								if ((GUI.Button (new Rect (tmp_anzeige.width - 40, 0, 40, 20), "Quit")) || (Input.GetKey (KeyCode.Escape))) {
										GUI_Equipment = false;
								}
								if (GUI.Button (new Rect (tmp_anzeige.width - 150, 0, 110, 20), "Swap to Iventory")) {
										GUI_Inventory = true;
										GUI_Equipment = false;
								}
								Zeile1.position = new Vector2 (Zeile1.position.x, Zeile1.position.y + 20);
				
								foreach (ItemData dieseitem in me.Creat.Equipment) {
					
										GUILayout.BeginHorizontal ();
										Spalte = new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width / 7, Zeile1.height);
										GUI.Label (Spalte, dieseitem.Name);
										Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
										switch (dieseitem.Type) {
												case ItemType.weapon_melee:
												case ItemType.weapon_range:
														GUI.Label (Spalte, "Physical Damage: " + dieseitem.PhyAttack);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Magical Damage: " + dieseitem.MagAttack);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Durability: " + dieseitem.Durability + " / " + dieseitem.MaxDurability);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														break;
												case ItemType.armor_feet:
												case ItemType.armor_hand:
												case ItemType.armor_head:
												case ItemType.armor_leg:
												case ItemType.armor_torso:
														GUI.Label (Spalte, "Physical Defense: " + dieseitem.PhyArmor);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Magical Defense: " + dieseitem.MagArmor);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Durability: " + dieseitem.Durability + " / " + dieseitem.MaxDurability);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														break;
												case ItemType.accessorie:
												case ItemType.potion:
														GUI.Label (Spalte, "Effect: " + dieseitem.Effect);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "" + dieseitem.EffectType.ToString ());
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														break;
												case ItemType.utility:
														GUI.Label (Spalte, "Capacity: " + dieseitem.Capacity + " / " + dieseitem.MaxCapacity);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "");
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														break;
							
										}
					
										GUI.Label (Spalte, "Weight: " + dieseitem.Weigth + " kg");
										Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
										if (GUI.Button (Spalte, "Drop")) {
												me.Creat.Equipment.Remove (dieseitem);
										}
										Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
										if (GUI.Button (Spalte, "Unequip")) {
												me.Unequip (dieseitem);
												return;
										}
										Zeile1.position = new Vector2 (0, Zeile1.position.y + 20);
					
										GUILayout.EndHorizontal ();
								}
						}
						GUILayout.EndArea ();
				}
		}
	
		void GUIInventory () {
				if (GUI_Inventory) {
						Rect tmp_anzeige = new Rect (Screen.width / 2 - 500, Screen.height / 2 - 200, 1000, 400);
						Rect Spalte;
						Rect Zeile1 = new Rect (0, 0, GUI_Scrollbereich.width, 20);
						Rect AnzeigeScrollView = new Rect (0, 0 + 50, tmp_anzeige.width, tmp_anzeige.height - 40);
						GUI_Scrollbereich.position = new Vector2 (0, 0);
						GUI_Scrollbereich.width = AnzeigeScrollView.width - 50;
			
						GUILayout.BeginArea (tmp_anzeige);
						{
								GUI.Box (new Rect (0, 0, tmp_anzeige.width, tmp_anzeige.height), "Inventar");
								GUILayout.Space (20);		
								if ((GUI.Button (new Rect (tmp_anzeige.width - 40, 0, 40, 20), "Quit")) || (Input.GetKey (KeyCode.Escape))) {
										GUI_Inventory = false;
								}
								if (GUI.Button (new Rect (tmp_anzeige.width - 190, 0, 40, 20), "Save")) {
										gameObject.GetComponent<Player_Save> ().Save ();
								}
								if (GUI.Button (new Rect (tmp_anzeige.width - 150, 0, 110, 20), "Swap to Equipped Items")) {
										GUI_Inventory = false;
										GUI_Equipment = true;
								}
			
								GUILayout.BeginHorizontal ();
								if (GUILayout.Button ("Meleeweapons")) {
										GUI_Anzeige_Kat = 1;
								}
								if (GUILayout.Button ("Rangeweapons")) {
										GUI_Anzeige_Kat = 2;
								}
								if (GUILayout.Button ("Armor")) {
										GUI_Anzeige_Kat = 3;
								}
								if (GUILayout.Button ("Potions")) {
										GUI_Anzeige_Kat = 4;
								}
								if (GUILayout.Button ("Ammo")) {
										GUI_Anzeige_Kat = 5;
								}
								if (GUILayout.Button ("Wearables")) {
										GUI_Anzeige_Kat = 6;
								}
								GUILayout.EndHorizontal ();
								GUILayout.FlexibleSpace ();
			
								GUI_Scroller = GUI.BeginScrollView (AnzeigeScrollView, GUI_Scroller, GUI_Scrollbereich);
								switch (GUI_Anzeige_Kat) {
										case 0:
												break;
										case 1:
												foreach (ItemData dieseitem in me.Creat.Inventory) {
						
														if (dieseitem.Type == ItemType.weapon_melee) {
																GUILayout.BeginHorizontal ();
																Spalte = new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width / 8, Zeile1.height);
																GUI.Label (Spalte, dieseitem.Name);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Physical Damage: " + dieseitem.PhyAttack);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Magical Damage: " + dieseitem.MagAttack);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Weight: " + dieseitem.Weigth + " kg");
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Durability: " + dieseitem.Durability + " / " + dieseitem.MaxDurability);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																
																if (GUI.Button (Spalte, "Drop")) {
																		me.Creat.Inventory.Remove (dieseitem);
																}
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																if (GUI.Button (Spalte, "Equip")) {
																		me.Equip (dieseitem);
																		return;
																}
																Zeile1.position = new Vector2 (0, Zeile1.position.y + 20);
							
																GUILayout.EndHorizontal ();
														}
												}
												break;
										case 2:
												foreach (ItemData dieseitem in me.Creat.Inventory) {
														if (dieseitem.Type == ItemType.weapon_range) {
																GUILayout.BeginHorizontal ();
																Spalte = new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width / 8, Zeile1.height);
																GUI.Label (Spalte, dieseitem.Name);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Physical Damage: " + dieseitem.PhyAttack);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Magical Damage: " + dieseitem.MagAttack);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Weight: " + dieseitem.Weigth + " kg");
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Durability: " + dieseitem.Durability + " / " + dieseitem.MaxDurability);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																if (GUI.Button (Spalte, "Drop")) {
																		me.Creat.Inventory.Remove (dieseitem);
																}
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																if (GUI.Button (Spalte, "Equip")) {
																		me.Equip (dieseitem);
																		return;
																}
																Zeile1.position = new Vector2 (0, Zeile1.position.y + 20);
							
																GUILayout.EndHorizontal ();
														}
												}
												break;
										case 3:
												foreach (ItemData dieseitem in me.Creat.Inventory) {
														if ((dieseitem.Type == ItemType.armor_feet) || 
																(dieseitem.Type == ItemType.armor_hand) ||
																(dieseitem.Type == ItemType.armor_head) ||
																(dieseitem.Type == ItemType.armor_leg) ||
																(dieseitem.Type == ItemType.armor_torso)) {
																GUILayout.BeginHorizontal ();
																Spalte = new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width / 8, Zeile1.height);
																GUI.Label (Spalte, dieseitem.Name);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Physical Defense: " + dieseitem.PhyArmor);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Magical Defense: " + dieseitem.MagArmor);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Weight: " + dieseitem.Weigth + " kg");
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Durability: " + dieseitem.Durability + " / " + dieseitem.MaxDurability);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																if (GUI.Button (Spalte, "Drop")) {
																		me.Creat.Inventory.Remove (dieseitem);
																}
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																if (GUI.Button (Spalte, "Equip")) {
																		me.Equip (dieseitem);
																		return;
																}
																Zeile1.position = new Vector2 (0, Zeile1.position.y + 20);
							
																GUILayout.EndHorizontal ();
														}
												}
												break;
										case 4:
												foreach (ItemData dieseitem in me.Creat.Inventory) {
														if (dieseitem.Type == ItemType.potion) {
																GUILayout.BeginHorizontal ();
																Spalte = new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width / 6, Zeile1.height);
																GUI.Label (Spalte, dieseitem.Name);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Effect: " + dieseitem.Effect + " " + dieseitem.EffectType.ToString ());
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Weight: " + dieseitem.Weigth + " kg");
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																if (GUI.Button (Spalte, "Drop")) {
																		me.Creat.Inventory.Remove (dieseitem);
																}
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																if (GUI.Button (Spalte, "Use")) {
																		ItemUse (dieseitem);
								
																}
																Zeile1.position = new Vector2 (0, Zeile1.position.y + 20);
							
																GUILayout.EndHorizontal ();
														}
												}
												break;
										case 5:
												foreach (ItemData dieseitem in me.Creat.Inventory) {
														if (dieseitem.Type == ItemType.utility) {
																GUILayout.BeginHorizontal ();
																Spalte = new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width / 7, Zeile1.height);
																GUI.Label (Spalte, dieseitem.Name);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Capacity: " + dieseitem.MaxCapacity); // ?
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Weight: " + dieseitem.Weigth + " kg");
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																if (GUI.Button (Spalte, "Drop")) {
																		me.Creat.Inventory.Remove (dieseitem);
																}
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																if (GUI.Button (Spalte, "Equip")) {
																		me.Equip (dieseitem);
																		return;
																}
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																if (GUI.Button (Spalte, "Show content.")) {
																		GUI_Anzeige_Kat = 7;
																}
																Zeile1.position = new Vector2 (0, Zeile1.position.y + 20);
							
																GUILayout.EndHorizontal ();
														}
												}
												break;
										case 6:
												foreach (ItemData dieseitem in me.Creat.Inventory) {
														if (dieseitem.Type == ItemType.accessorie) {
																GUILayout.BeginHorizontal ();
																Spalte = new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width / 6, Zeile1.height);
																GUI.Label (Spalte, dieseitem.Name);
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Effect: " + dieseitem.Effect + " " + dieseitem.EffectType.ToString ());
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																GUI.Label (Spalte, "Weight: " + dieseitem.Weigth + " kg");
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																if (GUI.Button (Spalte, "Drop")) {
																		me.Creat.Inventory.Remove (dieseitem);
																}
																Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																if (GUI.Button (Spalte, "Equip")) {
																		me.Equip (dieseitem);
																		return;
																}
																Zeile1.position = new Vector2 (0, Zeile1.position.y + 20);
							
																GUILayout.EndHorizontal ();
														}
												}
												break;
										case 7:
												foreach (ItemData dieseitem2 in me.Creat.Inventory) {
														if (dieseitem2.Type == ItemType.utility) {
																GUILayout.BeginHorizontal ();
																Spalte = new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width / 7, Zeile1.height);
																foreach (AmmoData dieseitem in dieseitem2.Ammo) {
																		GUI.Label (Spalte, dieseitem.Name);
																		Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																		GUI.Label (Spalte, "Physical Damage: " + dieseitem.PhyAttack);
																		Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																		GUI.Label (Spalte, "Magical Damage: " + dieseitem.MagAttack);
																		Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																		GUI.Label (Spalte, "Weight: " + dieseitem.Weigth + " kg");
																		Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
																		if (GUI.Button (Spalte, "Drop")) {
																				dieseitem2.Ammo.Remove (dieseitem);
																		}
																		Zeile1.position = new Vector2 (0, Zeile1.position.y + 20);
								
																		Spalte = new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width / 7, Zeile1.height);
								
								
																}
																GUILayout.EndHorizontal ();
														}
												}
												break;
					
								}
								GUI.EndScrollView ();
								GUI_Scrollbereich.height = Zeile1.position.y + Zeile1.height;
						}
						GUILayout.EndArea ();
		
				}
		}
	
		void GUIJournal () {
				if (GUI_journal) {
						Rect tmp_anzeige = new Rect (Screen.width / 2 - 500, Screen.height / 2 - 200, 1000, 400);
						GUI.Box (tmp_anzeige, "Journal");
						tmp_quests.Clear ();
					
						switch (GUI_journal_kat) {
								case 0:
										foreach (QuestStruct quest in qc.AlleQuests) {
												if ((quest.storyrelevant) && (!quest.finished) && (quest.accepted) && (!quest.failed)) {
														tmp_quests.Add (quest);
												}
										}
										foreach (QuestStruct quest in qc.AlleQuests) {
												if ((!quest.storyrelevant) && (!quest.finished) && (quest.accepted) && (!quest.failed)) {
														tmp_quests.Add (quest);
												}
										}
										break;
								case 1:
										foreach (QuestStruct quest in qc.AlleQuests) {
												if ((quest.storyrelevant) && (quest.finished) && (quest.accepted) && (!quest.failed)) {
														tmp_quests.Add (quest);
												}
										}
										foreach (QuestStruct quest in qc.AlleQuests) {
												if ((!quest.storyrelevant) && (quest.finished) && (quest.accepted) && (!quest.failed)) {
														tmp_quests.Add (quest);
												}
										}
										break;
								case 2:
										foreach (QuestStruct quest in qc.AlleQuests) {
												if ((!quest.storyrelevant) && (!quest.finished) && (quest.accepted) && (quest.failed)) {
														tmp_quests.Add (quest);
												}
										}
										break;
						}
			
						if (GUI.Button (new Rect (Screen.width / 2 - 500 + 075, Screen.height / 2 - 175, 250, 20), "Open")) {
								GUI_journal_kat = 0;
						}
						if (GUI.Button (new Rect (Screen.width / 2 - 500 + 375, Screen.height / 2 - 175, 250, 20), "Finished")) {
								GUI_journal_kat = 1;
						}
						if (GUI.Button (new Rect (Screen.width / 2 - 500 + 675, Screen.height / 2 - 175, 250, 20), "Failed")) {
								GUI_journal_kat = 2;
						}
						Rect Zeile = new Rect (Screen.width / 2 - 500 + 075, Screen.height / 2 - 175 + 25, 250, 20);
						foreach (QuestStruct tmp_q in tmp_quests) {
								GUI.Label (Zeile, "<b>" + tmp_q.Name + "</b>");
								if (tmp_q.EnemyTokill.Count > 0) {
										foreach (EnemyTokillStruct etk in tmp_q.EnemyTokill) {
												Zeile.position = new Vector2 (Zeile.position.x, Zeile.position.y + 25);
												GUI.Label (Zeile, "Kill " + etk.Amount + " x " + etk.Name);
										}
								}
								if (tmp_q.ItemsToCollect.Count > 0) {
										foreach (ItemsToCollectStruct itc in tmp_q.ItemsToCollect) {
												Zeile.position = new Vector2 (Zeile.position.x, Zeile.position.y + 25);
												GUI.Label (Zeile, "Collect " + itc.Amount + " x " + itc.Name);
										}
								}
								if (tmp_q.NPCToTalk.Count > 0) {
										foreach (string ntt in tmp_q.NPCToTalk) {
												Zeile.position = new Vector2 (Zeile.position.x, Zeile.position.y + 25);
												GUI.Label (Zeile, "Talk to " + ntt);
										}
								}
								if (!tmp_q.finished) {
										Zeile.position = new Vector2 (Zeile.position.x, Zeile.position.y + 25);
										GUI.Label (Zeile, "Return to " + tmp_q.NPC_Geber);
								}
								Zeile.position = new Vector2 (Zeile.position.x, Zeile.position.y + 25);
						}
				}		
		}
	
		public Texture2D HpBar_empty;
		public Texture2D ManaBar_empty;
		public Texture2D HpBar_full;
		public Texture2D ManaBar_full;
		public Texture2D XpBar_empty;
		public Texture2D XpBar_full;
	
		float hpbar = 100;
		float manabar = 100;
		float xpbar = 0;
	
		void BarBerechnung () {
				if (me.Creat.MaxHP > 0) {
						hpbar = (100 / (me.Creat.MaxHP + 0.00001f)) * (me.Creat.HP + 0.0001f);
						manabar = (100 / (0.00001f + me.Creat.MaxMP)) * (me.Creat.MP + 0.00001f);
						xpbar = (100 / (100 * ((0.00001f + me.Creat.Level) + 1))) * (me.Creat.XP + 0.00001f);
		
				}
		}
	
		void GUIStatsOverview () {
				BarBerechnung ();
				// Anzeigen für Health, Mana und XP später durch grafische Elemente zu ersetzen.
				GUI.Label (new Rect (5, Screen.height - 30, 170, 20), "Gold: " + me.Creat.Gold + "G");
				GUI.Label (new Rect (5, Screen.height - 55, 170, 20), "Battlemode: " + me.Creat.Stance.ToString ());
		
				// Quiver Inhalt
				ItemData Quiver = new ItemData ();
				if (me.Creat.Equipment.Count > 0) {
						foreach (ItemData c_obj in me.Creat.Equipment) {
								if (c_obj.Type == ItemType.utility) {
										Quiver = c_obj;
								}
						}
				}
				if (Quiver.Name != "") {
						GUI.Label (new Rect (5, Screen.height - 80, 170, 20), "Cap: " + Quiver.Capacity + " / " + Quiver.MaxCapacity);
				}
		
				//HPBAR
				Rect Bar_Pos = new Rect (5, 5, (HpBar_empty.width / 4), (HpBar_empty.height / 4));
				GUI.DrawTexture (Bar_Pos, HpBar_empty, ScaleMode.ScaleAndCrop);
				GUI.DrawTexture (new Rect (Bar_Pos.position.x, Bar_Pos.position.y, Bar_Pos.width * hpbar / 100, Bar_Pos.height), HpBar_full, ScaleMode.ScaleAndCrop);
				//MANABAR
				Bar_Pos = new Rect (Bar_Pos.position.x, Bar_Pos.position.y + Bar_Pos.height + 5, Bar_Pos.width, Bar_Pos.height);
				GUI.DrawTexture (Bar_Pos, ManaBar_empty, ScaleMode.ScaleAndCrop);
				GUI.DrawTexture (new Rect (Bar_Pos.position.x, Bar_Pos.position.y, Bar_Pos.width * manabar / 100, Bar_Pos.height), ManaBar_full, ScaleMode.ScaleAndCrop);
				//XPBAR
				Bar_Pos = new Rect (Bar_Pos.position.x, Bar_Pos.position.y + Bar_Pos.height + 5, Bar_Pos.width, Bar_Pos.height);
				GUI.DrawTexture (Bar_Pos, XpBar_empty, ScaleMode.ScaleAndCrop);
				GUI.DrawTexture (new Rect (Bar_Pos.position.x, Bar_Pos.position.y, Bar_Pos.width * xpbar / 100, Bar_Pos.height), XpBar_full, ScaleMode.ScaleAndCrop);
		
				
		}
		void GUINotification () {
				float tmp_pos_x = 0f;
				float tmp_pos_y = 100;
				float tmp_lineheigth = 20;
				Rect Pos;
				for (int i =0; i<PickupList.Count; i++) {
						Notification tmpnot;
						tmpnot = PickupList [i];
						tmpnot.time -= Time.deltaTime;
						PickupList [i] = tmpnot;
						if (tmpnot.time > 0) {
								tmp_pos_x = tmp_pos_x + tmp_lineheigth;
								Pos = new Rect (Screen.width - tmp_pos_y, Screen.height - tmp_pos_x, 100, tmp_lineheigth);
								GUI.Label (Pos, tmpnot.message);
								
						} else {
								PickupList.RemoveAt (i);
						}
				}
		}
}

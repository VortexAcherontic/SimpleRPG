using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct Notification {
		public string message;
		public float time;
}

[System.Serializable]
public struct SkillAndKeys {
		public string key;
		public string action;
}

public class PlayerBehaviour : MonoBehaviour {
		GUI_Helper GUI_ZoD = new GUI_Helper ();
		public static GameObject ThePlayer;
		public CreatureController me;
		public Sprite_CharController SC;
		public QuestController qc;
		public mainmenu mn;
	
		public List<Notification> PickupList = new List<Notification> ();
		public bool Death = false;
		
		bool GUI_Statverteilen = false;
		bool GUI_Inventory = false;
		bool GUI_Equipment = false;
		bool GUI_journal = false;
		bool GUI_Character = false;
		bool GUI_Hotbar = true;
		
	
		int GUI_Anzeige_Kat = 0;
		int GUI_journal_kat = 0;
		Vector2 GUI_Scroller = new Vector2 ();
		Rect GUI_Scrollbereich;
	
		public Texture2D HpBar_empty;
		public Texture2D ManaBar_empty;
		public Texture2D HpBar_full;
		public Texture2D ManaBar_full;
		public Texture2D XpBar_empty;
		public Texture2D XpBar_full;
		public Texture2D Staminabar_empty;
		public Texture2D Staminabar_full;
	
		public Texture2D[] SlotTexture = new Texture2D[10];
		public Texture2D skillactiv;
		public Texture2D skillinactiv;
		int skill_active = 1;
	
		float hpbar = 100;
		float manabar = 100;
		float xpbar = 0;
		float staminabar = 100;
	
		List<QuestStruct> tmp_quests = new List<QuestStruct> ();
	
		void Awake () {
				if (ThePlayer != null) {
						if (ThePlayer != gameObject) {
								Destroy (gameObject);
						}
				}
				ThePlayer = gameObject;
				DontDestroyOnLoad (gameObject);
		}
		// Use this for initialization
		void Start () {
				me = gameObject.GetComponent<CreatureController> ();
				SC = gameObject.GetComponentInChildren<Sprite_CharController> ();
				qc = gameObject.GetComponent<QuestController> ();
				mn = GameObject.Find ("Uebergabe").GetComponent<mainmenu> ();
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
						GUICharacter ();
						GUIBuff ();
						GUIHotbar ();
				}
		}
	
		void CheckKeyInput () {
				if (Input.GetKey ("1")) {
						skill_active = 1;
						me.Creat.Stance = BattleStance.melee;
				}
				if (Input.GetKey ("2")) {
						skill_active = 2;
						me.Creat.Stance = BattleStance.range;
				}
				if (Input.GetKey ("3")) {
						skill_active = 3;
						me.Creat.Stance = BattleStance.magic;
				}	
				int tmp_input;
				for (int i=0; i<10; i++) {
						tmp_input = i;
						if (i == 0) {
								tmp_input = 10;
						}
						if (Input.GetKey (i.ToString ())) {
								skill_active = tmp_input;
						}
				}
				if (Input.GetKeyDown (KeyCode.F1)) {
						GUI_Hotbar = !GUI_Hotbar;
				}
				foreach (SkillAndKeys inputkey in mn.KeySettings) {
						/*if (Input.GetKey (inputkey.key)) {
								switch (inputkey.action) {
										case "MoveForward":
												//SC.Action = AnimationTyp.MoveUp;
												me.Creat.Position = me.MoveTo (new Vector2 (0, 1));
												break;
										case "MoveLeft":
												//SC.Action = AnimationTyp.MoveLeft;
												me.Creat.Position = me.MoveTo (new Vector2 (- 1, 0));
												break;
										case "MoveBackward":
												//SC.Action = AnimationTyp.MoveDown;
												me.Creat.Position = me.MoveTo (new Vector2 (0, - 1));
												break;
										case "MoveRight":
												//SC.Action = AnimationTyp.MoveRight;
												me.Creat.Position = me.MoveTo (new Vector2 (1, 0));
												break;
								}
						}*/
						if (Input.GetKeyDown (inputkey.key)) {
								switch (inputkey.action) {
										case "ShowInventory":
												GUI_Inventory = !GUI_Inventory;
												mn.cammove = !mn.cammove;
												break;
										case "ShowJournal":
												GUI_journal = !GUI_journal;
												mn.cammove = !mn.cammove;
												break;
										case "UseHealPotion":
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
												break;
										case "ShowCharacter":
												GUI_Character = !GUI_Character;
												mn.cammove = !mn.cammove;
												break;
										case "ShowMap":
												GameObject.Find ("uebergabe").GetComponent<mainmenu> ().showmap = !GameObject.Find ("uebergabe").GetComponent<mainmenu> ().showmap;
												break;
								}
						}
				}
				for (int i = 0; i<skillkeys.Count; i++) {
						if (Input.GetKeyDown (skillkeys [i].key)) {
								SkillUse (skillkeys [i].action);
						}
				}
		}
	
		void CheckLevelUp () {
				if (me.Creat.XP >= 100 * (me.Creat.Level + 1)) {
						me.Creat.Level++;
						me.Creat.XP -= 100 * (me.Creat.Level);
						if (me.Creat.Level < 10) {
								me.Creat.StatPoints += 10;
								me.Creat.SkillPoints += 3;
						} else {
								me.Creat.StatPoints += 5;
								me.Creat.SkillPoints += 1;
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
	
		void SkillUse (string Skillname) {
				foreach (skill sk in me.Creat.Skills) {
						
						if (sk.name == Skillname) {
								foreach (Status st in me.alleStatus) {
										Status tmpst = st;
										if (st.name == sk.Effect [0]) {
												if (me.Creat.MP >= sk.cost) {
														me.Creat.MP -= sk.cost;
														for (int i = 0; i<me.Creat.StatusEffects.Count; i++) {
																if (me.Creat.StatusEffects [i].name == st.name) {
																		me.Creat.StatusEffects.RemoveAt (i);
																}
														}
														tmpst.lvl = sk.lvl;
														me.Creat.StatusEffects.Add (tmpst);
												}
										}
								}
						}
				}
		}
	
		public void SkillLearn (skill s) {
				Debug.Log (s.name);
				bool hatschon = false;
				int count_skills = 0;
				foreach (skill name in me.Creat.Skills) {
						skill tmps = name;
						if (tmps.name == s.name) {
								hatschon = true;
								tmps.lvl += 1;
								me.Creat.Skills [count_skills] = tmps;
						}
						count_skills++;
				}
				if (!hatschon) {
						me.Creat.Skills.Add (s);
				}
		}
	
		void GUILevelUP () {
				if (me.Creat.StatPoints > 0) {
						if (GUI_ZoD.Button_Text ("LevelUp - Skillpoints available!", 11, new Rect (1920 / 2 - 100, 1080 / 2 - 10, 200, 20))) {
								GUI_Statverteilen = true;
						}
				} else {
						GUI_Statverteilen = false;
				}
				if (GUI_Statverteilen) {
						Rect Anzeigebereich = new Rect (1920 / 3, 1080 / 3, 1920 / 3, 1080 / 3);
						GUI_ZoD.Box ("Statpoint", 11, Anzeigebereich);
			
						// Verteilbare Punkte
						Rect Zeile = new Rect (Anzeigebereich.position.x, Anzeigebereich.position.y, Anzeigebereich.width / 3, Anzeigebereich.height / 9);
						Rect Spalte1 = new Rect (Zeile.position.x, Zeile.position.y + Zeile.height, Zeile.width, Zeile.height);
						GUI_ZoD.Label ("Points : ", 11, Spalte1);
						Rect Spalte2 = new Rect (Spalte1.position.x + Spalte1.width, Spalte1.position.y, Spalte1.width, Spalte1.height);
						GUI_ZoD.Label ("" + me.Creat.StatPoints, 11, Spalte2);
						Rect Spalte3 = new Rect (Spalte2.position.x + Spalte2.width, Spalte2.position.y, Spalte2.width, Spalte2.height);
						GUI_ZoD.Label ("", 11, Spalte3);
			
						// Atkuelles Level
						Zeile = new Rect (Zeile.position.x, Zeile.position.y + Zeile.height, Zeile.width, Zeile.height);
						Spalte1 = new Rect (Zeile.position.x, Zeile.position.y + Zeile.height, Zeile.width, Zeile.height);
						GUI_ZoD.Label ("Level : ", 11, Spalte1);
						Spalte2 = new Rect (Spalte1.position.x + Spalte1.width, Spalte1.position.y, Spalte1.width, Spalte1.height);
						GUI_ZoD.Label ("" + me.Creat.Level, 11, Spalte2);
						Spalte3 = new Rect (Spalte2.position.x + Spalte2.width, Spalte2.position.y, Spalte2.width, Spalte2.height);
						GUI_ZoD.Label ("", 11, Spalte3);
			
						// Atkuelle STR
						Zeile = new Rect (Zeile.position.x, Zeile.position.y + Zeile.height, Zeile.width, Zeile.height);
						Spalte1 = new Rect (Zeile.position.x, Zeile.position.y + Zeile.height, Zeile.width, Zeile.height);
						GUI_ZoD.Label ("Strength : ", 11, Spalte1);
						Spalte2 = new Rect (Spalte1.position.x + Spalte1.width, Spalte1.position.y, Spalte1.width, Spalte1.height);
						GUI_ZoD.Label ("" + me.Creat.InitalStats.Str, 11, Spalte2);
						Spalte3 = new Rect (Spalte2.position.x + Spalte2.width, Spalte2.position.y, Spalte2.width, Spalte2.height);
						if (GUI_ZoD.Button_Text ("+", 11, Spalte3)) {
								me.Creat.InitalStats.Str += 5;
								me.Creat.StatPoints--;
						}
			
						// Atkuelle AGI
						Zeile = new Rect (Zeile.position.x, Zeile.position.y + Zeile.height, Zeile.width, Zeile.height);
						Spalte1 = new Rect (Zeile.position.x, Zeile.position.y + Zeile.height, Zeile.width, Zeile.height);
						GUI_ZoD.Label ("Agility : ", 11, Spalte1);
						Spalte2 = new Rect (Spalte1.position.x + Spalte1.width, Spalte1.position.y, Spalte1.width, Spalte1.height);
						GUI_ZoD.Label ("" + me.Creat.InitalStats.Agi, 11, Spalte2);
						Spalte3 = new Rect (Spalte2.position.x + Spalte2.width, Spalte2.position.y, Spalte2.width, Spalte2.height);
						if (GUI_ZoD.Button_Text ("+", 11, Spalte3)) {
								me.Creat.InitalStats.Agi += 5;
								me.Creat.StatPoints--;
						}
			
						// Atkuelle Dex
						Zeile = new Rect (Zeile.position.x, Zeile.position.y + Zeile.height, Zeile.width, Zeile.height);
						Spalte1 = new Rect (Zeile.position.x, Zeile.position.y + Zeile.height, Zeile.width, Zeile.height);
						GUI_ZoD.Label ("Dextery : ", 11, Spalte1);
						Spalte2 = new Rect (Spalte1.position.x + Spalte1.width, Spalte1.position.y, Spalte1.width, Spalte1.height);
						GUI_ZoD.Label ("" + me.Creat.InitalStats.Dex, 11, Spalte2);
						Spalte3 = new Rect (Spalte2.position.x + Spalte2.width, Spalte2.position.y, Spalte2.width, Spalte2.height);
						if (GUI_ZoD.Button_Text ("+", 11, Spalte3)) {
								me.Creat.InitalStats.Dex += 5;
								me.Creat.StatPoints--;
						}
			
						// Atkuelle VIT
						Zeile = new Rect (Zeile.position.x, Zeile.position.y + Zeile.height, Zeile.width, Zeile.height);
						Spalte1 = new Rect (Zeile.position.x, Zeile.position.y + Zeile.height, Zeile.width, Zeile.height);
						GUI_ZoD.Label ("Vitality : ", 11, Spalte1);
						Spalte2 = new Rect (Spalte1.position.x + Spalte1.width, Spalte1.position.y, Spalte1.width, Spalte1.height);
						GUI_ZoD.Label ("" + me.Creat.InitalStats.Vit, 11, Spalte2);
						Spalte3 = new Rect (Spalte2.position.x + Spalte2.width, Spalte2.position.y, Spalte2.width, Spalte2.height);
						if (GUI_ZoD.Button_Text ("+", 11, Spalte3)) {
								me.Creat.InitalStats.Vit += 5;
								me.Creat.StatPoints--;
						}
			
						// Atkuelle INT
						Zeile = new Rect (Zeile.position.x, Zeile.position.y + Zeile.height, Zeile.width, Zeile.height);
						Spalte1 = new Rect (Zeile.position.x, Zeile.position.y + Zeile.height, Zeile.width, Zeile.height);
						GUI_ZoD.Label ("Int : ", 11, Spalte1);
						Spalte2 = new Rect (Spalte1.position.x + Spalte1.width, Spalte1.position.y, Spalte1.width, Spalte1.height);
						GUI_ZoD.Label ("" + me.Creat.InitalStats.Int, 11, Spalte2);
						Spalte3 = new Rect (Spalte2.position.x + Spalte2.width, Spalte2.position.y, Spalte2.width, Spalte2.height);
						if (GUI_ZoD.Button_Text ("+", 11, Spalte3)) {
								me.Creat.InitalStats.Int += 5;
								me.Creat.StatPoints--;
						}
			
						// Atkuelle LUC
						Zeile = new Rect (Zeile.position.x, Zeile.position.y + Zeile.height, Zeile.width, Zeile.height);
						Spalte1 = new Rect (Zeile.position.x, Zeile.position.y + Zeile.height, Zeile.width, Zeile.height);
						GUI_ZoD.Label ("Luck : ", 11, Spalte1);
						Spalte2 = new Rect (Spalte1.position.x + Spalte1.width, Spalte1.position.y, Spalte1.width, Spalte1.height);
						GUI_ZoD.Label ("" + me.Creat.InitalStats.Luc, 11, Spalte2);
						Spalte3 = new Rect (Spalte2.position.x + Spalte2.width, Spalte2.position.y, Spalte2.width, Spalte2.height);
						if (GUI_ZoD.Button_Text ("+", 11, Spalte3)) {
								me.Creat.InitalStats.Luc += 5;
								me.Creat.StatPoints--;
						}		
				}
		}
	
		void GUIEquipment () {
				Rect tmp_anzeige = new Rect (1920 / 2 - 800, 1080 / 2 - 400, 1600, 800);
				Rect Spalte;
				Rect Zeile1 = new Rect (0, 0, GUI_Scrollbereich.width, 20);
				Rect AnzeigeScrollView = new Rect (0, 0 + 50, tmp_anzeige.width, tmp_anzeige.height - 40);
				GUI_Scrollbereich.position = new Vector2 (0, 0);
				GUI_Scrollbereich.width = AnzeigeScrollView.width - 50;
		
				if (GUI_Equipment) {
						GUI_ZoD.BeginArea ("Area", tmp_anzeige);
						{
				
								GUI_ZoD.Box ("Equip", 11, new Rect (0, 0, tmp_anzeige.width, tmp_anzeige.height));
								GUILayout.Space (20);								
								if ((GUI_ZoD.Button_Text ("Quit", 11, new Rect (tmp_anzeige.width - 40, 0, 40, 20))) || (Input.GetKey (KeyCode.Escape))) {
										GUI_Equipment = false;
								}
								if (GUI_ZoD.Button_Text ("Swap to Iventory", 11, new Rect (tmp_anzeige.width - 150, 0, 110, 20))) {
										GUI_Inventory = true;
										GUI_Equipment = false;
								}
								Zeile1.position = new Vector2 (Zeile1.position.x, Zeile1.position.y + 20);
				
								foreach (ItemData dieseitem in me.Creat.Equipment) {
					
										GUILayout.BeginHorizontal ();
										Spalte = new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width / 7, Zeile1.height);
										GUI_ZoD.Label (dieseitem.Name, 11, Spalte);
										Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
										switch (dieseitem.Type) {
												case ItemType.weapon_melee:
												case ItemType.weapon_range:
														GUI_ZoD.Label ("Physical Damage: " + dieseitem.PhyAttack, 11, Spalte);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI_ZoD.Label ("Magical Damage: " + dieseitem.MagAttack, 11, Spalte);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI_ZoD.Label ("Durability: " + dieseitem.Durability + " / " + dieseitem.MaxDurability, 11, Spalte);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														break;
												case ItemType.armor_feet:
												case ItemType.armor_hand:
												case ItemType.armor_head:
												case ItemType.armor_leg:
												case ItemType.armor_torso:
														GUI_ZoD.Label ("Physical Defense: " + dieseitem.PhyArmor, 11, Spalte);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI_ZoD.Label ("Magical Defense: " + dieseitem.MagArmor, 11, Spalte);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI_ZoD.Label ("Durability: " + dieseitem.Durability + " / " + dieseitem.MaxDurability, 11, Spalte);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														break;
												case ItemType.accessorie:
												case ItemType.potion:
														GUI_ZoD.Label ("Effect: " + dieseitem.Effect, 11, Spalte);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI_ZoD.Label ("" + dieseitem.EffectType.ToString (), 11, Spalte);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														break;
												case ItemType.utility:
														GUI_ZoD.Label ("Capacity: " + dieseitem.Capacity + " / " + dieseitem.MaxCapacity, 11, Spalte);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI_ZoD.Label ("", 11, Spalte);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														break;
							
										}
					
										GUI_ZoD.Label ("Weight: " + dieseitem.Weigth + " kg", 11, Spalte);
										Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
										if (GUI_ZoD.Button_Text ("Drop", 11, Spalte)) {
												me.Creat.Equipment.Remove (dieseitem);
										}
										Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
										if (GUI_ZoD.Button_Text ("Unequip", 11, Spalte)) {
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
		
		int seite = 0;
		int ausgewaehltesItem = 0;
		public Texture2D GUI_BG;
		public Texture2D GUI_Auswahl;
		void GUIInventory () {
				if (GUI_Inventory) {
						mn.cammove = false;
						Rect tmp_anzeige = new Rect (100, 100, 1720, 980);
						GUI_ZoD.BeginArea (GUI_BG.name, tmp_anzeige);
						{
								Rect BB = new Rect (0, 0, tmp_anzeige.width, 100);
								Rect KB = new Rect (BB.position.x, BB.position.y + BB.height, 200, tmp_anzeige.height - BB.height);
								Rect IB = new Rect (tmp_anzeige.width - KB.width, KB.position.y, KB.width, KB.height);
								Rect NB = new Rect (KB.position.x + KB.width, tmp_anzeige.height - 200, tmp_anzeige.width - KB.width - IB.width, 200);
								Rect ITB = new Rect (NB.position.x, KB.position.y, NB.width, KB.height - NB.height);
				
								int Anzahl_Zeilen = 1;
								int Anzahl_Spalten = 1;
								Rect Zeile = new Rect ();
								Rect Spalte = new Rect ();
				
								GUI_ZoD.BeginArea (GUI_Auswahl.name, BB); // ButtonBereich
								{
										Anzahl_Zeilen = 1;
										Anzahl_Spalten = 7;
					
										Zeile = new Rect (0, 0, BB.width, BB.height / Anzahl_Zeilen);
										Spalte = new Rect (Zeile.position.x, Zeile.position.y, Zeile.width / Anzahl_Spalten, Zeile.height);
					
										if ((GUI_ZoD.Button_Text ("Back", 11, Spalte)) || (Input.GetKey (KeyCode.Escape))) {
												GUI_Inventory = false;
										}
										Spalte.position = new Vector2 (Spalte.position.x + Spalte.width, Spalte.position.y);
										if (GUI_ZoD.Button_Text ("Inventory", 11, Spalte)) {
												GUI_Inventory = true;
												GUI_Equipment = false;
										}	
										Spalte.position = new Vector2 (Spalte.position.x + Spalte.width, Spalte.position.y);
										if (GUI_ZoD.Button_Text ("Equipment", 11, Spalte)) {
												GUI_Inventory = false;
												GUI_Equipment = true;
										}
										Spalte.position = new Vector2 (Spalte.position.x + Spalte.width, Spalte.position.y);
										if (GUI_ZoD.Button_Text ("Skill", 11, Spalte)) {
												GUI_Inventory = false;
												GUI_Character = true;
										}
										Spalte.position = new Vector2 (Spalte.position.x + Spalte.width, Spalte.position.y);
										// Platzhalter
										Spalte.position = new Vector2 (Spalte.position.x + Spalte.width, Spalte.position.y);
										GUI_ZoD.Label ("Gold: " + me.Creat.Gold, 11, Spalte);
										Spalte.position = new Vector2 (Spalte.position.x + Spalte.width, Spalte.position.y);
										GUI_ZoD.Label ("Weight: " + me.GGewicht + " / " + me.MaxGGewicht, 11, Spalte);
								}
								GUI_ZoD.EndBackground ();
				
								GUI_ZoD.BeginArea ("KategorieBereich", KB);
								{
										Anzahl_Zeilen = 6;
										Anzahl_Spalten = 1;
					
										Zeile = new Rect (0, 0, KB.width, KB.height / Anzahl_Zeilen);
										Spalte = new Rect (Zeile.position.x, Zeile.position.y, Zeile.width / Anzahl_Spalten, Zeile.height);
										
										if (GUI_ZoD.Button_Text ("Meleeweapons", 11, Spalte)) {
												GUI_Anzeige_Kat = 1;
										}
										Spalte.position = new Vector2 (Spalte.position.x, Spalte.position.y + Spalte.height);
										if (GUI_ZoD.Button_Text ("Rangeweapons", 11, Spalte)) {
												GUI_Anzeige_Kat = 2;
										}
										Spalte.position = new Vector2 (Spalte.position.x, Spalte.position.y + Spalte.height);
										if (GUI_ZoD.Button_Text ("Armor", 11, Spalte)) {
												GUI_Anzeige_Kat = 3;
										}
										Spalte.position = new Vector2 (Spalte.position.x, Spalte.position.y + Spalte.height);
										if (GUI_ZoD.Button_Text ("Potions", 11, Spalte)) {
												GUI_Anzeige_Kat = 4;
										}
										Spalte.position = new Vector2 (Spalte.position.x, Spalte.position.y + Spalte.height);
										if (GUI_ZoD.Button_Text ("Ammo", 11, Spalte)) {
												GUI_Anzeige_Kat = 5;
										}
										Spalte.position = new Vector2 (Spalte.position.x, Spalte.position.y + Spalte.height);
										if (GUI_ZoD.Button_Text ("Wearables", 11, Spalte)) {
												GUI_Anzeige_Kat = 6;
										}
								}
								GUI_ZoD.EndArea ();
				
								GUI_ZoD.BeginArea ("ItemBereich", ITB);
								{
										float Bilder_Q = 150;
										float Bilder_Abstand = 5;
										Anzahl_Zeilen = (int)(ITB.width / (Bilder_Q + Bilder_Abstand));
										Anzahl_Spalten = (int)(ITB.height / (Bilder_Q + Bilder_Abstand));
					
										Zeile = new Rect (0, 0, ITB.width, ITB.height / Anzahl_Zeilen);
										Spalte = new Rect (Zeile.position.x, Zeile.position.y, Zeile.width / Anzahl_Spalten, Zeile.height);
					
										int shown_on_page = Anzahl_Zeilen * Anzahl_Spalten;
										ausgewaehltesItem = 0;
										
										Spalte.width = Bilder_Q;
										Spalte.height = Bilder_Q;
					
										int tmp_count = 0;
										Texture2D Icon;
				
										for (int i=0; i<me.Creat.Inventory.Count; i++) {
												tmp_count++;
												ItemData dieseitem = me.Creat.Inventory [i];
												bool tmp_should_anzeige = false;
												switch (GUI_Anzeige_Kat) {
														case 0:
																break;
														case 1:
																if (dieseitem.Type == ItemType.weapon_melee) {
																		tmp_should_anzeige = true;	
																}
																break;
														case 2:
																if (dieseitem.Type == ItemType.weapon_range) {
																		tmp_should_anzeige = true;	
																}
																break;
														case 3:
																if ((dieseitem.Type == ItemType.armor_feet) || 
																		(dieseitem.Type == ItemType.armor_hand) ||
																		(dieseitem.Type == ItemType.armor_head) ||
																		(dieseitem.Type == ItemType.armor_leg) ||
																		(dieseitem.Type == ItemType.armor_torso)) {
																		tmp_should_anzeige = true;
																}
																break;
														case 4:
																if (dieseitem.Type == ItemType.potion) {
																		tmp_should_anzeige = true;
																}
																break;
														case 5:
																if (dieseitem.Type == ItemType.utility) {
																		tmp_should_anzeige = true;
																}
																break;
														case 6:
																if (dieseitem.Type == ItemType.accessorie) {
																		tmp_should_anzeige = true;
																}
																break;
														case 7:
							
																break;
							
												}
												
												if (tmp_should_anzeige && i > seite * shown_on_page && i <= seite * shown_on_page + shown_on_page) {
														Icon = me.Creat.Inventory [i].texture;
														if (GUI_ZoD.Button_Bild (Icon, Spalte)) {
																ausgewaehltesItem = i;
														}
														Spalte.position = new Vector2 (Spalte.position.x + Spalte.width + Bilder_Abstand, Spalte.position.y);
														if (tmp_count == Anzahl_Spalten) {
																Spalte.position = new Vector2 (0, Spalte.position.y + Spalte.height + Bilder_Abstand);
																tmp_count = 0;
														}
												}
										}
										if (GUI_Anzeige_Kat == 7) {
												foreach (ItemData dieseitem2 in me.Creat.Inventory) {
														if (dieseitem2.Type == ItemType.utility) {
																for (int i=0; i<=dieseitem2.Ammo.Count; i++) {
																		tmp_count++;
																		AmmoData dieseitem = dieseitem2.Ammo [i];
																		bool tmp_should_anzeige = true;
					
																		if (tmp_should_anzeige && i > seite * shown_on_page && i <= seite * shown_on_page + shown_on_page) {
																				Icon = me.Creat.Inventory [i].texture;
																				if (GUI_ZoD.Button_Bild (Icon, Spalte)) {
																						ausgewaehltesItem = i;
																				}
																				Spalte.position = new Vector2 (Spalte.position.x + Spalte.width + Bilder_Abstand, Spalte.position.y);
																				if (tmp_count == Anzahl_Spalten) {
																						Spalte.position = new Vector2 (0, Spalte.position.y + Spalte.height + Bilder_Abstand);
																						tmp_count = 0;
																				}
																		}
																}
														}
												}
										}
								}
								GUI_ZoD.EndArea ();
				
								GUI_ZoD.BeginArea ("NavigationsBereich", NB);
								{
										Anzahl_Zeilen = 1;
										Anzahl_Spalten = 2;
					
										Zeile = new Rect (0, 0, NB.width, NB.height / Anzahl_Zeilen);
										Spalte = new Rect (Zeile.position.x, Zeile.position.y, Zeile.width / Anzahl_Spalten, Zeile.height);
					
										GUI_ZoD.Button_Rahmen_weg ();
										if (GUI_ZoD.Button_Text ("DOWN", 11, Spalte)) {
												seite++;
										}
										Spalte.position = new Vector2 (Spalte.position.x + Spalte.width, Spalte.position.y);
										if (GUI_ZoD.Button_Text ("UP", 11, Spalte)) {
												seite--;
										}
										Spalte.position = new Vector2 (Spalte.position.x + Spalte.width, Spalte.position.y);
										GUI_ZoD.Button_Rahmen_hin ();
								}
								GUI_ZoD.EndArea ();
								
								GUI_ZoD.BeginArea ("InfoBereich", IB);
								{
					
										Anzahl_Zeilen = 8;
										Anzahl_Spalten = 1;
					
										Zeile = new Rect (0, 0, IB.width, IB.height / Anzahl_Zeilen);
										Spalte = new Rect (Zeile.position.x, Zeile.position.y, Zeile.width / Anzahl_Spalten, Zeile.height);
					
										if (me.Creat.Inventory.Count >= 1) {
												ItemData Anzeige_Item;
												Anzeige_Item = me.Creat.Inventory [ausgewaehltesItem];
						
												GUI_ZoD.Label (me.Creat.Inventory [ausgewaehltesItem].Name, 11, Spalte);
												Spalte.position = new Vector2 (Spalte.position.x, Spalte.position.y + Spalte.height);
												switch (Anzeige_Item.Type) {
														case ItemType.weapon_melee:
														case ItemType.weapon_range:
																Spalte.position = new Vector2 (Spalte.position.x, Spalte.position.y + Spalte.height);
																GUI_ZoD.Label ("Physical Damage: " + Anzeige_Item.PhyAttack, 11, Spalte);
																Spalte.position = new Vector2 (Spalte.position.x, Spalte.position.y + Spalte.height);
																GUI_ZoD.Label ("Magical Damage: " + Anzeige_Item.MagAttack, 11, Spalte);
																Spalte.position = new Vector2 (Spalte.position.x, Spalte.position.y + Spalte.height);
																GUI_ZoD.Label ("Durability: " + Anzeige_Item.Durability + " / " + Anzeige_Item.MaxDurability, 11, Spalte);
																break;
														case ItemType.armor_feet:
														case ItemType.armor_hand:
														case ItemType.armor_head:
														case ItemType.armor_leg:
														case ItemType.armor_torso:
																Spalte.position = new Vector2 (Spalte.position.x, Spalte.position.y + Spalte.height);
																GUI_ZoD.Label ("Physical Defense: " + Anzeige_Item.PhyArmor, 11, Spalte);
																Spalte.position = new Vector2 (Spalte.position.x, Spalte.position.y + Spalte.height);
																GUI_ZoD.Label ("Magical Defense: " + Anzeige_Item.MagArmor, 11, Spalte);
																Spalte.position = new Vector2 (Spalte.position.x, Spalte.position.y + Spalte.height);
																GUI_ZoD.Label ("Durability: " + Anzeige_Item.Durability + " / " + Anzeige_Item.MaxDurability, 11, Spalte);
																break;
														case ItemType.potion:
														case ItemType.accessorie:
														case ItemType.utility:
																Spalte.position = new Vector2 (Spalte.position.x, Spalte.position.y + Spalte.height);
																GUI_ZoD.Label ("Effect: + " + Anzeige_Item.Effect + " " + Anzeige_Item.EffectType.ToString (), 11, Spalte);
																break;
												}
			
												Spalte.position = new Vector2 (Spalte.position.x, Spalte.position.y + Spalte.height);
												GUI_ZoD.Label ("" + me.Creat.Inventory [ausgewaehltesItem].Weigth, 11, Spalte);
												Spalte.position = new Vector2 (Spalte.position.x, Spalte.position.y + Spalte.height);
												GUI_ZoD.Label ("" + me.Creat.Inventory [ausgewaehltesItem].Gold, 11, Spalte);
												Spalte.position = new Vector2 (Spalte.position.x, Spalte.position.y + Spalte.height);
												if (GUI_ZoD.Button_Text ("Equip", 11, Spalte)) {
														me.Equip (me.Creat.Inventory [ausgewaehltesItem]);
												}
												Spalte.position = new Vector2 (Spalte.position.x, Spalte.position.y + Spalte.height);
												if (GUI_ZoD.Button_Text ("Equip", 11, Spalte)) {
														me.Creat.Inventory.RemoveAt (ausgewaehltesItem);
												}
												Spalte.position = new Vector2 (Spalte.position.x, Spalte.position.y + Spalte.height);
			
												switch (me.Creat.Inventory [ausgewaehltesItem].Type) {
														case ItemType.utility:
																if (GUI_ZoD.Button_Text ("Show content.", 11, Spalte)) {
																		GUI_Anzeige_Kat = 7;
																}
																break;
														case ItemType.potion:
																if (GUI_ZoD.Button_Text ("Use", 11, Spalte)) {
																		ItemUse (me.Creat.Inventory [ausgewaehltesItem]);
																}
																break;
												}
										}
								}
								GUI_ZoD.EndArea ();
						}
						GUI_ZoD.EndBackground ();
				} else {
						mn.cammove = true;
				}
		}
		
	
		void GUIJournal () {
				if (GUI_journal) {
						Rect tmp_anzeige = new Rect (1920 / 2 - 500, 1080 / 2 - 200, 1000, 400);
						GUI_ZoD.Box ("Journal", 11, tmp_anzeige);
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
			
						if (GUI_ZoD.Button_Text ("Open", 11, new Rect (1920 / 2 - 500 + 075, 1080 / 2 - 175, 250, 20))) {
								GUI_journal_kat = 0;
						}
						if (GUI_ZoD.Button_Text ("Finished", 11, new Rect (1920 / 2 - 500 + 375, 1080 / 2 - 175, 250, 20))) {
								GUI_journal_kat = 1;
						}
						if (GUI_ZoD.Button_Text ("Failed", 11, new Rect (1920 / 2 - 500 + 675, 1080 / 2 - 175, 250, 20))) {
								GUI_journal_kat = 2;
						}
						Rect Zeile = new Rect (1920 / 2 - 500 + 075, 1080 / 2 - 175 + 25, 250, 20);
						foreach (QuestStruct tmp_q in tmp_quests) {
								GUI_ZoD.Label ("<b>" + tmp_q.Name + "</b>", 11, Zeile);
								if (tmp_q.EnemyTokill.Count > 0) {
										foreach (EnemyTokillStruct etk in tmp_q.EnemyTokill) {
												Zeile.position = new Vector2 (Zeile.position.x, Zeile.position.y + 25);
												GUI_ZoD.Label ("Kill " + etk.Amount + " x " + etk.Name, 11, Zeile);
										}
								}
								if (tmp_q.ItemsToCollect.Count > 0) {
										foreach (ItemsToCollectStruct itc in tmp_q.ItemsToCollect) {
												Zeile.position = new Vector2 (Zeile.position.x, Zeile.position.y + 25);
												GUI_ZoD.Label ("Collect " + itc.Amount + " x " + itc.Name, 11, Zeile);
										}
								}
								if (tmp_q.NPCToTalk.Count > 0) {
										foreach (string ntt in tmp_q.NPCToTalk) {
												Zeile.position = new Vector2 (Zeile.position.x, Zeile.position.y + 25);
												GUI_ZoD.Label ("Talk to " + ntt, 11, Zeile);
										}
								}
								if (!tmp_q.finished) {
										Zeile.position = new Vector2 (Zeile.position.x, Zeile.position.y + 25);
										GUI_ZoD.Label ("Return to " + tmp_q.NPC_Geber, 11, Zeile);
								}
								Zeile.position = new Vector2 (Zeile.position.x, Zeile.position.y + 25);
						}
				}		
		}
	
		void GUIHotbar () {
				int höhe = 70;
				int breite = 1920 - 100;
				if (GUI_Hotbar) {
						Rect Zeile = new Rect (50, 1080 - höhe, breite, höhe);
						Rect Spalte = new Rect (50, 1080 - höhe, breite / 10, höhe);
						GUI_ZoD.Box ("", 11, Zeile);
						for (int i=0; i<10; i++) {
								if (skill_active == i + 1) {
										GUI_ZoD.DrawTexture (skillactiv, Spalte);
								} else {
										GUI_ZoD.DrawTexture (skillinactiv, Spalte);
								}
								GUI_ZoD.DrawTexture (SlotTexture [i], Spalte);
								Spalte.position = new Vector2 (Spalte.position.x + Spalte.width, Spalte.position.y);
						}
				}
		}
	
		void BarBerechnung () {
				if (me.Creat.MaxHP > 0) {
						hpbar = (100 / (me.Creat.MaxHP + 0.00001f)) * (me.Creat.HP + 0.0001f);
						manabar = (100 / (0.00001f + me.Creat.MaxMP)) * (me.Creat.MP + 0.00001f);
						xpbar = (100 / (100 * ((0.00001f + me.Creat.Level) + 1))) * (me.Creat.XP + 0.00001f);
						staminabar = (100 / (0.00001f + me.Creat.MaxStamina)) * (me.Creat.Stamina + 0.00001f);
				}
		}
	
		void GUIStatsOverview () {
				BarBerechnung ();
				GUI_ZoD.Label ("Gold: " + me.Creat.Gold + "G", 11, new Rect (5, 1080 - 30, 170, 20));
				GUI_ZoD.Label ("Battlemode: " + me.Creat.Stance.ToString (), 11, new Rect (5, 1080 - 55, 170, 20));
		
				// Quiver Inhalt
				ItemData Quiver = new ItemData ();
				if (me.Creat.Equipment.Count > 0) {
						foreach (ItemData c_obj in me.Creat.Equipment) {
								if (c_obj.Type == ItemType.utility) {
										Quiver = c_obj;
								}
						}
				}
				
				if (Quiver.Name != "" && Quiver.Name != null) {
						GUI_ZoD.Label ("Cap: " + Quiver.Capacity + " / " + Quiver.MaxCapacity, 11, new Rect (5, 1080 - 80, 170, 20));
				}
		
				//HPBAR
				Rect Bar_Pos = new Rect (5, 5, (HpBar_empty.width / 4), (HpBar_empty.height / 4));
				GUI_ZoD.DrawTexture (HpBar_empty, Bar_Pos);
				GUI_ZoD.DrawTexture (HpBar_full, new Rect (Bar_Pos.position.x, Bar_Pos.position.y, Bar_Pos.width * hpbar / 100, Bar_Pos.height));
				//MANABAR
				Bar_Pos = new Rect (Bar_Pos.position.x, Bar_Pos.position.y + Bar_Pos.height + 5, Bar_Pos.width, Bar_Pos.height);
				GUI_ZoD.DrawTexture (ManaBar_empty, Bar_Pos);
				GUI_ZoD.DrawTexture (ManaBar_full, new Rect (Bar_Pos.position.x, Bar_Pos.position.y, Bar_Pos.width * manabar / 100, Bar_Pos.height));
				//STAMINABAR
				Bar_Pos = new Rect (Bar_Pos.position.x, Bar_Pos.position.y + Bar_Pos.height + 5, Bar_Pos.width, Bar_Pos.height);
				GUI_ZoD.DrawTexture (Staminabar_empty, Bar_Pos);
				GUI_ZoD.DrawTexture (Staminabar_full, new Rect (Bar_Pos.position.x, Bar_Pos.position.y, Bar_Pos.width * staminabar / 100, Bar_Pos.height));
				//XPBAR
				Bar_Pos = new Rect (Bar_Pos.position.x, Bar_Pos.position.y + Bar_Pos.height + 5, Bar_Pos.width, Bar_Pos.height);
				GUI_ZoD.DrawTexture (XpBar_empty, Bar_Pos);
				GUI_ZoD.DrawTexture (XpBar_full, new Rect (Bar_Pos.position.x, Bar_Pos.position.y, Bar_Pos.width * xpbar / 100, Bar_Pos.height));
		
				
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
								Pos = new Rect (1920 - tmp_pos_y, 1080 - tmp_pos_x, 100, tmp_lineheigth);
								GUI_ZoD.Label (tmpnot.message, 11, Pos);
								
						} else {
								PickupList.RemoveAt (i);
						}
				}
		}
	
	
		void GUIBuff () {
				float tmp_pos_x = 0f;
				float tmp_pos_y = 200;
				float tmp_lineheigth = 20;
				Rect Pos;
				for (int i =0; i<me.Creat.StatusEffects.Count; i++) {
						tmp_pos_x = tmp_pos_x + tmp_lineheigth;
						Pos = new Rect (1920 - tmp_pos_y, 0 + tmp_pos_x, tmp_pos_y, tmp_lineheigth);
						GUI_ZoD.Label (me.Creat.StatusEffects [i].name + ": " + Mathf.Round (me.Creat.StatusEffects [i].duration * 100) / 100, 11, Pos);
				}
		}
	
		string[] newKey = new string[0];
		public List<SkillAndKeys> skillkeys = new List<SkillAndKeys> ();
	
		void GUICharacter () {
		
				if (GUI_Character) {
			
						Rect tmp_anzeige = new Rect (1920 / 2 - 500, 1080 / 2 - 200, 1000, 400);
						Rect zeile = new Rect (tmp_anzeige.position.x, tmp_anzeige.position.y, tmp_anzeige.width - 500, 20);
						GUI_ZoD.Box ("Character", 11, tmp_anzeige);
						for (int i =0; i<me.Creat.Skills.Count; i++) {
								skill name = me.Creat.Skills [i];
				
								zeile.position = new Vector2 (tmp_anzeige.position.x, zeile.position.y + zeile.height);
								GUI_ZoD.Label (name.name + " | increases: " + name.Effect [0] + " | cost: " + name.cost, 11, zeile);
								zeile.position = new Vector2 (zeile.position.x + 300, zeile.position.y);
								zeile.width = 50;
								newKey [i] = GUI_ZoD.TextField (newKey [i], 11, zeile);
								zeile.position = new Vector2 (zeile.position.x + 60, zeile.position.y);
								zeile.width = 200;
								if (GUI_ZoD.Button_Text ("Assign skill to key!", 11, zeile)) {
										SkillAndKeys tmp_obj = new SkillAndKeys ();
										tmp_obj.key = newKey [i];
										tmp_obj.action = name.name;
										for (int j = 0; j<skillkeys.Count; j++) {
												if (skillkeys [j].key == newKey [i]) {
														skillkeys.RemoveAt (i);
												}
										}
										skillkeys.Add (tmp_obj);
								}
								zeile.width = tmp_anzeige.width - 500;
						}	
			
				} else {
						newKey = new string[me.Creat.Skills.Count];
						for (int i =0; i<me.Creat.Skills.Count; i++) {
								newKey [i] = "";
								for (int j=0; j<skillkeys.Count; j++) {
										if (skillkeys [j].action == me.Creat.Skills [i].name) {
												newKey [i] = skillkeys [j].key;
										}
								}
						}
				}
	
		}
}
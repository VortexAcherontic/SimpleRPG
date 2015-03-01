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
		public QuestController qc;
		public mainmenu mn;
		item ItemScript;	
	
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
		//Vector2 GUI_Scroller = new Vector2 ();
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
	
		Rect tmp_anzeige = new Rect (200, 200, 1520, 780);
	
		public Texture2D CharPic;
		int ausgewaehltesItem = 0;
		int ausgewaehltesTool = 0;
		int seite = 0;
		List<ItemData> ShowItems = new List<ItemData> ();
		List<AmmoData> ShowAmmo = new List<AmmoData> ();
	
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
				qc = gameObject.GetComponent<QuestController> ();
				mn = GameObject.Find ("Uebergabe").GetComponent<mainmenu> ();
				ItemScript = GameObject.Find ("Uebergabe").GetComponent<item> ();
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
	
		public void ItemUse (ItemData UsedItem) {
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
						
						if (sk.Name == Skillname) {
								foreach (Status st in me.alleStatus) {
										Status tmpst = st;
										if (st.Name == sk.Effect [0]) {
												if (me.Creat.MP >= sk.ManaCost) {
														me.Creat.MP -= sk.ManaCost;
														for (int i = 0; i<me.Creat.StatusEffects.Count; i++) {
																if (me.Creat.StatusEffects [i].Name == st.Name) {
																		me.Creat.StatusEffects.RemoveAt (i);
																}
														}
														tmpst.Level = sk.Level;
														me.Creat.StatusEffects.Add (tmpst);
												}
										}
								}
						}
				}
		}
	
		public void SkillLearn (skill s) {
				Debug.Log (s.Name);
				bool hatschon = false;
				int count_skills = 0;
				foreach (skill name in me.Creat.Skills) {
						skill tmps = name;
						if (tmps.Name == s.Name) {
								hatschon = true;
								tmps.Level += 1;
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
				if (GUI_Equipment) {
						mn.cammove = false;
						GUI_ZoD.BeginArea ("GUI_BG.name", tmp_anzeige);
						{
								Rect BB = new Rect (0, 0, tmp_anzeige.width, 100);
								Rect CS = new Rect (BB.position.x, BB.position.y + BB.height, 300, (tmp_anzeige.height - BB.height));
								Rect EQi = new Rect (BB.width - CS.width, CS.position.y, CS.width, CS.height);
								Rect RestFenster = new Rect (CS.position.x + CS.width, CS.position.y, tmp_anzeige.width - CS.width - EQi.width, tmp_anzeige.height - BB.height);
								
				
								int Anzahl_Zeilen = 1;
								int Anzahl_Spalten = 1;
								Rect Zeile = new Rect ();
								Rect Spalte = new Rect ();
				
								GUI_ZoD.BeginArea ("GUI_Auswahl.name", BB); // ButtonBereich
								{
										Anzahl_Zeilen = 1;
										Anzahl_Spalten = 7;
					
										Zeile = new Rect (0, 0, BB.width, BB.height / Anzahl_Zeilen);
										Spalte = new Rect (Zeile.position.x, Zeile.position.y, Zeile.width / Anzahl_Spalten, Zeile.height);
					
										if ((GUI_ZoD.Button_Text ("Back", 7, Spalte)) || (Input.GetKey (KeyCode.Escape))) {
												GUI_Inventory = false;
										}
										Spalte.position = new Vector2 (Spalte.position.x + Spalte.width, Spalte.position.y);
										if (GUI_ZoD.Button_Text ("Inventory", 7, Spalte)) {
												GUI_Inventory = true;
												GUI_Equipment = false;
										}	
										Spalte.position = new Vector2 (Spalte.position.x + Spalte.width, Spalte.position.y);
										if (GUI_ZoD.Button_Text ("Equipment", 7, Spalte)) {
												GUI_Inventory = false;
												GUI_Equipment = true;
										}
										Spalte.position = new Vector2 (Spalte.position.x + Spalte.width, Spalte.position.y);
										if (GUI_ZoD.Button_Text ("Skill", 7, Spalte)) {
												GUI_Inventory = false;
												GUI_Character = true;
										}
										Spalte.position = new Vector2 (Spalte.position.x + Spalte.width, Spalte.position.y);
										// Platzhalter
										Spalte.position = new Vector2 (Spalte.position.x + Spalte.width, Spalte.position.y);
										GUI_ZoD.Label ("Gold: " + me.Creat.Gold, 5, Spalte);
										Spalte.position = new Vector2 (Spalte.position.x + Spalte.width, Spalte.position.y);
										GUI_ZoD.Label ("Weight: " + me.GGewicht + " / " + me.MaxGGewicht, 5, Spalte);
								}
								GUI_ZoD.EndBackground ();
				
								//Characterbereich
								//9+x Equipslots
								// 4 links 1 helm 4 rechts und tools unterm character
								GUI_ZoD.BeginArea ("Equipment Interface", RestFenster);
								{
										Rect Button = new Rect (0, 0, RestFenster.width, RestFenster.height);
										ShowItems = new List<ItemData> ();
										// 3/5 Equip
										// 2/5 Tools
										Rect R_Equip = new Rect (5, 5, Button.width - 5, (Button.height - 5) / 5 * 3);
										Rect R_Tools = new Rect (R_Equip.position.x, R_Equip.position.y + R_Equip.height, R_Equip.width, (Button.height - 5) / 5 * 2);
					
										// Equip in 
										// 1/5 Items
										// 3/5 Char Bild
										// 1/5 Items
										Rect RE_Items1 = new Rect (R_Equip.position.x, R_Equip.position.y, R_Equip.width / 5, R_Equip.height);
										Rect RE_Char = new Rect (RE_Items1.position.x + RE_Items1.width, RE_Items1.position.y, RE_Items1.width * 3, RE_Items1.height);
										Rect RE_Items2 = new Rect (RE_Char.position.x + RE_Char.width, RE_Items1.position.y, RE_Items1.width, RE_Items1.height);
					
										// Nun erstmal alles Equip durchgehen was kein Tool ist!
										Rect ItemLeft = new Rect (RE_Items1.position.x, RE_Items1.position.y, RE_Items1.width, RE_Items1.height / 5);
										Rect ItemRight = new Rect (RE_Items2.position.x, RE_Items2.position.y, RE_Items2.width, RE_Items2.height / 5);
										for (int eqcount=0; eqcount<me.Creat.Equipment.Count; eqcount++) {
												ItemData tmpeq = me.Creat.Equipment [eqcount];
												if (tmpeq.Type == ItemType.armor_head) {
														int tmp_pos = 0;
														if (GUI_ZoD.Button_Bild (tmpeq.texture, new Rect (ItemLeft.position.x, ItemLeft.position.y + (tmp_pos * ItemLeft.height), ItemLeft.width, ItemLeft.height))) {
																ausgewaehltesItem = eqcount;
														}
												}
												if (tmpeq.Type == ItemType.armor_torso) {
														int tmp_pos = 1;
														if (GUI_ZoD.Button_Bild (tmpeq.texture, new Rect (ItemLeft.position.x, ItemLeft.position.y + (tmp_pos * ItemLeft.height), ItemLeft.width, ItemLeft.height))) {
																ausgewaehltesItem = eqcount;
														}
												}
												if (tmpeq.Type == ItemType.armor_hand) {
														int tmp_pos = 2;
														if (GUI_ZoD.Button_Bild (tmpeq.texture, new Rect (ItemLeft.position.x, ItemLeft.position.y + (tmp_pos * ItemLeft.height), ItemLeft.width, ItemLeft.height))) {
																ausgewaehltesItem = eqcount;
														}
												}
												if (tmpeq.Type == ItemType.armor_leg) {
														int tmp_pos = 3;
														if (GUI_ZoD.Button_Bild (tmpeq.texture, new Rect (ItemLeft.position.x, ItemLeft.position.y + (tmp_pos * ItemLeft.height), ItemLeft.width, ItemLeft.height))) {
																ausgewaehltesItem = eqcount;
														}
												}
												if (tmpeq.Type == ItemType.armor_feet) {
														int tmp_pos = 4;
														if (GUI_ZoD.Button_Bild (tmpeq.texture, new Rect (ItemLeft.position.x, ItemLeft.position.y + (tmp_pos * ItemLeft.height), ItemLeft.width, ItemLeft.height))) {
																ausgewaehltesItem = eqcount;
														}
												}
												if (tmpeq.Type == ItemType.weapon_melee) {
														int tmp_pos = 0;
														if (GUI_ZoD.Button_Bild (tmpeq.texture, new Rect (ItemRight.position.x, ItemRight.position.y + (tmp_pos * ItemRight.height), ItemRight.width, ItemRight.height))) {
																ausgewaehltesItem = eqcount;
														}
												}
												if (tmpeq.Type == ItemType.weapon_range) {
														int tmp_pos = 1;
														if (GUI_ZoD.Button_Bild (tmpeq.texture, new Rect (ItemRight.position.x, ItemRight.position.y + (tmp_pos * ItemRight.height), ItemRight.width, ItemRight.height))) {
																ausgewaehltesItem = eqcount;
														}
												}
												if (tmpeq.Type == ItemType.utility) {
														int tmp_pos = 2;
														if (GUI_ZoD.Button_Bild (tmpeq.texture, new Rect (ItemRight.position.x, ItemRight.position.y + (tmp_pos * ItemRight.height), ItemRight.width, ItemRight.height))) {
																ausgewaehltesItem = eqcount;
														}
												}
												if (tmpeq.Type == ItemType.accessorie) {
														int tmp_pos = 3;
														if (GUI_ZoD.Button_Bild (tmpeq.texture, new Rect (ItemRight.position.x, ItemRight.position.y + (tmp_pos * ItemRight.height), ItemRight.width, ItemRight.height))) {
																ausgewaehltesItem = eqcount;
														}
												}
												// Noch Leer
												if (tmpeq.Type == ItemType.junk) {
														int tmp_pos = 4;
														if (GUI_ZoD.Button_Bild (tmpeq.texture, new Rect (ItemRight.position.x, ItemRight.position.y + (tmp_pos * ItemRight.height), ItemRight.width, ItemRight.height))) {
																ausgewaehltesItem = eqcount;
														}
												}
						
												switch (tmpeq.Type) {
														case ItemType.tool_gardener:
														case ItemType.tool_herbalist:
														case ItemType.tool_hunter:
														case ItemType.tool_lumberjack:
														case ItemType.tool_miner:
																ShowItems.Add (tmpeq);
																break;
												}
						
										}
										// Tools Anzeigen
										ausgewaehltesTool = ItemScript.GUI_AnzeigeItemGrid (ShowItems, R_Tools, seite, ausgewaehltesTool);
					
					
										// Char Anzegite
										GUI_ZoD.DrawTexture (CharPic, RE_Char);
								}
								GUI_ZoD.EndArea ();
				
								GUI_ZoD.BeginArea ("Char Info", CS);
								{
					
								}
								GUI_ZoD.EndArea ();
								GUI_ZoD.BeginArea ("Equip Info", EQi);
								{
					
										Anzahl_Zeilen = 8;
										Anzahl_Spalten = 1;
					
										Zeile = new Rect (0, 0, EQi.width, EQi.height / Anzahl_Zeilen);
										Spalte = new Rect (Zeile.position.x, Zeile.position.y, Zeile.width / Anzahl_Spalten, Zeile.height);
					
										if (me.Creat.Equipment.Count >= ausgewaehltesItem + 1) {
												ItemData Anzeige_Item;
												Anzeige_Item = me.Creat.Equipment [ausgewaehltesItem];
												int FiktiveEquipmetnID = 0; // Weil ich nicht weiß wies besser geht
												for (int tmp=0; tmp<me.Creat.Equipment.Count; tmp++) {
														if (Anzeige_Item.Name == me.Creat.Equipment [tmp].Name) {
																FiktiveEquipmetnID = tmp;
														}
												}
												ItemScript.GUI_ItemDetails (Anzeige_Item, EQi, this, -1, FiktiveEquipmetnID, -1, false);
										}
								}
								GUI_ZoD.EndArea ();
						}
						GUI_ZoD.EndBackground ();
				} else {
						mn.cammove = true;
				}
		}
	
		void GUIInventory () {
				if (GUI_Inventory) {
						mn.cammove = false;
						GUI_ZoD.BeginArea ("Inventory", tmp_anzeige);
						{
								Rect BB = new Rect (0, 0, tmp_anzeige.width, 100);
								Rect KB = new Rect (BB.position.x, BB.position.y + BB.height, 200, tmp_anzeige.height - BB.height);
								Rect IB = new Rect (tmp_anzeige.width - 300, KB.position.y, 300, KB.height);
								Rect NB = new Rect (KB.position.x + KB.width, tmp_anzeige.height - 200, tmp_anzeige.width - KB.width - IB.width, 200);
								Rect ITB = new Rect (NB.position.x, KB.position.y, NB.width, KB.height - NB.height);
				
								int Anzahl_Zeilen = 1;
								int Anzahl_Spalten = 1;
								Rect Zeile = new Rect ();
								Rect Spalte = new Rect ();
				
								GUI_ZoD.BeginArea ("ButtonBereich", BB); // ButtonBereich
								{
										Anzahl_Zeilen = 1;
										Anzahl_Spalten = 7;
					
										Zeile = new Rect (0, 0, BB.width, BB.height / Anzahl_Zeilen);
										Spalte = new Rect (Zeile.position.x, Zeile.position.y, Zeile.width / Anzahl_Spalten, Zeile.height);
					
										if ((GUI_ZoD.Button_Text ("Back", 7, Spalte)) || (Input.GetKey (KeyCode.Escape))) {
												GUI_Inventory = false;
										}
										Spalte.position = new Vector2 (Spalte.position.x + Spalte.width, Spalte.position.y);
										if (GUI_ZoD.Button_Text ("Inventory", 7, Spalte)) {
												GUI_Inventory = true;
												GUI_Equipment = false;
										}	
										Spalte.position = new Vector2 (Spalte.position.x + Spalte.width, Spalte.position.y);
										if (GUI_ZoD.Button_Text ("Equipment", 7, Spalte)) {
												GUI_Inventory = false;
												GUI_Equipment = true;
										}
										Spalte.position = new Vector2 (Spalte.position.x + Spalte.width, Spalte.position.y);
										if (GUI_ZoD.Button_Text ("Skill", 7, Spalte)) {
												GUI_Inventory = false;
												GUI_Character = true;
										}
										Spalte.position = new Vector2 (Spalte.position.x + Spalte.width, Spalte.position.y);
										// Platzhalter
										Spalte.position = new Vector2 (Spalte.position.x + Spalte.width, Spalte.position.y);
										GUI_ZoD.Label ("Gold: " + me.Creat.Gold, 5, Spalte);
										Spalte.position = new Vector2 (Spalte.position.x + Spalte.width, Spalte.position.y);
										GUI_ZoD.Label ("Weight: " + me.GGewicht + " / " + me.MaxGGewicht, 5, Spalte);
								}
								GUI_ZoD.EndArea ();
				
								GUI_ZoD.BeginArea ("KategorieBereich", KB);
								{
										GUI_Anzeige_Kat = ItemScript.GUI_ItemKat (KB, GUI_Anzeige_Kat);
								}
								GUI_ZoD.EndArea ();
				
								GUI_ZoD.BeginArea ("ItemBereich", ITB);
								{
										ShowItems = new List<ItemData> ();
										ShowAmmo = new List<AmmoData> ();
										
										for (int i=0; i<me.Creat.Inventory.Count; i++) {
												ItemData dieseitem = me.Creat.Inventory [i];
												bool tmp_should_anzeige = ItemScript.Check_ItemTypeInKat (dieseitem.Type, GUI_Anzeige_Kat);
												if (tmp_should_anzeige) {
														ShowItems.Add (dieseitem);
												}
										}
										foreach (ItemData dieseitem2 in me.Creat.Inventory) {
												if (dieseitem2.Type == ItemType.utility) {
														for (int i=0; i<dieseitem2.Ammo.Count; i++) {
																AmmoData dieseitem = dieseitem2.Ammo [i];
																bool tmp_should_anzeige = ItemScript.Check_ItemTypeInKat (dieseitem.Type, GUI_Anzeige_Kat);
																if (tmp_should_anzeige) {
																		ShowAmmo.Add (dieseitem);
																}
														}
												}
										}
										if (ItemScript.Check_ItemTypeInKat (ItemType.weapon_ammo, GUI_Anzeige_Kat)) {
												ausgewaehltesItem = ItemScript.GUI_AnzeigeItemGrid (ShowItems, ITB, seite, ausgewaehltesItem);
										} else {
												ausgewaehltesItem = ItemScript.GUI_AnzeigeItemGrid (ShowItems, ITB, seite, ausgewaehltesItem);
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
										if (GUI_ZoD.Button_Text ("DOWN", 7, Spalte)) {
												seite++;
										}
										Spalte.position = new Vector2 (Spalte.position.x + Spalte.width, Spalte.position.y);
										if (GUI_ZoD.Button_Text ("UP", 7, Spalte)) {
												seite--;
										}
										Spalte.position = new Vector2 (Spalte.position.x + Spalte.width, Spalte.position.y);
										GUI_ZoD.Button_Rahmen_hin ();
								}
								GUI_ZoD.EndArea ();
								
								GUI_ZoD.BeginArea ("InfoBereich", IB);
								{
										if (ShowItems.Count >= ausgewaehltesItem + 1) {
												ItemData Anzeige_Item;
												Anzeige_Item = ShowItems [ausgewaehltesItem];
												int FiktiveInventoryID = 0; // Weil ich nicht weiß wies besser geht
												for (int tmp=0; tmp<me.Creat.Inventory.Count; tmp++) {
														if (Anzeige_Item.Name == me.Creat.Inventory [tmp].Name) {
																FiktiveInventoryID = tmp;
														}
												}
												ItemScript.GUI_ItemDetails (Anzeige_Item, IB, this, FiktiveInventoryID, -1, -1, false);
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
						GUI_ZoD.Label (me.Creat.StatusEffects [i].Name + ": " + Mathf.Round (me.Creat.StatusEffects [i].Duration * 100) / 100, 11, Pos);
				}
		}
	
		string[] newKey = new string[0];
		public List<SkillAndKeys> skillkeys = new List<SkillAndKeys> ();
	
		void GUICharacter () {
		
				if (GUI_Character) {
			
						Rect zeile = new Rect (tmp_anzeige.position.x, tmp_anzeige.position.y, tmp_anzeige.width - 500, 20);
						GUI_ZoD.Box ("Character", 11, tmp_anzeige);
						for (int i =0; i<me.Creat.Skills.Count; i++) {
								skill name = me.Creat.Skills [i];
				
								zeile.position = new Vector2 (tmp_anzeige.position.x, zeile.position.y + zeile.height);
								GUI_ZoD.Label (name.Name + " | increases: " + name.Effect [0] + " | cost: " + name.ManaCost, 11, zeile);
								zeile.position = new Vector2 (zeile.position.x + 300, zeile.position.y);
								zeile.width = 50;
								newKey [i] = GUI_ZoD.TextField (newKey [i], 11, zeile);
								zeile.position = new Vector2 (zeile.position.x + 60, zeile.position.y);
								zeile.width = 200;
								if (GUI_ZoD.Button_Text ("Assign skill to key!", 11, zeile)) {
										SkillAndKeys tmp_obj = new SkillAndKeys ();
										tmp_obj.key = newKey [i];
										tmp_obj.action = name.Name;
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
										if (skillkeys [j].action == me.Creat.Skills [i].Name) {
												newKey [i] = skillkeys [j].key;
										}
								}
						}
				}
	
		}
}
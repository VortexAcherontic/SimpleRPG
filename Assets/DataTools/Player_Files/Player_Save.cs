using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using DataManager;
using Boomlagoon.JSON;


public class Player_Save : MonoBehaviour {
		// Erstmal nur Lokal Game!
		// Klappt erstmal nur in UNITY!
		
		private Server _server_ = new Server ();
		public string logintext;
		bool isLogin = false;
		bool IsLoading = false;
		int id;
	
		public void Save () {
				_server_.data.Clear ();
		
				Savegame Game = ScriptableObject.CreateInstance<Savegame> ();
		
				#region Char
				Game.Creature = gameObject.GetComponent<PlayerBehaviour> ().me.Creat;
				JSONObject characterObj = new JSONObject ();
		
				characterObj.Add ("position", _server_.MakeVector3 (Game.Creature.Position));
				characterObj.Add ("gold", Game.Creature.Gold);
				characterObj.Add ("xp", Game.Creature.XP);
				
				characterObj.Add ("Str", Game.Creature.InitalStats.Str);
				characterObj.Add ("Dex", Game.Creature.InitalStats.Dex);
				characterObj.Add ("Agi", Game.Creature.InitalStats.Agi);
				characterObj.Add ("Int", Game.Creature.InitalStats.Int);
				characterObj.Add ("Vit", Game.Creature.InitalStats.Vit);
				characterObj.Add ("Luc", Game.Creature.InitalStats.Luc);
				characterObj.Add ("Stamina", Game.Creature.InitalStats.Stamina);
				characterObj.Add ("MaxStamina", Game.Creature.InitalStats.MaxStamina);
		
				characterObj.Add ("LVL", Game.Creature.InitalStats.Level);
				characterObj.Add ("Stats", Game.Creature.StatPoints);
				characterObj.Add ("SkP", Game.Creature.SkillPoints);
				characterObj.Add ("stance", (int)Game.Creature.Stance);
				#endregion Char
				
				#region Equipment
				JSONObject Equipment = new JSONObject ();
				int i = 0;
				Game.Creature.Equipment_Strings = new string[Game.Creature.Equipment.Count];
				foreach (ItemData tmpobj in Game.Creature.Equipment) {
						Game.Creature.Equipment_Strings [i] = tmpobj.Name;
						JSONObject diese = new JSONObject ();
						diese.Add ("name", tmpobj.Name);
						diese.Add ("durab", tmpobj.Durability);
						diese.Add ("type", (int)tmpobj.Type);
						if (tmpobj.Type == ItemType.utility) {
								diese.Add ("cap", tmpobj.Ammo.Count);	
								int j = 0;
								JSONObject Ammo = new JSONObject ();
								foreach (AmmoData am in tmpobj.Ammo) {
										Ammo.Add (j.ToString (), am.Name);
										j++;
								}
								diese.Add ("Ammo", Ammo);
						}
						Equipment.Add (i.ToString (), diese);
						i++;
				}
				Equipment.Add ("count", Game.Creature.Equipment.Count);
				characterObj.Add ("equip", Equipment);
				#endregion Equipment
		
				#region inventory
				Debug.Log ("Save Inventory");
				JSONObject Inventory = new JSONObject ();
				i = 0;
				Game.Creature.Inventory_Strings = new string[Game.Creature.Inventory.Count];
				foreach (ItemData tmpobj in Game.Creature.Inventory) {
						Game.Creature.Inventory_Strings [i] = tmpobj.Name;
						JSONObject diese = new JSONObject ();
						diese.Add ("name", tmpobj.Name);
						diese.Add ("durab", tmpobj.Durability);
						diese.Add ("type", (int)tmpobj.Type);
						if (tmpobj.Type == ItemType.utility) {
								diese.Add ("cap", tmpobj.Ammo.Count);	
								int j = 0;
								JSONObject Ammo = new JSONObject ();
								foreach (AmmoData am in tmpobj.Ammo) {
										Ammo.Add (j.ToString (), am.Name);
										j++;
								}
								diese.Add ("Ammo", Ammo);
						}
						Inventory.Add (i.ToString (), diese);
						i++;
				}
				Inventory.Add ("count", Game.Creature.Inventory.Count);
				characterObj.Add ("inventory", Inventory);
				#endregion inventory
		
				#region keys
				Debug.Log ("Save Keys");
				Game.Keys = gameObject.GetComponent<PlayerBehaviour> ().skillkeys;
				JSONObject SaveKey = new JSONObject ();
				i = 0;
				foreach (SkillAndKeys sak in Game.Keys) {
						JSONObject diese = new JSONObject ();
						diese.Add ("key", sak.key);
						diese.Add ("action", sak.action);
						SaveKey.Add (i.ToString (), diese);
						i++;
				}
				SaveKey.Add ("count", i);
				#endregion keys
		
				#region skills
				Debug.Log ("Save Skills");
				JSONObject Skills = new JSONObject ();
				i = 0;
				string[] tmpskills = new string[Game.Creature.Skills.Count];
				int[] tmplevel = new int[Game.Creature.Skills.Count];
				foreach (skill tmpobj in Game.Creature.Skills) {
						tmpskills [i] = tmpobj.Name;
						tmplevel [i] = tmpobj.Level;
						i++;
				}
				Skills.Add ("count", Game.Creature.Skills.Count);
				i = 0;
				for (i=0; i<tmpskills.Length; i++) {
						JSONObject dieserSkills = new JSONObject ();
						dieserSkills.Add ("name", tmpskills [i]);
						dieserSkills.Add ("lvl", tmplevel [i]);
						Skills.Add (i.ToString (), dieserSkills);
				}
				characterObj.Add ("skills", Skills);
				#endregion skills
		
				#region Quest
				Debug.Log ("Save Quests");
				JSONObject questObj = new JSONObject ();
				int count_quests = 0;
				Game.Quests = gameObject.GetComponent<QuestController> ().AlleQuests;
				foreach (QuestStruct q in Game.Quests) {
						if (q.accepted) {
								JSONObject thisquest = new JSONObject ();
								thisquest.Add ("Name", q.Name);
								thisquest.Add ("Failes", q.failed.ToString ());
								if (q.finished) {
										thisquest.Add ("End", "true");
								} else {
										thisquest.Add ("End", "false");
					
										JSONObject ETK = new JSONObject ();
										int ETK_count = 0;
										ETK.Add ("count", q.EnemyTokill.Count);
										foreach (EnemyTokillStruct e in q.EnemyTokill) {
												JSONObject ETKe = new JSONObject ();
												ETKe.Add ("Name", e.Name);
												ETKe.Add ("Amount", e.Amount);
												ETK.Add (ETK_count.ToString (), ETKe);
												ETK_count++;
										}
										thisquest.Add ("ETK", ETK);
					
										JSONObject ITC = new JSONObject ();
										int ITC_count = 0;
										ITC.Add ("count", q.ItemsToCollect.Count);
										foreach (ItemsToCollectStruct e in q.ItemsToCollect) {
												JSONObject ITCe = new JSONObject ();
												ITCe.Add ("Name", e.Name);
												ITCe.Add ("Amount", e.Amount);
												ITC.Add (ITC_count.ToString (), ITCe);
												ITC_count++;
										}
										thisquest.Add ("ITC", ITC);
					
										JSONObject NTT = new JSONObject ();
										int NTT_count = 0;
										NTT.Add ("count", q.NPCToTalk.Count);
										foreach (string e in q.NPCToTalk) {
												NTT.Add (NTT_count.ToString (), e);
												NTT_count++;
										}
										thisquest.Add ("NTT", NTT);
								}
				
								questObj.Add (count_quests.ToString (), thisquest);
								count_quests++;
						}
				}
		
				questObj.Add ("count", count_quests);
				#endregion Quest
		
				_server_.data.Add ("character", characterObj);
				_server_.data.Add ("quest", questObj);
				_server_.data.Add ("keys", SaveKey);
		
				
		
				StartCoroutine (_server_.SaveData (id, _server_.data));
		}
		
		public void Load () {
		
				//Savegame Game = ScriptableObject.CreateInstance<Savegame> ();
				Savegame Game = ScriptableObject.CreateInstance<Savegame> ();
				Game.Quests = new List<QuestStruct> ();
				#region Char
				Game.Creature.Position = _server_.GetVector3 ("character", "position");
				Game.Creature.InitalStats.Gold = (int)_server_.data.GetObject ("character").GetNumber ("gold");
				Game.Creature.Gold = Game.Creature.InitalStats.Gold;
				Game.Creature.InitalStats.XP = (int)_server_.data.GetObject ("character").GetNumber ("xp");
		
				Game.Creature.InitalStats.Str = (int)_server_.data.GetObject ("character").GetNumber ("Str");
				Game.Creature.InitalStats.Dex = (int)_server_.data.GetObject ("character").GetNumber ("Dex");
				Game.Creature.InitalStats.Agi = (int)_server_.data.GetObject ("character").GetNumber ("Agi");
				Game.Creature.InitalStats.Int = (int)_server_.data.GetObject ("character").GetNumber ("Int");
		
				Game.Creature.InitalStats.Vit = (int)_server_.data.GetObject ("character").GetNumber ("Vit");
				Game.Creature.InitalStats.Luc = (int)_server_.data.GetObject ("character").GetNumber ("Luc");
				Game.Creature.InitalStats.Stamina = (int)_server_.data.GetObject ("character").GetNumber ("Stamina");
				Game.Creature.InitalStats.MaxStamina = (int)_server_.data.GetObject ("character").GetNumber ("MaxStamina");		
		
				Game.Creature.InitalStats.Level = (int)_server_.data.GetObject ("character").GetNumber ("LVL");
				Game.Creature.InitalStats.StatPoints = (int)_server_.data.GetObject ("character").GetNumber ("Stats");
				Game.Creature.InitalStats.SkillPoints = (int)_server_.data.GetObject ("character").GetNumber ("SkP");
				Game.Creature.InitalStats.Stance = (BattleStance)_server_.data.GetObject ("character").GetNumber ("stance");
				#endregion Char
		
				#region Equipment
				int i = 0;
				int max_i = 0;
				// Equipment
				max_i = (int)_server_.data.GetObject ("character").GetObject ("equip").GetNumber ("count");
				Game.Creature.InitalStats.Equipment_Strings = new string[max_i];
				Game.Creature.Equipment = new List<ItemData> ();
				for (i=0; i<max_i; i++) {
						JSONObject dies = _server_.data.GetObject ("character").GetObject ("equip").GetObject (i.ToString ());
						ItemDataList idlist = (ItemDataList)Resources.Load ("Items");
						ItemData diesesitem = new ItemData ();
						foreach (ItemData tmpit in idlist.ItemList) {
								if (tmpit.Name == dies.GetString ("name")) {
										diesesitem = tmpit;
								}
						}
						diesesitem.Durability = (int)dies.GetNumber ("durab");
						if (diesesitem.Type == ItemType.utility) {
								for (int ammocount=0; ammocount<dies.GetNumber("cap"); ammocount++) {
										AmmoData dieseammo = new AmmoData ();
										foreach (AmmoData tmpit in idlist.AmmoList) {
												if (tmpit.Name == dies.GetObject ("Ammo").GetString (ammocount.ToString ())) {
														dieseammo = tmpit;
												}
										}
										diesesitem.Ammo.Add (dieseammo);
								}
						}
						Game.Creature.Equipment.Add (diesesitem);
				}
				#endregion equipment
		
				#region inventory
				// Inventory
				max_i = (int)_server_.data.GetObject ("character").GetObject ("inventory").GetNumber ("count");
				Game.Creature.InitalStats.Inventory_Strings = new string[max_i];
				Game.Creature.Inventory = new List<ItemData> ();
				for (i=0; i<max_i; i++) {
						JSONObject dies = _server_.data.GetObject ("character").GetObject ("inventory").GetObject (i.ToString ());
						ItemDataList idlist = (ItemDataList)Resources.Load ("Items");
						ItemData diesesitem = new ItemData ();
						foreach (ItemData tmpit in idlist.ItemList) {
								if (tmpit.Name == dies.GetString ("name")) {
										diesesitem = tmpit;
								}
						}
						diesesitem.Durability = (int)dies.GetNumber ("durab");
						if (diesesitem.Type == ItemType.utility) {
								for (int ammocount=0; ammocount<dies.GetNumber("cap"); ammocount++) {
										AmmoData dieseammo = new AmmoData ();
										foreach (AmmoData tmpit in idlist.AmmoList) {
												if (tmpit.Name == dies.GetObject ("Ammo").GetString (ammocount.ToString ())) {
														dieseammo = tmpit;
												}
										}
										diesesitem.Ammo.Add (dieseammo);
								}
						}
						Game.Creature.Inventory.Add (diesesitem);
				}
				#endregion inventroy		
		
			
		
				#region skills
				Game.Creature.Skills = new List<skill> ();
				if (_server_.data.GetObject ("character").ContainsKey ("skills")) {
						JSONObject skills = _server_.data.GetObject ("character").GetObject ("skills");
						int max_skills = (int)skills.GetNumber ("count");
						for (int count_skills=0; count_skills<max_skills; count_skills++) {
								JSONObject dies = skills.GetObject (count_skills.ToString ());
								SkillsDataList skilldata = (SkillsDataList)Resources.Load ("Skill");
								List<skill> tmpskilllist = skilldata.SkillList;
								skill skilltolearn = new skill ();
								for (int skillid=0; skillid<tmpskilllist.Count; skillid++) {
										
										if (tmpskilllist [skillid].Name == dies.GetString ("name")) {
												skilltolearn = tmpskilllist [skillid];
												skilltolearn.Level = (int)dies.GetNumber ("lvl");
										}
								}
								if (skilltolearn.Name != "") {
										Game.Creature.Skills.Add (skilltolearn);
								}
						}
				}
				#endregion skills
		
				#region keys
				Game.Keys = new List<SkillAndKeys> ();
				gameObject.GetComponent<PlayerBehaviour> ().skillkeys = new List<SkillAndKeys> ();
				if (_server_.data.ContainsKey ("keys")) {
						//Game.Keys
						int keycount = (int)_server_.data.GetObject ("keys").GetNumber ("count");
						for (int ck=0; ck<keycount; ck++) {
								SkillAndKeys tmp;
								tmp.action = _server_.data.GetObject ("keys").GetObject (ck.ToString ()).GetString ("action");
								tmp.key = _server_.data.GetObject ("keys").GetObject (ck.ToString ()).GetString ("key");
								Game.Keys.Add (tmp);
						}
						gameObject.GetComponent<PlayerBehaviour> ().skillkeys = Game.Keys;
				}
				#endregion keys
		
				#region Quest
				if (_server_.data.ContainsKey ("quest")) {
						int count = 0;
						int maxcount = 0;
						max_i = (int)_server_.data.GetObject ("quest").GetNumber ("count");
						for (i=0; i<max_i; i++) {
								QuestStruct q = new QuestStruct ();
								q.EnemyTokill = new List<EnemyTokillStruct> ();
								q.ItemsToCollect = new List<ItemsToCollectStruct> ();
								q.NPCToTalk = new List<string> ();
				
								q.Name = _server_.data.GetObject ("quest").GetObject (i.ToString ()).GetString ("Name");
								q.accepted = true;
								JSONObject tmpq = _server_.data.GetObject ("quest").GetObject (i.ToString ());
								string endstring = tmpq.GetString ("End");
								if (endstring == "true") {
										q.finished = true;
								} else {
										q.finished = false;
										maxcount = (int)tmpq.GetObject ("ETK").GetNumber ("count");
										for (count=0; count<maxcount; count++) {
												EnemyTokillStruct tmpe = new EnemyTokillStruct ();
												tmpe.Name = tmpq.GetObject ("ETK").GetObject (count.ToString ()).GetString ("Name");
												tmpe.Amount = (int)tmpq.GetObject ("ETK").GetObject (count.ToString ()).GetNumber ("Amount");
												q.EnemyTokill.Add (tmpe);
										}
				
										maxcount = (int)tmpq.GetObject ("ITC").GetNumber ("count");
										for (count=0; count<maxcount; count++) {
												ItemsToCollectStruct tmpe = new ItemsToCollectStruct ();
												tmpe.Name = tmpq.GetObject ("ITC").GetObject (count.ToString ()).GetString ("Name");
												tmpe.Amount = (int)tmpq.GetObject ("ITC").GetObject (count.ToString ()).GetNumber ("Amount");
												q.ItemsToCollect.Add (tmpe);
										}
				
										maxcount = (int)tmpq.GetObject ("NTT").GetNumber ("count");
										for (count=0; count<maxcount; count++) {
												string tmpe = "";
												tmpe = tmpq.GetObject ("NTT").GetObject (count.ToString ()).GetString ("Name");
												q.NPCToTalk.Add (tmpe);
										}
								}
								Game.Quests.Add (q);
						}
						gameObject.GetComponent<QuestController> ().UpdateOnLoad (Game.Quests);
				}
				#endregion Quest
		
		
				#region Statuseffecte
				gameObject.GetComponent<PlayerBehaviour> ().me.alleStatus = new List<Status> ();
				Game.Creature.StatusEffects = new List<Status> ();
				StatusDataList DataListObj_status;
				DataListObj_status = (StatusDataList)Resources.Load ("Status");
				Game.Creature.StatusEffects.Clear ();
				foreach (Status id in DataListObj_status.StatusList) {
						//Game.Creature.StatusEffects.Add (id);
						gameObject.GetComponent<PlayerBehaviour> ().me.alleStatus.Add (id);
				}
				//gameObject.GetComponent<PlayerBehaviour> ().me.alleStatus = Game.Creature.StatusEffects;
				#endregion St
		
				gameObject.GetComponent<PlayerBehaviour> ().me.Creat = Game.Creature;
				gameObject.GetComponent<PlayerBehaviour> ().me.Creat.InitalStats = Game.Creature.InitalStats;
				
			
				StartGame ();
		}
	
		public void LoadVorarbeit () {
				StartCoroutine (_server_.GetData (id));
				IsLoading = true;
		
		}
	
		public void StartGame () {
				//transform.FindChild ("Unit_Model").GetComponent<SpriteRenderer> ().enabled = true;
				//GameObject.Find ("Map").GetComponent<map> ().LoadMap ();
				//transform.position = gameObject.GetComponent<PlayerBehaviour> ().me.Creat.Position;
				//gameObject.GetComponent<PlayerBehaviour> ().me.CalculateStats ();
				gameObject.GetComponent<PlayerBehaviour> ().me.Creat.HP = 999999999;
				gameObject.GetComponent<PlayerBehaviour> ().me.Creat.MP = 999999999;
				gameObject.GetComponent<PlayerBehaviour> ().me.Creat.IsRegAble = true;
				gameObject.GetComponent<PlayerBehaviour> ().me.IsLoaded = true;
		}
	
		public IEnumerator Login (string name, string password) {
				string _host_ = "http://www.cards-of-destruction.com/SimpleRpg/login.php";	
				WWWForm form = new WWWForm ();
				form.AddField ("user_name", name);
				form.AddField ("user_password", password);
				WWW web = new WWW (_host_, form);
				yield return web;
		
				if (web.size <= 2) {
						// Mach nichts
				} else {
						logintext = web.text;
						isLogin = true;
				}
		}
	
		void Update () {
				if (IsLoading) {
						if (_server_.data.ContainsKey ("character")) {
								Load ();
								IsLoading = false;
						} else {
								//Debug.Log (_server_.data.ToString ());
						}
				}
		
				if (isLogin) {
						isLogin = false;
						string[] data = logintext.Split (';');
						if (data [1] == "false") {
								id = int.Parse (data [0]);
								Save ();
								transform.FindChild ("UnitModel_Kopie").GetComponent<SpriteRenderer> ().enabled = true;
								transform.FindChild ("UnitModel").GetComponent<MeshRenderer> ().enabled = true;
						} else {
								id = int.Parse (data [0]);
								LoadVorarbeit ();
						}
						
				}
		}
}


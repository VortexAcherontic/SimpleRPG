using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using DataManager;
using Boomlagoon.JSON;


public class Player_Save : MonoBehaviour {
		// Erstmal nur Lokal Game!
		// Klappt erstmal nur in UNITY!
		
		private Server _server_ = new Server ();
		bool IsLoading = false;
		int id;
	
	
		public void Save () {
				Savegame Game = ScriptableObject.CreateInstance<Savegame> ();
				Game.Creature = gameObject.GetComponent<PlayerBehaviour> ().me.Creat;
		
				//AssetDatabase.CreateAsset (Game, "Assets/Resources/" + Game.Creature.Name + ".asset");
		
				JSONObject characterObj = new JSONObject ();
		
				characterObj.Add ("position", _server_.MakeVector2 (Game.Creature.Position));
				characterObj.Add ("gold", Game.Creature.Gold);
				characterObj.Add ("xp", Game.Creature.XP);
				//characterObj.Add("name",Game.Creature.Name); prefab?
		
				/* timer
		public float MoveTimer;
		public bool IsRegAble;
		public float RegTimer;
		public float RegCooldown;
		public float AttackTimer;
		public float AttackCooldown;
		
		nicht speichern
		 */
		
				//stats
				characterObj.Add ("Str", Game.Creature.InitalStats.Str);
				characterObj.Add ("Dex", Game.Creature.InitalStats.Dex);
				characterObj.Add ("Agi", Game.Creature.InitalStats.Agi);
				characterObj.Add ("Int", Game.Creature.InitalStats.Int);
		
				characterObj.Add ("Vit", Game.Creature.InitalStats.Vit);
				characterObj.Add ("Luc", Game.Creature.InitalStats.Luc);
		
				characterObj.Add ("LVL", Game.Creature.InitalStats.Level);
				characterObj.Add ("Stats", Game.Creature.StatPoints);
				characterObj.Add ("stance", (int)Game.Creature.Stance);
		
				/* nonplayer
		public int AggroRange;
		public bool IsMoveable;
		public bool IsBoss;
		public bool DoRespawn; // NPC/Bosses?
		public float RespawnTimer;
		
		spawnregion
		
		nicht speichern
		 
		 */
		
				JSONObject Equipment = new JSONObject ();
				int i = 0;
				Game.Creature.Equipment_Strings = new string[Game.Creature.Equipment.Count];
				foreach (ItemData tmpobj in Game.Creature.Equipment) {
						Game.Creature.Equipment_Strings [i] = tmpobj.Name;
						i++;
				}
				Equipment.Add ("count", Game.Creature.Equipment.Count);
				i = 0;
				foreach (string tmpstr in Game.Creature.Equipment_Strings) {
						Equipment.Add (i.ToString (), tmpstr);
						i++;
				}
				characterObj.Add ("equip", Equipment);
		
				JSONObject Inventory = new JSONObject ();
				i = 0;
				Game.Creature.Inventory_Strings = new string[Game.Creature.Inventory.Count];
				foreach (ItemData tmpobj in Game.Creature.Inventory) {
						Game.Creature.Inventory_Strings [i] = tmpobj.Name;
						i++;
				}
				Inventory.Add ("count", Game.Creature.Inventory.Count);
				i = 0;
				foreach (string tmpstr in Game.Creature.Inventory_Strings) {
						Inventory.Add (i.ToString (), tmpstr);
						i++;
				}
				characterObj.Add ("inventory", Inventory);
		
				_server_.data.Add ("character", characterObj);
		
				
		
				StartCoroutine (_server_.SaveData (id, _server_.data));
		}
		public void LoadVorarbeit () {
				StartCoroutine (_server_.GetData (id));
				IsLoading = true;
		
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
		}
	
		public void Load () {
				
				//Savegame Game = ScriptableObject.CreateInstance<Savegame> ();
				Savegame Game = ScriptableObject.CreateInstance<Savegame> ();
				
				
				Game.Creature.InitalStats.Position = _server_.GetVector2 ("character", "position");
				Game.Creature.InitalStats.Gold = (int)_server_.data.GetObject ("character").GetNumber ("gold");
				Game.Creature.InitalStats.XP = (int)_server_.data.GetObject ("character").GetNumber ("xp");
		
				Game.Creature.InitalStats.Str = (int)_server_.data.GetObject ("character").GetNumber ("Str");
				Game.Creature.InitalStats.Dex = (int)_server_.data.GetObject ("character").GetNumber ("Dex");
				Game.Creature.InitalStats.Agi = (int)_server_.data.GetObject ("character").GetNumber ("Agi");
				Game.Creature.InitalStats.Int = (int)_server_.data.GetObject ("character").GetNumber ("Int");
		
				Game.Creature.InitalStats.Vit = (int)_server_.data.GetObject ("character").GetNumber ("Vit");
				Game.Creature.InitalStats.Luc = (int)_server_.data.GetObject ("character").GetNumber ("Luc");
		
				Game.Creature.InitalStats.Level = (int)_server_.data.GetObject ("character").GetNumber ("LVL");
				Game.Creature.InitalStats.StatPoints = (int)_server_.data.GetObject ("character").GetNumber ("Stats");
				Game.Creature.InitalStats.Stance = (BattleStance)_server_.data.GetObject ("character").GetNumber ("stance");
		
				int i = 0;
				int max_i = 0;
				// Equipment
				max_i = (int)_server_.data.GetObject ("character").GetObject ("equip").GetNumber ("count");
				Game.Creature.InitalStats.Equipment_Strings = new string[max_i];
				for (i=0; i<max_i; i++) {
						Game.Creature.InitalStats.Equipment_Strings [i] = _server_.data.GetObject ("character").GetObject ("equip").GetString (i.ToString ());
				}
				// Inventory
				max_i = (int)_server_.data.GetObject ("character").GetObject ("inventory").GetNumber ("count");
				Game.Creature.InitalStats.Inventory_Strings = new string[max_i];
				for (i=0; i<max_i; i++) {
						Game.Creature.InitalStats.Inventory_Strings [i] = _server_.data.GetObject ("character").GetObject ("inventory").GetString (i.ToString ());
				}
		
				gameObject.GetComponent<PlayerBehaviour> ().me.Creat = Game.Creature;
				gameObject.GetComponent<PlayerBehaviour> ().me.Create (Game.Creature.InitalStats);
				
				StartGame ();
		}
	
		public void StartGame () {
				transform.FindChild ("UnitModel").GetComponent<SpriteRenderer> ().enabled = true;
				GameObject.Find ("Map").GetComponent<map> ().LoadMap ();
				gameObject.GetComponent<PlayerBehaviour> ().me.IsLoaded = true;
				gameObject.GetComponent<PlayerBehaviour> ().me.Creat.IsRegAble = true;
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
						string[] data = web.text.Split (';');
						if (data [1] == "false") {
								id = int.Parse (data [0]);
								Save ();
								transform.FindChild ("UnitModel").GetComponent<MeshRenderer> ().enabled = true;
								GameObject.Find ("Map").GetComponent<map> ().LoadMap ();
								//gameObject.GetComponent<PlayerBehaviour> ().me.IsLoaded = true;
						} else {
								id = int.Parse (data [0]);
								LoadVorarbeit ();
						}
				}
		}
}
	

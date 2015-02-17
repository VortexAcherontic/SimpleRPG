using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using DataManager;
using Boomlagoon.JSON;


public class Player_Save : MonoBehaviour {
		// Erstmal nur Lokal Game!
		// Klappt erstmal nur in UNITY!
		
		private Server _server_ = new Server ();
		int id;
		public void Save () {
				Savegame Game = ScriptableObject.CreateInstance<Savegame> ();
				Game.Creature = gameObject.GetComponent<PlayerBehaviour> ().me.Creat;
		
				//AssetDatabase.CreateAsset (Game, "Assets/Resources/" + Game.Creature.Name + ".asset");
		
				JSONObject characterObj = new JSONObject ();
				JSONObject position = new JSONObject ();
		
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
				characterObj.Add ("Str", Game.Creature.Str);
				characterObj.Add ("Dex", Game.Creature.Dex);
				characterObj.Add ("Agi", Game.Creature.Agi);
				characterObj.Add ("Int", Game.Creature.Int);
		
				characterObj.Add ("Vit", Game.Creature.Vit);
				characterObj.Add ("Luc", Game.Creature.Luc);
		
				characterObj.Add ("LVL", Game.Creature.Level);
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
		
				characterObj.Add ("equip", Game.Creature.Equipment_Strings.ToString ());
				characterObj.Add ("inventory", Game.Creature.Inventory_Strings.ToString ());
		
				_server_.data.Add ("character", characterObj);
		
				
		
				StartCoroutine (_server_.SaveData (id, _server_.data));
		}
	
		public IEnumerator Load () {
				Debug.Log ("Load Start");
				yield return StartCoroutine (_server_.GetData (id));
				//Savegame Game = ScriptableObject.CreateInstance<Savegame> ();
				Debug.Log (_server_.data.ToString ());
				if (_server_.data.ContainsKey ("character")) {	
						Savegame Game = ScriptableObject.CreateInstance<Savegame> ();
				
				
						Game.Creature.Position = _server_.GetVector2 ("character", "position");
						Game.Creature.Gold = (int)_server_.data.GetObject ("character").GetNumber ("gold");
						Game.Creature.XP = (int)_server_.data.GetObject ("character").GetNumber ("xp");
		
						Game.Creature.Str = (int)_server_.data.GetObject ("character").GetNumber ("Str");
						Game.Creature.Dex = (int)_server_.data.GetObject ("character").GetNumber ("Dex");
						Game.Creature.Agi = (int)_server_.data.GetObject ("character").GetNumber ("Agi");
						Game.Creature.Int = (int)_server_.data.GetObject ("character").GetNumber ("Int");
		
						Game.Creature.Vit = (int)_server_.data.GetObject ("character").GetNumber ("Vit");
						Game.Creature.Luc = (int)_server_.data.GetObject ("character").GetNumber ("Luc");
		
						Game.Creature.Level = (int)_server_.data.GetObject ("character").GetNumber ("LVL");
						Game.Creature.StatPoints = (int)_server_.data.GetObject ("character").GetNumber ("Stats");
						Game.Creature.Stance = (BattleStance)_server_.data.GetObject ("character").GetNumber ("stance");
		
						gameObject.GetComponent<PlayerBehaviour> ().me.Creat = Game.Creature;
				}
				transform.FindChild ("UnitModel").GetComponent<MeshRenderer> ().enabled = true;
				GameObject.Find ("Map").GetComponent<map> ().LoadMap ();
				gameObject.GetComponent<PlayerBehaviour> ().me.IsLoaded = true;
		}
	
		public IEnumerator Login (string name, string password) {
				Debug.Log ("Try Login");
				string _host_ = "http://www.cards-of-destruction.com/SimpleRpg/login.php";	
				WWWForm form = new WWWForm ();
				form.AddField ("user_name", name);
				form.AddField ("user_password", password);
				WWW web = new WWW (_host_, form);
				Debug.Log ("Abfrage los");
				yield return web;
				Debug.Log ("Ende " + web.text);
			
				if (web.size <= 2) {
						// Mach nichts
				} else {
						string[] data = web.text.Split (';');
						if (data [1] == "false") {
								id = int.Parse (data [0]);
								//StartCoroutine (Save ());
								transform.FindChild ("UnitModel").GetComponent<MeshRenderer> ().enabled = true;
								GameObject.Find ("Map").GetComponent<map> ().LoadMap ();
								//gameObject.GetComponent<PlayerBehaviour> ().me.IsLoaded = true;
						} else {
								id = int.Parse (data [0]);
								StartCoroutine (Load ());
						}
				}
		}
}
	

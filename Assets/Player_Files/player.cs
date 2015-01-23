using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class player : MonoBehaviour {
		public int Player_ID;
		public int hp ; 
		public int maxhp ; 
		public int mana ; 
		public int maxmana ; 
		public int xp ; 
		public int lvl ; 
		public int gold ; 
		public int pwr ; 
		public int armor ; 
		public int agility ; 
		public string pname;
		public Vector2 pos;
		Vector2 lastpos;
		public inventory inv;
		public int rangeweapondistance = 5;
		public float movement_delay = 0f;
		public bool gameover = false;
		public List<items> Equip = new List<items> ();
		/*, list equip*/

		// Use this for initialization
		public void Beginn (int hp, int maxhp, int mana, int maxmana, int xp, int lvl, int gold, int pwr, int armor, int agility, string pname, int posx, int posy, int p_ID/*, list equip*/) {
				//Player = [] in python zum laden&speichern genutzte liste
				this.hp = hp;
				this.maxhp = maxhp;
				this.mana = mana;
				this.maxmana = maxmana;
				this.xp = xp;
				this.lvl = lvl;
				this.gold = gold;
				this.pwr = pwr;
				this.armor = armor;
				this.agility = agility;
				this.Player_ID = p_ID;
				//this.name = pname;
				this.pos = new Vector2 (posx, posy);
				inv = GameObject.Find ("Main Camera").GetComponent<inventory> ();
				
				//Equip = equip ausrüstungsslots
				//GameObject.Find ("Unit").transform.Find ("UnitModel").GetComponent<SkinnedMeshRenderer> ().enabled = true;
				GameObject.Find ("Unit").transform.Find ("UnitModel").GetComponent<MeshRenderer> ().enabled = true;
				GameObject.Find ("Unit").GetComponent<PlayerToPos> ().MovePlayer ();
				GameObject.Find ("Map").GetComponent<map> ().LoadMap ();
		
				// Game ist fertig geladen!
				GameObject.Find ("Main Camera").GetComponent<mainmenu> ().gameloaded = true;
		}
		bool equipcheck = false;
		
		public void equip (items obj) {
				foreach (items c_obj in Equip) {
						if (c_obj.type == obj.type) {
								equipcheck = true;
						}
				}
				if (equipcheck == false) {
						Equip.Add (obj);
						inv.sub (obj);
				}
		}

		public void unequip (items obj) {
				inv.add (obj);
				Equip.Remove (obj);
		}

		public void Move (string direction) {
				lastpos = pos;
				//playerpointer
				//Ereignis ();
				switch (direction) {
						case "w":
								pos.y++;
								break;
						case "s":
								pos.y--;
								break;
						case "a":
								pos.x--;
								break;
						case "d":
								pos.x++;
								break;
				}
				int tileID = GameObject.Find ("Map").GetComponent<TileMap> ().tiles [(int)pos.x, (int)pos.y];
				bool movebool = GameObject.Find ("Map").GetComponent<TileMap> ().tileTypes [tileID].isWalkable;
				if (!movebool) {
						pos = lastpos;
				}
				GameObject.Find ("Unit").GetComponent<PlayerToPos> ().MovePlayer ();
				BerechneMovmentDelay ();
		}
	
		public void OnGUI () {
				// Anzeigen für Health, Mana und XP später durch grafische Elemente zu ersetzen.
				GUI.Label (new Rect (5, Screen.height - 80, 170, 20), "Health: " + hp + " HP");
				GUI.Label (new Rect (5, Screen.height - 55, 170, 20), "Mana :" + mana + "MP");
				GUI.Label (new Rect (5, Screen.height - 30, 170, 20), "Experience: " + xp + "XP");
				
				//Debug um Regeneration und Lvl up zu testen
				/*		
		if (GUI.Button (new Rect (5, Screen.height - 105, 170, 20), "Hp down")) {
						hp = maxhp / 2;
				}
				if (GUI.Button (new Rect (5, Screen.height - 130, 170, 20), "Mana down")) {
						mana = maxmana / 2;
				}
				if (GUI.Button (new Rect (5, Screen.height - 155, 170, 20), "Xp UP")) {
						xp += 20;
				}
		*/
		}
	
		public int skillpoints;
		void LVLUP () {
				if (xp >= 100 * (lvl + 1)) {
						lvl++;
						xp -= 100 * (lvl);
						if (lvl < 10) {
								skillpoints = 10;
						} else {
								skillpoints = 5;
						}

				}
		}
		//regen im menü
		/*
		void Regen () {
				hp += maxhp / 100;
				mana += maxmana / 100;
				if (hp > maxhp) {
						hp = maxhp;
				}
				if (mana > maxmana) {
						mana = maxmana;
				}
		}
		*/
		//ereignis


		void Update () {
				LVLUP ();
				if (hp < 0) {
						gameover = true;
				}
				
		}
		public void BerechneMovmentDelay () {
				float GGewicht = 0.0f;
				if (GameObject.Find ("Main Camera").GetComponent<mainmenu> ().gameloaded) {
						foreach (items tmpitem in inv.Inventar) {
								GGewicht += tmpitem.gewicht;
						}
						foreach (items tmpitem in Equip) {
								GGewicht += tmpitem.gewicht;
						}
				}
				movement_delay = GGewicht;
		}
		
		public IEnumerator save (string Datenbank_URL, int player_id) {
				//Debug.Log ("Versuche zu speichern " + Datenbank_URL + "save_player.php");
				WWWForm LoginForm = new WWWForm ();
				LoginForm.AddField ("player_id", player_id);
				LoginForm.AddField ("hp", hp);
				LoginForm.AddField ("maxhp", maxhp);
				LoginForm.AddField ("mana", mana);
				LoginForm.AddField ("maxmana", maxmana);
				LoginForm.AddField ("xp", xp);
				LoginForm.AddField ("lvl", lvl);
				LoginForm.AddField ("gold", gold);
				LoginForm.AddField ("pwr", pwr);
				LoginForm.AddField ("armor", armor);
				LoginForm.AddField ("agility", agility);
				LoginForm.AddField ("posx", "" + pos.x);
				LoginForm.AddField ("posy", "" + pos.y);
				LoginForm.AddField ("lastposx", "" + lastpos.x);
				LoginForm.AddField ("lastposy", "" + lastpos.y);
				//public inventory inv;
				//public List<items> Equip = new List<items> ();
				WWW web = new WWW (Datenbank_URL + "save_player.php", LoginForm);
				yield return web;
				//Debug.Log ("Hab gespeichert" + web.text);
				StartCoroutine (save_inv (Datenbank_URL, Player_ID));
				
		}
		public IEnumerator load (string Datenbank_URL, string Name, string Password) {
				
				WWWForm TmpForm = new WWWForm ();
				TmpForm.AddField ("user_name", Name);
				TmpForm.AddField ("user_password", Password);
				WWW www = new WWW (Datenbank_URL + "load_player.php", TmpForm);
				yield return www;
				if (www.text != "-1") {
						//Debug.Log ("### LoginText: " + www.text);
						string[] zeilen = www.text.Split ("\n" [0]);
						string[] tmp_spalten = new string[0];
						foreach (string zeile in zeilen) {
								tmp_spalten = zeile.Split (";" [0]);
						}
						Beginn (int.Parse (tmp_spalten [1]), 
			        int.Parse (tmp_spalten [2]), 
			        int.Parse (tmp_spalten [3]), 
			        int.Parse (tmp_spalten [4]), 
			        int.Parse (tmp_spalten [5]), 
			        int.Parse (tmp_spalten [6]), 
			        int.Parse (tmp_spalten [7]), 
			        int.Parse (tmp_spalten [8]), 
			        int.Parse (tmp_spalten [9]), 
			        int.Parse (tmp_spalten [10]), 
			        tmp_spalten [0], 
			        int.Parse (tmp_spalten [11]),
			        int.Parse (tmp_spalten [12]),
			        int.Parse (tmp_spalten [15])
						);
						StartCoroutine (load_inv (Datenbank_URL, Player_ID));
						
						//Player_Name = Name;
				} else {
						GameObject.Find ("Main Camera").GetComponent<mainmenu> ().showgamemenue = true;
						Debug.LogError ("### Login Fail: " + www.error);
				}
		}
		public IEnumerator save_inv (string Datenbank_URL, int player_id) {
			
				// Clear inv vorm neuen speichern
				WWWForm LoginForm = new WWWForm ();
				LoginForm.AddField ("player_id", player_id);
				WWW web = new WWW (Datenbank_URL + "clear_player_inv.php", LoginForm);
				yield return web;
				// neues inv speichern
				foreach (items obj in inv.Inventar) {
					
						//Debug.Log ("Save Inv: " + obj.name);
						LoginForm = new WWWForm ();
						LoginForm.AddField ("player_id", player_id);
						LoginForm.AddField ("item_name", obj.name);
						web = new WWW (Datenbank_URL + "save_player_inv.php", LoginForm);
						yield return web;
				}
				StartCoroutine (save_equip (Datenbank_URL, Player_ID));
		}
		public IEnumerator load_inv (string Datenbank_URL, int player_id) {
				item tmp_item = GameObject.Find ("Main Camera").GetComponent<item> ();
				items tmpobj = tmp_item.item_mit_name ("xyz");
		
				WWWForm TmpForm = new WWWForm ();
				TmpForm.AddField ("player_id", player_id);
				WWW www = new WWW (Datenbank_URL + "load_player_inv.php", TmpForm);
				yield return www;
				//Debug.Log ("inv:: " + www.text);
				if (www.text != "-1") {
						//Debug.Log ("### LoginText: " + www.text);
						string[] zeilen = www.text.Split ("\n" [0]);
						string[] tmp_spalten = new string[0];
						foreach (string zeile in zeilen) {

								tmp_spalten = zeile.Split (";" [0]);
								//Debug.Log ("Load Inv: " + tmp_spalten [0]);
								if (tmp_spalten [0] != "") {
										inv.add (tmp_item.item_mit_name (tmp_spalten [0]));
								}
						}
				}
				StartCoroutine (load_equip (Datenbank_URL, Player_ID));
				
		}
		public IEnumerator save_equip (string Datenbank_URL, int player_id) {
				// Clear inv vorm neuen speichern
				WWWForm LoginForm = new WWWForm ();
				LoginForm.AddField ("player_id", player_id);
				WWW web = new WWW (Datenbank_URL + "clear_player_equip.php", LoginForm);
				yield return web;

				foreach (items obj in Equip) {
						//Debug.Log ("Save EQU: " + obj.name);
						LoginForm = new WWWForm ();
						LoginForm.AddField ("player_id", player_id);
						LoginForm.AddField ("item_name", obj.name);

						web = new WWW (Datenbank_URL + "save_player_equip.php", LoginForm);
						yield return web;
				}
		}
		public IEnumerator load_equip (string Datenbank_URL, int player_id) {
				item tmp_item = GameObject.Find ("Main Camera").GetComponent<item> ();
				items tmpobj = tmp_item.item_mit_name ("xyz");
				WWWForm TmpForm = new WWWForm ();
				TmpForm.AddField ("player_id", player_id);
				WWW www = new WWW (Datenbank_URL + "load_player_equip.php", TmpForm);
				yield return www;
				//Debug.Log ("equ:: " + www.text);
				if (www.text != "-1") {
						//Debug.Log ("### LoginText: " + www.text);
						string[] zeilen = www.text.Split ("\n" [0]);
						string[] tmp_spalten = new string[0];
						foreach (string zeile in zeilen) {
								tmp_spalten = zeile.Split (";" [0]);
								//Debug.Log ("Load EQU: " + tmp_spalten [0]);
								if (tmp_spalten [0] != "") {
										inv.add (tmp_item.item_mit_name (tmp_spalten [0]));
										equip (tmp_item.item_mit_name (tmp_spalten [0]));
								}
						}
				}
		}
}

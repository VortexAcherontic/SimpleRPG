using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class player : MonoBehaviour {
		public int hp ; 
		public bool gameover = false;


		void Update () {
				if (hp < 0) {
						gameover = true;
				}
		}
		
		/*
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
				foreach (ItemData obj in inv.Inventar) {
					
						//Debug.Log ("Save Inv: " + obj.name);
						LoginForm = new WWWForm ();
						LoginForm.AddField ("player_id", player_id);
						LoginForm.AddField ("item_name", obj.Name);
						web = new WWW (Datenbank_URL + "save_player_inv.php", LoginForm);
						yield return web;
				}
				StartCoroutine (save_equip (Datenbank_URL, Player_ID));
		}
		public IEnumerator load_inv (string Datenbank_URL, int player_id) {
				item tmp_item = GameObject.Find ("Main Camera").GetComponent<item> ();
				ItemData tmpobj = tmp_item.item_mit_name ("xyz");
		
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

				foreach (ItemData obj in Equip) {
						//Debug.Log ("Save EQU: " + obj.name);
						LoginForm = new WWWForm ();
						LoginForm.AddField ("player_id", player_id);
						LoginForm.AddField ("item_name", obj.Name);

						web = new WWW (Datenbank_URL + "save_player_equip.php", LoginForm);
						yield return web;
				}
		}
		public IEnumerator load_equip (string Datenbank_URL, int player_id) {
				item tmp_item = GameObject.Find ("Main Camera").GetComponent<item> ();
				ItemData tmpobj = tmp_item.item_mit_name ("xyz");
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
		*/
}

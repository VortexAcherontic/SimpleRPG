using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {
		CreatureController me;
	
		bool GUI_Statverteilen = false;
		// Use this for initialization
		void Start () {
				me = gameObject.GetComponent<CreatureController> ();
		}
	
		// Update is called once per frame
		void Update () {
				if (me.IsLoaded) {
						CheckMovement ();
						CheckLevelUp ();
				}
		}
	
		void OnGUI () {
				if (me.IsLoaded) {
						GUILevelUP ();
				}
		}
	
		void CheckMovement () {
				if (Input.GetKeyDown ("w")) {
						me.MoveTo (new Vector2 (me.Creat.Position.x, me.Creat.Position.y + 1));
				}
				if (Input.GetKeyDown ("a")) {
						me.MoveTo (new Vector2 (me.Creat.Position.x - 1, me.Creat.Position.y));
				}
				if (Input.GetKeyDown ("s")) {
						me.MoveTo (new Vector2 (me.Creat.Position.x, me.Creat.Position.y - 1));
				}
				if (Input.GetKeyDown ("d")) {
						me.MoveTo (new Vector2 (me.Creat.Position.x + 1, me.Creat.Position.y));
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
	
		void GUILevelUP () {
				if (me.Creat.StatPoints >= 0) {
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
}

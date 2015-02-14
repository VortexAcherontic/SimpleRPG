using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;




public class Player_Save : MonoBehaviour {
		// Erstmal nur Lokal Game!
		// Klappt erstmal nur in UNITY!
	
		public void Save () {
				Savegame Game = ScriptableObject.CreateInstance<Savegame> ();
				Game.Creature = gameObject.GetComponent<PlayerBehaviour> ().me.Creat;
		
				AssetDatabase.CreateAsset (Game, "Assets/Resources/" + Game.Creature.Name + ".asset");
		}
	
		public void Load (string Playername) {
				//Savegame Game = ScriptableObject.CreateInstance<Savegame> ();
				Savegame Game = (Savegame)Resources.Load (Playername);
				gameObject.GetComponent<PlayerBehaviour> ().me.Creat = Game.Creature;
				transform.FindChild ("UnitModel").GetComponent<MeshRenderer> ().enabled = true;
				GameObject.Find ("Map").GetComponent<map> ().LoadMap ();
				gameObject.GetComponent<PlayerBehaviour> ().me.IsLoaded = true;
		}
}

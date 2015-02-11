using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class inventory : MonoBehaviour {

		public List<ItemData> Inventar = new List<ItemData> ();
		//List<items> templist = new List<items> ();
		public mainmenu gui;
		int a;
		player p001;
		string Datenbank_URL = "http://www.cards-of-destruction.com/SimpleRpg/";

		// Use this for initialization
		void Start () {
				p001 = GameObject.Find ("Main Camera").GetComponent<player> ();
				gui = GameObject.Find ("Main Camera").GetComponent<mainmenu> ();
		}
	
		float savecooldown = 20.0f;
		float savetimer = 0.0f;
	
		void OnGUI () {
						
				savetimer -= Time.deltaTime;
				
		}
}

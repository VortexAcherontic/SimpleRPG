using UnityEngine;
using System.Collections;

public class map : MonoBehaviour
{

		public Texture minimap;
		public mainmenu gui;
		public player p001;

		// Use this for initialization
		void Start ()
		{
				gui = GameObject.Find ("Main Camera").GetComponent<mainmenu> ();
				p001 = GameObject.Find ("Main Camera").GetComponent<player> ();
		}

		void OnGUI ()
		{
				if (gui.showmap) {
						GUI.DrawTexture (new Rect (1180, 0, 100, 100), minimap);
				}
				//if(p001.pos)
		}

		// Update is called once per frame
		void Update ()
		{
	
		}
}

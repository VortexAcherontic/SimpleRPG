using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class shop : MonoBehaviour {
		public Vector2 pos;
		player p001;
		// Use this for initialization
	
		void Start () {
				p001 = GameObject.Find ("Main Camera").GetComponent<player> ();
		}
		public void Create (int posx, int posy) {
				this.pos = new Vector2 (posx, posy);
		}
		
		public bool kaufe_item (items diesesitem) {
				bool check = false;
				if ((p001.gold >= diesesitem.price) && (diesesitem.stock >= 10)) {
						p001.inv.add (diesesitem);
						p001.gold -= diesesitem.price;
						diesesitem.stock -= 10;
						check = true;
				}
				return check;
		}
}

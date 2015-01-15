using UnityEngine;
using System.Collections.Generic;


public class enemy : MonoBehaviour {
		public monsters thismob;
		Transform healthbar;
		Transform healthbarFilled;

		public void SettingStats (monsters mt) {
				thismob = mt;
				thismob.hp = thismob.maxhp;
				thismob.mana = thismob.maxmana;
		}
	
		// Update is called once per frame
		void Update () {
				thismob.pos = new Vector2 (transform.position.x, transform.position.y);
				var offset = Vector3.up;  
				if (healthbar == null) {
						healthbar = transform.FindChild ("Health").transform;
						healthbarFilled = transform.FindChild ("HealthBar").transform;
				} else {
						offset.y = 0.25f;
						healthbar.position = Camera.main.WorldToViewportPoint (transform.position + offset);
						offset.z -= 1;
						healthbarFilled.position = Camera.main.WorldToViewportPoint (transform.position + offset);
						Rect hpstatus = healthbar.GetComponent<GUITexture> ().pixelInset;
						hpstatus.width = (thismob.hp / thismob.maxhp) * healthbarFilled.GetComponent<GUITexture> ().pixelInset.width;
						healthbar.GetComponent<GUITexture> ().pixelInset = hpstatus;
				}
		}
}

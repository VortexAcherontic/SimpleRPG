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
				DrawHP ();
				
		}
	
		void DrawHP () {
				if (healthbar == null) {
						healthbar = transform.FindChild ("Health").transform;
						healthbarFilled = transform.FindChild ("HealthBar").transform;
				}
				if (thismob.maxhp > 0) {
						float HpProzent = thismob.hp * 100 / thismob.maxhp;
						// Bars Position
						Vector3 Pos_Healthbar = Vector3.up;
						Pos_Healthbar.y = 0.25f;
						healthbar.position = Camera.main.WorldToViewportPoint (transform.position + Pos_Healthbar);
						Pos_Healthbar.z -= 1;
						healthbarFilled.position = Camera.main.WorldToViewportPoint (transform.position + Pos_Healthbar);
						// Bar Fill Status
						Rect hpstatus = healthbar.GetComponent<GUITexture> ().pixelInset;
						hpstatus.width = (HpProzent * healthbarFilled.GetComponent<GUITexture> ().pixelInset.width) / 100;
						healthbar.GetComponent<GUITexture> ().pixelInset = hpstatus;
				}
		}
}

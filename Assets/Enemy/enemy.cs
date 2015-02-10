using UnityEngine;
using System.Collections.Generic;


public class enemy : MonoBehaviour {
		public CreatureData thismob = new CreatureData ();
		Transform healthbar;
		Transform healthbarFilled;

		public void SettingStats (CreatureData mt) {
				thismob = new CreatureData ();
				thismob = mt;
				thismob.Start (thismob.InitalStats);
		}
	
		// Update is called once per frame
		void Update () {
				thismob.Update ();
				DrawHP ();
				
		}
	
		void DrawHP () {
				if (healthbar == null) {
						healthbar = transform.FindChild ("Health").transform;
						healthbarFilled = transform.FindChild ("HealthBar").transform;
				}
				if (thismob.MaxHP > 0) {
						float HpProzent = thismob.HP * 100 / thismob.MaxHP;
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

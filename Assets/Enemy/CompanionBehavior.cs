using UnityEngine;
using System.Collections;

public class CompanionBehavior : MonoBehaviour {

		CreatureController me;
		PlayerBehaviour p001;
	
		void Start () {
				p001 = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerBehaviour> ();
				me = gameObject.GetComponent<CreatureController> ();
		}
	
		void Update () {
				if (followtoggle) {
						followPlayer ();
				}
				if (companiontoggle) {/*spawn das viech*/
				} else {
						Destroy (gameObject);
				}
		}

		void followPlayer () {
				me.MoveTo (p001.me.Creat.lastPos);
		}

		bool followtoggle = true;
		bool companiontoggle = false;
	
		void toggleCompanion () {
				if (Input.GetKeyDown ("c")) {
						companiontoggle = ! companiontoggle;
				}
		}
		void followToggle () {
				if (Input.GetKeyDown ("f")) {
						followtoggle = ! followtoggle;
				}
		}
}

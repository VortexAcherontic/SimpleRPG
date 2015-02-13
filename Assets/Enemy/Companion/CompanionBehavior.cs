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
				followPlayer ();
		}

		void followPlayer () {
				me.MoveTo (p001.me.Creat.lastPos);
		}

}

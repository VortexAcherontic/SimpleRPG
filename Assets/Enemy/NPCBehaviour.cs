using UnityEngine;
using System.Collections;

public class NPCBehaviour : MonoBehaviour {
	
		QuestController QuestObj;
		CreatureController me;
		PlayerBehaviour p001;
	
		void Start () {
				p001 = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerBehaviour> ();
				QuestObj = GameObject.FindGameObjectWithTag ("Player").GetComponent<QuestController> ();
				me = gameObject.GetComponent<CreatureController> ();
		}
	
		void Update () {
				Quest ();
		}
	
		bool Interacted () {
				if (interactable ()) {
						if (Input.GetKeyDown ("f")) {
								return true;
						} else {
								return false;
						}
				} else {
						return false;
				}
		}
	
		bool interactable () {
				if ((((me.Creat.Position.x == p001.me.Creat.Position.x + 1) || (me.Creat.Position.x == p001.me.Creat.Position.x - 1)) &&
						(me.Creat.Position.y == p001.me.Creat.Position.y)) || (((me.Creat.Position.y == p001.me.Creat.Position.y + 1) ||
						(me.Creat.Position.y == p001.me.Creat.Position.y - 1)) && (me.Creat.Position.x == p001.me.Creat.Position.x))) {
						return true;
				} else {
						return false;
				}
		}
	
		void Quest () {
				if (Interacted ()) {
						//Start quest;
						QuestObj.NPCTalk (me.Creat.Name);
						Debug.Log ("Quest angenommen!");
				}	
		}
	
}

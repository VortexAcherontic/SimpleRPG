using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
		CreatureController me;
		PlayerBehaviour p001;
		SpriteRenderer SR;
		public bool TD;
		public bool LR;
		bool open = false;
	
		public Sprite TD_DoorClose;
		public Sprite TD_DoorOpen;
		public Sprite LR_DoorClose;
		public Sprite LR_DoorOpen;
	
		Sprite helpme;
		Texture2D tmptexture;
	
		void Start () {
				p001 = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerBehaviour> ();
				me = gameObject.GetComponent<CreatureController> ();
				SR = gameObject.GetComponentInChildren<SpriteRenderer> ();
		}
	
		void Update () {
				Interacted ();
				if (TD) {
						if (open) {
								helpme = TD_DoorOpen;
								//Debug.Log ("open");
						} else {
								helpme = TD_DoorClose;
								//Debug.Log ("close");
						}
				}
				if (LR) {
						if (open) {
								helpme = LR_DoorOpen;
						} else {
								helpme = LR_DoorClose;
						}
				}
				tmptexture = new Texture2D ((int)helpme.rect.width, (int)helpme.rect.width);
				Color[] pixels = helpme.texture.GetPixels ((int)helpme.textureRect.x, (int)helpme.textureRect.y, (int)helpme.textureRect.width, (int)helpme.textureRect.height);
				tmptexture.SetPixels (pixels);
				tmptexture.Apply ();
				SR.material.mainTexture = new Texture2D ((int)TD_DoorOpen.rect.width, (int)TD_DoorOpen.rect.width);
		}
	
		void Interacted () {
				if (interactable ()) {
						if (Input.GetKeyDown ("f")) {
								open = !open;
								Debug.Log ("interacted");
						} 
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
	
}

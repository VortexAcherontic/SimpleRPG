using UnityEngine;
using System.Collections;

[System.Serializable]
public struct OneSideSprite {
		public Sprite RightFeet;
		public Sprite Middel;
		public Sprite LeftFeet;
}
[System.Serializable]
public struct AllSiedsSprite {
		public OneSideSprite FaceDown;
		public OneSideSprite FaceRight;
		public OneSideSprite FaceUp;
		public OneSideSprite FaceLeft;
}

public enum AnimationTyp {
		StandStillDown,
		StandStillRight,
		StandStillUp,
		StandStillLeft,
		MoveDown,
		MoveRight,
		MoveUp,
		MoveLeft
}

public class Sprite_CharController : MonoBehaviour {

		public AllSiedsSprite Sprites;
		public AnimationTyp Action;
		SpriteRenderer SR;
		float AniCooldown = 0.3f;
		float AniTimer;
		int AniStep = 0;
		Sprite[] AniSerie = new Sprite[0];
	
		void Start () {
				SR = GetComponent<SpriteRenderer> ();
		}
	
		void Update () {
				AniTimer -= Time.deltaTime;
				if (AniTimer <= 0) {
						DoAnimation ();
						AniTimer = AniCooldown;
				}
		}
	
		void DoAnimation () {
				Sprite CurrentSprite = null;
				switch (Action) {
						case AnimationTyp.StandStillDown:
								AniSerie = new Sprite[1];
								AniSerie [0] = Sprites.FaceDown.Middel;
								break;
						case AnimationTyp.StandStillLeft:
								AniSerie = new Sprite[1];
								AniSerie [0] = Sprites.FaceLeft.Middel;
								break;
						case AnimationTyp.StandStillRight:
								AniSerie = new Sprite[1];
								AniSerie [0] = Sprites.FaceRight.Middel;
								break;
						case AnimationTyp.StandStillUp:
								AniSerie = new Sprite[1];
								AniSerie [0] = Sprites.FaceUp.Middel;
								break;
				
						case AnimationTyp.MoveDown:
								AniSerie = new Sprite[4];
								AniSerie [0] = Sprites.FaceDown.Middel;
								AniSerie [1] = Sprites.FaceDown.LeftFeet;
								AniSerie [2] = Sprites.FaceDown.Middel;
								AniSerie [3] = Sprites.FaceDown.RightFeet;
								break;
						case AnimationTyp.MoveLeft:
								AniSerie = new Sprite[4];
								AniSerie [0] = Sprites.FaceLeft.Middel;
								AniSerie [1] = Sprites.FaceLeft.LeftFeet;
								AniSerie [2] = Sprites.FaceLeft.Middel;
								AniSerie [3] = Sprites.FaceLeft.RightFeet;
								break;
						case AnimationTyp.MoveRight:
								AniSerie = new Sprite[4];
								AniSerie [0] = Sprites.FaceRight.Middel;
								AniSerie [1] = Sprites.FaceRight.LeftFeet;
								AniSerie [2] = Sprites.FaceRight.Middel;
								AniSerie [3] = Sprites.FaceRight.RightFeet;
								break;
						case AnimationTyp.MoveUp:
								AniSerie = new Sprite[4];
								AniSerie [0] = Sprites.FaceUp.Middel;
								AniSerie [1] = Sprites.FaceUp.LeftFeet;
								AniSerie [2] = Sprites.FaceUp.Middel;
								AniSerie [3] = Sprites.FaceUp.RightFeet;
								break;
						
				}
				if (AniStep >= AniSerie.Length) {
						AniStep = 0;
				}
				CurrentSprite = AniSerie [AniStep];
				SR.sprite = CurrentSprite;
				AniStep++;
				
		}
}

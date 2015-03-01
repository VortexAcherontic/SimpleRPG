using UnityEngine;
using System.Collections;

//
// Ursprünglich eine ZoD Development entwicklung
// Aebr da ich es geschrieben habe und ich bei beiden mitmache, hier für SimpleRPG
//
// Sascha
//

public class GUI_Helper {

		private Vector2 Fix_Screen = new Vector2 (1920, 1080);
		private Vector2 Org_Screen;
		private Vector3 Screen_Scale = Vector3.one;
		public Vector2 New_Screen;

		public bool Show_Area = false; // Für Dev Tests ;)

		private GUISkin Default_Button_Skin;
		private Texture2D Button_Normal;
		private Texture2D Button_Hover;
		private Texture2D Button_Active;

		public void Screen_Settings () {
				Org_Screen = new Vector2 (Screen.width, Screen.height);
				Screen_Scale.x = Org_Screen.x / Fix_Screen.x;
				Screen_Scale.y = Org_Screen.y / Fix_Screen.y;
				if (Screen_Scale.x > Screen_Scale.y) {
						Screen_Scale.z = Screen_Scale.y;
				} else {
						Screen_Scale.z = Screen_Scale.x;
				}
				New_Screen.x = Fix_Screen.x * Screen_Scale.x;
				New_Screen.y = Fix_Screen.y * Screen_Scale.x;
		}
		private Rect Rect_Umrechnen (Rect fix_rect) {
				Screen_Settings ();
				Rect org_rect = new Rect ();
				Vector2 tmp_pos = org_rect.position;
				tmp_pos.x = fix_rect.position.x * Screen_Scale.z;
				tmp_pos.y = fix_rect.position.y * Screen_Scale.z;
				org_rect.height = fix_rect.height * Screen_Scale.z;
				org_rect.width = fix_rect.width * Screen_Scale.z;
				org_rect.position = tmp_pos;

				return org_rect;
		}

		public Rect BeginBackground (Texture background, Rect bereich) {
				Vector2 size = new Vector2 (bereich.width, bereich.height);
				Vector2 pos = bereich.position;
				Screen_Settings ();
				Camera.main.backgroundColor = Color.black;
				Vector3 bild_scale = new Vector3 ();
				bild_scale.y = (float)size.y / (float)background.height;
				bild_scale.x = (float)size.x / (float)background.width;
				if (bild_scale.x > bild_scale.y) {
						bild_scale.z = bild_scale.y;
				} else {
						bild_scale.z = bild_scale.x;
				}
				//bild_scale.z = bild_scale.z * Screen_Scale.z;
				Rect anzeige = new Rect ();
				Vector2 tmp_pos = new Vector2 ();
				anzeige.height = background.height * bild_scale.z;
				anzeige.width = background.width * bild_scale.z;
				tmp_pos.x = pos.x;
				tmp_pos.y = pos.y;
				anzeige.position = tmp_pos;
				DrawTexture (background, anzeige);
				BeginArea ("Background " + background.name, anzeige);
				return anzeige;
		}
		public void EndBackground () {
				EndArea ();
		}
		public void BeginArea (string bezeichnung, Rect bereich) {
				Rect new_bereich = Rect_Umrechnen (bereich);
				if (Show_Area) {
						GUI.Box (new_bereich, bezeichnung);
				}
				GUILayout.BeginArea (new_bereich);
		}
		public void EndArea () {
				GUILayout.EndArea ();
		}
		public void Box (string bezeichnung, int font_size, Rect bereich) {
				int org_font_size = GUI.skin.box.fontSize;
				Rect new_bereich = Rect_Umrechnen (bereich);
				GUI.skin.box.fontSize = (int)Mathf.Ceil (font_size * (new_bereich.height / 100));
				GUI.Box (new_bereich, bezeichnung);
				GUI.skin.box.fontSize = org_font_size;
		}
		public void DrawTexture (Texture bild, Rect bereich) {
				Rect new_bereich = Rect_Umrechnen (bereich);
				GUI.DrawTexture (new_bereich, bild, ScaleMode.ScaleAndCrop);
		}
		public void Label (string text, float font_size, Rect bereich) {
				BeginArea ("Label " + text, bereich);
				Rect new_bereich = Rect_Umrechnen (bereich);
				int org_font_size = GUI.skin.label.fontSize;
				GUI.skin.label.fixedHeight = 0;
				GUI.skin.label.stretchHeight = true;
				GUI.skin.label.fontSize = (int)Mathf.Ceil (font_size * (new_bereich.height / 20));
				GUILayout.Label (text + "");
				GUI.skin.label.fontSize = org_font_size;
				EndArea ();
		}
		public bool Button_Text (string text, float font_size, Rect bereich) {
				bool return_value = false;
				Rect new_bereich = Rect_Umrechnen (bereich);
				int org_font_size = GUI.skin.button.fontSize;
				GUI.skin.button.fixedHeight = 0;
				GUI.skin.button.stretchHeight = true;
				GUI.skin.button.fontSize = (int)Mathf.Ceil (font_size * (new_bereich.height / 20));
				if (GUI.Button (new_bereich, text)) {
						return_value = true;
				}
				GUI.skin.button.fontSize = org_font_size;
				return return_value;
		}
		public bool Button_Bild (Texture bild, Rect bereich) {
				bool return_value = false;
				Rect new_bereich = Rect_Umrechnen (bereich);
				GUI.skin.button.stretchHeight = true;
				if (GUI.Button (new_bereich, bild)) {
						return_value = true;
				}
				return return_value;
		}
		public string TextField (string text, float font_size, Rect bereich) {
				string return_value = "";
				Rect new_bereich = Rect_Umrechnen (bereich);
				int org_font_size = GUI.skin.textField.fontSize;
				GUI.skin.textField.fixedHeight = 0;
				GUI.skin.textField.stretchHeight = true;
				GUI.skin.textField.fontSize = (int)Mathf.Ceil (font_size * (new_bereich.height / 20));
				return_value = GUI.TextField (new_bereich, text);
				GUI.skin.textField.fontSize = org_font_size;
				return return_value;
		}
		public string PasswordField (string text, float font_size, Rect bereich) {
				string return_value = "";
				Rect new_bereich = Rect_Umrechnen (bereich);
				int org_font_size = GUI.skin.textField.fontSize;
				GUI.skin.textField.fixedHeight = 0;
				GUI.skin.textField.stretchHeight = true;
				GUI.skin.textField.fontSize = (int)Mathf.Ceil (font_size * (new_bereich.height / 20));
				return_value = GUI.PasswordField (new_bereich, text, '*');
				GUI.skin.textField.fontSize = org_font_size;
				return return_value;
		}
		public float HorizontalSlider (float slider, float min, float max, float size, Rect bereich) {
				Rect new_bereich = Rect_Umrechnen (bereich);
				slider = GUI.HorizontalSlider (new_bereich, slider, min, max);
				return slider;
		}
		public Vector2 BeginScrollView (Vector2 scroll, float size, Rect view, Rect bereich) {
				Rect new_bereich = Rect_Umrechnen (bereich);
				Rect new_view = Rect_Umrechnen (view);
				scroll = GUI.BeginScrollView (new_bereich, scroll, new_view);
				return scroll;
		}
		public void EndScrollView () {
				GUI.EndScrollView ();
		}

		public void Button_Rahmen_weg () {
				Default_Button_Skin = GUI.skin;
				Button_Normal = GUI.skin.button.normal.background;
				Button_Hover = GUI.skin.button.hover.background;
				Button_Active = GUI.skin.button.active.background;
				GUI.skin.button.normal.background = null;
				GUI.skin.button.active.background = null;
				GUI.skin.button.hover.background = null;
		}

		public void Button_Rahmen_hin () {
				GUI.skin.button = Default_Button_Skin.button;
				GUI.skin.button.normal.background = Button_Normal;
				GUI.skin.button.hover.background = Button_Hover;
				GUI.skin.button.active.background = Button_Active;
		}

}

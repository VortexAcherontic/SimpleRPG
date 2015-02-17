using UnityEngine;
using System;
using Boomlagoon.JSON;
using System.Collections;

namespace DataManager {
		public class Server {

				public JSONObject data = new JSONObject ();

				protected string _host_ = "http://www.cards-of-destruction.com/SimpleRpg/";
				protected string _controller_;

				public IEnumerator GetData (int id) {
						_controller_ = "load_player.php";

						string url = _host_ + _controller_;

						WWWForm form = new WWWForm ();
						form.AddField ("id", id);
			
						WWW www = new WWW (url, form);
						yield return www;

						if (www.size <= 2) {
								yield return null;
						} else {
								data = JSONObject.Parse (www.text);
						}
				}

				public IEnumerator SaveData (int id, JSONObject dat) {

						_controller_ = "save_player.php";

						string url = _host_ + _controller_;

						WWWForm form = new WWWForm ();
						form.AddField ("id", id);
						form.AddField ("data", dat.ToString ());
						WWW www = new WWW (url, form);
						yield return www;

				}
/*
		//warum farbe?
				public JSONObject MakeColor (Color parsedColor) {

						var color = new JSONObject{
				{"r",(float)(Convert.ToDouble(parsedColor.r))},
				{"g",(float)(Convert.ToDouble(parsedColor.g))},
				{"b",(float)(Convert.ToDouble(parsedColor.b))},
		    {"a",(float)(Convert.ToDouble(parsedColor.a))}
			};
						return color;
				public Color GetColor (string obj, string key) {

						JSONObject jsonObject = data.GetObject (obj);
						JSONObject colorobj = jsonObject.GetObject (key);

						float r = (float)(Convert.ToDecimal (colorobj.GetNumber ("r")));
						float g = (float)(Convert.ToDecimal (colorobj.GetNumber ("g")));
						float b = (float)(Convert.ToDecimal (colorobj.GetNumber ("b")));
						float a = (float)(Convert.ToDecimal (colorobj.GetNumber ("a")));

						Color color = new Color (r, g, b, a);
						return color;

				}
*/
				//position?
				public JSONObject MakeVector2 (Vector2 vector) {
						var v = new JSONObject{
				{"x",(float)(Convert.ToDouble(vector.x))},
				{"y",(float)(Convert.ToDouble(vector.y))}
				//{"z",(float)(Convert.ToDouble(vector.z))}
			};
						return v;
				}
				
				public Vector2 GetVector2 (string obj, string key) {

						JSONObject jsonObject = data.GetObject (obj);
						JSONObject vector = jsonObject.GetObject (key);

						float x = (float)(Convert.ToDecimal (vector.GetNumber ("x")));
						float y = (float)(Convert.ToDecimal (vector.GetNumber ("y")));
						//float z = (float)(Convert.ToDecimal (vector.GetNumber ("z")));

						Vector2 result = new Vector2 (x, y/*, z*/);
						return result;
				}
	
				/*
				// battlestance
				public JSONObject MakeStance (BattleStance stance) {
						var v = new JSONObject{
				{"Battlestance",stance.ToString()}
			};
						return v;
				}
				public BattleStance GetStance (string obj, string key) {
			
						JSONObject jsonObject = data.GetObject (obj);
						JSONObject saved_stance = jsonObject.GetObject (key);
			
						BattleStance stance = System.Enum.Parse (typeof(BattleStance), saved_stance.ToString ());
						return stance;			
				}
		
				//
		*/
		}

}
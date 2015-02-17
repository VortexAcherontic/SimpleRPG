using UnityEngine;
using DataManager;
using Boomlagoon.JSON;
using System.Collections;

public class CharacterCreator : MonoBehaviour {

		private GameObject _characterObject_;
		private Server _server_ = new Server ();
	
		int id; //PlayerID

		private IEnumerator Start () {
		
				yield return StartCoroutine (_server_.GetData (id));
				if (_server_.data.ContainsKey ("character")) {
						GameObject savedObject = Resources.Load (_server_.data.GetObject ("character").GetString ("body")) as GameObject;
				} 
		}
	
	
	
}

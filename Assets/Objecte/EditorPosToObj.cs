using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class EditorPosToObj : MonoBehaviour {
		public Transform MoveTo;
	
		// Use this for initialization
		void Update () {
				if (MoveTo != null) {
						transform.position = MoveTo.position;
				}
		}
	
}

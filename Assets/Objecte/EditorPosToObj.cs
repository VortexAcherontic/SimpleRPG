using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class EditorPosToObj : MonoBehaviour {
		public Transform MoveTo;
		Vector3 offset;
		void Start () {
				offset = MoveTo.transform.position - transform.position;
		}
	
		// Use this for initialization
		void Update () {
				if (MoveTo != null) {
						transform.position = MoveTo.position;
				}
		}
	
}

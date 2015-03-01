using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class EditorPosToObj : MonoBehaviour {
		public Transform MoveTo;
		public Vector3 offset = Vector3.zero;
		public bool RotateToo = false;
		void Start () {
				offset = MoveTo.transform.position - transform.position;
		}
	
		// Use this for initialization
		void Update () {
				if (MoveTo != null) {
						transform.position = MoveTo.position + offset;
						if (RotateToo) {
								transform.rotation = MoveTo.rotation;
						}
				}
		}
	
}

using UnityEngine;
using System.Collections;

public class TextureSacleToTransformScale : MonoBehaviour {
		public MeshRenderer ObjRenderer;
		// Use this for initialization
		void Start () {
				ObjRenderer = GetComponentInChildren<MeshRenderer> ();
				ObjRenderer.material.SetTextureScale ("_MainTex", new Vector2 (transform.localScale.x, transform.localScale.z));
		}
}

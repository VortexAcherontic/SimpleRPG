using UnityEngine;
using System.Collections;

public class TextureSacleToTransformScale : MonoBehaviour {
		public MeshRenderer ObjRenderer;
		// Use this for initialization
		void Start () {
				ObjRenderer = GetComponent<MeshRenderer> ();
				ObjRenderer.material.SetTextureScale ("_MainTex", new Vector2 (transform.localScale.x / 10, transform.localScale.z / 10));
		}
}

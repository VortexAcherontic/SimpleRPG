using UnityEngine;
using System.Collections;

public class WallTextureSacle : MonoBehaviour {
		public MeshRenderer WallRenderer;
		// Use this for initialization
		void Start () {
				WallRenderer = GetComponent<MeshRenderer> ();
				WallRenderer.material.SetTextureScale ("_MainTex", new Vector2 (transform.localScale.x / 10, transform.localScale.z / 10));
		}
}

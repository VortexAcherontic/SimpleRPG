using UnityEngine;
using System.Collections;

public class TileMap : MonoBehaviour {

		public TileType[] tileTypes;

		public int[,] tiles;

		int mapSizeX = 100;
		int mapSizeY = 100;

		void Start () {
				//GenerateMapData ();
				//GenerateMapVisuals ();
				// Nun alles wo anders aufgerufen
		}
		public void GenerateMapData () {
				Texture2D Map = gameObject.GetComponent<map> ().minimap;
				mapSizeX = Map.height;
				mapSizeY = Map.width;
		
				tiles = new int[mapSizeX, mapSizeY];
				int x, y;

				for (x = 0; x < mapSizeX; x++) {
						for (y = 0; y < mapSizeY; y++) {
								Color FeldFarbe = Map.GetPixel (x, y);
								// Viele Stellen genau sollte das sein? Ich denke 3 muss reichen
								FeldFarbe.r = Mathf.Round (FeldFarbe.r * 1000) / 1000;
								FeldFarbe.g = Mathf.Round (FeldFarbe.g * 1000) / 1000;
								FeldFarbe.b = Mathf.Round (FeldFarbe.b * 1000) / 1000;
								FeldFarbe.a = Mathf.Round (FeldFarbe.a * 1000) / 1000;
								// Tiles Ziffern noch anpassen
								if (FeldFarbe.r == 0.965f && FeldFarbe.g == 0.635f && FeldFarbe.b == 0.455f && FeldFarbe.a == 0.094f) {
										tiles [x, y] = 1;
								} else if (FeldFarbe.r == 0.996f && FeldFarbe.g == 0.678f && FeldFarbe.b == 0.784f && FeldFarbe.a == 0.125f) {
										tiles [x, y] = 2;
								} else if (FeldFarbe.r == 0.988f && FeldFarbe.g == 0.118f && FeldFarbe.b == 0.149f && FeldFarbe.a == 0.118f) {
										tiles [x, y] = 3;
								} else if (FeldFarbe.r == 0.976f && FeldFarbe.g == 0.976f && FeldFarbe.b == 0.976f && FeldFarbe.a == 0.098f) {
										tiles [x, y] = 1;
								} else if (FeldFarbe.r == 0.000f && FeldFarbe.g == 0.675f && FeldFarbe.b == 0.969f && FeldFarbe.a == 0.118f) {
										tiles [x, y] = 1;
								} else if (FeldFarbe.r == 0.996f && FeldFarbe.g == 0.945f && FeldFarbe.b == 0.000f && FeldFarbe.a == 0.125f) {
										tiles [x, y] = 1;
								} else if (FeldFarbe.r == 0.780f && FeldFarbe.g == 0.992f && FeldFarbe.b == 0.125f && FeldFarbe.a == 0.114f) {
										tiles [x, y] = 1;
								} else if (FeldFarbe.r == 0.184f && FeldFarbe.g == 0.961f && FeldFarbe.b == 0.412f && FeldFarbe.a == 0.090f) {
										tiles [x, y] = 1;
								} else if (FeldFarbe.r == 0.000f && FeldFarbe.g == 0.000f && FeldFarbe.b == 0.000f && FeldFarbe.a == 0.024f) {
										tiles [x, y] = 1;
								} else {
										Debug.LogError ("Unbekannte Map Farbe: R" + FeldFarbe.r + " G" + FeldFarbe.g + " B" + FeldFarbe.b + " A" + FeldFarbe.a + " !");								
										tiles [x, y] = 1;
								}
						}
				}
/*
				//U-Form Berg

				tiles [4, 4] = 3;
				tiles [5, 4] = 3;
				tiles [6, 4] = 3;
				tiles [7, 4] = 3;
				tiles [8, 4] = 3;

				tiles [4, 5] = 3;
				tiles [4, 6] = 3;
				tiles [8, 5] = 3;
				tiles [8, 6] = 3;

				//Sumpf

				for (x=3; x<=5; x++) {
						for (y = 0; y < 4; y++) {
								tiles [x, y] = 0;
						}
				}
				
				//Straße

				for (x=1; x<10; x++) {
						tiles [x, 8] = 2;
				}
				for (y=0; y<10; y++) {
						tiles [1, y] = 2;
				}
*/
				GenerateMapVisuals ();
		}
		void GenerateMapVisuals () {
				for (int x = 0; x < mapSizeX; x++) {
						for (int y = 0; y < mapSizeY; y++) {
								TileType tt = tileTypes [tiles [x, y]];
								GameObject tmpobjct = (GameObject)Instantiate (tt.tileVisualPrefab, new Vector3 (x, y, 0), Quaternion.identity);
								tmpobjct.transform.parent = GameObject.Find ("Map").transform;
						}
				}
		}

}

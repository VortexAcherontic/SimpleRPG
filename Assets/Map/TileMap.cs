using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct regions {
		public int Region_ID;
		public string name;
		public GameObject tileVisualPrefab;
	
		public bool isWalkable;
		public float walkEffect;
	
		public Color mapColor;
}

public class TileMap : MonoBehaviour {
		public List<regions> tileTypes = new List<regions> ();
		public int[,] tiles;

		int mapSizeX = 100;
		int mapSizeY = 100;

		regions CreateEmpty () {
				regions tmpreg = new regions ();
				tmpreg.Region_ID = tileTypes.Count + 1;
				tmpreg.name = "Tile";
				tmpreg.tileVisualPrefab = (GameObject)Resources.Load ("Tiles/TileForest");
				tmpreg.isWalkable = true;
				tmpreg.walkEffect = 1;
				return	tmpreg;
		}
	
		void Start () {
				regions tmpreg = CreateEmpty ();
				tmpreg.name = "Forest";
				tmpreg.tileVisualPrefab = (GameObject)Resources.Load ("Tiles/TileForest");
				tmpreg.walkEffect = 2;
				tmpreg.mapColor.r = 0.184f;
				tmpreg.mapColor.g = 0.961f;
				tmpreg.mapColor.b = 0.412f;
				tmpreg.mapColor.a = 0.090f;
				tileTypes.Add (tmpreg);
		
				tmpreg = CreateEmpty ();
				tmpreg.name = "Grass";
				tmpreg.tileVisualPrefab = (GameObject)Resources.Load ("Tiles/TileGrass");
				tmpreg.mapColor.r = 0.780f;
				tmpreg.mapColor.g = 0.992f;
				tmpreg.mapColor.b = 0.125f;
				tmpreg.mapColor.a = 0.114f;
				tileTypes.Add (tmpreg);
		
				tmpreg = CreateEmpty ();
				tmpreg.name = "Road";
				tmpreg.tileVisualPrefab = (GameObject)Resources.Load ("Tiles/TileRoad");
				tmpreg.walkEffect = 0.5f;
				tmpreg.mapColor.r = 0.996f;
				tmpreg.mapColor.g = 0.678f;
				tmpreg.mapColor.b = 0.784f;
				tmpreg.mapColor.a = 0.125f;
				tileTypes.Add (tmpreg);
		
				tmpreg = CreateEmpty ();
				tmpreg.name = "Mountain";
				tmpreg.tileVisualPrefab = (GameObject)Resources.Load ("Tiles/TileMountain");
				tmpreg.walkEffect = 8;
				tmpreg.mapColor.r = 0.976f;
				tmpreg.mapColor.g = 0.976f;
				tmpreg.mapColor.b = 0.976f;
				tmpreg.mapColor.a = 0.098f;
				tileTypes.Add (tmpreg);
		
				tmpreg = CreateEmpty ();
				tmpreg.name = "Water";
				tmpreg.tileVisualPrefab = (GameObject)Resources.Load ("Tiles/TileWater");
				tmpreg.walkEffect = 0;
				tmpreg.isWalkable = false;
				tmpreg.mapColor.r = 0.000f;
				tmpreg.mapColor.g = 0.675f;
				tmpreg.mapColor.b = 0.969f;
				tmpreg.mapColor.a = 0.118f;
				tileTypes.Add (tmpreg);
		
				tmpreg = CreateEmpty ();
				tmpreg.name = "Swamp";
				tmpreg.tileVisualPrefab = (GameObject)Resources.Load ("Tiles/TileSwamp");
				tmpreg.walkEffect = 5;
				tmpreg.mapColor.r = 0.965f;
				tmpreg.mapColor.g = 0.635f;
				tmpreg.mapColor.b = 0.455f;
				tmpreg.mapColor.a = 0.094f;
				tileTypes.Add (tmpreg);
		
				tmpreg = CreateEmpty ();
				tmpreg.name = "Beach";
				tmpreg.tileVisualPrefab = (GameObject)Resources.Load ("Tiles/TileBeach");
				tmpreg.walkEffect = 1.1f;
				tmpreg.mapColor.r = 0.996f;
				tmpreg.mapColor.g = 0.945f;
				tmpreg.mapColor.b = 0.000f;
				tmpreg.mapColor.a = 0.125f;
				tileTypes.Add (tmpreg);
		
				tmpreg = CreateEmpty ();
				tmpreg.name = "Lava";
				tmpreg.tileVisualPrefab = (GameObject)Resources.Load ("Tiles/TileLava");
				tmpreg.walkEffect = 0;
				tmpreg.isWalkable = false;
				tmpreg.mapColor.r = 0.988f;
				tmpreg.mapColor.g = 0.118f;
				tmpreg.mapColor.b = 0.149f;
				tmpreg.mapColor.a = 0.118f;
				tileTypes.Add (tmpreg);
		
				tmpreg = CreateEmpty ();
				tmpreg.name = "Entrance";
				tmpreg.tileVisualPrefab = (GameObject)Resources.Load ("Tiles/TileDoor");
				tmpreg.walkEffect = 0;
				tmpreg.mapColor.r = 0.000f;
				tmpreg.mapColor.g = 0.000f;
				tmpreg.mapColor.b = 0.000f;
				tmpreg.mapColor.a = 0.024f;
				tileTypes.Add (tmpreg);
		}
	
		public int GetRegionWithColor (Color Search) {
				foreach (regions tmpreg in tileTypes) {
						if (tmpreg.mapColor == Search) {
								return tmpreg.Region_ID;	
						}
				}
				regions tmpreg2 = CreateEmpty ();
				Debug.LogError ("Unbekannte Map Farbe: R" + Search.r + " G" + Search.g + " B" + Search.b + " A" + Search.a + " !");								
				return tmpreg2.Region_ID;
		}
		public int GetRegionWithName (string Search) {
				foreach (regions tmpreg in tileTypes) {
						if (tmpreg.name == Search) {
								return tmpreg.Region_ID;	
						}
				}
				regions tmpreg2 = CreateEmpty ();
				Debug.LogError ("Unbekannter Region Name: " + Search + " !");								
				return tmpreg2.Region_ID;
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
								tiles [x, y] = GetRegionWithColor (FeldFarbe);
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
								regions tt = tileTypes [tiles [x, y] - 1];
								GameObject tmpobjct = (GameObject)Instantiate (tt.tileVisualPrefab, new Vector3 (x, y, 0), Quaternion.identity);
								tmpobjct.transform.parent = GameObject.Find ("Map").transform;
						}
				}
		}

}

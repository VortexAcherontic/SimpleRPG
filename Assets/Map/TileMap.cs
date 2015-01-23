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
	
	int map_abstand=30; // Sollte So Groß sein wie der Spawn Radius!
	int mapSizeX;
	int mapSizeY;

		regions CreateEmpty () {
				regions tmpreg = new regions ();
				tmpreg.Region_ID = tileTypes.Count + 1;
				tmpreg.name = "Void Tile";
				tmpreg.tileVisualPrefab = (GameObject)Resources.Load ("Tiles/TileVoid");
				tmpreg.isWalkable = true;
				tmpreg.walkEffect = 1;
				tmpreg.mapColor.r = 1f;
				tmpreg.mapColor.g = 1f;
				tmpreg.mapColor.b = 1f;
				tmpreg.mapColor.a = 1f;
				return	tmpreg;
		}

	void OnGUI() {
		// Ich will die farben sehen
		bool farbensehen = false;
		if (farbensehen) {
			int breite=300;
			GUILayout.BeginArea (new Rect(Screen.width-breite,0,breite,Screen.height)); {
				foreach (regions tmpregion in tileTypes) {
					GUILayout.BeginHorizontal();
					GUILayout.Label (""+tmpregion.name);
					GUILayout.Space (5);
					Color32 tmpcolor=tmpregion.mapColor;
					GUILayout.Label (""+tmpcolor.r);
					GUILayout.Label (""+tmpcolor.g);
					GUILayout.Label (""+tmpcolor.b);
					GUILayout.Label (""+tmpcolor.a);
					GUILayout.EndHorizontal();
				}
			}
			GUILayout.EndArea ();
		}
	}
	
		void Start () {
			regions tmpreg = CreateEmpty (); // Void Tile für Abstand zwischen den Karten
			tmpreg.isWalkable = false;
			tileTypes.Add (tmpreg);
		
			tmpreg = CreateEmpty ();
			tmpreg.name = "Portal";
			tmpreg.tileVisualPrefab = (GameObject)Resources.Load ("Tiles/TileDoor");
			tmpreg.walkEffect = 0f;
			tileTypes.Add (tmpreg);
		
				tmpreg = CreateEmpty ();
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
				tmpreg.name = "Castel";
				tmpreg.tileVisualPrefab = (GameObject)Resources.Load ("Tiles/TileCastel");
				tmpreg.walkEffect = 0;
				tmpreg.mapColor.r = 0.000f;
				tmpreg.mapColor.g = 0.000f;
				tmpreg.mapColor.b = 0.000f;
				tmpreg.mapColor.a = 0.024f;
				tileTypes.Add (tmpreg);
		
				tmpreg = CreateEmpty ();
				tmpreg.name = "Wall";
				tmpreg.tileVisualPrefab = (GameObject)Resources.Load ("Tiles/TileWall");
				tmpreg.walkEffect = 0;
				tmpreg.isWalkable = false;
				tmpreg.mapColor.r = 0.996f;
				tmpreg.mapColor.g = 0.000f;
				tmpreg.mapColor.b = 0.996f;
				tmpreg.mapColor.a = 0.125f;
				tileTypes.Add (tmpreg);
		}
	
		public int GetRegionWithColor (Color Search) {
				foreach (regions tmpreg in tileTypes) {
						if (tmpreg.mapColor == Search) {
								return tmpreg.Region_ID - 1;	
						}
				}
				Debug.LogError ("Unbekannte Map Farbe: R" + Search.r + " G" + Search.g + " B" + Search.b + " A" + Search.a + " !");								
				return 0;
		}
		public int GetRegionWithName (string Search) {
				foreach (regions tmpreg in tileTypes) {
						if (tmpreg.name == Search) {
								return tmpreg.Region_ID - 1;	
						}
				}
				Debug.LogError ("Unbekannter Region Name: " + Search + " !");								
				return 0;
		}
		public void GenerateMapData () {
				int start_x = map_abstand;
				int start_y = map_abstand;
				mapSizeX = start_x;
				mapSizeY = start_y;
		
				List<Texture2D> ListMap = gameObject.GetComponent<map> ().Maps;
				foreach (Texture2D AktlMap in ListMap) {
					mapSizeX += map_abstand + AktlMap.height;
					if (2*map_abstand + AktlMap.width>=mapSizeY) {
							mapSizeY += map_abstand + AktlMap.width;
					}
				}
				tiles = new int[mapSizeX, mapSizeY];
				int x, y;
				for (x = 0; x < mapSizeX; x++) {
						for (y = 0; y < mapSizeY; y++) {
								tiles [x, y] = 0;
						}
				}
		
				// Nun alle Bilder durchgehen
				foreach (Texture2D AktlMap in ListMap) {
			for (x = 0; x < AktlMap.height; x++) {
				for (y = 0; y < AktlMap.width; y++) {
									Color FeldFarbe = AktlMap.GetPixel (x, y);
										// Viele Stellen genau sollte das sein? Ich denke 3 muss reichen
										FeldFarbe.r = Mathf.Round (FeldFarbe.r * 1000) / 1000;
										FeldFarbe.g = Mathf.Round (FeldFarbe.g * 1000) / 1000;
										FeldFarbe.b = Mathf.Round (FeldFarbe.b * 1000) / 1000;
										FeldFarbe.a = Mathf.Round (FeldFarbe.a * 1000) / 1000;
										// Tiles Ziffern noch anpassen
										tiles [start_x+x, start_y+y] = GetRegionWithColor (FeldFarbe);
								}
						}
					start_x += map_abstand + AktlMap.height;
					//start_y += map_abstand + AktlMap.width;
				}
				CheckForPortals ();
				GenerateMapVisuals ();
		}
	
		void GenerateMapVisuals () {
				for (int x = 0; x < mapSizeX; x++) {
						for (int y = 0; y < mapSizeY; y++) {
								regions tt = tileTypes [tiles [x, y]];
								GameObject tmpobjct = (GameObject)Instantiate (tt.tileVisualPrefab, new Vector3 (x, y, 0), Quaternion.identity);
								tmpobjct.transform.parent = GameObject.Find ("Map").transform;
						}
				}
		}
	
		void CheckForPortals () {
				List<ObjTele> tmplist = GameObject.Find ("Main Camera").GetComponent<teleporter> ().Porter;
				foreach (ObjTele tmpobj in tmplist) {
					tiles [(int)tmpobj.vonpos.x,(int)tmpobj.vonpos.y] = GetRegionWithName ("Portal");
				}
		}

}

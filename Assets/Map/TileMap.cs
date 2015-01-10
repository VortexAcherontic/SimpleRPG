using UnityEngine;
using System.Collections;

public class TileMap : MonoBehaviour
{

		public TileType[] tileTypes;

		int[,] tiles;

		int mapSizeX = 100;
		int mapSizeY = 100;

		void Start ()
		{
				GenerateMapData ();
				GenerateMapVisuals ();
				
		}
		void GenerateMapData ()
		{
				tiles = new int[mapSizeX, mapSizeY];

				int x, y;

				for (x = 0; x < mapSizeX; x++) {
						for (y = 0; y < mapSizeY; y++) {
								tiles [x, y] = 1;
						}
				}

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

		}
		void GenerateMapVisuals ()
		{
				for (int x = 0; x < mapSizeX; x++) {
						for (int y = 0; y < mapSizeY; y++) {
								TileType tt = tileTypes [tiles [x, y]];
								Instantiate (tt.tileVisualPrefab, new Vector3 (x, y, 0), Quaternion.identity);
						}
				}
		}

}

﻿using UnityEngine;
using System.Collections;

public class GameTime : MonoBehaviour {
		GUI_Helper GUI_ZoD = new GUI_Helper ();
		// Umrechnung in Sekunden
		[HideInInspector]
		public  int
				Sekunde;
		[HideInInspector]
		public  int
				Minute;
		[HideInInspector]
		public  int
				Stunde;
		[HideInInspector]
		public  int
				Tag;
	
		public float GameMinuteInRealSekunden = 1;
		public float Zeit = 0;
	
		public bool Dev_GUITime = false;
	
		void Start () {
				Sekunde = 1;
				Minute = 60 * Sekunde;
				Stunde = 60 * Minute;
				Tag = 24 * Stunde;
		
		}
		
		void Update () {
				Zeit += (GameMinuteInRealSekunden * Minute) * Time.deltaTime;
				if (Zeit >= Tag) {
						Zeit -= Tag;
				}
		}
	
		void OnGUI () {
				if (Dev_GUITime) {
						string FormatedTime;
						float TmpTime = Zeit;
						int FormatedStunde = 0;
						while (TmpTime>=Stunde) {
								TmpTime -= Stunde;
								FormatedStunde++;
						}
						int FormatedMinute = 0;
						while (TmpTime>=Minute) {
								TmpTime -= Minute;
								FormatedMinute++;
						}
						int FormatedSekunde = 0;
						while (TmpTime>=Sekunde) {
								TmpTime -= Sekunde;
								FormatedSekunde++;
						}
						FormatedTime = FormatedStunde + ":" + FormatedMinute + ":" + FormatedSekunde;
						GUI_ZoD.Label (FormatedTime, 11, new Rect (1920 - 100, 0, 100, 20));
				}
		}
}

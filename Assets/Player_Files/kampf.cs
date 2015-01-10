using UnityEngine;
using System.Collections;

public class kampf : MonoBehaviour
{
		player p001;
		player p002;
		npc n001;
		//public bool inarena; besser in map?
		//monster m001;


		// Use this for initialization
		void Start ()
		{
				p001 = GameObject.Find ("Main Camera").GetComponent<player> ();
				p002 = GameObject.Find ("Main Camera").GetComponent<player> ();
				n001 = GameObject.Find ("Main Camera").GetComponent<npc> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
				/*
		 * if abfrage ob einer der beiden gegner keine HP mehr hat
		 * linksklick abfragen > angelegte waffe abfragen (waffen mit 1 2 3 wechseln zu melee range und mage) > danach schaden berechnen
		 */
		}

		/*magieschaden berechnen*/
		/*fernkampfschaden berechnen (ammo abziehen nicht vergessen)*/
		/*nahkampfschaden berechnen*/

		/*variable die gegnertyp festlegt?*/

		/*schaden gegenüber player berechnen durch NPC*/
		/*schaden gegenüber player berechnen durch Monster*/

		/*pvp lieber im arena modus? dann könnte man direkt ne funktion schreiben bei der beide player aufeinander einprügeln*/

}

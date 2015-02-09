using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class inventory : MonoBehaviour {

		public List<ItemData> Inventar = new List<ItemData> ();
		//List<items> templist = new List<items> ();
		public mainmenu gui;
		int a;
		player p001;
		string Datenbank_URL = "http://www.cards-of-destruction.com/SimpleRpg/";

		// Use this for initialization
		void Start () {
				p001 = GameObject.Find ("Main Camera").GetComponent<player> ();
				gui = GameObject.Find ("Main Camera").GetComponent<mainmenu> ();
		}

		int anzeige_kat = 0;
		Vector2 scroller = new Vector2 ();
		Rect Scrollbereich; // Weil ka wie sonst XD
	
		int ItemCount (ItemData obj) {
				a = 0;
				//templist = Inventar.FindAll (FindItem);
				foreach (ItemData c_obj in Inventar) {
						if (c_obj.Name == obj.Name) {
								a++;
						}
				}
				//a = templist.Count;
				return a;
		}
	
		float savecooldown = 20.0f;
		float savetimer = 0.0f;
	
		void OnGUI () {
				if (gui.showequip == true) {
						gui.showinv = false;
				}
		   
				Rect tmp_anzeige = new Rect (Screen.width / 2 - 500, Screen.height / 2 - 200, 1000, 400);
				Rect Spalte;
				Rect Zeile1 = new Rect (0, 0, Scrollbereich.width, 20);
				Rect AnzeigeScrollView = new Rect (0, 0 + 50, tmp_anzeige.width, tmp_anzeige.height - 40);
				Scrollbereich.position = new Vector2 (0, 0);
				Scrollbereich.width = AnzeigeScrollView.width - 50;

				if (gui.showinv) {
						
						GUILayout.BeginArea (tmp_anzeige);
						{
								GUI.Box (new Rect (0, 0, tmp_anzeige.width, tmp_anzeige.height), "Inventar");
								GUILayout.Space (20);								
								if ((GUI.Button (new Rect (tmp_anzeige.width - 40, 0, 40, 20), "Quit")) || (Input.GetKey (KeyCode.Escape))) {
										gui.showinv = false;
								}
								if (GUI.Button (new Rect (tmp_anzeige.width - 190, 0, 40, 20), "Save")) {
										if (savetimer <= 0) {
												StartCoroutine (p001.save (Datenbank_URL, p001.Player_ID));
												savetimer = savecooldown;
										} else {
												Debug.Log ("noch nicht zum speichern bereit");
										}
								}
								if (GUI.Button (new Rect (tmp_anzeige.width - 150, 0, 110, 20), "Swap to Equipped Items")) {
										gui.showinv = false;
										gui.showequip = true;
								}
								GUILayout.BeginHorizontal ();
								if (GUILayout.Button ("Meleeweapons")) {
										anzeige_kat = 1;
								}
								if (GUILayout.Button ("Rangeweapons")) {
										anzeige_kat = 2;
								}
								if (GUILayout.Button ("Armor")) {
										anzeige_kat = 3;
								}
								if (GUILayout.Button ("Potions")) {
										anzeige_kat = 4;
								}
								if (GUILayout.Button ("Ammo")) {
										anzeige_kat = 5;
								}
								if (GUILayout.Button ("Wearables")) {
										anzeige_kat = 6;
								}
								GUILayout.EndHorizontal ();
								GUILayout.FlexibleSpace ();
						}
						
						scroller = GUI.BeginScrollView (AnzeigeScrollView, scroller, Scrollbereich);
						
						switch (anzeige_kat) {
								case 0:
										break;
								case 1:
										foreach (ItemData dieseitem in Inventar) {
					
												if (dieseitem.Type == ItemType.weapon_meele) {
														GUILayout.BeginHorizontal ();
														Spalte = new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width / 7, Zeile1.height);
														GUI.Label (Spalte, dieseitem.Name);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Items in inventory: " + ItemCount (dieseitem));
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Physical Damage: " + dieseitem.PhyAttack);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Magical Damage: " + dieseitem.MagAttack);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Weight: " + dieseitem.Weigth + " kg");
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														if (GUI.Button (Spalte, "Drop")) {
																sub (dieseitem);
														}
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														if (GUI.Button (Spalte, "Equip")) {
																p001.equip (dieseitem);
																return;
														}
														Zeile1.position = new Vector2 (0, Zeile1.position.y + 20);
						
														GUILayout.EndHorizontal ();
												}
										}
										break;
								case 2:
										foreach (ItemData dieseitem in Inventar) {
												if (dieseitem.Type == ItemType.weapon_range) {
														GUILayout.BeginHorizontal ();
														Spalte = new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width / 7, Zeile1.height);
														GUI.Label (Spalte, dieseitem.Name);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Items in inventory: " + ItemCount (dieseitem));
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Physical Damage: " + dieseitem.PhyAttack);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Magical Damage: " + dieseitem.MagAttack);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Weight: " + dieseitem.Weigth + " kg");
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														if (GUI.Button (Spalte, "Drop")) {
																sub (dieseitem);
														}
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														if (GUI.Button (Spalte, "Equip")) {
																p001.equip (dieseitem);
																return;
														}
														Zeile1.position = new Vector2 (0, Zeile1.position.y + 20);
						
														GUILayout.EndHorizontal ();
												}
										}
										break;
								case 3:
										foreach (ItemData dieseitem in Inventar) {
												if ((dieseitem.Type == ItemType.armor_feet) || 
														(dieseitem.Type == ItemType.armor_hand) ||
														(dieseitem.Type == ItemType.armor_head) ||
														(dieseitem.Type == ItemType.armor_leg) ||
														(dieseitem.Type == ItemType.armor_torso)) {
														GUILayout.BeginHorizontal ();
														Spalte = new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width / 7, Zeile1.height);
														GUI.Label (Spalte, dieseitem.Name);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Items in inventory: " + ItemCount (dieseitem));
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Physical Defense: " + dieseitem.PhyArmor);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Magical Defense: " + dieseitem.MagArmor);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Weight: " + dieseitem.Weigth + " kg");
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														if (GUI.Button (Spalte, "Drop")) {
																sub (dieseitem);
														}
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														if (GUI.Button (Spalte, "Equip")) {
																p001.equip (dieseitem);
																return;
														}
														Zeile1.position = new Vector2 (0, Zeile1.position.y + 20);
						
														GUILayout.EndHorizontal ();
												}
										}
										break;
								case 4:
										foreach (ItemData dieseitem in Inventar) {
												if (dieseitem.Type == ItemType.potion) {
														GUILayout.BeginHorizontal ();
														Spalte = new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width / 6, Zeile1.height);
														GUI.Label (Spalte, dieseitem.Name);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Items in inventory: " + ItemCount (dieseitem));
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Effect: " + dieseitem.Effect + " " + dieseitem.EffectType.ToString ());
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Weight: " + dieseitem.Weigth + " kg");
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														if (GUI.Button (Spalte, "Drop")) {
																sub (dieseitem);
														}
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														if (GUI.Button (Spalte, "Use")) {
																Use (dieseitem);
																
														}
														Zeile1.position = new Vector2 (0, Zeile1.position.y + 20);
						
														GUILayout.EndHorizontal ();
												}
										}
										break;
								case 5:
										foreach (ItemData dieseitem in Inventar) {
												if (dieseitem.Type == ItemType.utility) {
														GUILayout.BeginHorizontal ();
														Spalte = new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width / 7, Zeile1.height);
														GUI.Label (Spalte, dieseitem.Name);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Items in inventory: " + ItemCount (dieseitem));
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Capacity: " + dieseitem.MaxCapacity); // ?
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Weight: " + dieseitem.Weigth + " kg");
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														if (GUI.Button (Spalte, "Drop")) {
																sub (dieseitem);
														}
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														if (GUI.Button (Spalte, "Equip")) {
																p001.equip (dieseitem);
																return;
														}
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														if (GUI.Button (Spalte, "Show content.")) {
																//ShowContentfunktion
														}
														Zeile1.position = new Vector2 (0, Zeile1.position.y + 20);
						
														GUILayout.EndHorizontal ();
												}
										}
										break;
								case 6:
										foreach (ItemData dieseitem in Inventar) {
												if (dieseitem.Type == ItemType.accessorie) {
														GUILayout.BeginHorizontal ();
														Spalte = new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width / 6, Zeile1.height);
														GUI.Label (Spalte, dieseitem.Name);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Items in inventory: " + ItemCount (dieseitem));
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Effect: " + dieseitem.Effect + " " + dieseitem.EffectType.ToString ());
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Weight: " + dieseitem.Weigth + " kg");
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														if (GUI.Button (Spalte, "Drop")) {
																sub (dieseitem);
														}
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														if (GUI.Button (Spalte, "Equip")) {
																p001.equip (dieseitem);
																return;
														}
														Zeile1.position = new Vector2 (0, Zeile1.position.y + 20);
						
														GUILayout.EndHorizontal ();
												}
										}
										break;
				
						}
						GUI.EndScrollView ();
						Scrollbereich.height = Zeile1.position.y + Zeile1.height;
						GUILayout.EndArea ();
				}
				savetimer -= Time.deltaTime;
				if (gui.showequip) {
						GUILayout.BeginArea (tmp_anzeige);
						{

								GUI.Box (new Rect (0, 0, tmp_anzeige.width, tmp_anzeige.height), "Equip");
								GUILayout.Space (20);								
								if ((GUI.Button (new Rect (tmp_anzeige.width - 40, 0, 40, 20), "Quit")) || (Input.GetKey (KeyCode.Escape))) {
										gui.showequip = false;
								}
								if (GUI.Button (new Rect (tmp_anzeige.width - 150, 0, 110, 20), "Swap to Iventory")) {
										gui.showinv = true;
										gui.showequip = false;
								}
								Zeile1.position = new Vector2 (Zeile1.position.x, Zeile1.position.y + 20);

								foreach (ItemData dieseitem in p001.Equip) {

										GUILayout.BeginHorizontal ();
										Spalte = new Rect (Zeile1.position.x, Zeile1.position.y, Zeile1.width / 7, Zeile1.height);
										GUI.Label (Spalte, dieseitem.Name);
										Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
										GUI.Label (Spalte, "Items in inventory: " + ItemCount (dieseitem));
										Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
										switch (dieseitem.Type) {
												case ItemType.weapon_meele:
												case ItemType.weapon_range:
														GUI.Label (Spalte, "Physical Damage: " + dieseitem.PhyAttack);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Magical Damage: " + dieseitem.MagAttack);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														break;
												case ItemType.armor_feet:
												case ItemType.armor_hand:
												case ItemType.armor_head:
												case ItemType.armor_leg:
												case ItemType.armor_torso:
														GUI.Label (Spalte, "Physical Defense: " + dieseitem.PhyArmor);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "Magical Defense: " + dieseitem.MagArmor);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														break;
												case ItemType.accessorie:
												case ItemType.potion:
														GUI.Label (Spalte, "Effect: " + dieseitem.Effect);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "" + dieseitem.EffectType.ToString ());
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														break;
												case ItemType.utility:
														GUI.Label (Spalte, "Capacity: " + dieseitem.Capacity + " / " + dieseitem.MaxCapacity);
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														GUI.Label (Spalte, "");
														Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
														break;

										}

										GUI.Label (Spalte, "Weight: " + dieseitem.Weigth + " kg");
										Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
										if (GUI.Button (Spalte, "Drop")) {
												sub (dieseitem);
										}
										Spalte = new Rect (Spalte.position.x + Spalte.width, Spalte.position.y, Spalte.width, Spalte.height);
										if (GUI.Button (Spalte, "Unequip")) {
												p001.unequip (dieseitem);
												return;
										}
										Zeile1.position = new Vector2 (0, Zeile1.position.y + 20);
					
										GUILayout.EndHorizontal ();
								}
						}
						GUILayout.EndArea ();
				}
		}

		public void add (ItemData diesesitem) {
				Inventar.Add (diesesitem);
				p001.BerechneMovmentDelay ();
		}

		public void sub (ItemData diesesitem) {
				Inventar.Remove (diesesitem);
				p001.BerechneMovmentDelay ();
		}

		public void Use (ItemData diesesitem) {
				if (diesesitem.EffectType == EffectType.Mana) {
						p001.mana += diesesitem.Effect;
				}
				if (diesesitem.EffectType == EffectType.Health) {
						p001.hp += diesesitem.Effect;
				}
				if (p001.hp >= p001.maxhp) {
						p001.hp = p001.maxhp;
				}
				if (p001.mana >= p001.maxmana) {
						p001.mana = p001.maxmana;
				}
				sub (diesesitem);
		}
	
		// Update is called once per frame
		void Update () {
	
		}
}

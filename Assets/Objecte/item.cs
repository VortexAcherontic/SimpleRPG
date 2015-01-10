using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum itemtype
{
		Nahkampf,
		Fernkampf,
		Kopf_Rüstung,
		Torso_Rüstung,
		Beine_Rüstung,
		Stiefel_Rüstung,
		Handschuhe_Rüstung,
		Tränke,
		Munition,
		anglegbares,
		utility

}
public class items_s
{
		public string name;
		public itemtype type;
		public int price;
		public int mag_dmg;
		public int phy_dmg;
		public int stock;
		public int refill_mod;
		public int phy_arm;
		public int mag_arm;
		public int refill_hp;
		public int refill_mana;
		public float gewicht;
		public int effect;
		public string effecttyp;
		public int ammo_ammount;
		public int addmaxhp;
		public int addmaxmana;
		public int addpwr;
		public int addagility;
		//public List<items> capacity;//muss noch irgendwas hin oder auch nicht
		public int maxcapacity;
}
public class items:items_s
{
		/*public string name;
		public itemtype type;
		public int price;
		public int mag_dmg;
		public int phy_dmg;
		public int stock;
		public int refill_mod;
		public int phy_arm;
		public int mag_arm;
		public int refill_hp;
		public int refill_mana;
		public float gewicht;
		public int effect;
		public string effecttyp;
		public int ammo_ammount;
		public int addmaxhp;
		public int addmaxmana;
		public int addpwr;
		public int addagility;*/
		public List<items_s> capacity;
		//public int maxcapacity;
}

public class item : MonoBehaviour
{
		public List<items> Item_List = new List<items> ();
		// Use this for initialization
		items leeres_item ()
		{
				items tmp_item = new items ();
				tmp_item.name = "Standartwert";
				tmp_item.type = itemtype.Nahkampf;
				tmp_item.price = 0;
				tmp_item.mag_dmg = 0;
				tmp_item.phy_dmg = 0;
				tmp_item.stock = 10;
				tmp_item.refill_mod = 1;
				tmp_item.phy_arm = 0;
				tmp_item.mag_arm = 0;
				tmp_item.refill_hp = 0;
				tmp_item.refill_mana = 0;
				tmp_item.gewicht = 1;
				tmp_item.effect = 0;
				tmp_item.effecttyp = "none";
				tmp_item.ammo_ammount = 0;
				tmp_item.addmaxhp = 0;
				tmp_item.addmaxmana = 0;
				tmp_item.addpwr = 0;
				tmp_item.addagility = 0;
				tmp_item.maxcapacity = 0;
				return tmp_item;
		}
		public items item_mit_name (string itembez)
		{
				foreach (items obj in Item_List) {
						if (obj.name == itembez) {
								return obj;
						}
				}
				return leeres_item ();
		}
		void Start ()
		{
				items tmp_item = new items ();

				//Nahkampfwaffen

				tmp_item = leeres_item ();
				tmp_item.name = "Dagger";
				tmp_item.type = itemtype.Nahkampf;
				tmp_item.price = 10;
				tmp_item.phy_dmg = 25;
				tmp_item.gewicht = 1;
				Item_List.Add (tmp_item);

				tmp_item = leeres_item ();
				tmp_item.name = "Gladius";
				tmp_item.type = itemtype.Nahkampf;
				tmp_item.price = 25;
				tmp_item.phy_dmg = 35;
				tmp_item.gewicht = 1;
				Item_List.Add (tmp_item);

				tmp_item = leeres_item ();
				tmp_item.name = "Saber";
				tmp_item.type = itemtype.Nahkampf;
				tmp_item.price = 50;
				tmp_item.phy_dmg = 50;
				tmp_item.gewicht = 1;
				Item_List.Add (tmp_item);

				tmp_item = leeres_item ();
				tmp_item.name = "Longsword";
				tmp_item.type = itemtype.Nahkampf;
				tmp_item.price = 100;
				tmp_item.phy_dmg = 75;
				tmp_item.gewicht = 1;
				Item_List.Add (tmp_item);

				tmp_item = leeres_item ();
				tmp_item.name = "Broadsword";
				tmp_item.type = itemtype.Nahkampf;
				tmp_item.price = 125;
				tmp_item.phy_dmg = 90;
				tmp_item.gewicht = 1;
				Item_List.Add (tmp_item);

				tmp_item = leeres_item ();
				tmp_item.name = "Mastersword";
				tmp_item.type = itemtype.Nahkampf;
				tmp_item.price = 1000;
				tmp_item.mag_dmg = 50;
				tmp_item.phy_dmg = 150;
				tmp_item.gewicht = 1;
				Item_List.Add (tmp_item);

				// Fernkampfwaffen

				tmp_item = leeres_item ();
				tmp_item.name = "Shortbow";
				tmp_item.type = itemtype.Fernkampf;
				tmp_item.price = 100;
				tmp_item.phy_dmg = 50;
				tmp_item.gewicht = 1;
				Item_List.Add (tmp_item);

				tmp_item = leeres_item ();
				tmp_item.name = "Longbow";
				tmp_item.type = itemtype.Fernkampf;
				tmp_item.price = 250;
				tmp_item.phy_dmg = 125;
				tmp_item.gewicht = 1;
				Item_List.Add (tmp_item);

				tmp_item = leeres_item ();
				tmp_item.name = "Crossbow";
				tmp_item.type = itemtype.Fernkampf;
				tmp_item.price = 500;
				tmp_item.phy_dmg = 250;		
				tmp_item.gewicht = 3;
				Item_List.Add (tmp_item);

				//Tränke

				tmp_item = leeres_item ();
				tmp_item.type = itemtype.Tränke;
				tmp_item.name = "Small Potion of Healing";
				tmp_item.price = 10;
				tmp_item.refill_hp = 500;
				tmp_item.effect = tmp_item.refill_hp;
				tmp_item.effecttyp = "h";
				tmp_item.gewicht = 0.1f;
				tmp_item.stock = 50;
				Item_List.Add (tmp_item);

				tmp_item = leeres_item ();
				tmp_item.type = itemtype.Tränke;
				tmp_item.name = "Large Potion of Healing";
				tmp_item.price = 50;
				tmp_item.refill_hp = 2500;
				tmp_item.effect = tmp_item.refill_hp;
				tmp_item.effecttyp = "h";
				tmp_item.gewicht = 0.5f;
				tmp_item.stock = 50;
				Item_List.Add (tmp_item);

				tmp_item = leeres_item ();
				tmp_item.type = itemtype.Tränke;
				tmp_item.name = "Small Potion of Mana";
				tmp_item.price = 10;
				tmp_item.refill_mana = 500;
				tmp_item.effect = tmp_item.refill_mana;
				tmp_item.effecttyp = "m";
				tmp_item.gewicht = 0.1f;
				tmp_item.stock = 50;
				Item_List.Add (tmp_item);

				tmp_item = leeres_item ();
				tmp_item.type = itemtype.Tränke;
				tmp_item.name = "Large Potion of Mana";
				tmp_item.price = 50;
				tmp_item.refill_mana = 2500;
				tmp_item.effect = tmp_item.refill_mana;
				tmp_item.effecttyp = "m";
				tmp_item.gewicht = 0.5f;
				tmp_item.stock = 50;
				Item_List.Add (tmp_item);

				//Rüstung
				//Leather

				tmp_item = leeres_item ();
				tmp_item.type = itemtype.Kopf_Rüstung;
				tmp_item.name = "Leather Helmet";
				tmp_item.price = 25;
				tmp_item.phy_arm = 20;
				tmp_item.mag_arm = 10;
				tmp_item.gewicht = 1.5f;
				Item_List.Add (tmp_item);

				tmp_item = leeres_item ();
				tmp_item.type = itemtype.Torso_Rüstung;
				tmp_item.name = "Leather Chestplate";
				tmp_item.price = 50;
				tmp_item.phy_arm = 80;
				tmp_item.mag_arm = 30;
				tmp_item.gewicht = 5;
				Item_List.Add (tmp_item);

				tmp_item = leeres_item ();
				tmp_item.type = itemtype.Beine_Rüstung;
				tmp_item.name = "Leather Cuisse";
				tmp_item.price = 30;
				tmp_item.phy_arm = 22;
				tmp_item.mag_arm = 10;
				tmp_item.gewicht = 1.5f;
				Item_List.Add (tmp_item);

				tmp_item = leeres_item ();
				tmp_item.type = itemtype.Stiefel_Rüstung;
				tmp_item.name = "Leather Boots";
				tmp_item.price = 20;
				tmp_item.phy_arm = 15;
				tmp_item.mag_arm = 5;
				tmp_item.gewicht = 0.5f;
				Item_List.Add (tmp_item);

				tmp_item = leeres_item ();
				tmp_item.type = itemtype.Handschuhe_Rüstung;
				tmp_item.name = "Leather Gloves";
				tmp_item.price = 20;
				tmp_item.phy_arm = 15;
				tmp_item.mag_arm = 5;
				tmp_item.gewicht = 0.5f;
				Item_List.Add (tmp_item);

				//Iron
		
				tmp_item = leeres_item ();
				tmp_item.type = itemtype.Kopf_Rüstung;
				tmp_item.name = "Iron Helmet";
				tmp_item.price = 50;
				tmp_item.phy_arm = 40;
				tmp_item.mag_arm = 20;
				tmp_item.gewicht = 5.5f;
				Item_List.Add (tmp_item);
		
				tmp_item = leeres_item ();
				tmp_item.type = itemtype.Torso_Rüstung;
				tmp_item.name = "Iron Chestplate";
				tmp_item.price = 250;
				tmp_item.phy_arm = 300;
				tmp_item.mag_arm = 50;
				tmp_item.gewicht = 9;
				Item_List.Add (tmp_item);
		
				tmp_item = leeres_item ();
				tmp_item.type = itemtype.Beine_Rüstung;
				tmp_item.name = "Iron Cuisse";
				tmp_item.price = 60;
				tmp_item.phy_arm = 44;
				tmp_item.mag_arm = 20;
				tmp_item.gewicht = 4.5f;
				Item_List.Add (tmp_item);
		
				tmp_item = leeres_item ();
				tmp_item.type = itemtype.Stiefel_Rüstung;
				tmp_item.name = "Iron Boots";
				tmp_item.price = 50;
				tmp_item.phy_arm = 45;
				tmp_item.mag_arm = 15;
				tmp_item.gewicht = 1.5f;
				Item_List.Add (tmp_item);
		
				tmp_item = leeres_item ();
				tmp_item.type = itemtype.Handschuhe_Rüstung;
				tmp_item.name = "Iron Gloves";
				tmp_item.price = 40;
				tmp_item.phy_arm = 30;
				tmp_item.mag_arm = 15;
				tmp_item.gewicht = 1;
				Item_List.Add (tmp_item);

				//Magieresistenzrüstung

				tmp_item = leeres_item ();
				tmp_item.type = itemtype.Kopf_Rüstung;
				tmp_item.name = "Witcher's Cap";
				tmp_item.price = 75;
				tmp_item.phy_arm = 20;
				tmp_item.mag_arm = 100;
				tmp_item.gewicht = 1;
				Item_List.Add (tmp_item);
		
				tmp_item = leeres_item ();
				tmp_item.type = itemtype.Torso_Rüstung;
				tmp_item.name = "Witcher's Robe";
				tmp_item.price = 150;
				tmp_item.phy_arm = 40;
				tmp_item.mag_arm = 300;
				tmp_item.gewicht = 3;
				Item_List.Add (tmp_item);
		
				tmp_item = leeres_item ();
				tmp_item.type = itemtype.Beine_Rüstung;
				tmp_item.name = "Witcher's Tunic";
				tmp_item.price = 90;
				tmp_item.phy_arm = 22;
				tmp_item.mag_arm = 100;
				tmp_item.gewicht = 1;
				Item_List.Add (tmp_item);
		
				tmp_item = leeres_item ();
				tmp_item.type = itemtype.Stiefel_Rüstung;
				tmp_item.name = "Witcher's Slippers";
				tmp_item.price = 60;
				tmp_item.phy_arm = 15;
				tmp_item.mag_arm = 50;
				tmp_item.gewicht = 0.2f;
				Item_List.Add (tmp_item);
		
				tmp_item = leeres_item ();
				tmp_item.type = itemtype.Handschuhe_Rüstung;
				tmp_item.name = "Witcher's Gloves";
				tmp_item.price = 60;
				tmp_item.phy_arm = 15;
				tmp_item.mag_arm = 25;
				tmp_item.gewicht = 0.2f;
				Item_List.Add (tmp_item);

				//Kristalle

				tmp_item = leeres_item ();
				tmp_item.type = itemtype.anglegbares;
				tmp_item.name = "Health Crystal";
				tmp_item.price = 500;
				tmp_item.addmaxhp = 500;
				tmp_item.effect = tmp_item.addmaxhp;
				tmp_item.effecttyp = "h";
				tmp_item.gewicht = 0.3f;
				tmp_item.refill_mod = 0;
				Item_List.Add (tmp_item);

				tmp_item = leeres_item ();
				tmp_item.type = itemtype.anglegbares;
				tmp_item.name = "Mana Crystal";
				tmp_item.price = 500;
				tmp_item.addmaxmana = 500;
				tmp_item.effect = tmp_item.addmaxmana;
				tmp_item.effecttyp = "m";
				tmp_item.gewicht = 0.3f;
				tmp_item.refill_mod = 0;
				Item_List.Add (tmp_item);

				tmp_item = leeres_item ();
				tmp_item.type = itemtype.anglegbares;
				tmp_item.name = "Strenght Crystal";
				tmp_item.price = 50;
				tmp_item.addpwr = 500;
				tmp_item.effect = tmp_item.addpwr;
				tmp_item.effecttyp = "p";
				tmp_item.gewicht = 0.3f;
				tmp_item.refill_mod = 0;
				Item_List.Add (tmp_item);

				tmp_item = leeres_item ();
				tmp_item.type = itemtype.anglegbares;
				tmp_item.name = "Agility Crystal";
				tmp_item.price = 50;
				tmp_item.addagility = 500;
				tmp_item.effect = tmp_item.addagility;
				tmp_item.effecttyp = "a";
				tmp_item.gewicht = 0.3f;
				tmp_item.refill_mod = 0;
				Item_List.Add (tmp_item);

				//weitere wearables

				tmp_item = leeres_item ();
				tmp_item.type = itemtype.utility;
				tmp_item.name = "Small Quiver";
				tmp_item.price = 5;
				tmp_item.maxcapacity = 50;
				//liste mit hinzugefügter munition
				tmp_item.gewicht = 2;
				Item_List.Add (tmp_item);

				tmp_item = leeres_item ();
				tmp_item.type = itemtype.utility;
				tmp_item.name = "Large Quiver";
				tmp_item.price = 5;
				tmp_item.maxcapacity = 50;
				//liste mit hinzugefügter munition
				tmp_item.gewicht = 2;
				Item_List.Add (tmp_item);

				//Munition

				tmp_item = leeres_item ();
				tmp_item.type = itemtype.Munition;
				tmp_item.name = "Arrow";
				tmp_item.price = 15;
				tmp_item.phy_dmg = 10;
				tmp_item.ammo_ammount = 20;
				tmp_item.gewicht = 0.1f;
				Item_List.Add (tmp_item);

				tmp_item = leeres_item ();
				tmp_item.type = itemtype.Munition;
				tmp_item.name = "Bolt";
				tmp_item.price = 30;
				tmp_item.phy_dmg = 30;
				tmp_item.ammo_ammount = 10;
				tmp_item.gewicht = 0.3f;
				Item_List.Add (tmp_item);

				tmp_item = leeres_item ();
				tmp_item.type = itemtype.Munition;
				tmp_item.name = "Poisened Arrow";
				tmp_item.price = 25;
				tmp_item.phy_dmg = 10;
				tmp_item.mag_dmg = 20;
				tmp_item.ammo_ammount = 10;
				tmp_item.gewicht = 0.2f;
				Item_List.Add (tmp_item);

		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}

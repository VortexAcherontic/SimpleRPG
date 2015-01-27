using UnityEngine;
using UnityEditor;
using System.Collections;

/*
[CustomPropertyDrawer(typeof(ItemStats))]
public class ItemStatsDrawer: PropertyDrawer {
		public override void OnGUI (Rect pos, SerializedProperty prop, GUIContent label) {

	}
}
*/

public class MakeItemData {
		[MenuItem("DataTools/Create/ItemList")]
		public static void CreateItemData () {
				ItemDataList asset = ScriptableObject.CreateInstance<ItemDataList> ();
				AssetDatabase.CreateAsset (asset, "Assets/DataTools/Item/Resources/Items.asset");
				AssetDatabase.SaveAssets ();
		
				EditorUtility.FocusProjectWindow ();
				Selection.activeObject = asset;
		}
}
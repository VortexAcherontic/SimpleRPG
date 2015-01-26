using UnityEngine;
using UnityEditor;
using System.Collections;

/*
[CustomPropertyDrawer(typeof(CreatureOriginStats))]
public class CreatureOriginStatsDrawer: PropertyDrawer {
		public override void OnGUI (Rect pos, SerializedProperty prop, GUIContent label) {
				DrawDefaultInspector();
		}
}
*/

/*
[CustomPropertyDrawer(typeof(CreatureStats))]
public class CreatureStatsDrawer: PropertyDrawer {
		public override void OnGUI (Rect pos, SerializedProperty prop, GUIContent label) {

	}
}
*/

public class MakeCreatureData {
		[MenuItem("DataTools/Create/CreatureList")]
		public static void CreateCreatureData () {
				CreatureDataList asset = ScriptableObject.CreateInstance<CreatureDataList> ();
				AssetDatabase.CreateAsset (asset, "Assets/DataTools/Creature/Resources/Creatures.asset");
				AssetDatabase.SaveAssets ();
		
				EditorUtility.FocusProjectWindow ();
				Selection.activeObject = asset;
		}
}
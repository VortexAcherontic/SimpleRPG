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

public class MakeQuestData {
		[MenuItem("DataTools/Create/QuestList")]
		public static void CreateCreatureData () {
				QuestDataList asset = ScriptableObject.CreateInstance<QuestDataList> ();
				AssetDatabase.CreateAsset (asset, "Assets/DataTools/Quest/Resources/Quest.asset");
				AssetDatabase.SaveAssets ();
		
				EditorUtility.FocusProjectWindow ();
				Selection.activeObject = asset;
		}
}
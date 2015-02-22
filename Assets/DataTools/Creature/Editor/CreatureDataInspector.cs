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

public class MakeStatusData {
		[MenuItem("DataTools/Create/StatusList")]
		public static void CreateStatusData () {
				StatusDataList asset = ScriptableObject.CreateInstance<StatusDataList> ();
				AssetDatabase.CreateAsset (asset, "Assets/DataTools/Creature/Resources/Status.asset");
				AssetDatabase.SaveAssets ();
		
				EditorUtility.FocusProjectWindow ();
				Selection.activeObject = asset;
		}
}

public class MakeSkillsData {
		[MenuItem("DataTools/Create/SkillList")]
		public static void CreateSkillData () {
				SkillsDataList asset = ScriptableObject.CreateInstance<SkillsDataList> ();
				AssetDatabase.CreateAsset (asset, "Assets/DataTools/Creature/Resources/Skill.asset");
				AssetDatabase.SaveAssets ();
		
				EditorUtility.FocusProjectWindow ();
				Selection.activeObject = asset;
		}
}
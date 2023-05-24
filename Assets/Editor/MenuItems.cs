
using UnityEditor;


public static class MenuItems {
	/// <summary>
	/// �S�A�Z�b�g�� serialized version �𖾎��I�ɍX�V������
	/// </summary>
	[MenuItem("Custom/Verify to update Assets")]
	public static void VerifiedAllAssets() {
		var guids = AssetDatabase.FindAssets("t:Object", new[] { "Assets" });
		foreach (var guid in guids) {
			var path = AssetDatabase.GUIDToAssetPath(guid);
			var importer = AssetImporter.GetAtPath(path);
			EditorUtility.SetDirty(importer);
			importer.SaveAndReimport();
		}
	}
}
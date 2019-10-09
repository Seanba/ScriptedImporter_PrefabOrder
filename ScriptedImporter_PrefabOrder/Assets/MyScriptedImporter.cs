using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.AssetImporters;

[ScriptedImporter(1, "xxx")]
public class MyScriptedImporter : ScriptedImporter
{
    public override void OnImportAsset(AssetImportContext ctx)
    {
        // Create our main object
        var name = Path.GetFileNameWithoutExtension(ctx.assetPath);
        var main = new GameObject(name);
        ctx.AddObjectToAsset(name, main);
        ctx.SetMainObject(main);

        // Instantiate a prefab to add to our main object
        // Bug: The object heirarchy of children in our prefab is not preserved. Instead, the children are ordered alphabetically.
        var path = "Assets/TestPrefab.prefab";
        var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        var instance = PrefabUtility.InstantiatePrefab(prefab, main.transform) as GameObject;
        ctx.DependsOnSourceAsset(path);
    }
}


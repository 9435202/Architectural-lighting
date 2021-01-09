using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class CreateAssets
{
   // [MenuItem("Assets/Create/Create assets")]
    public static void CreateAsset()
    {
        //ScriptableObjectUtility.CreateAsset<BeamData>();
    }
}

public static class ScriptableObjectUtility
{
#if UNITY_EDITOR
    public static void CreateAsset<T>() where T : ScriptableObject
    {
        T asset = ScriptableObject.CreateInstance<T>();
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if( string.IsNullOrEmpty( path ) )
        {
            Debug.LogError("Not select files, select files first! ");
            return;
        }
        else if (!string.IsNullOrEmpty(Path.GetExtension(path)))
        {
            path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
        }
        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New" + typeof(T).ToString() + ".asset");
        AssetDatabase.CreateAsset(asset, assetPathAndName);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
#endif
}

 public class CreateAsset 
 {
#if UNITY_EDITOR
     [MenuItem("Assets/Create/CreateAsset")]
     public static void GenAsset()
     {
         string path = AssetDatabase.GetAssetPath(Selection.activeObject);
         string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/data" + ".asset");
         BeamData t = new BeamData();
         AssetDatabase.CreateAsset(t, assetPathAndName);
         AssetDatabase.SaveAssets();
         AssetDatabase.Refresh();
     }
#endif
 }
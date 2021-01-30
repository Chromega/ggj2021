using UnityEngine;
using UnityEditor;
using System;

public class UnitConversionOverride : AssetPostprocessor
{
    void OnPreprocessModel()
    {
        ModelImporter importer = assetImporter as ModelImporter;
        String name = importer.assetPath.ToLower();
        if (name.Substring(name.Length - 4, 4) == ".fbx")
        {
            importer.useFileUnits = false;
            importer.useFileScale = false;
        }
    }
}
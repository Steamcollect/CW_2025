using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MaterialVariantCreator : EditorWindow
{
    [MenuItem("Tools/Material Variant Creator")]
    public static void ShowWindow()
    {
        GetWindow<MaterialVariantCreator>("Material Variant Creator");
    }

    // Matériel de base sur lequel on va se baser pour créer des variants
    private SSO_MaterialVariantCreatorData data;

    // L'objet sérialisé qui permet d'éditer `data` via PropertyField
    private SerializedObject serializedData;

    // Les propriétés qu'on va afficher
    private SerializedProperty baseMaterialProp;
    private SerializedProperty spritesProp;
    private SerializedProperty saveFolderProp;
    
    private string valueOverride = "_MainTex";

    private void OnEnable()
    {
        // Crée un ScriptableObject en mémoire (non sauvegardé)
        data = CreateInstance<SSO_MaterialVariantCreatorData>();

        // On prépare la sérialisation
        serializedData = new SerializedObject(data);
        baseMaterialProp = serializedData.FindProperty("baseMaterial");
        spritesProp      = serializedData.FindProperty("sprites");
        saveFolderProp   = serializedData.FindProperty("saveFolder");
    }

    private void OnDisable()
    {
        DestroyImmediate(data);
    }

    void OnGUI()
    {
        serializedData.Update();

        GUILayout.Label("Création de Variants de Matériaux", EditorStyles.boldLabel);

        // On affiche chaque propriété
        EditorGUILayout.PropertyField(baseMaterialProp, new GUIContent("Base Material"));
        EditorGUILayout.PropertyField(saveFolderProp, new GUIContent("Dossier de sauvegarde"));

        // On affiche le tableau de Sprites
        EditorGUILayout.PropertyField(spritesProp, new GUIContent("Sprites"), true);
        serializedData.ApplyModifiedProperties();

        valueOverride = EditorGUILayout.TextField("Valeur de shader override", valueOverride);

        // Bouton de création
        if (GUILayout.Button("Créer les Variants"))
        {
            CreateMaterialVariants();
        }
    }

    private void CreateMaterialVariants()
    {
        if (data.baseMaterial == null)
        {
            Debug.LogError("Veuillez spécifier un matériel de base avant de créer des variants.");
            return;
        }

        if (data.sprites == null || data.sprites.Length == 0)
        {
            Debug.LogError("Veuillez ajouter au moins un Sprite dans la liste.");
            return;
        }

        // Parcourt chaque sprite sélectionné et crée un nouveau Material
        foreach (var sprite in data.sprites)
        {
            if (sprite == null) continue;

            // Nom du fichier .mat à créer
            string materialName = data.baseMaterial.name + "_" + sprite.name + ".mat";
            string assetPath = System.IO.Path.Combine(data.saveFolder, materialName);

            // Crée une copie du matériel de base
            Material variantMat = new Material(data.baseMaterial)
            {
                name = data.baseMaterial.name + "_" + sprite.name
            };

            // Assigne la texture du sprite (attention au nom de propriété, ça peut être _MainTex, _BaseMap, etc. selon le shader)
            variantMat.SetTexture(valueOverride, sprite.texture);

            // Sauvegarde en tant qu’asset dans le projet
            AssetDatabase.CreateAsset(variantMat, assetPath);
            Debug.Log("Variant créé : " + assetPath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
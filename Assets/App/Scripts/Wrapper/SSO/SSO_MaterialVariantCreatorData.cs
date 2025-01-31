using UnityEngine;

[CreateAssetMenu(fileName = "SSO_MaterialVariantCreatorData", menuName = "SSO/_/SSO_MaterialVariantCreatorData")]
public class SSO_MaterialVariantCreatorData : ScriptableObject
{
    public Material baseMaterial;
    public Sprite[] sprites;      // ou List<Sprite> si vous préférez
    public string saveFolder = "Assets/App/Art/Material/";
}
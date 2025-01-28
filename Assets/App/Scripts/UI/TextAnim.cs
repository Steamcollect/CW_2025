using System.Collections;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
public class TextAnim : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float charAnimationDuration;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float sizeScale;
    [SerializeField] private string message;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI text;

    public float duration;
    private float timeElapsed;

    private void Start()
    {
        PrintText(message);
    }

    private void PrintText(string message)
    {
        text.ForceMeshUpdate();
        int total = text.textInfo.characterCount;
        for(int i = 0; i < total; i++)
        {
            TMP_CharacterInfo charInfo = text.textInfo.characterInfo[i];

            if(!charInfo.isVisible)
            {
                continue;
            }

            Vector3[] verts = text.textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            for(int j = 0; j < 4; j++)
            {
                Vector3 orig = verts[charInfo.vertexIndex + j];
                verts[charInfo.vertexIndex + j] = orig + new Vector3(0, Mathf.Sin(Time.time * 2f + orig.x * 0) * 10f, 0);
            }
        }
    }
}
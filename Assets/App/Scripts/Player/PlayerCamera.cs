using System.Collections;
using UnityEngine;
public class PlayerCamera : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float smoothSpeed = 1.0f;

    [Header("References")]
    [SerializeField] private Transform drivingTransform;
    [SerializeField] private Transform datingTransform;

    [Header("RSO")]
    [SerializeField] private RSO_PlayerState playerState;

    private Vector3 velocity = Vector3.zero;

    private void OnEnable()
    {
        playerState.OnChanged += SwitchCamera;
    }

    private void OnDisable()
    {
        playerState.OnChanged -= SwitchCamera;
    }

    private void SwitchCamera(PlayerState playerState)
    {
        StopAllCoroutines();

        if (playerState == PlayerState.Driving) StartCoroutine(SmoothLerp(smoothSpeed, drivingTransform, datingTransform));
        else StartCoroutine(SmoothLerp(smoothSpeed, datingTransform, drivingTransform));
    }

    private IEnumerator SmoothLerp(float time, Transform startPos, Transform endPos)
    {
        float elapsedtime = 0.0f;

        while (elapsedtime < time)
        {
            transform.localEulerAngles = Vector3.Lerp(startPos.localEulerAngles, endPos.localEulerAngles, elapsedtime / time);
            elapsedtime += Time.deltaTime;
            yield return null;
        }
    }
}
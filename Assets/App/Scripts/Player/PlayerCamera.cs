using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
public class PlayerCamera : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float smoothSpeed = 1.0f;
    Vector3 targeForward = Vector3.zero;

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

    private void Start()
    {
        targeForward = drivingTransform.forward;
    }

    private void Update()
    {
        transform.forward = Vector3.SmoothDamp(
            transform.forward,
            targeForward,
            ref velocity,
            smoothSpeed);
    }

    private void SwitchCamera(PlayerState playerState)
    {
        targeForward = playerState == PlayerState.Driving ? drivingTransform.forward : datingTransform.forward;
    }
}
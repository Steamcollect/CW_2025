using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float switchDelay = 2.0f;

    [SerializeField] private RSO_PlayerState playerState;
    
    [SerializeField] private SoundComponent soundComponentSwoosh;
    
    private bool canSwitch = true;

    private void Awake()
    {
        playerState.Value = PlayerState.Driving;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canSwitch)
        {
            StartCoroutine(ResetDelay());
            soundComponentSwoosh.PlayClip();

            if(playerState.Value == PlayerState.Driving) playerState.Value = PlayerState.Dating;
            else playerState.Value = PlayerState.Driving;
        }
    }

    IEnumerator ResetDelay()
    {
        canSwitch = false;

        yield return new WaitForSeconds(switchDelay);

        canSwitch = true;
    }
}

public enum PlayerState
{
    Driving,
    Dating
}
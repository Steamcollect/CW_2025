using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private RSO_PlayerState playerState;

    private void Awake()
    {
        playerState.Value = PlayerState.Driving;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(playerState.Value == PlayerState.Driving) playerState.Value = PlayerState.Dating;
            else playerState.Value = PlayerState.Driving;
        }
    }
}

public enum PlayerState
{
    Driving,
    Dating
}
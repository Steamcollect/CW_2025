using UnityEngine;
public class GameLoopTimeManager : MonoBehaviour
{
    //[Header("Settings")]

    bool isLaunch = false;
    bool isPaused = false;

    [Header("References")]

    //[Space(10)]
    // RSO
    [SerializeField] RSO_GameLoopTime rsoGameLoopTime;
    // RSF
    // RSP

    //[Header("Input")]
    //[Header("Output")]

    private void Update()
    {
        if (isLaunch && !isPaused) rsoGameLoopTime.Value += Time.deltaTime;
    }

    void ResetTime()
    {
        rsoGameLoopTime.Value = 0;
        isLaunch = false;
    }
    void LaunchTime()
    {
        isLaunch = true;
    }

    void PauseTime()
    {
        isPaused = true;
    }
    void ResumeTime()
    {
        isPaused = false;
    }
}
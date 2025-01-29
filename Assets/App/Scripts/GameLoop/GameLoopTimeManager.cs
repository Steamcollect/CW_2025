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
        if (isLaunch && !isPaused)
        {
            rsoGameLoopTime.Value = new GameTime()
            {
                timeSinceStart = rsoGameLoopTime.Value.timeSinceStart + Time.deltaTime,
                systemTime = rsoGameLoopTime.Value.systemTime
            };
        }
    }

    void ResetTime()
    {
        rsoGameLoopTime.Value = new GameTime();
        isLaunch = false;
    }
    void LaunchTime()
    {
        rsoGameLoopTime.Value = new GameTime()
        {
            systemTime = 1f
        };
        isLaunch = true;
    }

    void PauseTime()
    {
        isPaused = true;
        rsoGameLoopTime.Value = new GameTime()
        {
            timeSinceStart = rsoGameLoopTime.Value.timeSinceStart,
            systemTime = 0f
        };
    }
    void ResumeTime()
    {
        isPaused = false;
        rsoGameLoopTime.Value = new GameTime()
        {
            timeSinceStart = rsoGameLoopTime.Value.timeSinceStart,
            systemTime = 1f
        };
    }
}

public struct GameTime
{
    public float timeSinceStart;
    public float systemTime;
}
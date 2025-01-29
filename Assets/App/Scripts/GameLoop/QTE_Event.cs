using System;
using UnityEngine;
public class QTE_Event : MonoBehaviour
{
    //[Header("Settings")]
    KeyCode[] currentQTE;
    int currentQTE_Index;

    bool isOnEvent = false;

    float timer;

    //[Header("References")]

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    [Header("Input")]
    [SerializeField] RSE_StartNewQTE rseStartQTE;
    
    [Header("Output")]
    [SerializeField] RSE_OnQTE_KeyMissed rseOnQTE_KeyMissed;
    [SerializeField] RSE_OnQTE_KeyWin rseOnQTE_KeyWin;
    [Space(5)]
    [SerializeField] RSE_OnQTE_Missed rseOnQTE_Missed;
    [SerializeField] RSE_OnQTE_Win rseOnQTE_Win;
    [SerializeField] RSE_OnQTE_End rseOnQTE_End;

    private void OnEnable()
    {
        rseStartQTE.action += LaunchNewQTE;
    }
    private void OnDisable()
    {
        rseStartQTE.action -= LaunchNewQTE;
    }

    private void Update()
    {
        if (isOnEvent)
        {
            timer -= Time.deltaTime;

            if (Input.anyKeyDown)
            {
                foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(kcode))
                    {
                        if(kcode == currentQTE[currentQTE_Index])
                        {
                            Debug.Log("KeyCode down: " + kcode);
                            rseOnQTE_KeyWin.Call();
                            currentQTE_Index++;

                            if(currentQTE_Index >= currentQTE.Length)
                            {
                                rseOnQTE_Win.Call();
                                OnQTE_End();
                            }
                        }
                        else
                        {
                            Debug.Log("Wrong key");

                            rseOnQTE_KeyMissed.Call();
                        }

                        break;
                    }
                }
            }

            if(timer <= 0)
            {
                rseOnQTE_Missed.Call();
                OnQTE_End();
            }
        }
    }

    void LaunchNewQTE(KeyCode[] qte, float time)
    {
        currentQTE = qte;
        currentQTE_Index = 0;

        timer = time;

        isOnEvent = true;
    }

    void OnQTE_End()
    {
        isOnEvent = false;

        rseOnQTE_End.Call();
    }
}
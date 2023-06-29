using System;
using UnityEngine;

public class FrameRateManager : MonoBehaviour
{
    public int RefreshRate;
#if !UNITY_EDITOR

    void Awake()
    {
        RefreshRate = (int)Math.Round(Screen.currentResolution.refreshRateRatio.value, 0);
        Application.targetFrameRate = RefreshRate;
    }
#endif
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Toggle fullScreen, vSync;
    void Start()
    {
        fullScreen.isOn = Screen.fullScreen;
        if(QualitySettings.vSyncCount==0)
        {
            vSync.isOn = false;
        }
        else
        {
            vSync.isOn = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ApplyGraphics()
    {
        Screen.fullScreen = fullScreen.isOn;
        if (vSync.isOn)
            QualitySettings.vSyncCount = 1;
        else
            QualitySettings.vSyncCount = 0;
    }

}

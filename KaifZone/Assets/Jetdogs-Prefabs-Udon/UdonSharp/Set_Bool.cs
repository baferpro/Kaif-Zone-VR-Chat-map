/*
*===========================================================*
*       _      _   ____              _          _           *
*      | | ___| |_|  _ \  ___   __ _| |    __ _| |__  ___   *
*   _  | |/ _ \ __| | | |/ _ \ / _` | |   / _` | '_ \/ __|  *
*  | |_| |  __/ |_| |_| | (_) | (_| | |__| (_| | |_) \__ \  *
*   \___/ \___|\__|____/ \___/ \__, |_____\__,_|_.__/|___/  *
*                              |___/                        *
*===========================================================*
*                                                           *
*                  Auther: Jetdog8808                       *
*                                                           *
*===========================================================*
*/

using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class Set_Bool : UdonSharpBehaviour
{
    public UdonBehaviour udonBehaviour;
    public string variableName;
    public bool value;
    public bool getOnStart = false;

    public Text display;
    public Toggle toggle;

    private bool toggleInitialize = false;

    void Start()
    {
        if (getOnStart)
        {
            if (udonBehaviour)
            {
                value = (bool)udonBehaviour.GetProgramVariable(variableName);
            }
        }
        
        if (display)
        {
            display.text = value.ToString();
        }

        if (toggle)
        {
            toggle.isOn = value;
            toggleInitialize = true;
        }

    }

    public void SetBool()
    {
        if (udonBehaviour)
        {
            udonBehaviour.SetProgramVariable(variableName, value);
        }

        if (toggle)
        {
            if(toggle.isOn != value)
            {
                toggleInitialize = false;
                toggle.isOn = value;
                toggleInitialize = true;
            }
        }

        if (display)
        {
            display.text = value.ToString();
        }
    }

    public void SetTrue()
    {
        value = true;
        SetBool();
    }

    public void SetFalse()
    {
        value = false;
        SetBool();
    }

    public void Toggle()
    {
        value = !value;
        SetBool();
    }

    public void SetToggle()
    {
        if (toggle && toggleInitialize)
        {
            value = toggle.isOn;
            SetBool();
        }
    }
}

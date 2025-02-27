using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSystem : MonoBehaviour
{
    private int comboIndex;
    private float timeToCancel;
    private readonly float comboResetTime = 1.0f;
    private int maxCombo = 3;

    public void SetMaxCombo(int NewMaxCombo)
    {
        maxCombo = NewMaxCombo;
        ResetCombo();
    }

    public int GetMaxCombo() 
    {
        if(Time.time - timeToCancel > comboResetTime)
        {
            comboIndex = 0;
        }

        timeToCancel = Time.time;
        int currentCombo = comboIndex;
        comboIndex = (comboIndex + 1) % maxCombo;
        return currentCombo;
    }

    public void ResetCombo()
    {
        comboIndex = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UltimateColourManager : MonoBehaviour
{
    private Image currentImage;
    public Material GrayScale;
    public float energyNeeded;

    private void Start()
    {
        currentImage = GetComponent<Image>();
    }
    private void Update()
    {
        if(EnergyManager.Instance.currentEnergy >= energyNeeded)
        {
            currentImage.material = null;
        }
        else
        {
            currentImage.material = GrayScale;
        }

    }
}

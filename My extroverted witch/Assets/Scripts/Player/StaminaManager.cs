using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class StaminaManager : MonoBehaviour
{
    public Slider staminaSlider; 
    public float stamina; 
    public float staminaRegenSpeed; 
    public float maxStamina; 


    private void Update()
    {
        StaminaCheck();
    }

    void StaminaCheck()
    {
        
        if (stamina < maxStamina)
        {
            stamina += staminaRegenSpeed * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0, maxStamina); 
        }

    
        staminaSlider.value = stamina;
    }

   public void UseStamina(float amount)
    {
        if (stamina >= amount) 
        {
            stamina = stamina - amount;
        }
        
    }
}

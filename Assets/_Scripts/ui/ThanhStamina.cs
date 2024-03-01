using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThanhStamina : MonoBehaviour
{
    public Image _stamina;
    public void capNhatStamina(float currentStamina, float maxStamina){
        _stamina.fillAmount = currentStamina / maxStamina;
    }
}

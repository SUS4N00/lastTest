using UnityEngine;
using UnityEngine.UI;

public class potionControl : MonoBehaviour
{
    public Image _potion;
    public void control(float timming, float timeToNextHeal, float potionColdDown){
        float counting;
        if(timming > timeToNextHeal){
            counting = 0;
        }else{
            counting = timeToNextHeal - timming;
        }
        _potion.fillAmount = counting / potionColdDown;
    }
}

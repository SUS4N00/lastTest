using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 1f;
    public Vector3 offset;
    public float bonus = 0.75f;
    int bonusDamage;
    float bonusStamina;

    void Start()
    {
        smoothSpeed = 2;
        player = GameObject.Find("player").transform;
        bonusDamage = player.GetComponent<atack>().attackDamage;
        bonusStamina = player.GetComponent<player>().staminaRegenRate * bonus;
        player.GetComponent<playerHit>().attackDamageBonus += bonusDamage;
        player.GetComponent<player>().staminaRegenBonus += bonusStamina;
        Invoke("countDown", 15f);
    }
    void FixedUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
    // Start is called before the first frame update
    void countDown(){
        player.GetComponent<playerHit>().attackDamageBonus -= bonusDamage;
        player.GetComponent<player>().staminaRegenBonus -= bonusStamina;
        Destroy(gameObject);
    }
}

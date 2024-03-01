using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class playerHit : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform attackPoint;
    public Transform bloodPoint;
    public LayerMask enemyLayers;
    private AudioManager audioManager;
    public GameObject bloodPrefab;
    private float attackRange;
    private int attackDamage;
    public int attackDamageBonus;

    void Start(){
        attackRange = GetComponent<atack>().attackRange;
        attackDamage = GetComponent<atack>().attackDamage;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void hit()
    {
        //detect enemies in range of attack
        Collider2D checkEnemy = Physics2D.OverlapCircle(attackPoint.position, attackRange, enemyLayers);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //damage them
        if(checkEnemy == null){
            audioManager.playSfx(audioManager.miss);
        }else{
            Instantiate(bloodPrefab, bloodPoint.position, bloodPoint.rotation);
            audioManager.playSfx(audioManager.hit);
            GetComponent<player>().currentStamina += 10;
        }
        foreach(Collider2D enemy in hitEnemies){
            GetComponent<atack>().currentMadNess += 5;
            enemy.GetComponent<enemy>().TakeDamage(attackDamage + attackDamageBonus);
        }
    }
}

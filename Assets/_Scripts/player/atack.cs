using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Scripting.APIUpdating;

public class atack : MonoBehaviour
{
    // Start is called before the first frame update
    float stamina;
    public int currentMadNess;
    public int maxMadNess = 100;
    public float staminaAttack;
    public float attackRange = 0.44f;
    public int attackDamage = 35;
    public float attackRate = 1.44f;
    float nextAttackTime = 0f;
    public GameObject buffPrefab;
    private Animator anim;
    public Transform attackPoint;
    public AudioManager audioManager;
    void Start()
    {
        currentMadNess = 0;
        attackRate = 1.74f;
        staminaAttack = 44;
        anim = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("MadNess").GetComponent<thanhMau>().capNhatThanhMau(currentMadNess, maxMadNess);
        stamina = GetComponent<player>().currentStamina;
        if(Time.time >= nextAttackTime && !GetComponent<player>().hited && stamina>= staminaAttack){
            if(Input.GetKeyDown(KeyCode.J)){
                GetComponent<player>().DecreaseStamina(staminaAttack);
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        if(Input.GetKeyDown(KeyCode.U) && currentMadNess >= maxMadNess){
            currentMadNess = 0;
            buff();
        }
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("atack")){
            GetComponent<NewBehaviourScript>().move = 0;
        }
    }

    void Attack(){
        //animation
        anim.SetTrigger("atack");
    }

    void buff(){
        audioManager.playSfx(audioManager.buff);
        Instantiate(buffPrefab, transform.position, transform.rotation);
    }
    void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class enemyMove : MonoBehaviour
{
    public LayerMask playerLayer;
    private float horizontalValue = 0f;
    public float changeSpeed = 0.1f;
    private Animator anim;
    public Transform playerPosition;
    public Transform attackPoint;
    public Transform attackPoint2;
    public float attackRange = 0.4f;
    public float attackRange2;
    public float speed;
    public float attackRate = 0.84f;
    bool atacking = false;
    float nextTimeAttack;
    public int attackDamage;
    public int attackDamage2;
    public bool isFacingRight = false;
    public AudioManager audioManager;
    public float move;
    public bool foundedTarget;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        foundedTarget = false;
        playerPosition = GameObject.Find("player").transform;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        anim = GetComponent<Animator>();
    }

    void Update(){
        if(foundedTarget == false){
            timer += Time.deltaTime;
        }
        findTarget();
    }
    void FixedUpdate(){
        if(Time.time >= nextTimeAttack){
            Move();
        }
        if(attackPoint.position.x < playerPosition.position.x+attackRange+0.3f && attackPoint.position.x > playerPosition.position.x-attackRange-0.3f){
            if(Time.time >= nextTimeAttack && move == 0 && !GetComponent<enemy>().hited && foundedTarget){
                Attack();
            }
        }
        if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("attack") || GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("attack&hurt")){
            atacking = true;
        }else{
            atacking = false;
        }
    }

    public bool seePlayer(){
        if(isFacingRight && playerPosition.transform.position.x >= transform.position.x || !isFacingRight && playerPosition.transform.position.x < transform.position.x){
            return true;
        }else{
            return false;
        }
    }

    private void findTarget(){
        if(foundedTarget == false){
            if(timer > 2 && timer <4){
                move = -2;
            }else if(timer > 6 && timer < 8){
                move = 2;
            }else if(timer > 8){
                timer -= timer;
            }else{
                move = 0;
            }
            if(gameObject.tag == "canChien" || gameObject.tag == "canChien2"){
                if(seePlayer() == true && Mathf.Abs(transform.position.x - playerPosition.position.x) <= 3){
                    foundedTarget = true;
                }
            }else if(gameObject.tag == "tamXa"){
                if(seePlayer() == true && Mathf.Abs(attackPoint.position.x - playerPosition.position.x) <= attackRange){
                    foundedTarget = true;
                }
            }
            if(GetComponent<enemy>().hited || anim.GetCurrentAnimatorStateInfo(0).IsTag("attack")){
                foundedTarget = true;
            }
        }else{
            if(playerPosition.GetComponent<player>().isDead == true){
                foundedTarget = false;
                return;
            }
            if(gameObject.tag == "canChien" || gameObject.tag == "canChien2"){
                if(attackPoint.position.x > playerPosition.position.x + attackRange + 0.16f){
                    move = -5;
                }else if(attackPoint.position.x < playerPosition.position.x - attackRange - 0.16f){
                    move = 5;
                }else{
                    move = 0f;
                }
            }else if(gameObject.tag == "tamXa"){
                if(attackPoint.position.x > playerPosition.position.x + attackRange + 0.16f){
                    move = -5;
                }else if(attackPoint.position.x < playerPosition.position.x - attackRange - 0.16f){
                    move = 5;
                }else{
                    if(isFacingRight && attackPoint.position.x > playerPosition.position.x + 0.2f){
                        move = -1;
                    }else if(!isFacingRight && attackPoint.position.x < playerPosition.position.x - 0.2f){
                        move = 1;
                    }else{
                        move = 0;
                    }
                }
            }
        }
    }
    void Attack(){
        //atacking = true;
        nextTimeAttack = Time.time + 1/attackRate;
        //animation
        anim.SetTrigger("attack");
        //atacking = false;
    }

    void Move(){
        if(anim.GetCurrentAnimatorStateInfo(0).IsTag("getHurt")){
            move = 0;
        }
        if(GetComponent<enemy>().hited){
            move = 0;
        }
        if(move != 0f && atacking == false && !GetComponent<enemy>().hited){
            anim.SetBool("move", true);
            transform.Translate(Vector3.right * GetCustomHorizontalValue() * speed * Time.deltaTime);
            if(isFacingRight && move < 0 || !isFacingRight && move > 0){
                isFacingRight = !isFacingRight;
                Vector3 scale = transform.localScale;
                scale.x = scale.x * -1;
                transform.localScale = scale;
            }
        }else{
            anim.SetBool("move", false);
        }
    }

    void OnDrawGizmosSelected(){
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(attackPoint2.position, attackRange2);
    }
    private float GetCustomHorizontalValue()
    {
        // Kiểm tra điều kiện (ví dụ: nếu player đang bị khóa hoặc không thể di chuyển)
        if (move == 0f)
        {
            // Trả về giá trị 0 nếu player không thể di chuyển
            return 0f;
        }
        else
        {
            // Nếu không, trả về giá trị gần với giá trị mục tiêu
            return Mathf.Lerp(horizontalValue, move, Time.deltaTime * changeSpeed);
        }
    }
}
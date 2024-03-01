using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemyMove : MonoBehaviour
{
    public LayerMask playerLayer;
    private float horizontalValue = 0f;
    public float changeSpeed = 0.1f;
    public Rigidbody2D enemySelf;
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
    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.Find("player").transform;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        enemySelf = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame

    public bool seePlayer(){
        if(isFacingRight && playerPosition.transform.position.x >= transform.position.x || !isFacingRight && playerPosition.transform.position.x < transform.position.x){
            return true;
        }else{
            return false;
        }
    }
    void FixedUpdate(){
        if(Time.time >= nextTimeAttack){
            Move();
        }
        if(attackPoint.position.x < playerPosition.position.x+attackRange+0.3f && attackPoint.position.x > playerPosition.position.x-attackRange-0.3f){
            if(Time.time >= nextTimeAttack && move == 0 && !GetComponent<enemy>().hited){
                findAndAttack();
            }
        }
        if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("attack") || GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("attack&hurt")){
            atacking = true;
        }else{
            atacking = false;
        }
    }

    void findAndAttack(){
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
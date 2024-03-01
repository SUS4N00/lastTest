using UnityEngine;

public class enemy : MonoBehaviour
{
    int scoreInEnemy;
    private float timer;
    private float timeToNextCounter;
    public float counterColdDown;
    public int maxHealth = 100;
    private float stunTime;
    private float stunTimer;
    public int currentHealth;
    public bool hited = false;
    private Animator anim;
    public AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        stunTime = 0.7f;
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<enemyHpBar>().capNhatThanhMau(currentHealth, maxHealth, GetComponent<enemyMove>().isFacingRight);
        efect();
        timer = Time.time;
    }

    public void TakeDamage(int damage){
        if(timer >= timeToNextCounter && gameObject.tag == "canChien2" && GetComponent<enemyMove>().seePlayer()){
            timeToNextCounter = timer + counterColdDown;
            damage = 0;
            anim.SetTrigger("counter");
            audioManager.playSfx(audioManager.shield);
        }
        currentHealth -= damage;
        if(!anim.GetCurrentAnimatorStateInfo(0).IsTag("attack") && damage > 0){
            hited = true;
            anim.SetTrigger("hurt");
        }
        if(currentHealth > 0 && damage > 0){
            audioManager.playSfx(audioManager.enemyHurt);
        }
        if(currentHealth <= 0){
            audioManager.playSfx(audioManager.enenyDie);
            Die();
        }
    }

    void efect(){
        if(hited){
            stunTimer += Time.deltaTime;
            if(stunTimer >= stunTime){
                hited = false;
                stunTimer = 0;
            }
        }
    }
    void Die(){
        //Debug.Log("Enemy died");
        int numberKnife = GameObject.Find("player").GetComponent<weapon>().numberKnife;
        int max = GameObject.Find("player").GetComponent<weapon>().numberKnifeMax;
        if(numberKnife < max){
            GameObject.Find("player").GetComponent<weapon>().numberKnife++;
        }
        if(gameObject.tag == "tamXa"){
            scoreInEnemy = 20;
        }else if(gameObject.tag == "canChien"){
            scoreInEnemy = 30;
        }else if(gameObject.tag == "canChien2"){
            scoreInEnemy = 40;
        }
        GameObject.Find("diem").GetComponent<score>().currentScore+= scoreInEnemy;

        //die animation
        anim.SetBool("isDead", true);

        //disable the enemy
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<enemyMove>().enabled = false;
    }
}
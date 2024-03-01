using UnityEngine;

public class enemyHit2 : MonoBehaviour
{
    public Transform attackPoint2;
    public float attackRange2;
    public LayerMask playerLayer;
    private AudioManager audioManager;
    private int attackDamage2;

    void Start(){
        attackDamage2 = GetComponent<enemyMove>().attackDamage2;
        attackRange2 = GetComponent<enemyMove>().attackRange2;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void attack2(){
        if(GetComponent<enemy>().currentHealth > 0){
            //detect player in range of attack
            Collider2D hitPlayer = Physics2D.OverlapCircle(attackPoint2.position, attackRange2, playerLayer);
            //damage
            if(hitPlayer != null && !hitPlayer.GetComponent<Animator>().GetCurrentAnimatorStateInfo(2).IsName("surfers")){
                hitPlayer.GetComponent<player>().TakeDamage(attackDamage2);
                audioManager.playSfx(audioManager.enemyHit);
            }else{
                audioManager.playSfx(audioManager.enemyMiss);
            }
        }
    }
}

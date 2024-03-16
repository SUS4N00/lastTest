using UnityEngine;

public class enemyHit : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange;
    public LayerMask playerLayer;
    private AudioManager audioManager;
    private int attackDamage;

    void Start(){
        attackDamage = GetComponent<enemyMove>().attackDamage;
        attackRange = GetComponent<enemyMove>().attackRange;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void attack(){
        if(GetComponent<enemy>().currentHealth > 0){
            //detect player in range of attack
            Collider2D hitPlayer = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
            //damage
            if(hitPlayer != null && !hitPlayer.GetComponent<Animator>().GetCurrentAnimatorStateInfo(2).IsName("dash")){
                hitPlayer.GetComponent<player>().TakeDamage(attackDamage);
                audioManager.playSfx(audioManager.enemyHit);
            }else{
                audioManager.playSfx(audioManager.enemyMiss);
            }
        }
    }
}

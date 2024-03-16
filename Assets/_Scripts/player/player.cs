using System.Collections;
using UnityEngine;

public class player : MonoBehaviour
{
    float timming;
    float potionColdDown;
    float stunTimer;
    float timeToNextHeal;
    public float currentStamina;
    public float staminaRegenRate;
    public float staminaRegenBonus;
    public float maxStamina = 100;
    public int heal;
    public int maxHeal = 150;
    private float stunTime = 0.3f;
    public bool hited = false;
    public Transform healPoint;
    public GameObject healingPrefab;
    public AudioManager audioManager;
    private Animator anim;
    public bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        staminaRegenBonus = 0;
        potionColdDown = 20;
        staminaRegenRate = 1f;
        currentStamina = maxStamina;
        heal = maxHeal;
        anim = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        StartCoroutine(RegenerateStamina());
    }

    // Update is called once per frame
    void Update()
    {
        timming = Time.time;
        if(Input.GetKeyDown(KeyCode.O) && timming > timeToNextHeal){
            StartCoroutine(healing());
        }
        efect();
        GameObject.Find("ThanhStamina").GetComponent<ThanhStamina>().capNhatStamina(currentStamina, maxStamina);
        GameObject.Find("ThanhMau").GetComponent<thanhMau>().capNhatThanhMau(heal, maxHeal);
        GameObject.Find("healingPotion").GetComponent<potionControl>().control(timming, timeToNextHeal, potionColdDown);
    }
    public void TakeDamage(int damage){
        heal -= damage;
        anim.SetTrigger("hurt");
        hited = true;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        if(heal > 0){
            GetComponent<atack>().currentMadNess += 10;
            audioManager.playSfx(audioManager.hurt);
        }
        if(heal <= 0){
            audioManager.playSfx(audioManager.die);
            Die();
        }
    }
    void efect(){
        if(hited){
            stunTimer += Time.deltaTime;
            if(stunTimer >= stunTime){
                hited = false;
                stunTimer = 0;
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                GetComponent<Rigidbody2D>().freezeRotation = true;
            }
        }
    }
    IEnumerator healing(){
        if(!hited){
            timeToNextHeal = timming + potionColdDown;
            Instantiate(healingPrefab, healPoint.position, healPoint.rotation);
            anim.SetTrigger("isHealing");
            audioManager.playSfx(audioManager.potion);
            yield return new WaitForSeconds(0.35f);
            if(anim.GetCurrentAnimatorStateInfo(2).IsName("playerHealing")){
                int healAmount = Mathf.Min(50, maxHeal - heal);
                heal +=healAmount;
                audioManager.playSfx(audioManager.drink);
            }
            yield return null;
        }
    }
    void Die(){
        isDead = true;
        anim.SetBool("isDead", true);
        GetComponent<NewBehaviourScript>().enabled = false;
        GetComponent<atack>().enabled = false;
    }
    IEnumerator RegenerateStamina()
    {
        while (true)
        {
                yield return new WaitForSeconds(0.05f); // Chờ mỗi giây

                if (currentStamina < maxStamina && !anim.GetCurrentAnimatorStateInfo(2).IsTag("cannotRegen") && !anim.GetCurrentAnimatorStateInfo(3).IsTag("cannotRegen"))
                {
                    // Tăng stamina dựa trên tốc độ hồi phục và thời gian chờ đợi
                    currentStamina += staminaRegenRate + staminaRegenBonus;
                    currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina); // Đảm bảo stamina không vượt quá giới hạn
                }
        }
    }

    // Hàm này để giảm stamina khi cần
    public void DecreaseStamina(float amount)
    {
        currentStamina -= amount;
        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina); // Đảm bảo stamina không vượt quá giới hạn
    }
}
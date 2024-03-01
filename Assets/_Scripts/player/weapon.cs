using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class weapon : MonoBehaviour
{
    float stamina;
    float staminaThrow;
    public Transform throwPoint;
    public GameObject kunaiPrefab;
    public Animator anim;
    public AudioManager audioManager;
    public int numberKnifeMax = 3;
    public int numberKnife;
    [SerializeField] TextMeshProUGUI text_numberKnife;

    void Start(){
        staminaThrow = 5f;
        numberKnife = numberKnifeMax;
        anim = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        stamina = GetComponent<player>().currentStamina;
        text_numberKnife.text = "x " + numberKnife;
        if(Input.GetButtonDown("Fire1") && numberKnife > 0 && stamina >= staminaThrow){
            GetComponent<player>().DecreaseStamina(staminaThrow);
            audioManager.playSfx(audioManager.throwKnife);
            Shoot();
            anim.SetTrigger("throw");
            numberKnife--;
        }
    }

    void Shoot(){
        Instantiate(kunaiPrefab, throwPoint.position, throwPoint.rotation);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knife : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 10;
    public Rigidbody2D rb;
    public GameObject impact;
    public GameObject blood;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        Invoke("hehe", 4.44f);
    }

    void OnTriggerEnter2D( Collider2D hitInfo){
        enemy target = hitInfo.GetComponent<enemy>();
        if(target != null){
            target.TakeDamage(damage);
            GameObject.Find("player").GetComponent<atack>().currentMadNess += 1;
            Instantiate(blood, transform.position, transform.rotation);
        }else{
            Instantiate(impact, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }

    void hehe(){
        Destroy(gameObject);
    }
}

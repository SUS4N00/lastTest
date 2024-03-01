using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpThru : MonoBehaviour
{
    private int layerAir;
    private int layerWall;
    private bool onWall;
    private Collider2D colli;

    // Start is called before the first frame update
    void Start()
    {
        layerAir = 10;
        layerWall = 11;
        colli = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(onWall && Input.GetKeyDown(KeyCode.S) && colli != null){
            onWall = false;
            colli.gameObject.layer = layerAir;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == layerAir && !Input.GetKeyDown(KeyCode.S)){
            colli = other;
            other.gameObject.layer = layerWall;
            onWall = true;
            //Debug.Log("now u can stand");
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.layer == layerWall){
            colli = null;
            other.gameObject.layer = layerAir;
            onWall = false;
            //Debug.Log("now u dont");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class bow : MonoBehaviour
{
    public GameObject arrow;
    public Transform bowPos;
    void shoot(){
        Instantiate(arrow, bowPos.position, Quaternion.identity);
    }
}

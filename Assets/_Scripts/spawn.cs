using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    public Transform spawnWhere;
    public float ResetTime;
    private float timer;
    List<GameObject> minions;
    public GameObject minionPrefab;
    // Start is called before the first frame update
    void Start()
    {
        this.minions = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        this.Spawn();
        checkMinionDead();
    }

    void checkMinionDead(){
        GameObject minion;
        for(int i = 0;i < this.minions.Count;i++){
            minion = this.minions[i];
            if(minion == null){
                this.minions.RemoveAt(i);
            }
        }
    }
    void Spawn(){
        if(this.minions.Count >=1) return;
        timer += Time.deltaTime;
        if(timer >= ResetTime){
            timer = 0;
            GameObject minion = Instantiate(this.minionPrefab);
            minion.transform.position = spawnWhere.position;
            minion.SetActive(true);
            this.minions.Add(minion);
        }
    }
}
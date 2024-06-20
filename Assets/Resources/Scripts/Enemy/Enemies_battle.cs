using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemies_battle : MonoBehaviour
{
    //적은 최대 4명까지있다.
    public Enemy_battle[] enemies;
    // Start is called before the first frame update
    void Start()
    {
        //적 정보 가져오기
        enemies = GetComponentsInChildren<Enemy_battle>();
        foreach(Enemy_battle enemy in enemies)
        {
            enemy.Init();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}

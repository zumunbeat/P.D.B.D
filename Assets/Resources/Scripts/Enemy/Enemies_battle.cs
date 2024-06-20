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
    //적이 돌아가면서 랜덤한 스킬을 사용할 수 있게 해주자.
    public Skill ThisTurnEnemyDecision(Enemy_battle obj)
    {
        //랜덤한 적의 스킬을 선택해서 해당정보를 넘겨주자.
        int decision = UnityEngine.Random.Range(0, obj.skills.skills.Length);
        Debug.Log(obj.skills.skills[decision].name);    
        return obj.skills.skills[decision];
    }
}

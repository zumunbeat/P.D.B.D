using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemies_battle : MonoBehaviour
{
    //���� �ִ� 4������ִ�.
    public Enemy_battle[] enemies;
    // Start is called before the first frame update
    void Start()
    {
        //�� ���� ��������
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colleagues_battle: MonoBehaviour
{
    //동료는 최대 3명까지 존재할 수 있다.
    public Colleague_battle[] colleagues;
    // Start is called before the first frame update
    void Start()
    {
        //동료 정보 가져오기
        colleagues = GetComponentsInChildren<Colleague_battle>();
        foreach (Colleague_battle colleague in colleagues)
        {
           colleague.Init();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

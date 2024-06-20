using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colleagues_battle: MonoBehaviour
{
    //����� �ִ� 3����� ������ �� �ִ�.
    public Colleague_battle[] colleagues;
    // Start is called before the first frame update
    void Start()
    {
        //���� ���� ��������
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

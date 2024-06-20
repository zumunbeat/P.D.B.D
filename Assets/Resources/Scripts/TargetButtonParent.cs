using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetButtonParent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 타겟 선택 버튼은 초기에 비활성화
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //ESC누르면 기술 선택 취소 되는 기능 추가
    }
}

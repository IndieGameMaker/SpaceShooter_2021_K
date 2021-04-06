using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    //선언부
    private float h;
    private float v;

    // 시작시 1번 호출
    void Start()
    {

    }

    // 매 프레임마다 호출 함수
    // 화면을 렌더링하는 주기
    void Update()
    {
        h = Input.GetAxis("Horizontal");  // -1.0f ~ 0.0f ~ +1.0f
        v = Input.GetAxis("Vertical");    // -1.0f ~ 0.0f ~ +1.0f

        Debug.Log($"h={h} / v={v}");
    }
}

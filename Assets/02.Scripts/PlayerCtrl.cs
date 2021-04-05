using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    // 변수의 선언부(전역변수)
    int i = 0;     
    int [] xp = new int[5] {10, 20, 30, 40, 50};

    // 시작시 1번 호출
    void Start()
    {   
        Debug.Log("Hello World!");

        //배열
        for(int i=0; i<5; i++)
        {
            Debug.Log($"i={xp[i]}");
        }

        //List

        //Hashtable

        //Dictionary
        InvokeRepeating("ShowTime", 0.0f, 1.0f);
    }

    // 매 프레임마다 호출 함수
    // 화면을 렌더링하는 주기
    void Update()
    {
        //Debug.Log($"i={i++}");
    }

    void ShowTime()
    {
        Debug.Log($"Time={Time.time}");
    }

}

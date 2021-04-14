using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCtrl : MonoBehaviour
{
    //몬스터와 주인공의 Transform 컴포넌트를 저장할 변수 선언
    private Transform monsterTr;
    private Transform playerTr;

    void Start()
    {
        monsterTr = GetComponent<Transform>(); // monsterTr = transform;

        // GameObject playerObj = GameObject.FindGameObjectWithTag("PLAYER");
        // if (playerObj != null) // if (!playerObj)
        // {
        //     playerTr = playerObj.GetComponent<Transform>();
        // }

        playerTr = GameObject.FindGameObjectWithTag("PLAYER")?.GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

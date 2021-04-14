using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCtrl : MonoBehaviour
{
    //몬스터와 주인공의 Transform 컴포넌트를 저장할 변수 선언
    private Transform monsterTr;
    private Transform playerTr;

    //몬스터의 상태를 나타내는 열거형 변수 정의
    public enum STATE {IDLE, TRACE, ATTACK, DIE}

    //몬스터의 상태를 저장하는 변수를 선언
    public STATE state = STATE.IDLE;

    // 추적 사정거리
    [Range(10, 50)]
    public float traceDist = 10.0f;
    // 공격 사정거리
    public float attackDist = 2.0f;

    // 몬스터의 사망여부 변수를 선언
    public bool isDie = false;

    //int state = 0; //0: IDLE, 1: Trace, 2, Attack

    void Start()
    {
        monsterTr = GetComponent<Transform>(); // monsterTr = transform;
        playerTr = GameObject.FindGameObjectWithTag("PLAYER")?.GetComponent<Transform>();

        StartCoroutine(CheckState()); //추후에 개별적으로 코루함수를 정지 가능
        //StartCoroutine("CheckState", 0.5f); 
    }

    //몬스터의 상태값을 결정하는 코루틴
    IEnumerator CheckState()
    {
        while (isDie == false)
        {
            //몬스터의 상태는 주인공 <--> 몬스터 거리
            float distance = Vector3.Distance(monsterTr.position, playerTr.position);

            //공격 사정거리 이내인지 여부 판단
            if (distance <= attackDist)
            {
                state = STATE.ATTACK;
            }
            else if (distance <= traceDist) // 공격 사정거리 < 주인공 <= 추적 사정거리
            {
                state = STATE.TRACE;
            }
            else
            {
                state = STATE.IDLE;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    //몬스터의 상태값에 따라서 행동을 처리하는 코루틴
    IEnumerator MonsterAction()
    {
        while(!isDie) //(isDie == false)
        {
            //몬스터의 상태에 따라서 분기처리
            switch (state)
            {
                case STATE.IDLE:
                    Debug.Log($"State = {state}");
                    break;
                case STATE.TRACE:
                    Debug.Log($"State = {state}");
                    break;
                case STATE.ATTACK:
                    Debug.Log($"State = {state}");
                    break;
                case STATE.DIE:
                    //
                    break;    
            }

            yield return new WaitForSeconds(0.3f);
        }
    }
}

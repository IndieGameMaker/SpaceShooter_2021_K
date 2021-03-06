using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterCtrl : MonoBehaviour
{
    // A*PathFinding 길찾기 알고리즘
    // 네비게이션 시스템 (Navigation Mesh)

    //몬스터와 주인공의 Transform 컴포넌트를 저장할 변수 선언
    private Transform monsterTr;
    private Transform playerTr;
    //네비메시 에이전트 컴포넌트
    private NavMeshAgent agent;

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

    //Animator 컴포넌트를 저장할 변수를 선언
    private Animator anim;

    private readonly int hashTrace  = Animator.StringToHash("IsTrace");
    private readonly int hashAttack = Animator.StringToHash("IsAttack");
    private readonly int hashHit    = Animator.StringToHash("Hit");
    private readonly int hashDie    = Animator.StringToHash("Die");

    private float hp = 100.0f;

    public GameObject bloodEffect;

    //유니티 콜백 함수
    //스크립트가 활성화 될때마다 호출
    //생성 1, 비활성화 -> 활성화 1
    void OnEnable()
    {
        //이벤트의 연결정보 등록
        //PlayerCtrl 클래스(스크립트)에 정의한 OnPlayerDie 이벤트가 발생(Raise)하면
        //현재 클래스(스크립트)에 있는 YouWin 함수를 호출한다.
        PlayerCtrl.OnPlayerDie += this.YouWin;

        StartCoroutine(CheckState()); //추후에 개별적으로 코루함수를 정지 가능
        StartCoroutine(MonsterAction());        
    }

    void OnDisable()
    {
        //이벤트의 연결정보 해지
        PlayerCtrl.OnPlayerDie -= this.YouWin;
    }

    void Awake()
    {
        monsterTr = GetComponent<Transform>(); // monsterTr = transform;
        playerTr = GameObject.FindGameObjectWithTag("PLAYER")?.GetComponent<Transform>();

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    //몬스터의 상태값을 결정하는 코루틴
    IEnumerator CheckState()
    {
        while (isDie == false)
        {
            if (state == STATE.DIE)
            {
                yield break; //해당 코루틴을 정지시킴.
            }

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
                    agent.isStopped = true;
                    anim.SetBool(hashTrace, false);
                    break;

                //추적 상태
                case STATE.TRACE:
                    agent.SetDestination(playerTr.position);
                    agent.isStopped = false;
                    anim.SetBool(hashAttack, false);
                    anim.SetBool(hashTrace , true);
                    break;

                //공격 상태
                case STATE.ATTACK:
                    anim.SetBool(hashAttack, true);
                    break;

                case STATE.DIE:
                    anim.SetTrigger(hashDie);
                    GetComponent<CapsuleCollider>().enabled = false;
                    agent.isStopped = true;
                    isDie = true;

                    Invoke("ReturnPool", 3.0f);
                    break;    
            }

            yield return new WaitForSeconds(0.3f);
        }
    }

    void ReturnPool()
    {
        //몬스터 초기화
        GetComponent<CapsuleCollider>().enabled = true;
        agent.isStopped = false;
        isDie = false;
        hp = 100.0f; 
        state = STATE.IDLE; 
        //비활성
        this.gameObject.SetActive(false);      
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("BULLET"))
        {
            //Bullet 삭제
            Destroy(coll.gameObject);
        }
    }

    public void OnDamage(Vector3 pos, Quaternion rot)
    {
        GameObject obj = Instantiate(bloodEffect, pos, rot, monsterTr);
        Destroy(obj, 2.0f);

        //Hit reaction 애니메이션 실행
        anim.SetTrigger(hashHit);

        //몬스터의 HP 차감
        hp -= 20.0f; //hp = hp - 20;
        if (hp <= 0.0f)
        {
            state = STATE.DIE;
            GameManager.instance.DisplayScore(50);
        }        
    }

    void OnTriggerEnter(Collider coll)
    {
        Debug.Log($"Punch = {coll.gameObject.name}");
    }

    //public 접근제한자로 선언한 함수는 외부 클래스(스크립트)에서 호출이 가능
    public void YouWin()
    {
        //몬스터의 모든 로직을 정지
        // StopCoroutine(CheckState());
        // StopCoroutine(MonsterAction());
        StopAllCoroutines();
        agent.isStopped = true;

        anim.SetTrigger("PlayerDie");
    }
}

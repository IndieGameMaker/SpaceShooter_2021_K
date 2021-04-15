using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    //선언부
    private float h;
    private float v;
    private float r;

    [Header("Move/Rotate Speed")]
    [Range(3.0f, 10.0f)]
    public float moveSpeed = 20.0f;
    [Range(30.0f, 150.0f)]
    public float turnSpeedValue = 100.0f;
    private float turnSpeed;

    private Transform tr;
    [System.NonSerialized]
    public Animation anim;

    // currHp / initHp = 백분율(%)
    private float initHp = 100.0f;  //초기 생명수치
    public float currHp = 100.0f;   //현재 생명수치

    // 시작시 1번 호출
    IEnumerator Start()
    {
        turnSpeed = 0.0f;

        //Component Cache 처리
        tr = GetComponent<Transform>();
        anim = GetComponent<Animation>();
        //Idle 애니메이션 실행
        anim.Play("Idle");

        yield return new WaitForSeconds(0.3f);
        turnSpeed = turnSpeedValue;
    }

    // 매 프레임마다 호출 함수, 호출주기가 불규칙, 60fps, 30fps
    // 화면을 렌더링하는 주기
    void Update()
    {
        h = Input.GetAxis("Horizontal");  // -1.0f ~ 0.0f ~ +1.0f
        v = Input.GetAxis("Vertical");    // -1.0f ~ 0.0f ~ +1.0f
        r = Input.GetAxis("Mouse X");

        //(전후방향 벡터) + (좌우방향의 벡터)
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        tr.Translate(moveDir.normalized * Time.deltaTime * moveSpeed);
        //회전처리
        tr.Rotate(Vector3.up * Time.deltaTime * turnSpeed * r);

        //애니메이션 처리
        PlayerAnimation();
        //Debug.Log("v="+v);
    }

    void PlayerAnimation()
    {
        if (v >= 0.01f) //전진
        {
            anim.CrossFade("RunF", 0.25f);
        }
        else if (v <= -0.01f) //후진
        {
            anim.CrossFade("RunB", 0.25f);
        }
        else if (h >= 0.1f) //오른쪽
        {
            anim.CrossFade("RunR", 0.25f);
        }
        else if (h <= -0.1f) //왼쪽
        {
            anim.CrossFade("RunL", 0.25f);
        }
        else
        {
            anim.CrossFade("Idle", 0.1f);
        }
    }

    /*
        Collider 컴포넌트의 Is Trigger 속성에 따른 충돌 콜백함수
        1. 언체크 일 경우 : Collision 타입
            OnCollisionEnter, Stay, Exit
        2. 체크된 경우 : Collider 컴포넌트
            OnTriggerEnter, Stay, Exit
    */

    // void OnCollisionEnter(Collision coll)
    // {
    //     if (coll.collider.tag == "BULLET")
    //     {

    //     }
    // }
    //충돌 콜백 함수
   
    void OnTriggerEnter(Collider coll)
    {
        if (currHp > 0.0f && coll.CompareTag("PUNCH"))
        {
            currHp -= 10.0f;
            //Debug.Log($"Hit = {coll.gameObject.name} Hp = {currHp},{currHp/initHp}");
            if (currHp <= 0.0f)
            {
                PlayerDie();
            }
        }
    }

    void PlayerDie()
    {
        //현재 스테이지에 생성된 모든 몬스터를 추출
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("MONSTER");
        //monsters 배열을 순회하면서 모든 몬스터의 YouWin함수를 호출
        foreach (GameObject monster in monsters)
        {
            //monster.GetComponent<MonsterCtrl>().YouWin();
            monster.SendMessage("YouWin" , SendMessageOptions.DontRequireReceiver);
        }
    }
}

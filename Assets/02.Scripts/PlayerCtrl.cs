using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    //선언부
    private float h;
    private float v;
    private float r;

    [Header("이동속도 및 회전속도")]
    [Range(3.0f, 10.0f)]
    public float moveSpeed = 5.0f;
    [Range(30.0f, 150.0f)]
    public float turnSpeed = 100.0f;

    // 시작시 1번 호출
    void Start()
    {

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
        transform.Translate(moveDir.normalized * Time.deltaTime * moveSpeed);
        //회전처리
        transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * r);


        /* 정규화 벡터(Normalized Vector), 단위 벡터(Unit Vector)

        Vector3.forward == new Vector3(0, 0, 1)
        Vector3.up      == new Vector3(0, 1, 0)
        Vector3.right   == new Vector3(1, 0, 0)

        Vector3.one     == new Vector3(1, 1, 1)
        Vector3.zero    == new Vector3(0, 0, 0)
        */

        //Debug.Log($"h={h} / v={v}");
    }
}

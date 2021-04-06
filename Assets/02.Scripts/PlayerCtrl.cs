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

        //transform.position += new Vector3(0, 0, 0.1f);
        //transform.position += Vector3.forward * 0.1f; // (0, 0, 1) * 0.1f = (0, 0, 0.1f)

        // transform.Translate(Vector3.forward * 0.1f * v, Space.Self);
        // transform.Translate(Vector3.right * 0.1f * h);

        //(전후방향 벡터) + (좌우방향의 벡터)
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        transform.Translate(moveDir.normalized * 0.1f);

        Debug.Log($"moveDir={moveDir.magnitude}");
        Debug.Log($"moveDir.normal={moveDir.normalized.magnitude}");

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

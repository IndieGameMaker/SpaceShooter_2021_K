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
        transform.position += Vector3.forward * 0.1f; // (0, 0, 1) * 0.1f = (0, 0, 0.1f)

        /* 정규화 벡터(Normalized Vector), 단위 벡터(Unit Vector)
            
            Vector3.forward == new Vector3(0, 0, 1)
            Vector3.up      == new Vector3(0, 1, 0)
            Vector3.right   == new Vector3(1, 0, 0)

            Vector3.one     == new Vector3(1, 1, 1)
            Vector3.zero    == new Vector3(0, 0, 0)
        */

        Debug.Log($"h={h} / v={v}");
    }
}

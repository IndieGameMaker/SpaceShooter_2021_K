using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    public GameObject sparkEffect;

    //충돌 콜백함수 (Collision Call Back Function)
    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("BULLET"))
        {
            //충돌 지점에 대한 정보를 추출
            //ContactPoint[] points = coll.contacts; //Garbage Collection
            ContactPoint cont = coll.GetContact(0);

            //법선 벡터
            Vector3 normal = cont.normal; //Vector3(0, 0, 1)
            // x 30, y 20, z 0
            //Quaternion.Euler(30, 20, 0)
            //법선 벡터를 쿼터니언 타입으로 변환
            Quaternion rot = Quaternion.LookRotation(-normal);

            //스파크 이펙트를 발생(생성)
            //Instantiate(생성객체, 좌표, 회전각도);
            GameObject spark = Instantiate(sparkEffect, cont.point, rot);
            Destroy (spark, 0.8f);

            Destroy(coll.gameObject);
        }
        

    }

    /*
    void OnCollisionStay(Collision coll)
    void OnCollisionExit(Collision coll)
    */

    /* Is Trigger 옵션이 체크됐을 경우 호출되는 콜백함수
    void OnTriggerEnter(Collider coll)
    void OnTriggerStay(Collider coll)
    void OnTriggerExit(Collider coll)   
    */

}

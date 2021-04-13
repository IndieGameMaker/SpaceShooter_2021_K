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

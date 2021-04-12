using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    //충돌 콜백함수 (Collision Call Back Function)
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "BULLET")
        {
            Destroy(coll.gameObject);
        }
    }

    /*
    void OnCollisionStay(Collision coll)
    void OnCollisionExit(Collision coll)
    */

}

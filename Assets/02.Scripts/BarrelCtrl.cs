using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
//using Random = System.Random; //C# API의 Random 클래스를 사용한다.

public class BarrelCtrl : MonoBehaviour
{
    public GameObject expEffect;
    public Texture[] textures;

    /*
        C# Standard Naming Rule

        Camel Case (카멜 케이스)   
        : nickName, userName, playerID
        : 변수(필드)

        Pascal Case(파스칼 케이스)
        : GetComponent<T>, SetPlayerID()
        : 클래스 명, 구조체 명, 메소드(함수), C# 프로퍼티
    */

    private int hitCount = 0;
    private new MeshRenderer renderer;

    void Start()
    {
        renderer = GetComponentInChildren<MeshRenderer>();
        //난수 발생
        int idx = Random.Range(0, textures.Length); //(0,3) => 0, 1, 2
        renderer.material.mainTexture = textures[idx];
    }

    //충돌 콜백함수 (충돌시 1번 호출)
    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("BULLET"))
        {
            ++hitCount;  //hitCount = hitCount + 1;
            if (hitCount == 3)
            {
                //폭발함수 호출
                ExpBarrel();
            }
        }
    }

    void ExpBarrel()
    {
        Rigidbody rb = this.gameObject.AddComponent<Rigidbody>();
        rb.AddForce(Vector3.up * 2000.0f);
        Destroy(this.gameObject, 2.0f);

        GameObject obj = Instantiate(expEffect,
                                     this.transform.position,
                                     Quaternion.identity);
        Destroy(obj, 5.0f);
    }
}

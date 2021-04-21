using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
//using Random = System.Random; //C# API의 Random 클래스를 사용한다.

public class BarrelCtrl : MonoBehaviour
{
    public GameObject expEffect;
    public Texture[] textures;
    public AudioClip expSFX;

    private int hitCount = 0;
    private new MeshRenderer renderer;
    private new AudioSource audio;

    void Start()
    {
        Debug.Log(Application.persistentDataPath);
        //AudioSource 컴포넌트 추출후 변수할당
        audio = GetComponent<AudioSource>();
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
        audio.PlayOneShot(expSFX, 1.0f);
        Rigidbody rb = this.gameObject.AddComponent<Rigidbody>();
        rb.AddForce(Vector3.up * 2000.0f);
        Destroy(this.gameObject, 2.0f);

        GameObject obj = Instantiate(expEffect,
                                     this.transform.position,
                                     Quaternion.identity);
        Destroy(obj, 5.0f);
    }
}

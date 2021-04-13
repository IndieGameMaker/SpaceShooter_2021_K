using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    AudioListener : 1

    AudioSource   : n

*/

[RequireComponent(typeof(AudioSource))]
public class FireCtrl : MonoBehaviour
{
    public Transform firePos;
    public GameObject bulletPrefab;
    public MeshRenderer muzzleFlash;  //public Renderer muzzleFlash;

    //[System.NonSerialized]
    [HideInInspector]
    public new AudioSource audio;
    public AudioClip fireSfx;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();   
        muzzleFlash = firePos.GetComponentInChildren<MeshRenderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void Fire()
    {
        //동적으로 프리팹을 복제하는 함수
        //Instantiate(생성할 객체, 위치값, 회전값)
        Instantiate(bulletPrefab, firePos.position, firePos.rotation);

        //총소리 발생
        // audio.clip = fireSfx;
        // audio.Play();
        audio.PlayOneShot(fireSfx , 0.8f);

        ShowMuzzleFlash();
    }

    void ShowMuzzleFlash()
    {
        // 난수 발생
        // Random.Range(min, max)
        // Random.Range(0, 3)  =>  0, 1, 2
        // Random.Range(0.0f, 3.0f)  => 0.0f ~ 3.0f

        // 텍스처의 Offset 값을 변경
        // (0, 0), (0.5, 0), (0.5, 0.5), (0, 0.5)
        //new Vector2(Random.Range(0,2) * 0.5f, Random.Range(0,2) * 0.5f);
        //Random.Range(0,2) * 0.5f //(0,1) * 0.5 => 0, 0.5

        Vector2 offset = new Vector2(Random.Range(0,2), Random.Range(0,2)) * 0.5f;
        //(0, 1) * 0.5f = (0, 0.5)
        //(1, 1) * 0.5f = (0.5, 0.5)

        muzzleFlash.material.mainTextureOffset = offset;
        //muzzleFlash.material.SetTextureOffset("_MainTex", offset);

        //Muzzle Flash 불규칙한 회전처리
        //Quaternion.Euler 오일러각도를 쿼터니언 타입으로 변환하는 함수(메소드)
        //Quaternion rot = Quaternion.Euler(0, 0, Random.Range(0, 360));
        Quaternion rot = Quaternion.Euler(Vector3.forward * Random.Range(0, 360));
        muzzleFlash.transform.localRotation = rot;

    }

}

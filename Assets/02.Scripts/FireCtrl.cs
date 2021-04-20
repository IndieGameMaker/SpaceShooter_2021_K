using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    AudioListener : 1

    AudioSource   : n

    Projectile 방식
    Raycasting 방식

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
    IEnumerator Start()
    {
        audio = GetComponent<AudioSource>();   
        muzzleFlash = firePos.GetComponentInChildren<MeshRenderer>(); 
        muzzleFlash.enabled = false;

        Debug.Log("1st");

        yield return null;

        Debug.Log("2nd");

        yield return new WaitForSeconds(10.0f);

        Debug.Log("3rd");
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

        StartCoroutine(ShowMuzzleFlash());
    }

    IEnumerator ShowMuzzleFlash()
    {
        Vector2 offset = new Vector2(Random.Range(0,2), Random.Range(0,2)) * 0.5f;

        muzzleFlash.material.mainTextureOffset = offset;
        //muzzleFlash.material.SetTextureOffset("_MainTex", offset);

        //Muzzle Flash 불규칙한 회전처리
        //Quaternion.Euler 오일러각도를 쿼터니언 타입으로 변환하는 함수(메소드)
        //Quaternion rot = Quaternion.Euler(0, 0, Random.Range(0, 360));
        Quaternion rot = Quaternion.Euler(Vector3.forward * Random.Range(0, 360));
        muzzleFlash.transform.localRotation = rot;

        // Muzzle Flash의 크기를 불규칙하게 변경처리
        // localScale 
        // 균등 스케일로 설정
        muzzleFlash.transform.localScale = Vector3.one * Random.Range(1.0f, 3.0f);

        muzzleFlash.enabled = true;

        yield return new WaitForSeconds(0.3f);

        muzzleFlash.enabled = false;

    }

}

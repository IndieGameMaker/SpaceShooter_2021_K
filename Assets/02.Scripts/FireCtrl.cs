using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    AudioListener : 1

    AudioSource   : n

*/

public class FireCtrl : MonoBehaviour
{
    public Transform firePos;
    public GameObject bulletPrefab;

    public new AudioSource audio;
    public AudioClip fireSfx;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();    
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
    }
}

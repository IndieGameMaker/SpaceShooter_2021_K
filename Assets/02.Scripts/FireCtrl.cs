using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    public Transform firePos;
    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //동적으로 프리팹을 복제하는 함수
            //Instantiate(생성할 객체, 위치값, 회전값)
            Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        }
    }
}

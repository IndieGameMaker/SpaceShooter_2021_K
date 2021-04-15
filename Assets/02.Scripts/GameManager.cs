using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public Transform[] points;
    public List<Transform> points = new List<Transform>();
    public GameObject monsterPrefab;

    void Start()
    {
        //List에 저장할 경우
        GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>(points);

        //Resources 폴더에 있는 프리핍을 로딩
        monsterPrefab = Resources.Load<GameObject>("monster");

        //배열에 저장할 경우
        //points = GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>();      
        
        
        // Transform tr = GameObject.Find("SpawnPointGroup").transform;
        
        // foreach (Transform _tr in tr) //배열 또는 List
        // {
        //     points.Add(_tr);
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
싱글턴 디자인 패턴
Singleton Design Pattern
*/
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    //public Transform[] points;
    public List<Transform> points = new List<Transform>();
    public GameObject monsterPrefab;

    //몬스터의 생성 주기(sec)
    public float createTime = 3.0f;

    public bool isGameOver = false;

    void Awake()
    {
        //싱글턴 초기화
        if (instance == null) //처음 실행했을 경우
        {
            instance = this; //Instance ID
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this) //두번째 호출됐을 경우
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        //List에 저장할 경우
        GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>(points);

        //Resources 폴더에 있는 프리핍을 로딩
        monsterPrefab = Resources.Load<GameObject>("monster");
        
        //(호출할_함수, 지연시간, 반복시간)
        //InvokeRepeating("CreateMonster", 2.0f, createTime);
        //CancelInvoke("CreateMonster");

        StartCoroutine(CreateMonster());
    }

    IEnumerator CreateMonster()
    {
        yield return new WaitForSeconds(2.0f);

        while (!isGameOver)
        {
            //난수 발생
            int idx = Random.Range(1, points.Count);
            //몬스터를 생성
            GameObject monster = Instantiate<GameObject>(monsterPrefab);
            monster.name = "Monster";
            //몬스터의 위치설정
            monster.transform.position = points[idx].position;
            
            //벡터의 뺄셈연산 = (원점 - 몬스터의좌표)
            Vector3 dir = points[0].position - points[idx].position;
            //벡터를 이용해 쿼터니언 타입의 각도를 산출
            Quaternion rot = Quaternion.LookRotation(dir);
            //몬스터의 각도를 설정
            monster.transform.rotation = rot;

            yield return new WaitForSeconds(createTime);
        }
    }

}

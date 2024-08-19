using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public int stagelevel;
    //public int stageindex;
    //public GameObject[] Stage;
    //public Playermove player;
    // Start is called before the first frame update
    void Start()
    {
       // stagelevel = 0; 여기에 NextStage함수 만들고 플레이어 스크립트에 충돌할 때 호출하려는 걸 실패함
    }


    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   


}

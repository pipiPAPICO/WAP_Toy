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
       // stagelevel = 0; ���⿡ NextStage�Լ� ����� �÷��̾� ��ũ��Ʈ�� �浹�� �� ȣ���Ϸ��� �� ������
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

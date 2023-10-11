using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    private int life;

    private void Start()
    {
        life = 3;
        
        EventMgr.Instance.AddListener("GameSucess",GameSucess);
    }

    private void Update()
    {
        TestGameEnd();
    }
    /*private void OnTriggerEnter(Collider other) //怪物碰到终点
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("碰到了");
            life -= 1;
            EventMgr.Instance.Emit("LifeChange",life);
            Destroy(other.gameObject);
        }
    }*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("pengdao ");
        }
    }

    private void GameSucess(string eventname, object udata)
    {
        UIMgr.Instance.ShowUIView("GameSucess");
    }

    private void TestGameEnd()
    {
        if (life <= 0)//游戏失败，结束
        {
            Time.timeScale = 0;
            UIMgr.Instance.ShowUIView("GameFailed");
        }
    }
}

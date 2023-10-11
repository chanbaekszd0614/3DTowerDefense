using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLunch : UnitySingleton<GameLunch>
{
    public override void Awake()
    {
        base.Awake();
        //初始化游戏框架
        this.gameObject.AddComponent<ResMgr>();
        this.gameObject.AddComponent<UIMgr>();
        this.gameObject.AddComponent<EventMgr>();
        //初始化游戏模块
        this.gameObject.AddComponent<Game>();
    }

    private void Start()
    {
        Game.Instance.GameStart();
    }
}

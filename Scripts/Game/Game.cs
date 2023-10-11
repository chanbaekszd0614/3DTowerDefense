using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : UnitySingleton<Game>
{
    public override void Awake()
    {
        base.Awake();
    }

    //游戏逻辑入口
    public void GameStart()
    {
        //释放游戏开始UI
        UIMgr.Instance.ShowUIView("StartUI");
    }
    

    //释放游戏场景+ui
    public void EnterIndexGameScene(int index)
    {
        //释放地图
        GameObject mapPrefab = ResMgr.Instance.GetAssetCache<GameObject>("Map/Level"+index+".prefab");
        GameObject map = GameObject.Instantiate(mapPrefab);
        map.AddComponent<GameMgr>().InitGame(index);
        
        //释放UI
        UIMgr.Instance.ShowUIView("GameUI");
        //添加摄像机脚本
        //GameObject.FindGameObjectWithTag("MainCamera").AddComponent<CameraCtrl>();
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    public static GameMgr Instance = null;
    private EnemyCreator creator;

    private void Awake() {
        GameMgr.Instance = this;
        
    }

    public void InitGame(int index)
    {
        int levelID = index + 100;
        LevelConfig levelData = GameConfigDataBase.GetConfigData<LevelConfig>(levelID.ToString());
        //绑定敌人生成器代码
        this.gameObject.transform.GetChild(0).AddComponent<EnemyCreator>().EnemyGenerator(levelData);
        //添加炮台点击交互脚本
        this.gameObject.AddComponent<ClickChooseBase>();
        //添加金币管理脚本
        this.gameObject.AddComponent<GoldMgr>();
        //添加终点代码
        this.gameObject.transform.GetChild(1).AddComponent<GameEnd>();

    }

    public void DestroyMap()
    {
        Destroy(this.gameObject);
    }
}

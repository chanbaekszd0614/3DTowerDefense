using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCreator : MonoBehaviour
{
    private int time;//每个丧尸生成间隔时间
    private int times;//每波丧尸生成间隔
    private int zombiCount;//每波丧尸数
    private int waveCount;//每关波数
    public  GameObject[] enemys;//每关内丧尸种类

    public void EnemyGenerator(LevelConfig level)
    {
        time = level.time;
        times = level.times;
        zombiCount = level.zombiCount;
        waveCount = level.waveCount;
        string[] typeStr=level.zombiType.Split(',');
        enemys = new GameObject[typeStr.Length];
        for (int i = 0; i < typeStr.Length; i++)//将关卡配置表中丧尸种类添加到GameObject组中
        {
            enemys[i] = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(
                "Assets/AssetPackages/Zombies/Enemys/Zombie_" + int.Parse(typeStr[i]) + ".prefab");
        }
        StartCoroutine(CreateEnemy());

        Debug.Log(TestZombisIsAllDie());
        if (TestZombisIsAllDie())
        {
            EventMgr.Instance.Emit("GameSucess",true);
        }
    }

    private IEnumerator CreateEnemy()
    {
        for (int i = waveCount; i >0; i--)
        {
            for (int j = 0; j < zombiCount; j++)
            {
                GameObject agentEnemy=Instantiate(enemys[Random.Range(0,enemys.Length)],transform.position,Quaternion.identity);
                /*GameObject agentEnemy = ObjectPool.Instance.GetObject(enemys[Random.Range(0, enemys.Length)]);
                agentEnemy.transform.position = transform.position; 
                agentEnemy.transform.rotation=transform.rotation;*/
                agentEnemy.transform.SetParent(this.gameObject.transform);
                agentEnemy.AddComponent<EnemySport>();
                agentEnemy.AddComponent<EnemyChangeHp>();
                yield return new WaitForSeconds(times);
            }
            yield return new WaitForSeconds(time);
            EventMgr.Instance.Emit("ChangeWaveCount",i-1);
        }
    }

    private bool TestZombisIsAllDie()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).gameObject.activeInHierarchy)
                return false;
        }
        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public GameObject target = null;
    public  TowerAI towerAI=null;
    private float time;
    private string turret;
    private List<TurretConfig> turretConfig;
    Dictionary<string, int> turretDic = new Dictionary<string, int>();
    void Start()
    {
        turretConfig = GameConfigDataBase.GetConfigDatas<TurretConfig>();
        Tempmapping();
        time = 0;
        turret = this.gameObject.name;
    }

    void Update()
    {
        //子弹移动
        transform.Translate(Vector3.forward * Time.deltaTime * 15f);
        time += Time.deltaTime;
        //子弹出发3s后销毁
        if (time >= 3.0f)
        {
            Destroy(gameObject);
            time = 0;
        }
        BulletAttack();
    }

    private void BulletAttack()
    {
        if (target != null)
        {
            if (Vector3.Distance(transform.position, target.transform.position) < 1f)
            {
                Destroy(target);
                towerAI.enemys.Remove(target);
                towerAI.targetEnemy = null;
                Destroy(gameObject);
                EventMgr.Instance.Emit("KillZombiesGold",100);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Tempmapping()
    {
        for (int i = 0; i < turretConfig.Count; i++)
        {
            int index = int.Parse(turretConfig[i].id) % 100;
            turretDic.Add(
                turretConfig[i].turretName, index);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerAI : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> enemys;//可以攻击的列表
    [HideInInspector]
    public GameObject targetEnemy;//目标怪物
    private float distance;
    private Transform turret;//炮台可旋转部分
    private float times;
    private GameObject bulletPrefab;
    private Transform firPos;
    private List<TurretConfig> turretConfig;
    Dictionary<string, int> turretDic = new Dictionary<string, int>();
    public static TurretConfig turretInfo;
    private string selectTurret;
    private int indexer;
    
    void Start()
    {
        distance = 100000f;
        enemys = new List<GameObject>();
        targetEnemy = null;
        turret = transform.GetChild(0);
        times = 0;
        firPos = turret.GetChild(0);
        turretConfig = GameConfigDataBase.GetConfigDatas<TurretConfig>();
        Tempmapping();
        selectTurret = SelectTurretUI_UICtrl.selectTurret;
        indexer = NextTurretUI_UICtrl.indexer;
        turretInfo=GameConfigDataBase.GetConfigData<TurretConfig>("3" + indexer + turretDic[selectTurret]);
        Debug.Log(turretInfo.id);
        bulletPrefab =
            ResMgr.Instance.GetAssetCache<GameObject>("Turrets/Bullets/" + turretInfo.BulletPrefab + ".prefab");
    }

    void Update()
        {
            if (enemys.Count > 0)
            {
                if (targetEnemy == null) //没有目标时，选择目标
                {
                    targetEnemy = SelectTarget();
                }
            }

            if (targetEnemy != null)
            {
                //前奏攻击
                LookTarget();
            }
        }

        private void OnTriggerEnter(Collider other) //怪物进入攻击范围
        {
            if (other.tag == "Enemy"&&!enemys.Contains(other.gameObject))
            {
                //加入可攻击列表
                enemys.Add(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other) //当怪物离开
        {
            if (other.tag == "Enemy")
            {
                //移出可攻击列表,当目标敌人离开攻击范围，将目标敌人置空，然后寻找下一个目标
                if (other.name == targetEnemy.name)
                {
                    targetEnemy = null;
                }
                if (enemys.Contains(other.gameObject))
                {
                    enemys.Remove(other.gameObject);
                }
            }
        }

        private GameObject SelectTarget() //选择攻击目标
        {
            GameObject temp = null;
            float distances = 0f;
            //找到可攻击列表中离炮台距离最近的敌人作为目标
            for (int i = 0; i < enemys.Count; i++)
            {
                if (enemys[i].gameObject != null)//解决已加入攻击列表，但是被提前销毁的敌人
                {
                    distances = Vector3.Distance(this.transform.position, enemys[i].transform.position);
                    if (distances <= distance) 
                    {
                        distance = distances;
                        temp = enemys[i];
                    }
                }
            }
            distance = 10000f;
            return temp;
        }

        //炮台瞄准
        private void LookTarget()
        {
            Vector3 pos = targetEnemy.transform.position;
            pos.y = turret.position.y;
            turret.LookAt(pos);
            times += Time.deltaTime;
            if (times >= 1)
            {
                //产生实际打击效果
                Attack();
                times = 0;
            }
        }

        private void Attack()
        {
            //生产子弹
            GameObject bullet = Instantiate(bulletPrefab, firPos.position, Quaternion.identity);
            //给子弹挂脚本
            bullet.AddComponent<BulletMove>().target=targetEnemy;
            bullet.transform.LookAt(targetEnemy.transform);
            bullet.GetComponent<BulletMove>().towerAI = this;
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

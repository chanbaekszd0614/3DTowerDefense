using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;

public class NextTurretUI_UICtrl : UI_ctrl
{
	private List<TurretConfig> turretConfig;
	Dictionary<string, int> turretDic = new Dictionary<string, int>();
	private GameObject chooseTurretBase; //选择地基
	private string selectTurret;
	public static int indexer;

	public override void Awake()
	{
		base.Awake();
	}

	void Start()
	{
		selectTurret = null;
		indexer = 0;
		turretConfig = GameConfigDataBase.GetConfigDatas<TurretConfig>();
		Tempmapping();

		this.add_button_listener("Create", CreateTurret);
		this.add_button_listener("LevelUp", LevelUpTurret);
		this.add_button_listener("Sell", SellTurret);
		this.add_button_listener("CloseBtn", ColseUI);
	}

	private void CreateTurret()
	{
		selectTurret = SelectTurretUI_UICtrl.selectTurret;
		chooseTurretBase = ClickChooseBase.chooseTurretBase;
		int cout = GetActiveChildCount(chooseTurretBase);
		if (cout < 2) //此时该基地无塔
		{
			indexer = 0;
			TurretConfig turretConfig =
				GameConfigDataBase.GetConfigData<TurretConfig>("3" + indexer + turretDic[selectTurret]);
			GameObject turretPrefab = ResMgr.Instance.GetAssetCache<GameObject>("Turrets/" + selectTurret + ".prefab");
			GameObject tempTurret = ObjectPool.Instance.GetObject(turretPrefab);
			tempTurret.transform.SetParent(chooseTurretBase.transform);
			tempTurret.GetComponent<SphereCollider>().radius = turretConfig.turretATKR;
			tempTurret.AddComponent<TowerAI>();
			tempTurret.transform.localPosition = new Vector3(0, 2.79f, 0);
			ColseUI();
			EventMgr.Instance.Emit("CostGold",turretConfig.turretPrice);
		}
		else
		{
			Debug.Log("有塔，请升级");
			this.transform.Find("Create").GetComponent<Button>().interactable = false;
		}
	}

	private void LevelUpTurret()
	{
		selectTurret = SelectTurretUI_UICtrl.selectTurret;
		chooseTurretBase = ClickChooseBase.chooseTurretBase;
		int cout = GetActiveChildCount(chooseTurretBase);
		GameObject turretPrefab = null;
		if (cout >= 2)
		{
			GameObject activeChild = GetActiveChild(chooseTurretBase);
			ObjectPool.Instance.PushObject(activeChild);
			if (indexer <=2)
			{
				Debug.Log(selectTurret);
				indexer++;
				if (indexer == 1)
				{
					turretPrefab =
						ResMgr.Instance.GetAssetCache<GameObject>("Turrets/Mid" + selectTurret + ".prefab");
				}
				else
				{
					turretPrefab =
						ResMgr.Instance.GetAssetCache<GameObject>("Turrets/Sen" + selectTurret + ".prefab");
				}
				
				GameObject tempTurret = ObjectPool.Instance.GetObject(turretPrefab);
				
				tempTurret.transform.SetParent(chooseTurretBase.transform);
				TurretConfig turretConfig =
					GameConfigDataBase.GetConfigData<TurretConfig>("3" + indexer + turretDic[selectTurret]);
				tempTurret.GetComponent<SphereCollider>().radius = turretConfig.turretATKR;
				tempTurret.AddComponent<TowerAI>();
				tempTurret.transform.localPosition = new Vector3(0, 2.79f, 0);
				ColseUI();
				EventMgr.Instance.Emit("CostGold",turretConfig.turretPrice);
			}
		}
	}

	private void SellTurret()
	{
		selectTurret = SelectTurretUI_UICtrl.selectTurret;
		chooseTurretBase = ClickChooseBase.chooseTurretBase;
		int cout = GetActiveChildCount(chooseTurretBase);
		//Debug.Log(cout);
		if (cout >= 2) //此时该基地有塔
		{
			ObjectPool.Instance.PushObject(chooseTurretBase.transform.GetChild(1).gameObject);
			
			string name = chooseTurretBase.transform.GetChild(1).gameObject.name.Replace("(Clone)",string.Empty);
			TurretConfig turret = GameConfigDataBase.GetConfigData<TurretConfig>("3" + indexer + turretDic[name]);
			
			selectTurret = null;
			EventMgr.Instance.Emit("GetGold",turret.turretPrice);
			indexer = 0;
			ColseUI();
		}
		else
		{
			Debug.Log("无塔不可卖");
		}
	}

	private void ColseUI()
	{
		selectTurret = null;
		this.gameObject.SetActive(false);
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

	public static int GetActiveChildCount(GameObject chooseTurretBase)
	{
		int cout = 0;
		for (int i = 0; i < chooseTurretBase.transform.childCount; i++)
		{
			if (chooseTurretBase.transform.GetChild(i).gameObject.activeInHierarchy)
			{
				cout++;
			}
		}
		return cout;
	}

	private GameObject GetActiveChild(GameObject chooseTurretBase)
	{
		GameObject activeTurret=null;
		for (int i = 1; i < chooseTurretBase.transform.childCount; i++)
		{
			if (chooseTurretBase.transform.GetChild(i).gameObject.activeInHierarchy)
				activeTurret = chooseTurretBase.transform.GetChild(i).gameObject;
		}
		return activeTurret;
	}
	
}

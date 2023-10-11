using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SelectTurretUI_UICtrl : UI_ctrl
{
	public static string selectTurret;
	private int turretCount;
	public override void Awake() {
		base.Awake();
	}

	void Start()
	{
		selectTurret = null;
		foreach (var i in GameConfigDataBase.GetConfigDatas<TurretConfig>())
		{
			if (int.Parse(i.id) % 300 < 10)
			{
				turretCount++;
			}
		}

		for (int i = 1; i <= turretCount; i++)
		{
			int index = 300 + i;
			string name = GameConfigDataBase.GetConfigData<TurretConfig>(index.ToString()).turretName;
			this.add_button_listener(name,()=>OpenNextPanel(name));
		}
		this.add_button_listener("CloseBtn",CloseSelectTurretUI);
		SetPrice(0);
	}

	private void SetPrice(int index)
	{
		for (int i = 1; i <= 4; i++)
		{
			TurretConfig turretConfig = GameConfigDataBase.GetConfigData<TurretConfig>("3" + index + i);
			this.view[turretConfig.turretName + "/SaleTex"].GetComponent<Text>().text = turretConfig.turretPrice.ToString();
		}
	}

	private void OpenNextPanel(string name)
	{
		selectTurret = name;
		this.transform.parent.transform.Find("NextTurretUI").gameObject.SetActive(true);
		this.gameObject.SetActive(false);
	}

	private void CloseSelectTurretUI()
	{
		selectTurret = null;
		this.transform.parent.gameObject.SetActive(false);
	}


}

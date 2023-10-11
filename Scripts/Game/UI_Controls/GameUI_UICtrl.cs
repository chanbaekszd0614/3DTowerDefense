using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameUI_UICtrl : UI_ctrl
{
	private int Levelid;
	private LevelConfig level;

	public override void Awake() {
		base.Awake();
	}

	void Start()
	{
		Levelid = LevelSelect_UICtrl.LevelID;
		level = GameConfigDataBase.GetConfigData<LevelConfig>(Levelid.ToString());
		
		this.view["RightBottom/Slider/ShowZombi"].GetComponent<Text>().text =
			level.waveCount.ToString() + "/" + level.waveCount.ToString();
		EventMgr.Instance.AddListener("ChangeWaveCount",ChangeWaveCount);

		this.view["LeftTop/ShowGold"].GetComponent<Text>().text = "500";
		EventMgr.Instance.AddListener("ChangeGold",ChangeGold);
		
		EventMgr.Instance.AddListener("LifeChange",LifeChange);
		
		this.add_button_listener("RightTop/SettingBtn",()=>OnOpenUI("SettingUI"));
		this.add_button_listener("LeftTop/AddBtn",()=>OnOpenUI("StoreUI"));
	}

	private void ChangeWaveCount(string eventName, object udata)
	{
		this.view["RightBottom/Slider/ShowZombi"].GetComponent<Text>().text =
			udata + "/" + level.waveCount.ToString();
		this.view["RightBottom/Slider"].GetComponent<Slider>().value = float.Parse(udata.ToString()) / level.waveCount;
	}

	private void ChangeGold(string eventName, object udata)
	{
		int gold = int.Parse(udata.ToString());
		if (gold <= 0)
		{
			this.view["LeftTop/ShowGold"].GetComponent<Text>().text = "0";
		}
		else
		{
			this.view["LeftTop/ShowGold"].GetComponent<Text>().text = udata.ToString();
		}
	}

	private void LifeChange(string eventName, object udata)
	{
		int temp = int.Parse(udata.ToString());
		this.view["LeftBottom"].transform.GetChild(temp).gameObject.SetActive(false);
	}

	private void OnOpenUI(string uiName)//打开uiName面板
	{
		Time.timeScale = 0;
		if (!this.transform.Find(uiName))
		{
			GameObject setting=UIMgr.Instance.ShowUIView(uiName).gameObject;
			setting.transform.SetParent(this.gameObject.transform);
		}
		else
		{
			this.transform.Find(uiName).gameObject.SetActive(true);
		}
	}

}

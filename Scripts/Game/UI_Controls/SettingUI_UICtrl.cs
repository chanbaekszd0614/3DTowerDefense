using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SettingUI_UICtrl : UI_ctrl {

	public override void Awake() {
		base.Awake();
	}

	void Start()
	{
		this.add_button_listener("RestartBtn",RestartGame);
		this.add_button_listener("ExitBtn",ExitGameScene);
		this.add_button_listener("CloseLevelChooseBtn",BackGame);
		//this.add_slider_listener("VoiceSlider",ChangeVoice);
	}

	private void BackGame()
	{
		this.gameObject.SetActive(false);
		Time.timeScale = 1;
	}

	private void ExitGameScene()
	{
		this.transform.parent.parent.Find("StartUI").gameObject.SetActive(true);
		GameMgr.Instance.DestroyMap();//销毁地图
		this.gameObject.SetActive(false);
		Destroy(this.transform.parent.gameObject);
	}

	private void RestartGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	private void ChangeVoice()
	{
		
	}

}

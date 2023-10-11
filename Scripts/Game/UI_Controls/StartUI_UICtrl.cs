using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;

public class StartUI_UICtrl : UI_ctrl
{
	public static Animator startAim;
	private GameObject levelSelect;
	[HideInInspector]
	public bool isPlay;
	public override void Awake() {
		base.Awake();
	}
	void Start() {
		this.add_button_listener("StartBtn",OnStartClick);
		this.add_button_listener("LeaveBtn",OnExitGameClick);
		startAim=this.GetComponent<Animator>();
		startAim.enabled = false;
	}

	private void OnStartClick()//进入关卡选择UI
	{
		startAim.enabled = true;
		startAim.SetBool("play",false);
		StartCoroutine(timer());//动画播完在跳UI【另一种实现方法：动画事件实现】
	}

	IEnumerator timer()
	{
		yield return new WaitForSeconds(1.0f);
		if (!this.transform.Find("LevelSelect"))
		{
			levelSelect=UIMgr.Instance.ShowUIView("LevelSelect").gameObject;
			levelSelect.transform.SetParent(this.transform);
		}
		else
		{
			this.transform.Find("LevelSelect").gameObject.SetActive(true);
		}
	}

	private void OnExitGameClick()
	{
		Application.Quit();
	}
	public static void PlayBackAnim()
	{
		startAim.SetBool("play",true);
	}

}

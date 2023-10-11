using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelSelect_UICtrl : UI_ctrl
{
	private int cout;
	public static int LevelID=0;
	public override void Awake() {
		base.Awake();
	}

	void Start()
	{
		cout = 8;
		for (int i = 1; i <= cout; i++)
		{
			GameObject levelPrefab = ResMgr.Instance.GetAssetCache<GameObject>("GUI/UI_SpritePrefabs/Level.prefab");
			GameObject level = GameObject.Instantiate(levelPrefab);
			level.transform.SetParent(this.transform.Find("Levels"));
			level.name = "Level" + i;
			level.transform.GetChild(0).gameObject.GetComponent<Text>().text = i.ToString();
			int temp = i;
			//this.add_button_listener("Levels/Level"+temp,()=>EnterIndexGameScene(temp));
			level.GetComponent<Button>().onClick.AddListener(()=>EnterIndexGameScene(temp));
		}
		this.add_button_listener("CloseLevelChooseBtn",BackStartUI);
	}

	private void EnterIndexGameScene(int temp)
	{
		LevelID = 100 + temp;
		Game.Instance.EnterIndexGameScene(temp);
		this.gameObject.transform.parent.gameObject.SetActive(false);
	}

	private void BackStartUI()
	{
		StartUI_UICtrl.PlayBackAnim();
		this.gameObject.SetActive(false);
	}
}

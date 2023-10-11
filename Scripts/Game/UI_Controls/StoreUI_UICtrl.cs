using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class StoreUI_UICtrl : UI_ctrl {

	public override void Awake() {
		base.Awake();
	}

	void Start()
	{
		this.add_button_listener("CloseBtn",OnCloseUI);
	}

	private void OnCloseUI()
	{
		this.gameObject.SetActive(false);
	}

}

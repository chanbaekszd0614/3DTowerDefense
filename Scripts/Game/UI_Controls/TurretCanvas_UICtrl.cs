using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class TurretCanvas_UICtrl : UI_ctrl {

	public override void Awake() {
		base.Awake();
		this.transform.Find("SelectTurretUI").gameObject.AddComponent<SelectTurretUI_UICtrl>();
		this.transform.Find("NextTurretUI").gameObject.AddComponent<NextTurretUI_UICtrl>();
	}
}

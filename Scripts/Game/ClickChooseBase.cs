using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class ClickChooseBase : MonoBehaviour
{
    private GameObject turretCanvas;
    public static GameObject chooseTurretBase;

    void Start()
    {
        turretCanvas=UIMgr.Instance.ShowUIView("TurretCanvas").gameObject;
        chooseTurretBase = null;
    }
    

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            ChooseBase();
        }
    }

    private void ChooseBase()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 50) &&
            EventSystem.current.IsPointerOverGameObject() == false) //当射线碰到物体，且此时没有点击到UI时
        {
            if (hit.transform.tag == "TowerBase") //点击了地基，可以创建
            {
                ShowChooseUI(hit.transform);//在选择的基地上显示炮塔UI
                chooseTurretBase = hit.transform.gameObject;
            }
        }
    }

    private void ShowChooseUI(Transform chooseBase)
    {
        int cout = NextTurretUI_UICtrl.GetActiveChildCount(chooseBase.gameObject);
        if (cout >= 2)//有塔
        {
            turretCanvas.transform.GetChild(1).gameObject.SetActive(true);
            turretCanvas.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            turretCanvas.transform.GetChild(0).gameObject.SetActive(true);
            turretCanvas.transform.GetChild(1).gameObject.SetActive(false);
        }
        turretCanvas.transform.SetParent(chooseBase);
        turretCanvas.transform.localPosition = new Vector3(0, 10, 0);
        turretCanvas.transform.localRotation=Quaternion.Euler(60,0,0);
    }

}

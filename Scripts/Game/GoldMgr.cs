using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldMgr : MonoBehaviour
{
    private int gold;
    void Start()
    {
        gold = 500;
        EventMgr.Instance.AddListener("CostGold",this.CostGold);
        EventMgr.Instance.AddListener("GetGold",this.GetGold);
        //StartCoroutine(AddGoldPerseconds());
    }

    IEnumerator AddGoldPerseconds()
    {
        while (true)
        {
            gold += 20;
            EventMgr.Instance.Emit("ChangeGold", gold);
            Debug.Log(gold);
            yield return new WaitForSeconds(2.0f);
        }
    }

    private void CostGold(string name, object udata)
    {
        int costGold = int.Parse(udata.ToString());
        gold -= costGold;
        EventMgr.Instance.Emit("ChangeGold",gold);
    }

    private void GetGold(string name, object udata)
    {
        int getGold = int.Parse(udata.ToString());
        gold += getGold;
        EventMgr.Instance.Emit("ChangeGold",gold);
    }
}

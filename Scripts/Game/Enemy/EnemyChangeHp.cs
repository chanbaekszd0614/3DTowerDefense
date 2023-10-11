using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyChangeHp : MonoBehaviour
{
    private Slider hpSlider;
    private int zombiesHp;
    void Start()
    {
        hpSlider = this.transform.GetChild(2).GetComponent<Slider>();
    }
    
    void Update()
    {
        
    }
    
}

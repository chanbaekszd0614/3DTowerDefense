using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    private float scrollSpeed;
    private float MaxScale = 53f;
    private float MinScale = 10f;
    void Start()
    {
        scrollSpeed = 500f;
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");//获取鼠标滚轮值，范围（-1,1）
        if (scroll != 0)
        {
            transform.position += Vector3.up * scroll * scrollSpeed * Time.deltaTime;
        }

        Vector3 pos = transform.position;
        pos.y = Mathf.Clamp(pos.y,MinScale, MaxScale);
        transform.position = pos;
    }
    
}

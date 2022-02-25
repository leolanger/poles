using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorial : MonoBehaviour
{
    private Text rule;
    private float waitTimer;
    private int sur;

    // Start is called before the first frame update
    void Start()
    {
        rule = GetComponent<Text>();
        waitTimer = 4f;
        rule.text = "W上移、A左移、\nS下移、D右移";
        sur = 0;
    }

    // Update is called once per frame
    void Update()
    {
        waitTimer -= Time.deltaTime;
        if(waitTimer <=0 )
        {
            sur++;
            if (sur == 1)
            { 
                rule.text = "地图中红色物体为北极，\n蓝色物体为南极";
                waitTimer = 4f;
            }
            if (sur == 2)
            {
                rule.text = "Q切换自身属性为北极，\nE切换自身属性为南极，\n空格切换自身属性为无磁性";
                waitTimer = 4f;
            }
            if (sur == 3)
            { 
                rule.text = "同极相斥、异极相吸哦~";
                waitTimer = 4f;
            }
            if (sur == 4)
                Destroy(this.gameObject);
        }
    }
}

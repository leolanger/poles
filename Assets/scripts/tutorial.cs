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
        rule.text = "W���ơ�A���ơ�\nS���ơ�D����";
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
                rule.text = "��ͼ�к�ɫ����Ϊ������\n��ɫ����Ϊ�ϼ�";
                waitTimer = 4f;
            }
            if (sur == 2)
            {
                rule.text = "Q�л���������Ϊ������\nE�л���������Ϊ�ϼ���\n�ո��л���������Ϊ�޴���";
                waitTimer = 4f;
            }
            if (sur == 3)
            { 
                rule.text = "ͬ����⡢�켫����Ŷ~";
                waitTimer = 4f;
            }
            if (sur == 4)
                Destroy(this.gameObject);
        }
    }
}

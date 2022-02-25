using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    private GameObject mag;
    private Rigidbody2D rb2d;

    public float magnetScope = 3;
    public int magnetism;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        mag = this.gameObject;
        if(mag.tag =="north_pole")
        {
            Debug.Log(mag.tag);
            magnetism = 1;
        }
        else if(mag.tag == "south_pole")
        {
            magnetism = 2;
        }
        else if(mag.tag == "fe")
        {
            magnetism = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((Player.instance.myPosition - rb2d.position).magnitude <= 3 && magnetism != 0)
        {
            if (magnetism == Player.instance.myMagnetism)
            {
                Vector2 direction = Player.instance.myPosition - rb2d.position;
                Player.instance.moveByForce(direction);
            }
            else if(magnetism + Player.instance.myMagnetism == 3)
            {
                Vector2 direction = rb2d.position - Player.instance.myPosition;
                Player.instance.moveByForce(direction);
            }
        }
        else if (magnetism == 0 && Player.instance.myMagnetism != 0 && (Player.instance.myPosition - rb2d.position).magnitude <= 3)
        {
            Vector2 direction = rb2d.position - Player.instance.myPosition;
            Player.instance.moveByForce(direction);
        }
    }
}

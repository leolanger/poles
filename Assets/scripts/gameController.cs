using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour
{
    private int count;
    private int pieceCount;
    private bool flag = true;

    public AudioSource step;
    public AudioSource getPiece;

    public static gameController instance;
    public int myCount { get { return count; } }
    public int myPieceCount { get { return pieceCount; } }

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        count = 0;
        pieceCount = 0;
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Player.instance != null && Player.instance.myIsMoving == true && flag)
        {
            step.Play();
            flag = false;
        }
        else if (Player.instance.myIsMoving == false)
        {
            flag = true;
            step.Stop();
        }
    }

    public void addCount()
    {
        count++;
    }

    public void addPieceCount()
    {
        getPiece.Play();
        pieceCount++;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class button : MonoBehaviour
{
    public void Yes()
    {
        SceneManager.LoadScene("chapter1");
    }

    public void No()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}

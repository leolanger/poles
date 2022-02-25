using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class text : MonoBehaviour
{
    private Text countText;

    // Start is called before the first frame update
    void Start()
    {
        countText = this.GetComponent<Text>();
        countText.text = gameController.instance.myCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("workers");
        }
    }
}

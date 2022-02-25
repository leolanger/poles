using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piece : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void picked()
    {
        gameController.instance.addPieceCount();
        Destroy(this.gameObject);
    }
}

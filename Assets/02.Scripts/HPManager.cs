using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{


    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D HP)
    {
        if (HP.tag == "obstacle")
        {
            
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

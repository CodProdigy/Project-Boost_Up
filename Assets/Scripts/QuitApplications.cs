using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplications : MonoBehaviour
{
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("We pushed");
            Application.Quit();
        }
    }
}

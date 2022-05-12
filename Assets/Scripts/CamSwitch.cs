using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera cam1;
    public Camera cam2;
    
    void Start()
    {
        cam1.enabled = true;
        cam2.enabled = false;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            cam1.enabled = !cam1.enabled;
            cam2.enabled = !cam2.enabled;
        }
    }
}
   

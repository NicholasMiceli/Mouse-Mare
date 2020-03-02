using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLock : MonoBehaviour
{

    bool gamePaused = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("Escape") && (gamePaused == false))
        {
            GetComponent<PlayerLook>().enabled = false;
            gamePaused = true;
        }
        if (Input.GetKeyDown("Escape") && (gamePaused == true))
        {
            GetComponent<PlayerLook>().enabled = true;
            gamePaused = false;
        }
    }


}

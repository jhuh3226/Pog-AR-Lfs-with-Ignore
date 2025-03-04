﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case10p2SceneFound : MonoBehaviour
{
    public GameObject gameObContainingefaultTrackableEventHandlerCase2Script;
    public GameObject POGCrossing;
    public GameObject car;

    public GameObject CV1Text;

    //canvas back QR
    public Canvas CVGoBackQR;

    public bool scriptTurnOnDone = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DefaultTrackableEventHandlerCase2 script = gameObContainingefaultTrackableEventHandlerCase2Script.GetComponent<DefaultTrackableEventHandlerCase2>();

        if (script.targetFound == true)
        {

            if (scriptTurnOnDone == false)
            {
                turnOnScripts();
                CVGoBackQR.enabled = true;

                scriptTurnOnDone = true;
            }
        }
    }

    //when the scene starts, enable pogbot movement and car movement
    public void turnOnScripts()
    {
        POGCrossing.GetComponent<Case10p2BeizerCurvePogBot>().enabled = true;

        car.GetComponent<Case10p2BeizerCurveCar>().enabled = true;

        CV1Text.GetComponent<Canvas>().enabled = true;
    }
}

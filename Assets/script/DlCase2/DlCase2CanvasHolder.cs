﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DLTool;

public class DlCase2CanvasHolder : MonoBehaviour

{
    public Button btRunnerStart;
    // runner script
    public GameObject eventSystem;
    // canvas
    public Canvas CVStartRunner;

    // script DlCase2BeizerCurveCar
    public GameObject newCar;
    // script DlCase2BeizerCurvePogBot
    public GameObject pogBot;

    // get info from runner
    public GameObject gameObRunner;

    // Start is called before the first frame update
    void Start()
    {
        Button btnRunnerStart = btRunnerStart.GetComponent<Button>();
        btnRunnerStart.onClick.AddListener(TaskOnClickBtRunnerStart);
    }

    // Update is called once per frame
    void Update()
    {
        // move car according to the beizercurve only after deeplearning
        DLTool.DlCase2Runner runner = gameObRunner.GetComponent<DLTool.DlCase2Runner>();
        if(runner.runnerDone)
        {
            newCar.GetComponent<DlCase2BeizerCurveCar>().enabled = true;
            pogBot.GetComponent<DlCase2BeizerCurvePogBot>().enabled = true;
        }
    }

    void TaskOnClickBtRunnerStart()
    {
        print("bt runnerStart Clicked");

        // disable CVStartRunner
        CVStartRunner.enabled = false;

        // enable runner script
        eventSystem.GetComponent<DLTool.DlCase2Runner>().enabled = true;
    }
}
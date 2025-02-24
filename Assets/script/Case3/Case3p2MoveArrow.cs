﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case3p2MoveArrow : MonoBehaviour
{
    //public Canvas pedestrianArrow;
    //public RectTransform pedestrianArrow;
    Vector3 startingPosition;

    Vector3 startPosition;
    Vector3 endPosition;
    public float speed;

    int countArrowRepeat;
    bool moveArrow;

    //bool sending to Case2p2CanvasHolder
    public bool arrowMovedThreeTimes;


    // Start is called before the first frame update
    void Start()
    {
        //pedestrianArrow = GameObject.Find("CanvasArrow").GetComponent<RectTransform>();
        startingPosition = transform.localPosition;

        countArrowRepeat = 0;
        moveArrow = true;

        arrowMovedThreeTimes = false;
    }

    // Update is called once per frame
    void Update()
    {
        MoveArrow();

        if (this.transform.localPosition.z >= 0.8f)
        {
            if (moveArrow == true)
            {
                GoBackToOriginalPosition();
            }
        }

        if (countArrowRepeat >= 4)
        {
            //stop moving the arrow
            moveArrow = false;

            this.transform.localPosition = new Vector3(0.54f, 0f, 0.19f);

            //bool sending to Case2p2CanvasHolder
            arrowMovedThreeTimes = true;
        }
    }

    void MoveArrow()
    {
        startPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        endPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0.89f);

        transform.localPosition = Vector3.Lerp(startPosition, endPosition, speed * Time.deltaTime);
    }

    void GoBackToOriginalPosition()
    {
        this.transform.localPosition = new Vector3(0.54f, 0f, 0.19f);
        //stopPedestrianArrowMovement = true;

        //Debug.Log(countArrowRepeat);

        //restartArrow = true;

        startPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        endPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0.89f);

        transform.localPosition = Vector3.Lerp(startPosition, endPosition, speed * Time.deltaTime);

        countArrowRepeat = countArrowRepeat + 1;
    }
}

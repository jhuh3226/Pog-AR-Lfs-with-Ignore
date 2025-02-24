﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

namespace DLTool {

    public class DlCase2Runner : MonoBehaviour
    {
        private PIXEL_FORMAT mPixelFormat = PIXEL_FORMAT.UNKNOWN_FORMAT;
        private bool mAccessCameraImage = true;
        private bool mFormatRegistered = false;

        private int ORIGINAL_WIDTH;
        private int ORIGINAL_HEIGHT;
        public const int IMAGE_WIDTH = 256;
        public const int IMAGE_HEIGHT = 320;

        public static Camera cam;

        public int speed;

        Vector3 startPosition;
        Vector3 endPosition;
        int counter = 0;
        public GameObject car;
        public GameObject carPoint0;
        public GameObject carPoint1;
        public GameObject carPoint2;
        public GameObject carPoint3;
        public float carStartXAdjustValue;
        public float carStartYAdjustValue;
        public float carStartZAdjustValue;
        public float carLastXAdjustValue;
        public float carLastYAdjustValue;
        public float carLastZAdjustValue;

        public GameObject pogBot;
        public GameObject pogPoint0;
        public GameObject pogPoint1;
        public GameObject pogPoint2;
        public GameObject pogPoint3;
        public float pogStartXAdjustValue;
        public float pogStartYAdjustValue;
        public float pogStartZAdjustValue;
        public float pogLastXAdjustValue;
        public float pogLastYAdjustValue;
        public float pogLastZAdjustValue;

        public bool runnerDone = false;     // var to check deeplearning done or not

        private bool isDone = false;

        public Texture2D textureMap;

        public List<Vector3> a;
        public List<Vector3> x;
        public List<Vector3> y;

        /**/
        //public GameObject checkXLast;
        //public float yDLValue;
        //public GameObject checkYLast;
        /**/

        void Start()
        {
            // set candidate pixel format
            mPixelFormat = PIXEL_FORMAT.RGBA8888;
            // Register Vuforia life-cycle callbacks:
            VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
            VuforiaARController.Instance.RegisterTrackablesUpdatedCallback(OnTrackablesUpdated);
            cam = Camera.main;
        }

        void OnVuforiaStarted()
        {
        // set image format
        if (CameraDevice.Instance.SetFrameFormat(mPixelFormat, true))
        {
            Debug.Log("Successfully registered pixel format " + mPixelFormat.ToString());
            mFormatRegistered = true;
        }
        else
        {
            Debug.LogError(
                "\nFailed to register pixel format: " + mPixelFormat.ToString() +
                "\nThe format may be unsupported by your device." +
                "\nConsider using a different pixel format.\n");
            mFormatRegistered = false;
        }
        }

        void Update()
        {
            if (isDone)
            {
                runnerDone = true;      // deeplearning done


                if (counter < a.Count)
                {
                    /**/
                    //checkYLast.transform.position = new Vector3(y[y.Count - 1].x, y[y.Count - 1].y, a[counter].z);
                    /**/

                    //float newX = a[counter].x - a[0].x + a[0].x + 3.0f;
                    //float newY = a[counter].y - a[0].y + a[0].y + 2.0f;

                    // original counter value is 5, substituted to 'speed'
                    //counter += speed;

                    // new setting for the Y position, where car is situated on right side(upper line)
                    //float carNewX = a[a.Count - 1].x + carXAdjustValue;
                    //carXAdjustValue = 7.5
                    float carStartNewX = a[0].x * carStartXAdjustValue;
                    float carLastNewX = a[a.Count - 1].x * carLastXAdjustValue;
                    float carNewY = a[counter].y * carStartYAdjustValue;
                    
                    float carP0X = carLastNewX;
                    float carP1X = carLastNewX - (carLastNewX - carStartNewX) * 1 / 3;
                    float carP2X = carLastNewX - (carLastNewX - carStartNewX) * 2 / 3;
                    float carP3X = carStartNewX;

                    //car.transform.position = new Vector3(carNewX, carNewY, a[counter].z);

                    // setting position of the car beizercarve
                    carPoint0.transform.position = new Vector3(carP0X, carNewY, 3.0f);
                    carPoint1.transform.position = new Vector3(carP1X, carNewY, 3.0f);
                    carPoint2.transform.position = new Vector3(carP2X, carNewY, 3.0f);
                    carPoint3.transform.position = new Vector3(carP3X, carNewY, 3.0f);

                    // console out
                    //Debug.Log("X position: " + newX);
                    //Debug.Log("Y position: " + newY);
                    Debug.Log(a[counter].z);

                    //Debug.Log("X length first: " + a[0].x);
                    //Debug.Log("X length last: " + a[a.Count - 1].x);
                    //Debug.Log("Y length first: " + a[0].y);
                    //Debug.Log("Y length last: " + a[a.Count - 1].y);

                    //Debug.Log("X length first: " + x[0].x);
                    //Debug.Log("Y length first: " + y[0].y);

                    float pogStartNewX = (a[a.Count - 1].x - a[0].x) * pogStartXAdjustValue;
                    float pogStartNewY = a[0].y * pogStartYAdjustValue;
                    float pogStartNewZ = 3.0f * pogStartZAdjustValue;
                    float pogLastNewX = (a[a.Count - 1].x - a[0].x) * pogLastXAdjustValue;
                    float pogLastNewY = a[a.Count - 1].y * pogLastYAdjustValue;
                    float pogLastNewZ = 3.0f * pogLastZAdjustValue;

                    pogPoint0.transform.position = new Vector3(pogStartNewX, pogStartNewY, pogStartNewZ);
                    pogPoint1.transform.position = new Vector3(pogLastNewX-(pogLastNewX-pogStartNewX)*2/3, pogLastNewY - (pogLastNewY - pogStartNewY) * 2 / 3, pogLastNewZ - (pogLastNewZ - pogStartNewZ) * 2 / 3);
                    pogPoint2.transform.position = new Vector3(pogLastNewX - (pogLastNewX - pogStartNewX) * 1 / 3, pogLastNewY - (pogLastNewY - pogStartNewY) * 1 / 3, pogLastNewZ - (pogLastNewZ - pogStartNewZ) * 1 / 3);
                    pogPoint3.transform.position = new Vector3(pogLastNewX, pogLastNewY, pogLastNewZ);


                    // initial pogbot position is relative to the position oof newX and Y
                    //pogBot.transform.localPosition = new Vector3(pogLastNewX, pogLastNewY, pogLastNewZ);
                }
                else
                {
                    counter = 0;
                }
            }
        }

        public void OnTrackablesUpdated()
        {
            if (mFormatRegistered)
            {
                if (mAccessCameraImage)
                {
                    Vuforia.Image image = CameraDevice.Instance.GetCameraImage(mPixelFormat);
                    ORIGINAL_WIDTH = image.Width;
                    ORIGINAL_HEIGHT = image.Height;
                    if (image != null)
                    {
                        if (counter == 100)
                        {
                            Texture2D texture = new Texture2D(image.Width, image.Height, TextureFormat.RGBA32, false);
                            image.CopyToTexture(texture);
                            texture = ImageTool.ScaleTexture(texture, IMAGE_WIDTH, IMAGE_HEIGHT);
                            // ORIGINAL_WIDTH = textureMap.width;
                            // ORIGINAL_HEIGHT = textureMap.height;

                            // Texture2D texture = new Texture2D(textureMap.width, textureMap.height, TextureFormat.RGBA32, false);
                            // texture = ImageTool.ScaleTexture(textureMap, IMAGE_WIDTH, IMAGE_HEIGHT);
                            Color32[] pixels = texture.GetPixels32();
                            Detection model = new Detection();
                            Texture2D result = model.Segmentation(pixels);
                            // result = FlipTexture(result);
                            // StreamWriter writer = new StreamWriter("./test.txt", true);
                            // for (int i = 0; i < result.width; ++i)
                            // {
                            //   for (int j = 0; j < result.height; ++j)
                            //   {
                            //     Color a = result.GetPixel(i, j);
                            //     int r = a.r > 0.5 ? 1 : 0;
                            //     writer.Write(r);
                            //   }
                            //   writer.Write('\n');
                            // }
                            // writer.Close();
                            result = ImageTool.ScaleTexture(result, ORIGINAL_WIDTH, ORIGINAL_HEIGHT);
                            Dictionary<int, int> line = ImageTool.GetLine(result, 'x', 2.3f);

                            // StreamWriter writer1 = new StreamWriter("./indices.txt", true);
                            List<Vector3> WorldPoints = ImageTool.GetWorldPoints(line);

                            // StreamWriter writer = new StreamWriter("./test.txt", true);

                            // for (int i = 0; i < WorldPoints.Count; ++i)
                            // {
                            //   writer.Write("x: ");
                            //   writer.Write(WorldPoints[i].x);
                            //   writer.Write(",");
                            //   writer.Write("y: ");
                            //   writer.Write(WorldPoints[i].y);
                            //   writer.Write("\n");
                            // }
                            // writer.Close();
                            this.a = WorldPoints;

                            /*added*/
                            //Dictionary<int, int> lineX = ImageTool.GetLine(result, 'x', 0.5f);
                            //Dictionary<int, int> lineY = ImageTool.GetLine(result, 'y', yDLValue);

                            //List<Vector3> WorldPointsX = ImageTool.GetWorldPoints(lineX);
                            //List<Vector3> WorldPointsY = ImageTool.GetWorldPoints(lineY);

                            //this.x = WorldPointsX;
                            //this.y = WorldPointsY;
                            /*-----*/

                            mAccessCameraImage = false;
                            isDone = true;
                        }
                        else
                        {
                        counter += 1;
                        }
                    }
                }
            }
        }
    }
}
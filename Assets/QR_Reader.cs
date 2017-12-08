using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using ZXing;
using ZXing.QrCode;

public class QR_Reader : MonoBehaviour
{

    private WebCamTexture camTexture;
    private Rect screenRect;
    public Texture2D image_file;
    public Text texteroo;
    public Button accept;
    public Button reject;
 
    void Start()
    {
        screenRect = new Rect(0, 0, Screen.width, Screen.height);
        camTexture = new WebCamTexture();
        camTexture.requestedHeight = Screen.height;
        camTexture.requestedWidth = Screen.width;
        if (camTexture != null)
        {
            camTexture.Play();
        }

        //testing!
        try
        {
            IBarcodeReader barcodeReader = new BarcodeReader();
            // decode the current frame
            var result = barcodeReader.Decode(image_file.GetPixels32(),
              image_file.width, image_file.height);
            Debug.Log(result.Text);
            if (result != null)
            {
                string [] pro_results = result.Text.Split(new string [] {"/!/"}, StringSplitOptions.None);
                texteroo.text = "would you like to send " + pro_results[0] + "to " + pro_results[1] + " at the address " + pro_results[2];

            }
        }
        catch (Exception ex) { Debug.LogWarning(ex.Message); }
    }

    void OnGUI()
    {
        // drawing the camera on screen
        GUI.DrawTexture(screenRect, camTexture, ScaleMode.ScaleToFit);
        // do the reading — you might want to attempt to read less often than you draw on the screen for performance sake
        try
        {
            IBarcodeReader barcodeReader = new BarcodeReader();
            // decode the current frame
            var result = barcodeReader.Decode(camTexture.GetPixels32(),
              camTexture.width, camTexture.height);
            if (result != null)
            {
                Debug.Log("DECODED TEXT FROM QR:" + result.Text);
            }
        }
        catch (Exception ex) { Debug.LogWarning(ex.Message); }
    }
}
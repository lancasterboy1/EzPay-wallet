using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;

public class Generate_QR : MonoBehaviour {

    public InputField amount_imp;
    public Image image;
    public MasterScript master;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.Return))
            {
            Debug.Log("going");
            generate_QR_Code();
        }
	}
    void generate_QR_Code()
    {
        string amount = amount_imp.text;
        string alias = master.alias;
        string address = master.current_default_address;
        string data = amount + "/!/" + alias + "/!/" + address;
        Texture2D myQR = generateQR(data);
        Sprite spritey = Sprite.Create(myQR, new Rect(0, 0, myQR.width, myQR.height), new Vector2(0, 0));
        image.sprite = spritey;
    }
    private static Color32[] Encode(string textForEncoding,
  int width, int height)
    {
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };
        return writer.Write(textForEncoding);
    }

    public Texture2D generateQR(string text)
    {
        var encoded = new Texture2D(256, 256);
        var color32 = Encode(text, encoded.width, encoded.height);
        encoded.SetPixels32(color32);
        encoded.Apply();
        return encoded;
    }
    
}

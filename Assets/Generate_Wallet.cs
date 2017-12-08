using System.Collections;
using System.Security.Cryptography;
using System.Collections.Generic;
using UnityEngine;

public class Generate_Wallet : MonoBehaviour {

    // Use this for initialization
    void Generate_Wallet_Function ()
    {
        ECDsaCng key = new ECDsaCng(512);
        Debug.Log(key.Key);
    }
    byte[] Sign_Data (ECDsaCng key, byte[] data)
    {
        byte[] signed_data = key.SignData(data);
        return signed_data;
    }


}
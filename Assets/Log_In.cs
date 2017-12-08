using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Log_In : MonoBehaviour
{
    private HandleTextFile txtFileHandler = new HandleTextFile();
    //https://forum.unity.com/threads/mmorpg-style-login-and-registration-system.385066/
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Log_In_Attempt()
    {
        Local_Log_In();
    }
    void Local_Log_In()
    {
        txtFileHandler.ReadCredentials("Assets/Resources/credentials.txt");
    }
}

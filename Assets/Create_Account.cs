using System.Collections;
using System.Security.Cryptography;
using System.Text;
using System;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Create_Account : MonoBehaviour {
    public InputField username_inp;
    public InputField pass_inp;
    public InputField confirm_pass_inp;
    public Toggle online;
    private HandleTextFile txtFileHandler = new HandleTextFile();
    // Use this for initialization
    void Start () {
        pass_inp.contentType = InputField.ContentType.Password;
        confirm_pass_inp.contentType = InputField.ContentType.Password;
        username_inp.contentType = InputField.ContentType.Alphanumeric;
    }

    // Update is called once per frame
    public void Create_Account_Pressed()
    {
        if (online.isOn) { create_online_account(); }
        else { create_offline_account(); }
    }

    void create_online_account()
    {
        hash_pass();
    }
    void create_offline_account()
    {
        string pass= hash_pass();
        txtFileHandler.Writecredentails(username_inp.text, pass);
    }
    string hash_pass ()
    {
        string username;
        StreamReader reader = new StreamReader("Assets/Resources/credentials.txt");
        while (!reader.EndOfStream)
        {
            user_info = reader.ReadLine;

        }
        username = username_inp.text;
        if (pass_inp.text == confirm_pass_inp.text)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(pass_inp.text, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            return savedPasswordHash;

        }
        else
        {
            Debug.Log("ERROR1");
            return null;
        }
    }

}
public class HandleTextFile
{
    public void Writecredentails(string username, string passhash)
    {
        string path = "Assets/Resources/credentials.txt";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(username + "/!/" + passhash);
        writer.Close();

        //Re-import the file to update the reference in the editor
        AssetDatabase.ImportAsset(path);
        TextAsset asset = Resources.Load<TextAsset>("credentials.txt");

        //Print the text from the file
        Debug.Log(asset.text);
    }

    [MenuItem("Tools/Read file")]
    public void ReadCredentials(string path)
    {

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }

}

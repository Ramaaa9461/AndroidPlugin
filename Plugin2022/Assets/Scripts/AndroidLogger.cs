using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AndroidLogger : Logger_Base
{
    const string PLUGIN_CLASS_NAME = "com.example.logger2022.MainLogger";
    string LocationsLogs;

    AndroidJavaClass androidJavaClass;
    AndroidJavaObject androidJavaObject;

    public AndroidLogger(string LocationsLogs, TMPro.TextMeshProUGUI loggerText)
    {
        this.loggerText = loggerText;
        this.LocationsLogs = LocationsLogs;

        androidJavaClass = new AndroidJavaClass(PLUGIN_CLASS_NAME);
        androidJavaObject = androidJavaClass.CallStatic<AndroidJavaObject>("GetInstance", this.LocationsLogs);

        AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject Activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");

        androidJavaObject.CallStatic("receiveUnityActivity", Activity);
    }

    private void ShowAlert(string title, string message, Action confirm = null, Action cancel = null)
    {
        AlertCallback alertCallback = new AlertCallback();
        alertCallback.positiveAction = confirm;
        alertCallback.negativeAction = cancel;

        androidJavaObject.Call("CreateAlert", new object[] { title, message, alertCallback });
        androidJavaObject.Call("ShowAlert");
    }

    public override void ClearAllLogs()
    {
        ShowAlert("Clear all Logs.", "Do you want to continue?", () =>
        {
            androidJavaObject.Call("ClearAllLogs"); 
            loggerText.text = ""; 
        });   
    }

    public override void NewLog(string log)
    {
        androidJavaObject.Call("NewLog", log + "\n");

    }

    public override void ShowAllLogs()
    {
        loggerText.text = androidJavaObject.Call<string>("GetAllLogs");
    }

   
}

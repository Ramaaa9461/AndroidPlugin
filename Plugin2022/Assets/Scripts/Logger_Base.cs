using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Logger_Base
{
    private static string path = Application.persistentDataPath + "/Logs.txt";
    protected TMPro.TextMeshProUGUI loggerText;

    public abstract void NewLog(string logMessage);

    public abstract void ShowAllLogs();

    public abstract void ClearAllLogs();

    public static Logger_Base CreateLogger(TMPro.TextMeshProUGUI loggerText)
    {
        #if UNITY_ANDROID
            return new AndroidLogger(path, loggerText);
#else
            return new Default_Logger(path, loggerText);
#endif
    }
}

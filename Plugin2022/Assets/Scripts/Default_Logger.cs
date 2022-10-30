using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Default_Logger : Logger_Base
{
    private string LogsLocation;

    private string logs;

    public Default_Logger(string LogsLocation, TMPro.TextMeshProUGUI loggerText)
    {

        this.loggerText = loggerText;
        this.LogsLocation = LogsLocation;

        if (File.Exists(LogsLocation))
            logs = File.ReadAllText(LogsLocation);
    }

    public override void ClearAllLogs()
    {
        logs = "";

        loggerText.text = "";

        if (File.Exists(LogsLocation))
        {
            File.Delete(LogsLocation);
        }
    }

    public override void ShowAllLogs()
    {
        loggerText.text = logs;
    }

    public override void NewLog(string log_Message)
    {
        logs += log_Message + "\n";
        File.WriteAllText(LogsLocation, logs);

    }

   
}

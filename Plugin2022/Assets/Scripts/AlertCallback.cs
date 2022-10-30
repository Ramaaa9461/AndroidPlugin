using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertCallback : AndroidJavaProxy
{
    public Action positiveAction;
    public Action negativeAction;
    const string PLUGIN_INTERFACE_NAME = "com.example.logger2022.AlertCallback";

    public AlertCallback() : base(PLUGIN_INTERFACE_NAME){}

    public void OnPositive()
    {
        positiveAction?.Invoke();
    }

    public void OnNegative()
    {
        negativeAction?.Invoke();
    }
}

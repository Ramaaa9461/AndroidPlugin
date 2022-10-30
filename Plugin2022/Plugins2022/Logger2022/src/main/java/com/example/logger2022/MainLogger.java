package com.example.logger2022;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.util.Log;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;



public class MainLogger {

    private static MainLogger instance = new MainLogger();
    private static final String LOGTAG = "GuzmanLogger -> ";
    static String path;
    static String currentLogs = "";
    static File file;
    private static Activity unityActivity;
    AlertDialog.Builder builder;

    public static MainLogger GetInstance(String p)
    {
        path = p;
        ReadLog();
        return instance;
    }

    public static void  receiveUnityActivity(Activity activity)
    {
        unityActivity = activity;
    }

    public String GetAllLogs()
    {
        return currentLogs;
    }

    public void CreateAlert(String title, String message, AlertCallback alertCallback)
    {
        builder = new AlertDialog.Builder(unityActivity);
        builder.setTitle(title);
        builder.setMessage(message);
        builder.setCancelable(false);
        builder.setPositiveButton(
                "Confirm",
                new DialogInterface.OnClickListener()
                {
                    @Override
                    public void onClick(DialogInterface dialogInterface, int which)
                    {
                        alertCallback.OnPositive();
                        dialogInterface.cancel();
                    }
                }
        );

        builder.setNegativeButton(
                "Cancel",
                new DialogInterface.OnClickListener()
                {
                    @Override
                    public void onClick(DialogInterface dialogInterface, int which)
                    {
                        alertCallback.OnNegative();
                        dialogInterface.cancel();
                    }
                }
        );
    }

    public void ShowAlert()
    {
        AlertDialog alert = builder.create();
        alert.show();
    }


    private static void ReadLog()
    {
        File file = new File(path);
        if(file.exists())
        {
            try {
                FileInputStream fistream = new FileInputStream(file);
                byte[] bytes = new byte[(int) file.length()];
                try {
                    fistream.read(bytes);
                }
                catch (IOException e)
                {
                    e.printStackTrace();
                }
                finally
                {
                    try
                    {
                        fistream.close();
                        currentLogs = new String(bytes);
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                }
            } catch (FileNotFoundException e) {
                e.printStackTrace();
            }
        }
    }

    public void NewLog(String message)
    {
        currentLogs += message;

        if(file == null)
            file = new File(path);
        else
            file.delete();

        try {
            if(file.createNewFile()) {
                FileOutputStream fostream = new FileOutputStream(file);
                try {
                    fostream.write(currentLogs.getBytes());
                } finally {
                    fostream.close();
                }
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public void ClearAllLogs()
    {
        currentLogs = "";

        if (file != null)
            file.delete();

    }
}
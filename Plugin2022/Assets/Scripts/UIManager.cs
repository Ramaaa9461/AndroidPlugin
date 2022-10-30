using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI text;
    [SerializeField] private TMPro.TMP_InputField LogInput;
    [SerializeField] Button clearButton;
    [SerializeField] Button sendButton;
    private Logger_Base logger;

    private void Awake()
    {
        logger = Logger_Base.CreateLogger(text);

        sendButton.onClick.AddListener(SendLogsButtonPressed);
        clearButton.onClick.AddListener(ClearLogsButtonPressed);
    }

    private void Start()
    {
        logger.ShowAllLogs();
    }

    public void SendLogsButtonPressed()
    {
        logger.NewLog(LogInput.text);
        LogInput.text = "";
        logger.ShowAllLogs();
    }

    public void ClearLogsButtonPressed()
    {

        logger.ClearAllLogs();
        
    }

}

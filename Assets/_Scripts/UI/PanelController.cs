using System;
using TMPro;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private TMP_Text finalPercentText;
    [SerializeField] private TMP_Text statusText;

    private void OnEnable()
    {
        EventManager.OnGameStart += CloseStartPanel;
        EventManager.OnGameFinished += OpenEndLevelPanel;
        EventManager.OnGameFinished += SetEndLevelPercentText;
    }
    
    private void OnDisable()
    {
        EventManager.OnGameStart -= CloseStartPanel;
        EventManager.OnGameFinished -= OpenEndLevelPanel;
        EventManager.OnGameFinished -= SetEndLevelPercentText;
    }

    private void CloseStartPanel()
    {
        startPanel.SetActive(false);
    }

    private void OpenEndLevelPanel()
    {
        endPanel.SetActive(true);
    }

    private void SetEndLevelPercentText()
    {
        var sliderController = GetComponent<SliderController>();
        finalPercentText.text = sliderController.SliderPercentText.text;
        
        if (sliderController.Percent < .5f)
        {
            statusText.text = "Failed";
            statusText.color = Color.red;
        }
    }

    public void StartGame()
    {
        EventManager.OnGameStart();
    }
    
    
}

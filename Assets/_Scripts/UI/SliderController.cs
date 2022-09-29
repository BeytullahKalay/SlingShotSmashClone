using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text sliderPercentText;
    private void OnEnable()
    {
        EventManager.UpdateSliderValue += SetSliderValues;
        EventManager.OnGameStart += ActivateSliderAndPercentText;
        EventManager.OnGameFinished += DeactivateSliderAndPercentText;
    }

    private void OnDisable()
    {
        EventManager.UpdateSliderValue -= SetSliderValues;
        EventManager.OnGameStart -= ActivateSliderAndPercentText;
        EventManager.OnGameFinished -= DeactivateSliderAndPercentText;
    }

    private void SetSliderValues(float newSliderVal)
    {
        _slider.value = newSliderVal;

        sliderPercentText.text ="%" + Mathf.Round((newSliderVal * 100)).ToString();
        
        if (newSliderVal >= 1)
        {
            EventManager.OnGameFinished();
        }
    }

    private void ActivateSliderAndPercentText()
    {
        _slider.gameObject.SetActive(true);
        sliderPercentText.gameObject.SetActive(true);
    }

    private void DeactivateSliderAndPercentText()
    {
        _slider.gameObject.SetActive(false);
        sliderPercentText.gameObject.SetActive(false);
    }

    public TMP_Text SliderPercentText => sliderPercentText;
    public float Percent => _slider.value;
}

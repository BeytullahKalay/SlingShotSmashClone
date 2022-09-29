using System.Collections;
using TMPro;
using UnityEngine;

public class ShotLeftController : MonoBehaviour
{
    [SerializeField] private TMP_Text shotLeftTMP;

    private void OnEnable()
    {
        EventManager.UpdateShotLeft += UpdateShotLeft;
        EventManager.OnGameStart += ActivateShotLeftText;
        EventManager.OnGameFinished += DeactivateShotLeftText;
    }

    private void OnDisable()
    {
        EventManager.UpdateShotLeft -= UpdateShotLeft;
        EventManager.OnGameStart -= ActivateShotLeftText;
        EventManager.OnGameFinished -= DeactivateShotLeftText;
    }

    private void Start()
    {
        EventManager.UpdateShotLeft(ManHolderManager.Instance.Men.Count);
    }

    private void UpdateShotLeft(int comingInt)
    {
        if (comingInt >= 1)
        {
            shotLeftTMP.text = comingInt.ToString() + " Shot Left";
        }
        else
        {
            shotLeftTMP.text = "No More Shots";
            
            // calling game finished event after 5 seconds
            StartCoroutine(CallOnGameFinished(3));
        }
    }

    private void ActivateShotLeftText()
    {
        shotLeftTMP.gameObject.SetActive(true);
    }

    private void DeactivateShotLeftText()
    {
        shotLeftTMP.gameObject.SetActive(false);
    }

    private IEnumerator CallOnGameFinished(float afterSeconds)
    {
        var finalTime = Time.time + afterSeconds;

        yield return new WaitForSeconds(finalTime);

        EventManager.OnGameFinished();
    }

}


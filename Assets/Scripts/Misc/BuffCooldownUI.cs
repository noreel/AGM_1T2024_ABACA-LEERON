using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffCooldownUI : MonoBehaviour
{

    public Image speedBuffIcon;
    public Image damageBuffIcon;
    public Image speedBuffRadialOverlay;
    public Image damageBuffRadialOverlay;

    private Coroutine speedBuffCoroutine;
    private Coroutine damageBuffCoroutine;
    void Start()
    {
        // Initially hide the buff icons
        speedBuffIcon.gameObject.SetActive(false);
        speedBuffRadialOverlay.gameObject.SetActive(false);
        damageBuffIcon.gameObject.SetActive(false);
        damageBuffRadialOverlay.gameObject.SetActive(false);
    }

    public void ShowSpeedBuff(float duration)
    {
        if (speedBuffCoroutine != null) 
        { 
            StopCoroutine(speedBuffCoroutine); 
        }
        speedBuffCoroutine = StartCoroutine(DisplayBuff(speedBuffIcon, speedBuffRadialOverlay, duration));
    }

    public void ShowDamageBuff(float duration)
    {
        if (damageBuffCoroutine != null)
        {
            StopCoroutine(damageBuffCoroutine);
        }
        damageBuffCoroutine = StartCoroutine(DisplayBuff(damageBuffIcon, damageBuffRadialOverlay, duration));
    }

    private IEnumerator DisplayBuff(Image buffIcon, Image radialOverlay, float duration)
    {
        buffIcon.gameObject.SetActive(true);
        radialOverlay.gameObject.SetActive(true);
        radialOverlay.fillAmount = 1f;

        float timeRemaining = duration;
        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            radialOverlay.fillAmount = timeRemaining / duration;
            yield return null;
        }

        buffIcon.gameObject.SetActive(false);
        radialOverlay.gameObject.SetActive(false);
    }
}



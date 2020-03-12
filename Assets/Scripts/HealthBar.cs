using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image ForegroundImage = null;
    [SerializeField] private float _updateSpeedSeconds = 0;


    void Awake()
    {
        GetComponentInParent<ApplyDamage>().OnHealthChanged += HandleHealthChanged;
    }

    private void HandleHealthChanged(float pct)
    {
        StartCoroutine(ChangeToPct(pct));
    }

    private IEnumerator ChangeToPct(float pct)
    {
        float preChangePct = ForegroundImage.fillAmount;
        float elapsed = 0f;

        while (elapsed < _updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            ForegroundImage.fillAmount = Mathf.Lerp(preChangePct,pct,elapsed/_updateSpeedSeconds);
            yield return null;
        }
        ForegroundImage.fillAmount = pct;
    }

}

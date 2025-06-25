
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;
    [SerializeField] private Image barImage;

    private void Start()
    {
        cuttingCounter.OnProgressBarChanged += CuttingCounter_OnProgressChanged;
        barImage.fillAmount = 0;
        Hide();
    }

    private void CuttingCounter_OnProgressChanged(object sender, CuttingCounter.OnProgressBarChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;
        if(e.progressNormalized == 0 || e.progressNormalized >= 1f)
            Hide();
        else 
            Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}

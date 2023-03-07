using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] GameObject hasProgressBarGameObject;
    [SerializeField] Image progressBarImg;

    private IHasProgressBar hasProgressBar;

   

    private void Start()
    {
        hasProgressBar = hasProgressBarGameObject.GetComponent<IHasProgressBar>();

        if(hasProgressBar == null)
        {
            Debug.LogError("GameObject " + hasProgressBarGameObject + "don't have IHasProgress Component");
        }
        hasProgressBar.OnProgressChanged += HasProgressBar_OnProgressChanged;

        progressBarImg.fillAmount = 0f;

        Hide();
    }

    private void HasProgressBar_OnProgressChanged(object sender, IHasProgressBar.OnProgressChangedEventArgs e)
    {
        progressBarImg.fillAmount = e.progressNormalized;

        if(e.progressNormalized == 0f || e.progressNormalized == 1f)
        {
            Hide();
        }
        else
        {
            Show();
        }
        
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

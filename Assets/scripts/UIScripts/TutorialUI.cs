using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    private void Start()
    {
        KitchenGameManager.Instance.OnStateChange += KitchenGameManager_OnStateChange;
        Show();
    }

    private void KitchenGameManager_OnStateChange(object sender, System.EventArgs e)
    {
        
        if (KitchenGameManager.Instance.IsCountDownToStartActive())
        { Hide(); }
    }

    private void Show()
    { gameObject.SetActive(true); }


    private void Hide()
    { gameObject.SetActive(false); }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;

    private void Start()
    {
        KitchenGameManager.Instance.OnStateChange += Instance_OnStateChange;
        Hide();
    }

    private void Instance_OnStateChange(object sender, System.EventArgs e)
    {
        if (KitchenGameManager.Instance.IsGameOver())
        {
            recipesDeliveredText.text = DeliveryManager.Instance.GetSuccessfulRecipesDelivered().ToString();
            Show();
        }
        else
        {
            Hide();
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

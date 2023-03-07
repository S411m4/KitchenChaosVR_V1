using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjects;

    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Instance_OnSelectedCounterChanged;
    }

    private void Instance_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }

    }
    private void Show()
    {
        foreach(GameObject visual in visualGameObjects)
        {
            visual.SetActive(true);

        }
    }

    private void Hide()
    {
        foreach (GameObject visual in visualGameObjects)
        {
            visual.SetActive(false);

        }
    }
}

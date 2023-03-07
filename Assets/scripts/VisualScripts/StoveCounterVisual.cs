using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] GameObject stoveFireEmissionGameObject;
    [SerializeField] GameObject particleSystemGameObject;

    [SerializeField] StoveCounter stoveCounter;



    private bool stoveOn;



    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }



    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        if (e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried)
        {
            stoveOn = true;
        }
        else
        {
            stoveOn = false;
        }

        stoveFireEmissionGameObject.SetActive(stoveOn);
        particleSystemGameObject.SetActive(stoveOn);
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveAudioScript : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] StoveCounter stoveCounter;

    private float warningSoundTimer;
    bool playWarningSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private void Start()
    {
        stoveCounter.OnStateChanged += Instance_OnStateChanged;
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgressBar.OnProgressChangedEventArgs e)
    {

        float burningShowPorgressAmount = .5f;

        playWarningSound = stoveCounter.IsFried() && e.progressNormalized >= burningShowPorgressAmount;
    }

    private void Instance_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool playSound = e.state == StoveCounter.State.Fried || e.state == StoveCounter.State.Frying;

        if(playSound)
        {

            audioSource.Play();
        }
        else
        {

            audioSource.Pause();
        }

    }

    private void Update()
    {
        if (playWarningSound)
        {
            warningSoundTimer -= Time.deltaTime;

            if (warningSoundTimer < 0f)
            {
                float warningSoundTimerMax = .2f;
                warningSoundTimer = warningSoundTimerMax;

                AudioManager.Instance.PlayWarnigSound(stoveCounter.transform.position);
            }
        }
        
    }
}

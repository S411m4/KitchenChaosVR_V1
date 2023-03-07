using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnFlashingUI : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;

    private Animator animator;

    private const string IS_FLASHING = "flash";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        animator.SetBool(IS_FLASHING, false);
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgressBar.OnProgressChangedEventArgs e)
    {
        float burnShowPorgressAmount = .5f;

        bool show = stoveCounter.IsFried() && e.progressNormalized >= burnShowPorgressAmount;

        animator.SetBool(IS_FLASHING, show);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class countDownToStartUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countDownText;

    private Animator animator;

    private int previousCountdownNo;

    private const string COUNTDOWN_TRIGGER = "NoPopUp";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        KitchenGameManager.Instance.OnStateChange += Instance_OnStateChange;
        Hide();
    }

    private void Instance_OnStateChange(object sender, System.EventArgs e)
    {
        if(KitchenGameManager.Instance.IsCountDownToStartActive())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Update()
    {
        //countDownText.text = KitchenGameManager.Instance.GetCountDownToStartTimer().ToString("#.##");

        int countDownNo = Mathf.CeilToInt(KitchenGameManager.Instance.GetCountDownToStartTimer());
        countDownText.text = countDownNo.ToString();

        if (previousCountdownNo != countDownNo)
        {
            
            previousCountdownNo = countDownNo;

            animator.SetTrigger(COUNTDOWN_TRIGGER);
            AudioManager.Instance.PlayCountDownSound();
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

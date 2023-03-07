using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;

    private void Awake()
    {
        soundEffectsButton.onClick.AddListener(()=> { AudioManager.Instance.ChangeVolume(); UpdateVisual(); });
        musicButton.onClick.AddListener(()=> { MusicManager.Instance.ChangeVolume();UpdateVisual(); });
        closeButton.onClick.AddListener(()=> { Hide(); GamePauseUI.Instance.Show(); });

        Instance = this;
    }

    private void Start()
    {
        UpdateVisual();
        Hide();
    }
    private void UpdateVisual()
    {
        KitchenGameManager.Instance.OnGameUnPaused += KitchenGameManager_OnGameUnPaused;

        soundEffectsText.text = "Sound Effects: " + Mathf.Round(AudioManager.Instance.GetVolume() * 10f);
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume()*10f);
    }

    private void KitchenGameManager_OnGameUnPaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    public void Show() { gameObject.SetActive(true) ; }
    public void Hide() { gameObject.SetActive(false) ; }
}

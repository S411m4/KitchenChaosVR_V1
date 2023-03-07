using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GamePauseUI : MonoBehaviour
{
    public static GamePauseUI Instance { get; private set; }
    
    [SerializeField] private GameObject gamePauseUI;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionsButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(()=> { KitchenGameManager.Instance.ToggleGamePause(); });
        mainMenuButton.onClick.AddListener(()=> { SceneLoader.Load(SceneLoader.Scene.MainMenuScene); });
        optionsButton.onClick.AddListener(()=> { OptionsUI.Instance.Show(); Hide(); });

        Instance = this;

    }
    private void Start()
    {
        KitchenGameManager.Instance.OnGamePaused += KitchenGameManager_OnGamePaused;
        KitchenGameManager.Instance.OnGameUnPaused += KitchenGameManager_OnGameUnPaused;
        Hide();
    }

    private void KitchenGameManager_OnGameUnPaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void KitchenGameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        Show();
    }

    public void Show(){ gamePauseUI.SetActive(true); }
    public void Hide(){ gamePauseUI.SetActive(false); }
}

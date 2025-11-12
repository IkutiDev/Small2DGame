using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour, InputSystem_Actions.IUIActions
{
    
    private enum GameState
    {
        MainMenu,
        Gameplay
    }

    private InputSystem_Actions _input;
    [Header("Cameras")]
    
    [SerializeField] private Camera _UICamera;
    
    [SerializeField] private Camera _GameCamera;

    [Header("UI")] 
    [SerializeField] private EventSystem _eventSystem;
    
    [SerializeField] private GameObject _mainMenu;

    [SerializeField] private Button _playGameButton;
    
    [SerializeField] private Button _exitGameButton;

    [Header("Settings")] 
    [SerializeField] private GameObject _firstSelectedElementInSettings;
    
    [SerializeField] private GameObject _settingsMenu;

    [SerializeField] private Button _settingsButton;

    [SerializeField] private Button _exitSettingsButton;


    private GameState _currentGameState = GameState.MainMenu;

    private void Awake()
    {
        _input = new InputSystem_Actions();
        _input.UI.SetCallbacks(this);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _playGameButton.onClick.AddListener(ShowGame);
        
        _exitGameButton.onClick.AddListener(ExitGame);
        
        _exitSettingsButton.onClick.AddListener(ShowMainMenu);
        
        _settingsButton.onClick.AddListener(ShowSettingsMenu);
        
        ShowMainMenu();

    }

    private void ShowMainMenu()
    {
        _eventSystem.SetSelectedGameObject(_playGameButton.gameObject);
        _currentGameState = GameState.MainMenu;
        UpdateCameras();
        _mainMenu.SetActive(true);
        _settingsMenu.SetActive(false);
    }

    private void ShowGame()
    {
        _currentGameState = GameState.Gameplay;
        UpdateCameras();
    }

    private void UpdateCameras()
    {
        _UICamera.gameObject.SetActive(_currentGameState == GameState.MainMenu);
        _GameCamera.gameObject.SetActive(_currentGameState == GameState.Gameplay);
    }

    private void ShowSettingsMenu()
    {
        _eventSystem.SetSelectedGameObject(_firstSelectedElementInSettings);
        _mainMenu.SetActive(false);
        _settingsMenu.SetActive(true);
    }
    
    private void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }

    public void OnNavigate(InputAction.CallbackContext context)
    {
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        ShowMainMenu();
    }
}

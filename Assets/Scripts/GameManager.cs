using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    
    private enum GameState
    {
        MainMenu,
        Gameplay
    }

    private const int ENEMY_COUNT = 1000;

    [SerializeField] private PlayerInput _input;
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

    [Header("Gameplay")] 
    [SerializeField] private PlayerController _playerInstance;

    [SerializeField] private EnemyController _enemyPrefab;


    private GameState _currentGameState = GameState.MainMenu;

    private List<EnemyController> _spawnedEnemies = new List<EnemyController>();

    private void Awake()
    {
        _input.actions["GoToMainMenu"].started += CancelOnstarted;
    }

    private void CancelOnstarted(InputAction.CallbackContext callback)
    {
        ShowMainMenu();
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
        ClearEnemies();
        _eventSystem.SetSelectedGameObject(_playGameButton.gameObject);
        _currentGameState = GameState.MainMenu;
        UpdateCameras();
        _mainMenu.SetActive(true);
        _settingsMenu.SetActive(false);
    }

    private void ShowGame()
    {
        SpawnEnemies();
        _playerInstance.transform.position = Vector3.zero;
        _currentGameState = GameState.Gameplay;
        UpdateCameras();
    }

    private void ClearEnemies()
    {
        for (int i = 0; i < _spawnedEnemies.Count; i++)
        {
            Destroy(_spawnedEnemies[i].gameObject);
        }
        _spawnedEnemies.Clear();
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < ENEMY_COUNT; i++)
        {
            Vector2 randomPoint = new(Random.Range(0f, 1f), Random.Range(0f, 1f));
            Vector2 enemyPosition = Camera.main.ViewportToWorldPoint(randomPoint);
            var enemyInstance = Instantiate(_enemyPrefab, enemyPosition, Quaternion.identity, _GameCamera.transform);
            _spawnedEnemies.Add(enemyInstance);
        }
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
}

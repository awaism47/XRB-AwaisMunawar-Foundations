using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameEventManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private CanvasGroup _canvasGroup;

    [SerializeField] private GameObject _failedPanel;
    [SerializeField] private GameObject _successPanel;
    [SerializeField] private float _canvasFadeTime = 2f;

    [Header("Audio")] 
    [SerializeField] private AudioSource _bgmSource;

    [SerializeField] private AudioClip _caughtMusic;
    
    private PlayerInput _playerInput;
    private FirstPersonController _fpController;
    private bool _isFadingIn = false;
    private float _fadeLevel = 0;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();
        foreach (EnemyController enemy in enemies)
        {
            enemy.onInvestigate.AddListener(EnemyInvestigating);
            enemy.onPlayerFound.AddListener(PlayerFound);
            enemy.onReturnToPatrol.AddListener(EnemyReturnToPatrol);
        }

        GameObject player = GameObject.FindWithTag("Player");
        if (player)
        {
            _playerInput =player.GetComponent<PlayerInput>();
            _fpController = player.GetComponent<FirstPersonController>();

        }
        else
        {
            Debug.LogWarning("There is no player in the scene");
        }

        _canvasGroup.alpha = 0;



    }

    private void EnemyReturnToPatrol()
    {
        
    }

    private void PlayerFound(Transform enemyThatFoundPlayer)
    {
        _isFadingIn = true;
        _fpController.CinemachineCameraTarget.transform.LookAt(enemyThatFoundPlayer);
        DeactivateInput();
        PlayBGM(_caughtMusic);
    }

    private void DeactivateInput()
    {
        _playerInput.DeactivateInput();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void PlayBGM(AudioClip music)
    {
        if (_bgmSource.clip == music) return;
        
        _bgmSource.clip = music;
        _bgmSource.Play();
    }

    private void EnemyInvestigating()
    {
        
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        _isFadingIn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isFadingIn)
        {
            if (_fadeLevel < 1f)
            {
                _fadeLevel += Time.deltaTime / _canvasFadeTime;
                
            }
            
        }
        else
        {
            if (_fadeLevel >0f)
            {
                _fadeLevel -= Time.deltaTime / _canvasFadeTime;
                
            }
            
        }
        _canvasGroup.alpha = _fadeLevel;
    }
}

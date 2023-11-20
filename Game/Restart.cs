using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class Restart : MonoBehaviour, IHealthObserver
{
    private HP _health;
    private HealthUI _healthUI;
    private LevelManager _levelManager;
    [SerializeField] private GameObject restartUI; 
    [SerializeField] private GameObject pauseUI; 
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject gameHUD;
    [SerializeField] private GameObject tutorialUI;
    private bool _isTutorial =  true;


    private void OnEnable()
    {
        _health.AddObserver(this);
    }
    private void OnDisable()
    {
        _health.RemoveObserver(this);
    }
    private void Awake()
    {
        _healthUI = gameObject.GetComponent<HealthUI>();
        _health = gameObject.GetComponent<HP>();
        _levelManager = gameObject.GetComponent<LevelManager>();
    }

    private void Start()
    {
        menuUI.SetActive(true);
    }

    public void OnHPChange(int hpChange)
    {
        CheckDeath(hpChange);
    }

    private void CheckDeath(int temp)
    {
        //Debug.Log("Restart.cs/ hpChange: " + temp + ". Current health: " + _health.Health);
        if (_health.Health <= 0)
        {
            GameLoose();
        }
    }
    private void GameLoose()
    {
        _levelManager.GameState(false);
        restartUI.SetActive(true);
        gameHUD.SetActive(false);

    }



    public void GameStart()
    {
        _levelManager.Restart();
        menuUI.SetActive(false);
        _health.Restart();
        _healthUI.Restart();
        if (_isTutorial) tutorialUI.SetActive(true);
        _isTutorial = false;
        gameHUD.SetActive(true);
    }

    public void GameRestart()
    {
        _levelManager.Restart();
        _health.Restart();
        _healthUI.Restart();
        restartUI.SetActive(false);
        gameHUD.SetActive(true);
    }

    public void ToMenu()
    {
        _levelManager.GameState(false);
        _levelManager.DestroyLevel();
        pauseUI.SetActive(false);
        restartUI.SetActive(false);
        menuUI.SetActive(true);
        gameHUD.SetActive(false);
    }

}

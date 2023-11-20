using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class LevelManager : MonoBehaviour, IHealthObserver
{
    [SerializeField] private List<GameObject> _levels = new();
    [SerializeField] private List<int> _ballsOnLevel = new();
    private int _currentLevelId = 0;
    private GameObject _currentLevel;

    [SerializeField] private TextMeshProUGUI textBallsLeft;
    private int _ballsLeft;
    private bool _isGame = true;
    private int _coinsEarned = 0;
    [SerializeField] private int _ballsToPointsCoef = 10;

    private HP _health;
    private Shop _shop;
    [SerializeField] private TextMeshProUGUI coinsEarnedText;

    void Awake()
    {
        _health = GetComponent<HP>();
        _shop = FindObjectOfType<Shop>();
    }

    private void OnEnable()
    {
        _health.AddObserver(this);
    }
    private void OnDisable()
    {
        _health.RemoveObserver(this);
    }

    public void OnHPChange(int hpChange)
    {
        if (hpChange < 1)
        {
            RemoveBall();
        }
    }

    public void RemoveBall()
    {
        _coinsEarned += 1;
        _ballsLeft -= 1;
        BallsLeftUI();
        Debug.Log("LvlManager.cs/ CoinsEarned: " + _coinsEarned);
        if (_ballsLeft == 0 && _isGame == true)
        {
            _currentLevelId += 1;
            if (_currentLevelId > 7) _currentLevelId = 7;
            PlayerPrefs.SetInt("Level", _currentLevelId);
            PlayerPrefs.Save();

            Destroy(_currentLevel);

            _currentLevel = Instantiate(_levels[_currentLevelId]);
            _ballsLeft = _ballsOnLevel[_currentLevelId];
            BallsLeftUI();

        }
    }

    public void GameState(bool isGame)
    {
       _isGame = isGame;
        if (_shop != null)
        {
            _shop.CangeCoins(_coinsEarned * _ballsToPointsCoef);
            coinsEarnedText.text = (_coinsEarned * _ballsToPointsCoef).ToString();
        }
        _coinsEarned = 0;
        PlayerPrefs.SetInt("Level", 0);
        PlayerPrefs.Save();
    }

    public void DestroyLevel()
    {
        Destroy(_currentLevel);
    }

    private void BallsLeftUI()
    {
        textBallsLeft.text = "LEFT:" + _ballsLeft;
    }


    public void Restart()
    {
        Destroy(_currentLevel);

        _currentLevelId = 0;
        _currentLevel = Instantiate(_levels[_currentLevelId]);
        _ballsLeft = _ballsOnLevel[_currentLevelId];
        BallsLeftUI();

        _isGame = true;
    }

}

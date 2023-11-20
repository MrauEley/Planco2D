using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour, IHealthObserver
{
    [SerializeField] private Image _healthStatic;
    [SerializeField] private Image _healthDinamic;
    [SerializeField] private Transform _healthBar;
    private List<Image> _healthList = new(); 
    private HP _health;
    void Awake()
    {
        _health = GetComponent<HP>();
    }

    void Start()
    {
        Restart();
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
        int currentHP = _healthList.Count + hpChange;
        if (currentHP < _healthList.Count && currentHP >= 0)
        {
            RemoveHP();
        }
        else if (currentHP > _healthList.Count)
        {
            AddHP();
        }
    }

    public void Restart()
    {
        _healthList = new();
        ClearHealthBar();
        ResetHP();
    }
    private void ClearHealthBar()
    {
        if (_healthBar.transform.childCount > 0)
        {
            foreach (Transform t in _healthBar.transform)
            {
                Destroy(t.gameObject);
            }
        }
    }
    private void ResetHP()
    {
        int maxHP = _health.startHealth;

        Image instantiatedObject = Instantiate(_healthDinamic, _healthBar);
        instantiatedObject.transform.SetAsFirstSibling();
        _healthList.Add(instantiatedObject);
        for (int i = 0; i < maxHP - 1; i++)
        {
            instantiatedObject = Instantiate(_healthStatic, _healthBar);
            instantiatedObject.transform.SetAsFirstSibling();
            _healthList.Insert(0, instantiatedObject);
        }
    }
    private void RemoveHP()
    {
        Destroy(_healthList[0].gameObject);
        _healthList.RemoveAt(0);
    }
    private void AddHP()
    {
        Image instantiatedObject = Instantiate(_healthStatic, _healthBar);
        instantiatedObject.transform.SetAsFirstSibling();
        _healthList.Insert(0, instantiatedObject);
    }
}

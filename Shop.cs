using System.Diagnostics;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject shopUI;
    [SerializeField] private int _priceRemoval = 200, _priceHP = 150;
    private int _coinsBalance = 300;
    [SerializeField] private TextMeshProUGUI coinsBalanceUI;
    private InstrumentManager _instrumentManager;

    private void Awake()
    {
        _instrumentManager = FindAnyObjectByType<InstrumentManager>();
        if (PlayerPrefs.HasKey("Coins"))
        {
            _coinsBalance = PlayerPrefs.GetInt("Coins");
            coinsBalanceUI.text = _coinsBalance.ToString();
        }
    }
    public void CangeCoins(int amount)
    {
        _coinsBalance += amount;
        coinsBalanceUI.text = _coinsBalance.ToString();
        PlayerPrefs.SetInt("Coins", _coinsBalance);
        PlayerPrefs.Save();
    }


    private bool BuyInstrumnt(int price)
    {
        if (_coinsBalance - price >= 0)
        {
            CangeCoins(-price);
            return true;
        }
        else return false;
    }

    public void BuyHP()
    {
        if(BuyInstrumnt(_priceHP))
            _instrumentManager.CountChangeHP(1);
    }

    public void BuyRemoval()
    {
        if(BuyInstrumnt(_priceRemoval))
            _instrumentManager.CountChangeRemoval(1);
    }
}

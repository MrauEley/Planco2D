using JetBrains.Annotations;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static InstrumentManager;

public class InstrumentManager : MonoBehaviour
{
    private int _countRemoval = 0, _countHP = 0, _countSizer = 0, _countFaster;
    private string _saveNameRemoval = "countRemoval", _nameHP = "countHP", _nameSizer= "countSizer", _nameFaster = "countFaster"; 
    public bool _isEnoughRemoval = false, _isEnoughHP = false, _isEnoughSizer = false, _isEnoughFaster = false;
    [SerializeField] private TextMeshProUGUI _textCountRemoval, _textCountHP, _textCountSizer, _textCountFaster;
    private InstrumentRemoval _instrumentRemoval;
    private InstrumentHP _instrumentHP;
    //private InstrumentSizer _instrumentSizer;
    //private InstrumentFaster _instrumentFaster;

    public struct Instrument
    {
        public int instrumentCount;
        public string instrumentSaveName;
        public TextMeshProUGUI instrumentTextUI;


        public Instrument(int countContainer, string saveContrainerName, TextMeshProUGUI textUI)
        {
            instrumentCount = countContainer;
            instrumentSaveName = saveContrainerName;
            instrumentTextUI = textUI;
        }   
        public Instrument UpdateCount(int newCount)
        {
            return new Instrument(newCount, instrumentSaveName, instrumentTextUI);
        } 
    }

    public List<Instrument> _listInstruments = new List<Instrument>(); // 0 -> removal, 1 -> +HP

    private void Awake()
    {
        _instrumentRemoval = FindObjectOfType<InstrumentRemoval>();
        _instrumentHP = FindObjectOfType<InstrumentHP>();
    }

    private void Start()
    {
        AddInstrument(_countRemoval, _saveNameRemoval, _textCountRemoval);
        AddInstrument(_countHP, _nameHP, _textCountHP);
        AddInstrument(_countSizer, _nameSizer, _textCountSizer);
        AddInstrument(_countFaster, _nameFaster, _textCountFaster);
        CountChangeRemoval(0);
        CountChangeHP(0);
        //CountChangeSizer(0);
        //CountChangeFaster(0);

    }

    private void AddInstrument(int countContainer, string saveContrainerName, TextMeshProUGUI textUI)
    {
        if (PlayerPrefs.HasKey(saveContrainerName))
        {
            countContainer = PlayerPrefs.GetInt(saveContrainerName);
        }
        Instrument temp = new Instrument(countContainer, saveContrainerName, textUI);
        _listInstruments.Add(temp);
    }

    public void CountChangeRemoval(int changeAmount)
    {
        CountChange(_listInstruments[0], changeAmount, _isEnoughRemoval);
    }

    public void CountChangeHP(int changeAmount)
    {
        CountChange(_listInstruments[1], changeAmount, _isEnoughHP);
    }

    /*public void CountChangeSizer(int changeAmount)
    {
        CountChange(_listInstruments[2], changeAmount, _isEnoughSizer);
    }*/

    /*public void CountChangeSizer(int changeAmount)
    {
        CountChange(_listInstruments[2], changeAmount, _isEnoughSizer);
    }*/

    private void CountChange(Instrument instrument, int changeAmount, bool state)
    {
        instrument.instrumentCount += changeAmount;
        PlayerPrefs.SetInt(instrument.instrumentSaveName, instrument.instrumentCount);
        PlayerPrefs.Save();
        if (instrument.instrumentCount > 0)
        {
            state = true;
        }
        else state = false;

        instrument.instrumentTextUI.text = instrument.instrumentCount.ToString();
    }

}

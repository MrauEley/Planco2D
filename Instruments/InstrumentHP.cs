using UnityEngine;

public class InstrumentHP : MonoBehaviour
{
    [SerializeField]private HP _health;
    private InstrumentManager _manager;

    private void Awake()
    {
        _manager = FindObjectOfType<InstrumentManager>();
    }

    public void AddHP()
    {
        if (_health.Health < _health.startHealth && _manager._isEnoughHP == true)
        {
            _health.HPChange(1);
            _manager.CountChangeHP(-1);
        }
    }
}

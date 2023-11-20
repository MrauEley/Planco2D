using UnityEngine;

public class SpawnBall : MonoBehaviour, IHealthObserver
{
    [SerializeField] private GameObject _ball;
    [SerializeField] private GameObject _particleSystem;
    [SerializeField] private HP _health;


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
        if(hpChange < 1)
        {
            _particleSystem.SetActive(true);
        }
    }
    public void Spawn()
    {
        if(transform.childCount == 0)
        {
            GameObject temp = Instantiate(_ball, transform.position, transform.rotation);
            temp.transform.SetParent(transform);
            _particleSystem.SetActive(false);
        }
    }

}

using UnityEngine;

public class KillZone: MonoBehaviour
{
    private HP health;
    [SerializeField] private int _hpChange = -1;
    private void Awake()
    {
        health = FindAnyObjectByType<HP>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.transform.parent.gameObject);
        health.HPChange(_hpChange);
    }

    
}

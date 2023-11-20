using UnityEngine;

public class HP : Subject
{
    [SerializeField] public int startHealth = 3;
    private int currentHealth;
    public int Health
    {
        get { return currentHealth; }
    }

    private void Awake()
    {
        Restart();
    }


    private void NotifyObservers(int hp)
    {
        foreach (IHealthObserver observer in _observers)
        {
            observer.OnHPChange(hp);
        }
    }
    public void HPChange(int hpChange)
    {
        currentHealth += hpChange;
        NotifyObservers(hpChange);
    }

    public void Restart()
    {
        currentHealth = startHealth;

    }


}

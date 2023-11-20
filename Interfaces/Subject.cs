
using System.Collections.Generic;
using UnityEngine;

public class Subject : MonoBehaviour
{
    protected List<IHealthObserver> _observers = new();

    public void AddObserver(IHealthObserver observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(IHealthObserver observer)
    {
        _observers.Remove(observer);
    }
}

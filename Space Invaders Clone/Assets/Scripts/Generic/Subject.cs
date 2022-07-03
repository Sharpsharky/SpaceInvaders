using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    private List<Observer> observers = new List<Observer>();

    public void RegisterObserver(Observer observer)
    {
        observers.Add(observer);
    }

    public void Notify(object value, NotificationType notificationType)
    {
        foreach (var observer in observers)
            observer.OnNotify(value, notificationType);
    }

}

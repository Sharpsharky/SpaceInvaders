using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInitialize : MonoBehaviour
{
    [SerializeField]
    private Vector3 initialPosition;

    public event Action OnInitialize = delegate { };

    private void Awake()
    {
        WaveManager.OnTurnOnPlayer += TurnOnObject;
        WaveManager.OnPlayerSetHP += SetHP;
        WaveManager.OnPlayerChangePos += SetPosition;
    }

    public void SetPosition()
    {
        transform.position = initialPosition;
    }

    public void SetHP()
    {
        OnInitialize();
    }

    public void TurnOnObject(bool isGoingToTurnOn)
    {
        if (gameObject.activeSelf != isGoingToTurnOn) gameObject.SetActive(isGoingToTurnOn);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipEffects : MonoBehaviour
{

    private void Awake()
    {
        //GetComponent<PlayerHealth>().OnTakeDamage += HandleInvulnerability;
        PlayerHealth.OnDie += HandleDeathEffect;
    }

    private void HandleDeathEffect()
    {
        gameObject.SetActive(false);
    }

}

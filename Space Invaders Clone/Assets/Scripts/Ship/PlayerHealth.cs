using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDemageble
{
    private int maxHealth = 3;
    private int invulnerabilityTime = 3;

    private int currentHealth;
    private bool isinvulnerable = false;

    public static event Action OnDie;
    public event Action OnTakeDamage = delegate { };
    public event Action OnRefillHeartsUI = delegate { };
    public int MaxHealth { get => maxHealth;}
    public int InvulnerabilityTime { get => invulnerabilityTime;}
    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }

    private ShipInitialize shipInitialize;
    private PlayerConfigHolder playerConfigHolder;
    private void Awake()
    {
        shipInitialize = GetComponent<ShipInitialize>();
        playerConfigHolder = GetComponent<PlayerConfigHolder>();

        maxHealth = playerConfigHolder.ShipSettings.MaxHealth;
        invulnerabilityTime = playerConfigHolder.ShipSettings.InvulnerabilityTime;

        InitializePlayer();
        shipInitialize.OnInitialize += InitializePlayer;
    }

    private void InitializePlayer()
    {
        CurrentHealth = MaxHealth;
        isinvulnerable = false;
        OnRefillHeartsUI();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy") OnDie();
    }

    public void DealDamage(int damage)
    {
        if (!isinvulnerable)
        {
            StartCoroutine(MakeThePlayerInvulnerable());
            CurrentHealth -= damage;
            if (CurrentHealth <= 0) OnDie();
            else OnTakeDamage();
        }
    }

    private IEnumerator MakeThePlayerInvulnerable()
    {
        isinvulnerable = true;
        yield return new WaitForSeconds(InvulnerabilityTime);
        isinvulnerable = false;
        yield return null;

    }

}

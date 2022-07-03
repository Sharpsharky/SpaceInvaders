using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIHandler : MonoBehaviour
{
    [SerializeField] GameObject heartPrefab;
    [SerializeField] Transform heartsParent;

    PlayerHealth playerHealth;
    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerHealth.OnTakeDamage += TakeHeart;
        playerHealth.OnRefillHeartsUI += InitializeHearts;
        PlayerHealth.OnDie += TakeHeart;
        //InitializeHearts();
    }

    private void InitializeHearts()
    {
        foreach(Transform heart in heartsParent)
        {
            Destroy(heart.gameObject);
        }

        for(int i = 0; i < playerHealth.MaxHealth; i++)
        {
            Instantiate(heartPrefab, heartsParent);
        }
    }

    private void TakeHeart()
    {
        if(heartsParent.childCount >= 0) Destroy(heartsParent.GetChild(0).gameObject);
        else Debug.LogError("Too many hearts to take");
    }
}

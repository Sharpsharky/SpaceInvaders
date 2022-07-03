using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemy : MonoBehaviour
{
    private bool cooldown = false; //In case of multiple enemies hitting the trigger simultaneously

    public static event Action OnEndGame;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Enemy" || cooldown == true) return;

        cooldown = true;
        Invoke(nameof(DisableCooldown), 2);
        OnEndGame();
    }

    private void DisableCooldown()
    {
        cooldown = false;
    }

}

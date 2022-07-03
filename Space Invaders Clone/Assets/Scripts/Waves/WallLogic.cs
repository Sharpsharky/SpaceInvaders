using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallLogic : MonoBehaviour
{
    public static event Action<string> OnEnemyCollide;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
            OnEnemyCollide(tag);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGiveScore : MonoBehaviour
{
    [SerializeField]
    private int scoreForKilling = 10;

    private Enemy enemy;
    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        enemy.OnEnemyGetScore += AddScore;
    }

    public void AddScore()
    {
        Results.AddScore(scoreForKilling);
    }
}

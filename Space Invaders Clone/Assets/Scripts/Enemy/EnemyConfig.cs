using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Settings", fileName = "EnemyData")]
public class EnemyConfig : ScriptableObject
{
    [SerializeField] private int hp = 1;

    public int Hp { get => hp;}
}

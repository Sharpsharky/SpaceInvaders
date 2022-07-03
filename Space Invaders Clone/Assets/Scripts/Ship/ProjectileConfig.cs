using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Projectile/Settings", fileName = "ProjectileData")]
public class ProjectileConfig : ScriptableObject
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private int livingTime = 7;

    public float MoveSpeed { get => moveSpeed;}
    public int LivingTime { get => livingTime;}
}


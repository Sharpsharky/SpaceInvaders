using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Ship/Settings", fileName = "ShipData")]
public class ShipSettings : ScriptableObject
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float shootingSpeed = 10f;
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int invulnerabilityTime = 3;
    [SerializeField] private float maxMoveOffset = 17;

    public float MoveSpeed { get => moveSpeed; }
    public float ShootingSpeed { get => shootingSpeed; }
    public int MaxHealth { get => maxHealth; }
    public int InvulnerabilityTime { get => invulnerabilityTime; }
    public float MaxMoveOffset { get => maxMoveOffset; }
}

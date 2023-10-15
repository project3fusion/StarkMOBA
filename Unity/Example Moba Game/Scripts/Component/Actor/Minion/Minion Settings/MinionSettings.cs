using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

[System.Serializable]
public class MinionSettings
{
    [Header("Minion General")]
    public GameObject minionProjectileBluePrefab;
    public GameObject minionProjectileRedPrefab;
    public float minionProjectileSpeed = 10f;
    [Header("Minion Attack")]
    public float attackCooldown = 3;
    [Header("Minion Health")]
    public float totalHealth = 20;
    [Header("Minion Navmesh Agent")]
    public float angularSpeed = 0f;
    public float acceleration = 1000f;
    public float stoppingDistance = 0f;
    public bool autoBraking = false;
    [Header("Minion UI")]
    public Transform minionStatsTransform;
    public Image minionHealthBar;
    public RotationConstraint minionRotationConstraint;
    [Header("Minion VFX")]
    public GameObject hitVFX;
}

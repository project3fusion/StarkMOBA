using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

[System.Serializable]
public class TowerSettings
{
    public enum Team { Blue, Red }
    [Header("Player General")]
    public Team towerTeam;
    [Header("Tower Attack")]
    public GameObject towerProjectilePrefab;
    public float towerAttackRange = 10;
    public float towerAttackCooldown = 2;
    public float towerAttackHeight = 3;
    public float projectileSpeed = 8f;
    [Header("Tower Health")]
    public float towerTotalHealth;
    [Header("Tower UI")]
    public Transform towerStatsTransform;
    public TMP_Text towerHealthText;
    public Image towerHealthBar;
    public RotationConstraint towerRotationConstraint;
}

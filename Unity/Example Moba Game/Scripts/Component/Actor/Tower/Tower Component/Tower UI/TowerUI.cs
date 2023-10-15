using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class TowerUI
{
    private Tower tower;

    private TMP_Text towerHealthText;
    private Image towerHealthBar;
    private RotationConstraint towerRotationConstraint;

    public TowerUI(Tower tower) => this.tower = tower;

    public void OnStart()
    {
        towerHealthText = tower.towerSettings.towerHealthText;
        towerHealthBar = tower.towerSettings.towerHealthBar;
        towerRotationConstraint = tower.towerSettings.towerRotationConstraint;
        SetRotationConstraint();
    }

    public void OnUpdate() => UpdateTowerUI();

    public void SetRotationConstraint()
    {
        ConstraintSource constraintSource = new ConstraintSource();
        constraintSource.sourceTransform = Camera.main.transform;
        constraintSource.weight = 1;
        towerRotationConstraint.AddSource(constraintSource);
    }

    public void UpdateTowerUI()
    {
        towerHealthText.text = tower.towerData.Value.towerHealthData.towerHealth + " / " + tower.towerData.Value.towerHealthData.towerTotalHealth;
        towerHealthBar.fillAmount = tower.towerData.Value.towerHealthData.towerHealth / tower.towerData.Value.towerHealthData.towerTotalHealth;
    }
}

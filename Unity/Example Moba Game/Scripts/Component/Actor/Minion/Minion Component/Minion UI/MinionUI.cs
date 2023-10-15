using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class MinionUI
{
    private Minion minion;

    private Transform minionStatsTransform;
    private Image minionHealthBar;
    private RotationConstraint minionRotationConstraint;

    public MinionUI(Minion minion) => this.minion = minion;

    public void OnStart()
    {
        minionStatsTransform = minion.minionSettings.minionStatsTransform;
        minionHealthBar = minion.minionSettings.minionHealthBar;
        minionRotationConstraint = minion.minionSettings.minionRotationConstraint;
        SetRotationConstraint();
    }

    public void OnUpdate() => UpdateMinionUI();

    public void SetRotationConstraint()
    {
        ConstraintSource constraintSource = new ConstraintSource();
        constraintSource.sourceTransform = Camera.main.transform;
        constraintSource.weight = 1;
        minionRotationConstraint.AddSource(constraintSource);
    }

    public void UpdateMinionUI() => minionHealthBar.fillAmount = minion.minionData.Value.minionHealthData.minionHealth / minion.minionData.Value.minionHealthData.minionTotalHealth;

    public void ToggleUI() => minionStatsTransform.gameObject.SetActive(!minionStatsTransform.gameObject.activeSelf);
}

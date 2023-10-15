using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class PlayerUI
{
    private Player player;

    private TMP_Text playerNameText, playerHealthText, playerManaText;
    private Image playerHealthBar, playerManaBar;
    private RotationConstraint playerCanvasRotationConstraint;

    public PlayerUI(Player player) => this.player = player;

    public void OnStart()
    {
        playerNameText = player.playerSettings.playerNameText;
        playerHealthText = player.playerSettings.playerHealthText;
        playerManaText = player.playerSettings.playerManaText;
        playerHealthBar = player.playerSettings.playerHealthBar;
        playerManaBar = player.playerSettings.playerManaBar;
        playerCanvasRotationConstraint = player.playerSettings.playerCanvasRotationConstraint;
        SetRotationConstraint();
    }

    public void OnUpdate() => UpdatePlayerUI();

    public void UpdatePlayerUI()
    {
        playerNameText.text = player.gameObject.name;
        playerHealthText.text = player.playerData.Value.playerHealthData.playerHealth.ToString() + " / " + player.playerData.Value.playerHealthData.playerTotalHealth.ToString();
        playerManaText.text = player.playerData.Value.playerManaData.playerMana.ToString() + " / " + player.playerData.Value.playerManaData.playerTotalMana.ToString();
        playerHealthBar.fillAmount = player.playerData.Value.playerHealthData.playerHealth / player.playerData.Value.playerHealthData.playerTotalHealth;
        playerManaBar.fillAmount = player.playerData.Value.playerManaData.playerMana / player.playerData.Value.playerManaData.playerTotalMana;
    }

    public void SetRotationConstraint()
    {
        ConstraintSource constraintSource = new ConstraintSource();
        constraintSource.sourceTransform = Camera.main.transform;
        constraintSource.weight = 1;
        playerCanvasRotationConstraint.AddSource(constraintSource);
    }
}

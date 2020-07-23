using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTurnButton : MonoBehaviour
{
    public Image image;
    public Sprite enabledSprite;
    public Sprite disabledSprite;

    TurnManager turnManager;

    private void Awake() {
        turnManager = FindObjectOfType<TurnManager>();
    }

    private void Start() {
        turnManager.OnPlayerTurnStart += EnableButton;
        turnManager.OnPlayerTurnEnd += DisableButton;
    }

    void EnableButton(object sender, EventArgs e) {
        image.sprite = enabledSprite;
    }

    void DisableButton(object sender, EventArgs e) {
        image.sprite = disabledSprite;
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {
    public CardObject cardObject;
    public int id;
    SpriteRenderer spriteR;
    GameManager gameManager;
    TurnManager turnManager;
    Sprite artwork;
    new string name;
    string description;
    int manaCost;
    int attack;
    Vector3 startingScale;
    Ray ray;
    RaycastHit hit;

    private void Awake() {
        spriteR = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
        turnManager = FindObjectOfType<TurnManager>();
    }

    private void OnMouseEnter() {
        gameManager.SetLastTouchedCard(id);
    }

    private void OnMouseUpAsButton() {
        if (turnManager.state == GameState.MULLIGAN) {
            gameManager.SetMulligan(id);
        }
    }

    public void LoadCard(CardObject card) {
        name = card.name;
        description = card.description;
        artwork = card.artwork;
        manaCost = card.manaCost;
        attack = card.attack;
        id = card.id;
        //id = System.Guid.NewGuid().ToString();

        spriteR.sprite = artwork;
    }
}

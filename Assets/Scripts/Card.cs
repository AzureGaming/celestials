using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    public System.Guid id;
    public GameObject prefab;
    Image imageDisplay;
    TextMeshProUGUI nameDisplay;
    TextMeshProUGUI manaDisplay;
    TextMeshProUGUI attackDisplay;
    GameManager gameManager;
    TurnManager turnManager;
    Sprite artwork;
    new string name;
    string description;
    public int manaCost;
    int attack;
    Vector3 startingScale;
    public int range;

    private void Awake() {
        imageDisplay = transform.Find("Artwork").GetComponent<Image>();
        nameDisplay = transform.Find("Name").GetComponent<TextMeshProUGUI>();
        manaDisplay = transform.Find("Mana").GetComponentInChildren<TextMeshProUGUI>();
        attackDisplay = transform.Find("Attack").GetComponentInChildren<TextMeshProUGUI>();
        gameManager = FindObjectOfType<GameManager>();
        turnManager = FindObjectOfType<TurnManager>();
    }

    private void Start() {
        startingScale = transform.localScale;
    }

    public void LoadCard(CardObject card) {
        name = card.name;
        description = card.description;
        artwork = card.artwork;
        manaCost = card.manaCost;
        attack = card.attack;
        id = System.Guid.NewGuid();
        prefab = card.prefab;
        range = card.range;

        imageDisplay.sprite = artwork;
        nameDisplay.text = name;
        manaDisplay.text = manaCost.ToString();
        attackDisplay.text = attack.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        transform.localScale = startingScale * 2;
    }

    public void OnPointerExit(PointerEventData eventData) {
        transform.localScale = startingScale;
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (turnManager.state == GameState.MULLIGAN) {
            gameManager.SetMulligan(id);
        }
    }
}

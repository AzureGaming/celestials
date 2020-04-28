using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    public System.Guid id;
    public new string name;
    public int manaCost;
    public int attack;
    public int range;
    public CardType type;
    public int movementSpeed;
    public string description;
    public Sprite artwork;
    public GameObject summonPrefab;
    CardEffect effect;
    Image imageDisplay;
    TextMeshProUGUI nameDisplay;
    TextMeshProUGUI manaDisplay;
    TextMeshProUGUI attackDisplay;
    TextMeshProUGUI descriptionDisplay;
    GameManager gameManager;
    TurnManager turnManager;
    Vector3 startingScale;
    Player player;

    private void Awake() {
        imageDisplay = transform.Find("Artwork").GetComponent<Image>();
        nameDisplay = transform.Find("Name").GetComponent<TextMeshProUGUI>();
        manaDisplay = transform.Find("Mana").GetComponentInChildren<TextMeshProUGUI>();
        attackDisplay = transform.Find("Attack").GetComponentInChildren<TextMeshProUGUI>();
        descriptionDisplay = transform.Find("Description").GetComponent<TextMeshProUGUI>();
        gameManager = FindObjectOfType<GameManager>();
        turnManager = FindObjectOfType<TurnManager>();
        effect = GetComponent<CardEffect>();
        player = FindObjectOfType<Player>();
    }

    private void Start() {
        startingScale = transform.localScale;
        imageDisplay.sprite = artwork;
        nameDisplay.text = name;
        manaDisplay.text = manaCost.ToString();
        attackDisplay.text = attack.ToString();
        descriptionDisplay.text = description;
        id = System.Guid.NewGuid();
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

    public void ActivateEffect() {
        effect?.Apply();
    }
}

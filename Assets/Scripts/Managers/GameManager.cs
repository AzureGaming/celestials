using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CONSTANTS {
    summonSpacing = (int)2.15,
}

public class GameManager : MonoBehaviour {
    public List<System.Guid> mulligans;
    public Card cardToSummon;

    TurnManager turnManager;
    UIManager uiManager;
    BoardManager boardManager;
    SpellManager spellManager;
    Player player;
    Boss boss;

    private void Awake() {
        player = FindObjectOfType<Player>();
        turnManager = FindObjectOfType<TurnManager>();
        uiManager = FindObjectOfType<UIManager>();
        boardManager = FindObjectOfType<BoardManager>();
        boss = FindObjectOfType<Boss>();
        spellManager = FindObjectOfType<SpellManager>();
    }

    private void Start() {
        mulligans = new List<System.Guid>(2);
        SetupGame();
    }

    public void SetMulligan(System.Guid cardId) {
        if (mulligans.Contains(cardId)) {
            Debug.Log("Remove " + cardId + " from mulligan");
            mulligans.Remove(cardId);
        } else if (mulligans.Count < 2) {
            Debug.Log("Add" + cardId + " to mulligan");
            mulligans.Add(cardId);
        }
    }

    public void PlayCard(Card card) {
        Debug.Log("Play card " + card.name);
        if (card.type == CardType.Summon) {
            PlaySummon(card);
        } else if (card.type == CardType.Spell) {
            PlaySpell(card);
        } else {
            Debug.Log("Unknown cardtype encountered: " + card.type);
        }
    }

    void PlaySummon(Card card) {
        cardToSummon = card;
        player.LoseMana(card.manaCost);
        uiManager.SetLocationSelectionPrompt(true);
        boardManager.DetectSummonableSpace();
    }

    void PlaySpell(Card card) {
        player.LoseMana(card.manaCost);
        spellManager.HandleSpell(card);
    }

    void SetupGame() {
        uiManager.SetLocationSelectionPrompt(false);
        StartCoroutine(turnManager.Initialize());
    }
}

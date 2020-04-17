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
    Player player;
    Boss boss;

    private void Awake() {
        player = FindObjectOfType<Player>();
        turnManager = FindObjectOfType<TurnManager>();
        uiManager = FindObjectOfType<UIManager>();
        boardManager = FindObjectOfType<BoardManager>();
        boss = FindObjectOfType<Boss>();
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

    public IEnumerator StartCardSummon(Card card) {
        cardToSummon = card;
        Debug.Log("Hello" + card.name);
        player.LoseMana(card.manaCost);
        uiManager.SetLocationSelectionPrompt(true);
        boardManager.DetectTileState();
        yield break;
    }

    void SetupGame() {
        uiManager.SetLocationSelectionPrompt(false);
        StartCoroutine(turnManager.Initialize());
    }
}

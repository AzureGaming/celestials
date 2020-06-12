using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour {
    public CardManager cardManager;
    int deckLimit = 30;
    List<Card> cards = new List<Card>();

    private void Start() {
        FillDeck();
    }

    public void AddCard(Card card) {
        int randomIndex = Random.Range(0, cards.Count);
        cards.Insert(randomIndex, card);
    }

    public Card RemoveCard() {
        if (cards.Count < 0) {
            Reload();
        }
        Card cardToRemove = cards.SkipWhile(card => !card).Skip(1).DefaultIfEmpty(cards[0]).FirstOrDefault();
        cards.Remove(cardToRemove);
        return cardToRemove;
    }

    void FillDeck() {
        Card[] loadedCards = Resources.LoadAll<Card>("Loadable Cards");
        int randomIndex = Random.Range(0, loadedCards.Length);
        Card randomCard = loadedCards[randomIndex];
        for (int i = 0; i < 30; i++) {
            //AddCard(Instantiate(randomCard));
            AddCard(randomCard);
        }
    }

    void Reload() {
        cards = cardManager.GetDiscardPile();
        cardManager.ClearDiscardPile();
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour {
    public CardManager cardManager;
    public GameObject display;
    int deckLimit = 30;
    List<Card> cards = new List<Card>();

    private void Start() {
        FillDeck();
    }

    public void AddCard(Card card) {
        int randomIndex = Random.Range(0, cards.Count);
        cards.Insert(randomIndex, card);
        display.GetComponent<CardsInDeck>().UpdateText(cards.Count, deckLimit);
    }

    public GameObject RemoveCard() {
        if (cards.Count < 0) {
            Reload();
        }
        Card cardToRemove = cards.SkipWhile(card => !card).Skip(1).DefaultIfEmpty(cards[0]).FirstOrDefault();
        cards.Remove(cardToRemove);
        display.GetComponent<CardsInDeck>().UpdateText(cards.Count, deckLimit);
        return Instantiate(cardToRemove.gameObject);
    }

    void FillDeck() {
        Card[] loadedCards = Resources.LoadAll<Card>("Loadable Cards");
        for (int i = 0; i < 30; i++) {
        int randomIndex = Random.Range(0, loadedCards.Length);
        Card randomCard = loadedCards[randomIndex];
            AddCard(randomCard);
        }
        display.GetComponent<CardsInDeck>().UpdateText(cards.Count, deckLimit);
    }

    void Reload() {
        cards = cardManager.GetDiscardPile();
        cardManager.ClearDiscardPile();
        display.GetComponent<CardsInDeck>().UpdateText(cards.Count, deckLimit);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour {
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
        Card card = cards[0];
        cards.RemoveAt(0);
        return card;
    }

    void FillDeck() {
        Card[] loadedCards = Resources.LoadAll<Card>("Loadable Cards");
        int randomIndex = Random.Range(0, loadedCards.Length);
        Card randomCard = loadedCards[randomIndex];
        for (int i = 0; i < 5; i++) {
            // TODO: Place instantiated cards in a container
            AddCard(Instantiate(randomCard));
        }
    }
}

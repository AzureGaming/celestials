using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour {
    public GameObject cardPrefab;
    Card[] cards;
    Deck deck;
    Hand hand;
    DiscardPile discardPile;

    private void Awake() {
        hand = FindObjectOfType<Hand>();
        cards = Resources.LoadAll<Card>("Loadable Cards");
        Debug.Log("Loaded cards" + cards.Length);
        deck = FindObjectOfType<Deck>();
        discardPile = FindObjectOfType<DiscardPile>();
    }

    public GameObject CreateCard() {
        GameObject cardPrefab = Instantiate(GetCardPrefab(), transform);
        cardPrefab.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        //card.GetComponent<Card>().LoadCard(GetValidCardData());

        return cardPrefab;
    }

    public IEnumerator HandleCardDraw() {
        Card card = deck.RemoveCard();
        card.transform.SetParent(hand.transform);
        yield break;
    }

    public void AddToDeck(Card card) {
        deck.AddCard(card);
    }

    public Card[] GetCardsInHand() {
        return hand.GetCards();
    }

    public void AddToDiscard(Card card) {
        discardPile.AddCard(card);
    }

    public void TrashCard(Card card) {
        Destroy(card.gameObject);
    }

    GameObject CompileCard(Entity data) {
        return cardPrefab;
    }

    GameObject GetCardPrefab() {
        // Will break if no valid cards
        int randomIndex = Random.Range(0, cards.Length);
        return cards[randomIndex].gameObject;
        //if (!hand.handCardIds.Contains(cards[randomIndex].id)) {
        //    return cards[randomIndex];
        //}
        //return GetValidCardData();
        //return cards[1];
    }
}

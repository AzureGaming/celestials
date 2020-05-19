using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour {
    public GameObject cardPrefab;
    Card[] cards;
    Deck deck;
    Hand hand;
    List<int> cardsInHand;

    private void Awake() {
        hand = FindObjectOfType<Hand>();
        cards = Resources.LoadAll<Card>("Loadable Cards");
        Debug.Log("Loaded cards" + cards.Length);
        deck = FindObjectOfType<Deck>();
        cardsInHand = new List<int>();
    }

    public GameObject CreateCard() {
        GameObject cardPrefab = Instantiate(GetCardPrefab(), transform);
        cardPrefab.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        //card.GetComponent<Card>().LoadCard(GetValidCardData());

        return cardPrefab;
    }

    public IEnumerator HandleCardDraw() {
        deck.RemoveCard();
        GameObject card = CreateCard();
        card.transform.SetParent(hand.transform);
        cardsInHand.Add(card.GetInstanceID());
        yield break;
    }

    public List<int> GetCardsInHand() {
        return cardsInHand;
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

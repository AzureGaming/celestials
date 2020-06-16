using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardsInDeck : MonoBehaviour {
    public TextMeshProUGUI text;

    public void UpdateText(int currentNumber, int maxNumber) {
        text.text = currentNumber.ToString() + "/" + maxNumber.ToString();
    }
}

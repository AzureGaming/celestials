using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour {
    public void HandleSpell(Card card) {
        if (card.name == "March") {
            March();
        }
    }

    void March() {

    }
}

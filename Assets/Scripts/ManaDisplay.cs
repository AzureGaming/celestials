using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManaDisplay : MonoBehaviour {
    TextMeshProUGUI currentMana;
    TextMeshProUGUI maxMana;

    private void Awake() {
        TextMeshProUGUI[] refs = GetComponentsInChildren<TextMeshProUGUI>();
        currentMana = refs[0];
        maxMana = refs[1];
    }

    public void UpdateCurrentMana(int mana) {
        currentMana.text = mana.ToString();
    }

    public void UpdateMaxMana(int mana) {
        maxMana.text = mana.ToString();
    }
}

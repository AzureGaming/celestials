using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManaDisplay : MonoBehaviour {
    public TextMeshProUGUI currentMana;
    public TextMeshProUGUI maxMana;

    public void UpdateCurrentMana(int mana) {
        Debug.Log("update current mana");
        currentMana.text = mana.ToString();
    }

    public void UpdateMaxMana(int mana) {
        Debug.Log("Update max mana");
        maxMana.text = mana.ToString();
    }
}

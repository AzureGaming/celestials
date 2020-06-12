using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManaDisplay : MonoBehaviour {
    public TextMeshProUGUI text;

    public void UpdateText(int currentMana, int maxMana) {
        text.text = currentMana.ToString() + "/" + maxMana.ToString();
    }
}

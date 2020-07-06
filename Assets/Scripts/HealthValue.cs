using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthValue : MonoBehaviour {
    public Sprite[] sprites;
    public GameObject slot1;
    public GameObject slot2;

    public void SetValue(int value) {
        if (value > 99) {
            Debug.LogWarning("Value: " + value + " is not supported");
            return;
        }
        if (value <= 0) {
            slot1.GetComponent<SpriteRenderer>().sprite = sprites[0];
            slot2.GetComponent<SpriteRenderer>().sprite = sprites[0];
            return;
        }

        if (value < 10) {
            slot1.GetComponent<SpriteRenderer>().sprite = sprites[0];
            slot2.GetComponent<SpriteRenderer>().sprite = sprites[value];
        } else {
            slot1.GetComponent<SpriteRenderer>().sprite = sprites[(int)char.GetNumericValue(value.ToString()[0])];
            slot2.GetComponent<SpriteRenderer>().sprite = sprites[(int)char.GetNumericValue(value.ToString()[1])];
        }
    }
}

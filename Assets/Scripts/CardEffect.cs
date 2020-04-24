using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardEffect : MonoBehaviour {
    // movement to character(s)
    // giving status to character(s)
    // doing damage
    // boss does last move
    // save mana
    // see next 3 cards on deck
    public virtual IEnumerator Apply() {
        yield break;
    }
}

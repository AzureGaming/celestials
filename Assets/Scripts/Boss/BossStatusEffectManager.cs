using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatusEffectManager : MonoBehaviour {
    public BossStatusEffect stasis;

    public void EnableStasis() {
        stasis.Enable();
    }

    public void DisableStasis() {
        stasis.Disable();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthElementalSkillIndicators : BossSkillIndicators {
    public void SetCrystalize() {
        SetIndicator(0);
    }

    public void SetBlockingCrystal() {
        SetIndicator(2);
    }

    public void SetPebbleStorm() {
        SetIndicator(1);
    }

    public void SetBoulderDrop() {
        SetIndicator(3);
    }

    public void SetBoulderThrow() {
        SetIndicator(4);
    }
}

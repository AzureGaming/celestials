using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CelestialSoldierEntity))]
public class CelestialSoldierSummon : Summon {
    CelestialSoldierEntity entity;

    public override void Awake() {
        base.Awake();
        entity = GetComponent<CelestialSoldierEntity>();
    }

    public override void Attack() {
        base.Attack();
    }
}


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

    public override void Walk(int tiles) {
        base.Walk(entity.movementSpeed);
    }

    public override void Attack(int damage) {
        base.Attack(entity.attack);
    }
}


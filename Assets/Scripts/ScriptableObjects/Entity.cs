
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType {
    Summon,
    Spell
}

[CreateAssetMenu(fileName = "New Entity Object", menuName = "Entity")]
public class Entity : ScriptableObject {
    public new string name;
    public string description;
    public Sprite artwork;
    public int manaCost;
    public int attack;
    public GameObject summonPrefab;
    public int range = 1;
    public CardType type;
    public int movementSpeed = 1;
    public CardEffect effect;
}

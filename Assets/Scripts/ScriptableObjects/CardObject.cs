using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards")]
public class CardObject : ScriptableObject {
    public int id;
    public new string name;
    public string description;
    public Sprite artwork;
    public int manaCost;
    public int attack;
    public GameObject prefab;
    public int range;
}

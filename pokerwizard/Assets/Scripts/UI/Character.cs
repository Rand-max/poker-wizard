using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class Character : ScriptableObject {
    //ui select
    public string characterName;
    public Sprite characterSprite;
    public GameObject characterLock;
    //gameplay
    public float rankNum;
    public float hitCount;
    public float eggCount;
}
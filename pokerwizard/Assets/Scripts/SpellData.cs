using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[CreateAssetMenu(menuName ="Create Spell/a spell")]
[System.Serializable]
public class SpellData : ScriptableObject
{
    public int ID;
    public string SpellName="Unnammed";
    public Sprite SpellSprite;
    public Texture IconTexture;
    public Color IconColor;
    public VertexGradient TextColor;
    public GameObject SpellIcon;
    public int SpellAmout=1;
    public float BulletPrepareTime=0.5f;
    public float BulletCoolDownTime=0.4f;
    [TextArea(4,4)]
    public string SpellInfo="will pursuit target enemy utill die";
    [Space(10)]
    [Header("Bullet effect")]
    public float BulletLifeTime=3f;
    public float MaxDistance=10;
    public List<BulletEffect> Bullets;
    [Space(10)]
    [Header("Bullet effects when started")]
    public float EffectLifeTime=0.5f;
    public List<BulletEffect> BulletStartEffects;
    [Space(10)]
    [Header("Bullet effects when contact")]
    public float BoomLifeTime=3f;
    public List<BulletEffect> BulletEndEffects;
    [System.Serializable]public class BulletEffect{
    public GameObject Object;
    public Sound sounfx;
    public float LifeTime=3f;
    [Tooltip("Will Collide with: 0=ground 1=enemy 2=self 3=friend 4=bullet")]
    public List<bool> targettype=new List<bool>(5){true,true,false,false,false};
    [Tooltip("Parent is: 0=not attached 1=enemy 2=player 3=friend 4=bullet")]
    public int AttachedTarget;
    public float BulletSpeed=70f;
    public bool HasPursuit=true;
    public float PursuitStrength=3f;
    public bool HasSlowdown;
    [Range(0.01f,0.99f)]
    public float BulletSlowdownRate;
    public float BulletSlowdownTime;
    public bool HasStun;
    public float BulletStundownTime;
    public bool HasClear;
    public bool HasConfusion;
    public float BulletConfusionTime;
    public bool IsShield;
    public bool IsAccelerator;
    public float BulletBurstTime;
    public float BulletBurstSpeed;
    public bool IsTeleporter;
    public float TeleportTiming;
    public bool IsJoke;
    public float JokeTime;
    public int JokeStrength;
    public Vector3 EffectOffset;
    public bool canScore;
    public int score;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
[CreateAssetMenu(menuName ="Create Spell/a spell")]
public class SpellData : ScriptableObject
{
    public int ID;
    public string SpellName="Unnammed";
    public GameObject Bullet;
    public GameObject BulletEffect;
    public GameObject BulletEndEffect;
    public GameObject SpellIcon;
    public int SpellAmout=1;
    public float BulletPrepareTime=0.5f;
    public float BulletCoolDownTime=0.4f;
    [TextArea(4,4)]
    public string SpellInfo="will pursuit target enemy utill die";
    [Tooltip("0=ground 1=enemy 2=self 3=ground&enemy")]
    [Range(0,3)]
    public int targettype=3;
    [Space(10)]
    [Header("Bullet effect")]
    public float BulletLifeTime=3f;
    public float MaxDistance=10;
    public bool IsAttachedToPlayer;
    public bool HasMovement=true;
    public float BulletSpeed=70f;
    public bool HasPursuit=true;
    public bool HasSlowdown;
    public float BulletSlowdownRate;
    public float BulletSlowdownTime;
    public bool HasStun;
    public float BulletStundownTime;
    public bool HasClear;
    public bool HasConfusion;
    public float BulletConfusionTime;
    public bool HasShield;
    public bool BulletShieldTime;
    public bool HasBurst;
    public float BulletBurstTime;
    public float BulletBurstSpeed;
    public bool HasTeleport;
    [Space(10)]
    [Header("Bullet effects when started")]
    public float EffectLifeTime=0.5f;
    public bool IsAttachedToPlayer_Effect;
    public bool EffectHasMovement;
    public float EffectSpeed;
    public bool HasPursuit_Effect;
    public bool HasSlowdown_Effect;
    public float EffectSlowdownRate;
    public float EffectSlowdownTime;
    public bool HasStun_Effect;
    public float EffectStundownTime;
    public bool HasClear_Effect;
    public bool HasConfusion_Effect;
    public float EffectConfusionTime;
    public bool HasShield_Effect;
    public bool EffectShieldTime;
    public bool HasBurst_Effect;
    public float EffectBurstTime;
    public float EffectBurstSpeed;
    public bool HasTeleport_Effect;
    [Space(10)]
    [Header("Bullet effects when contact")]
    public float BoomLifeTime=3f;
    public bool IsAttachedToPlayer_Boom;
    public bool BoomHasMovement;
    public float BoomSpeed;
    public bool HasPursuit_Boom;
    public bool HasSlowdown_Boom=true;
    public float BoomSlowdownRate=0.3f;
    public float BoomSlowdownTime=3f;
    public bool HasStun_Boom;
    public float BoomStundownTime;
    public bool HasClear_Boom;
    public bool HasConfusion_Boom;
    public float BoomConfusionTime;
    public bool HasShield_Boom;
    public bool BoomShieldTime;
    public bool HasBurst_Boom;
    public float BoomBurstTime;
    public float BoomBurstSpeed;
    public bool HasTeleport_Boom;
}

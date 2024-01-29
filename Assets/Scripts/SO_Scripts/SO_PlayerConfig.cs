using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Config", menuName = "Custom Configuration/ Player Configuration")]
public class SO_PlayerConfig : ScriptableObject
{
    [Header("Jump Height 跳跃高度")]
    [Range(10f, 30f)] public float jumpHeight;
    [Header("Horizontal Moving Speed  水平移动速度")]
    [Range(1f, 10f)] public float runningSpeed;
    [Header("Horizontal Moving Speed Multiplier when attacking  攻击时水平移速减小倍率")]
    [Range(0f, 1f)] public float attackHoriSpeedScale;
    [Header("Decrese Gravity Multiplier when LongPress jump   长按跳跃时重力的减小倍率")]
    [Range(-1.0f, 1f)] public float jumpGravityScale;
    [Header("Horizontal mobility in air 滞空时的水平操作能力")]
    [Range(0.0f, 1f)] public float inputRate;
    [Header("Coyote Time length  土狼时间长度")]
    [Range(0.0f, 1f)] public float coyoteTime;
    [Header("Size of Ground Check Box  玩家脚底和地面的判定盒尺寸")]
    public Vector2 groundCheckBoxSize;
    [Header("Default Gravity默认重力")]
    [Range(1f, 5f)] public float initGravityScale;
    [Header("Jump Input Buffer Time  跳跃的输入缓存时间")]
    [Range(0.05f, 0.5f)] public float inputCacheTime;
    [Header("Increase Gravity multiplier when falling下落时的重力增加倍率")]
    [Range(1f, 3f)] public float gravityFalling;
    [Header("Gravity ascending step when Holding Jump 按住跳跃时的重力增加步进")]
    [Range(0.01f, 0.5f)] public float ascendingGravityStep;
    [Header("Horizontal Acceleration, 1 means no acceleration  水平移动加速度，1为保持原样")]
    [Range(1f, 10f)] public float runningAcceleration;
    [Header("Ability to Horizontally brake, 1 means no brake 水平刹车性能，1为保持原样")]
    [Range(1f, 5f)] public float runningBrakingAbility;
    [Header("Input frames limit of Least Jump Height(tap Jump) 最低跳跃高度的输入帧上限")]
    [Range(0, 10)] public int smallestJumpFrameCount;

    // combat related 战斗相关
    [Header("player maximum health points 血量上限")]
    [Range(0, 10)] public int maxPlayerHp;
    [Header("player melee value 近战伤害")]
    [Range(0, 10)] public int playerMeleeDmg;
    [Header("player stun time when being attacked 受击硬直时间")]
    [Range(0f, 1f)] public float playerStunTime;
    [Header("player invincible time after being attacked 受击后无敌时间")]
    [Range(0f, 1f)] public float playerInvincibleTime;



    // Sound related音效相关
    [Header("Jumping SFX ScriptableObject 跳跃音效SO资产")]
    public SO_AudioConfig jumpSoundAsset;
    [Header("Dyinging SFX ScriptableObject 死亡音效SO资产")]
    public SO_AudioConfig dieSoundAsset;
}

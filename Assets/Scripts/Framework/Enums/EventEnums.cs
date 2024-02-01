

public enum EventName
{
    playerMelees,       // 玩家近战, param: int dmg, gameobject target
    playerDied,         // 玩家死亡, param: null
    playerRespawn,      // 玩家重生, param: null
    playerSpawn,        // 玩家生成, param: null


    enemyRespawn,       // 敌人重置,  param: null


    respawnUpdated,     // 重生点更新, param: transform respawn

    getDoubleJump,
    getDash,
}


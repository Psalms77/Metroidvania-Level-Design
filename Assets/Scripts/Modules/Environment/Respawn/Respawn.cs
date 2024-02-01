using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : Observer
{



    private void Awake()
    {
        AddEventListener(EventName.playerDied, (object[] arg) => {

            RespawnAll();

        });
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RespawnAll()
    {
        EventManager.SendNotification(EventName.playerRespawn);
        EventManager.SendNotification(EventName.enemyRespawn);


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            EventManager.SendNotification(EventName.respawnUpdated, gameObject.transform);
        }
    }
}

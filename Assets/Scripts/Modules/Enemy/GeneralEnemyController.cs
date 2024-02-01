using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralEnemyController : Observer
{
    public float hp;

    private float dmg;
    public bool hasTakenHit = false;

    public float hitCooldown;
    private float hitTimer = 0;

    private void Awake()
    {
        AddEventListener(EventName.playerMelees, (object[] arg) => {
            dmg = (float)arg[0];
            GameObject go = (GameObject)arg[1];
            Debug.Log("event received");
            if (go == this.gameObject)
            {
                Takehit();
                Debug.Log("enemy take hit");
            }
        
        
        });
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hitTimer < hitCooldown && hasTakenHit)
        {
            hitTimer += Time.deltaTime;

        }else if (hitTimer >= hitCooldown) { hasTakenHit = false; hitTimer = 0; }
    }

    void Takehit()
    {
        if (hasTakenHit)
        {
            return;
        }
        else
        {
            hp = hp - dmg;
            hasTakenHit = true;
        }
    }
}

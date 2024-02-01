using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralEnemyController : Observer
{
    public float hp;



    private void Awake()
    {
        AddEventListener(EventName.playerMelees, (object[] arg) => {
            float dmg = (float)arg[0];
            GameObject go = (GameObject)arg[1];
            Debug.Log("event received");
            if (go == this.gameObject)
            {
                hp = hp - dmg;
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
        
    }
}

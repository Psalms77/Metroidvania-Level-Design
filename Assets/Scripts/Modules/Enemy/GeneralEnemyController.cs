using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralEnemyController : Observer
{
    public float maxhp;
    public float hp;

    private float dmg;
    public bool hasTakenHit = false;

    public float hitCooldown;
    private float hitTimer = 0;

    public bool isdead = false;
    private SpriteRenderer sr;
    public Material blankMaterial;
    private Material defaultMaterial;



    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        defaultMaterial = sr.material;
        hp = maxhp;
        AddEventListener(EventName.playerMelees, (object[] arg) => {
            dmg = (float)arg[0];
            GameObject go = (GameObject)arg[1];
            if (go == this.gameObject)
            {
                Takehit();
            }
        
        
        });
        AddEventListener(EventName.enemyRespawn, (object[] arg) => {
            Respawn();
        });
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hp<0) { isdead = true; }
        if (isdead)
        {
            this.gameObject.SetActive(false);
        }
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
            sr.material = blankMaterial;
            StartCoroutine(ResetMaterial());
        }
    }

    IEnumerator ResetMaterial()
    {
        yield return new WaitForSeconds(0.2f);
        sr.material = defaultMaterial;
    }

    void Respawn()
    {
        isdead = false;
        hp = maxhp;
        this.gameObject.SetActive(true);
    }


}

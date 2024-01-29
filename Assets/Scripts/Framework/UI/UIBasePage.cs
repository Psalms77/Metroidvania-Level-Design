using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBasePage : ObserverNoMono
{
    public GameObject gameObject;
    public Transform transform;
    // public bool isDestroyed { get; private set; } = false;
    
    public virtual void InitParams(params object[] args)
    {
        // Object.DontDestroyOnLoad(gameObject);
    }
    
    public override void DestroySelf()
    {
        base.DestroySelf();
        Object.Destroy(gameObject);
        onDestroy();
        isDestroyed = true;
    }
    
    public virtual void onStart() {}
    public virtual void onDestroy() {}
    public virtual void Update() {}
}


public class UISinglePage : UIBasePage
{
    public override void onStart()
    {
        base.onStart();

    }
}

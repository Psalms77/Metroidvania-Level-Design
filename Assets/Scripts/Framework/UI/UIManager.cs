using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;


public class PageInfo
{
    public string prefabPath;
    public string pageName;
    public Type pageType;
    public PageInfo(string path, string name, Type type)
    {
        prefabPath = "UI_prefabs/" + path;
        pageName = name;
        pageType = type;
    }
}


public partial class UIManager : SingletonNoMono<UIManager>
{
    private List<UIBasePage> pageList = new List<UIBasePage>();
    private static int singletonNum = 0;
    public UIManager()
    {
        // UnityEngine.Object.DontDestroyOnLoad(GameObject.Find("Canvas"));
        ++singletonNum;
        if (singletonNum > 1)
        {
            Debug.LogError("Singleton Error, More than one UI manager! " + singletonNum);
        }
        InitPageDict();
        AutoInitPageDict();
    }
    
    public T CreateUI<T>(params object[] args) where T : UIBasePage, new()
    {
        var newPage = new T();
        newPage.gameObject = GameObject.Instantiate(Resources.Load<GameObject>(pageDict[typeof(T)].prefabPath), GameObject.Find("Canvas").transform);
        newPage.transform = newPage.gameObject.transform;
        newPage.InitParams(args);
        pageList.Add(newPage);
        newPage.onStart();
        return newPage;
    }

    public void DestroyUI(UIBasePage page)
    {
        pageList.Remove(page);
        page.DestroySelf();
    }

    public void DestroyFirstUIWithType<T>() where T : UIBasePage
    {
        for (int i = pageList.Count - 1; i >= 0; i--)
        {
            if (pageList[i].GetType() == typeof(T))
            {
                DestroyUI(pageList[i]);
                break;
            }
        }
    }
    
    public UIBasePage GetFirstUIWithType<T>() where T : UIBasePage
    {
        for (int i = pageList.Count - 1; i >= 0; i--)
        {
            if (pageList[i].GetType() == typeof(T))
            {
                return pageList[i];
            }
        }
        return null;
    }

    public void DestroyAllUI()
    {
        for (int i = pageList.Count - 1; i >= 0; i--)
        {
            DestroyUI(pageList[i]);
        }
    }

    public void DestroyAllUIWithType<T>() where T : UIBasePage
    {
        for (int i = pageList.Count - 1; i >= 0; i--)
        {
            if (pageList[i].GetType() == typeof(T))
            {
                DestroyUI(pageList[i]);
            }
        }
    }

    public Camera GetUICamera()
    {
        return GameObject.Find("UICamera").GetComponent<Camera>();
    }

    public void Update()
    {
        // foreach (var item in pageList)
        // {
        //     if (item != null && !item.isDestroyed)
        //     {
        //         item.Update();
        //     }
        // }
        for (int i = pageList.Count - 1; i >= 0; i--)
        {
            if (pageList[i] != null && !pageList[i].isDestroyed)
            {
                pageList[i].Update();
            }
        }
    }
}

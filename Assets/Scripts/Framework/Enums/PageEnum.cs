using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

/// <summary>
/// 使用这个特性可以自动加进UIManager的pageDict当中
/// </summary>
public class UIPageAttribute : Attribute
{
    public string path;
    public string name;
    public Type type;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="path">预制体相对于Resources/UIPrefabs的目录</param>
    public UIPageAttribute(string path, string name, Type type)
    {
        this.path = path;
        this.name = name;
        this.type = type;
    }
}

public partial class UIManager
{
    private static Dictionary<Type, PageInfo> pageDict = new Dictionary<Type, PageInfo>();
    // register here when you create a new page
    private void InitPageDict()
    {

        //pageDict[typeof(GameMainPage)] = new PageInfo("empty", "GameMainPage", typeof(GameMainPage));
        //pageDict[typeof(CountDownPage)] = new PageInfo("count_down_UI", "CountDownPage", typeof(CountDownPage));
        //pageDict[typeof(LevelSelectPage)] = new PageInfo("level_select/level_select_ui", "LevelSelectPage", typeof(LevelSelectPage));
        //pageDict[typeof(StartMenuPage)] = new PageInfo("StartMenuUI", "StartMenuPage", typeof(StartMenuPage));
        //pageDict[typeof(ChallengeSelectPage)] = new PageInfo("level_select/challenge_select_ui", "ChallengeSelectPage", typeof(ChallengeSelectPage));
        //pageDict[typeof(GMPage)] = new PageInfo("GM/gm_page", "GMPage", typeof(GMPage));
        //pageDict[typeof(GeneralFadePage)] = new PageInfo("general_fade_ui", "GeneralFadePage", typeof(GeneralFadePage));
        //pageDict[typeof(TutorialBubbleUI)] = new PageInfo("tutorial_bubble_ui", "TutorialBubbleUI", typeof(TutorialBubbleUI));
        // pageDict[typeof(PauseMenuPage)] = new PageInfo("pause_menu_panel", "PauseMenuUI", typeof(PauseMenuPage));
        // pageDict[typeof(PlayerStatusUI)] = new PageInfo("status_page", "PauseMenuUI", typeof(PlayerStatusUI));
        // pageDict[typeof(CountDownUI)] = new PageInfo("count_down", "CountDownUI", typeof(CountDownUI));
        // pageDict[typeof(GameOverPage)] = new PageInfo("game_over_ui", "GameOverPage", typeof(GameOverPage));
        // pageDict[typeof(KillComboCountPage)] = new PageInfo("kill_combo_count", "KillComboCountPage", typeof(KillComboCountPage));
        // pageDict[typeof(GameClearPage)] = new PageInfo("success_end", "GameClearPage", typeof(GameClearPage));
    }
    
    // two ways to add page to pageDict
    private void AutoInitPageDict()
    {
        var UIPagesType = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(type => type.IsSubclassOf(typeof(UIBasePage)))
            .Where(type => type.GetCustomAttribute<UIPageAttribute>() != null);
        foreach (var type in UIPagesType)
        {
            if (!pageDict.ContainsKey(type)) // check if repeats
                pageDict.Add(type,
                    new PageInfo(type.GetCustomAttribute<UIPageAttribute>().path,
                        type.GetCustomAttribute<UIPageAttribute>().name,
                        type.GetCustomAttribute<UIPageAttribute>().type));
        }
    }
}

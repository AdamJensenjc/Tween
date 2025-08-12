using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tween管理器
/// </summary>
public class TweenManager : MonoBehaviour
{
    private static TweenManager _instance;//管理器单例
    private readonly List<Tween> _activeTweens = new List<Tween>();//存放所有活动中的Tween

    /// <summary>
    /// 单例访问点
    /// </summary>
    public static TweenManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TweenManager>();//在场景中查找现有实例
                if (_instance == null)
                {
                    GameObject go = new GameObject("TweenManager");//自动创建管理器对象
                    _instance = go.AddComponent<TweenManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    /// <summary>
    /// 每帧更新所有活动中的Tween
    /// </summary>
    private void Update()
    {
        //从后向前遍历避免删除时的索引问题
        for (int i = _activeTweens.Count - 1; i >= 0; i--)
        {
            if (!_activeTweens[i].Update(Time.deltaTime))
            {
                _activeTweens.RemoveAt(i);//移除已完成和被杀掉的动画
            }
        }
    }

    /// <summary>
    /// 添加新的Tween到列表
    /// </summary>
    /// <param name="tween"></param>
    public void AddTween(Tween tween)
    {
        if (tween.IsActive)
        {
            _activeTweens.Add(tween);
        }
    }
}

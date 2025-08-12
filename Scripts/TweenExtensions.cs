using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tween扩展方法
/// </summary>
public static class TweenExtensions
{
    /// <summary>
    /// 创建移动动画
    /// </summary>
    /// <param name="transform">目标Transform</param>
    /// <param name="endValue">目标位置</param>
    /// <param name="duration">持续时间</param>
    /// <param name="easeType">缓动类型</param>
    public static Tween DoMove(this Transform transform, Vector3 endValue, float duration, EaseType easeType = EaseType.Linear)
    {
        Tween tween = Tween.DoMove(transform, endValue, duration, easeType);
        TweenManager.Instance.AddTween(tween);
        return tween;
    }

    /// <summary>
    /// 创建缩放动画
    /// </summary>
    /// <param name="transform">目标Transform</param>
    /// <param name="endValue">目标缩放</param>
    /// <param name="duration">持续时间</param>
    /// <param name="easeType">缓动类型</param>
    public static Tween DoScale(this Transform transform, Vector3 endValue, float duration, EaseType easeType = EaseType.Linear)
    {
        Tween tween = Tween.DoScale(transform, endValue, duration, easeType);
        TweenManager.Instance.AddTween(tween);
        return tween;
    }

    /// <summary>
    /// 终止Tween动画
    /// </summary>
    /// <param name="tween">要终止的Tween实例</param>
    public static void Kill(this Tween tween)
    {
        tween.Kill();
    }
}

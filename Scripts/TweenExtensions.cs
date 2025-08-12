using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tween��չ����
/// </summary>
public static class TweenExtensions
{
    /// <summary>
    /// �����ƶ�����
    /// </summary>
    /// <param name="transform">Ŀ��Transform</param>
    /// <param name="endValue">Ŀ��λ��</param>
    /// <param name="duration">����ʱ��</param>
    /// <param name="easeType">��������</param>
    public static Tween DoMove(this Transform transform, Vector3 endValue, float duration, EaseType easeType = EaseType.Linear)
    {
        Tween tween = Tween.DoMove(transform, endValue, duration, easeType);
        TweenManager.Instance.AddTween(tween);
        return tween;
    }

    /// <summary>
    /// �������Ŷ���
    /// </summary>
    /// <param name="transform">Ŀ��Transform</param>
    /// <param name="endValue">Ŀ������</param>
    /// <param name="duration">����ʱ��</param>
    /// <param name="easeType">��������</param>
    public static Tween DoScale(this Transform transform, Vector3 endValue, float duration, EaseType easeType = EaseType.Linear)
    {
        Tween tween = Tween.DoScale(transform, endValue, duration, easeType);
        TweenManager.Instance.AddTween(tween);
        return tween;
    }

    /// <summary>
    /// ��ֹTween����
    /// </summary>
    /// <param name="tween">Ҫ��ֹ��Tweenʵ��</param>
    public static void Kill(this Tween tween)
    {
        tween.Kill();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 缓动函数计算器
/// </summary>
public static class EaseEvaluator
{
    /// <summary>
    /// 根据缓动类型计算当前进度值
    /// </summary>
    /// <param name="type"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public static float Evaluate(EaseType type, float t)
    {
        switch (type)
        {
            case EaseType.Linear: return Linear(t);
            case EaseType.EaseInSine: return EaseInSine(t);
            case EaseType.EaseOutSine: return EaseOutSine(t);
            case EaseType.EaseInOutSine: return EaseInOutSine(t);
            case EaseType.EaseInBack: return EaseInBack(t);
            case EaseType.EaseOutBack: return EaseOutBack(t);
            case EaseType.EaseInOutBack: return EaseInOutBack(t);
            default: return Linear(t);
        }
    }

    private static float Linear(float t)
    {
        return t;
    }

    private static float EaseInSine(float t)
    {
        return 1 - Mathf.Cos(t * Mathf.PI / 2);
    }

    private static float EaseOutSine(float t)
    {
        return Mathf.Sin(t * Mathf.PI / 2);
    }

    private static float EaseInOutSine(float t)
    {
        return -(Mathf.Cos(Mathf.PI * t) - 1) / 2;
    }

    private static float EaseInBack(float t)
    {
        const float c1 = 1.70158f;
        const float c3 = c1 + 1;
        return c3 * t * t * t - c1 * t * t;
    }

    private static float EaseOutBack(float t)
    {
        const float c1 = 1.70158f;
        const float c3 = c1 + 1;
        return 1 + c3 * Mathf.Pow(t - 1, 3) + c1 * Mathf.Pow(t - 1, 2);
    }

    private static float EaseInOutBack(float t)
    {
        const float c1 = 1.70158f;
        const float c2 = c1 * 1.525f;

        if (t < 0.5f)
        {
            return (Mathf.Pow(2 * t, 2) * ((c2 + 1) * 2 * t - c2)) / 2;
        }
        else
        {
            t = 2 * t - 2;
            return (Mathf.Pow(t, 2) * ((c2 + 1) * t + c2) + 2) / 2;
        }
    }
}

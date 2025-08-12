using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tween动画核心类
/// </summary>
public class Tween
{
    private Transform _target;//目标
    private Vector3 _startValue;//动画起始值
    private Vector3 _endValue;//动画结束值
    private float _duration;//动画持续时间(秒)
    private float _elapsedTime;//已过去的时间
    private bool _isPlaying;//是否正在播放
    private Action<Tween> _onUpdate;//每帧更新回调
    private Action _onComplete;//完成回调
    private EaseType _easeType;//缓动类型

    /// <summary>
    /// 动画是否处于活动状态
    /// </summary>
    public bool IsActive
    {
        get { return _isPlaying; }
    }

    /// <summary>
    /// 当前动画值
    /// </summary>
    public Vector3 CurrentValue { get; private set; }

    /// <summary>
    /// Tween构造函数
    /// </summary>
    /// <param name="target">目标</param>
    /// <param name="endValue">动画结束值</param>
    /// <param name="duration">动画持续时间(秒)</param>
    /// <param name="easeType">缓动类型</param>
    /// <param name="tweenType">Tween类型</param>
    /// <exception cref="ArgumentException"></exception>
    private Tween(Transform target, Vector3 endValue, float duration, EaseType easeType, TweenType tweenType)
    {
        _target = target;
        _endValue = endValue;
        _duration = duration;
        _easeType = easeType;
        _elapsedTime = 0f;
        _isPlaying = true;

        //根据动画类型初始化值和更新函数
        switch (tweenType)
        {
            case TweenType.Move:
                _startValue = target.position;
                _onUpdate = t => _target.position = t.CurrentValue;
                break;
            case TweenType.Scale:
                _startValue = target.localScale;
                _onUpdate = t => _target.localScale = t.CurrentValue;
                break;
            default:
                Debug.LogError("不支持的Tween类型！");
                break;
        }
    }

    /// <summary>
    /// 创建移动动画
    /// </summary>
    /// <param name="target">目标Transform</param>
    /// <param name="endValue">目标位置</param>
    /// <param name="duration">持续时间</param>
    /// <param name="easeType">缓动类型</param>
    public static Tween DoMove(Transform target, Vector3 endValue, float duration, EaseType easeType = EaseType.Linear)
    {
        return new Tween(target, endValue, duration, easeType, TweenType.Move);
    }

    /// <summary>
    /// 创建缩放动画
    /// </summary>
    /// <param name="target">目标Transform</param>
    /// <param name="endValue">目标缩放</param>
    /// <param name="duration">持续时间</param>
    /// <param name="easeType">缓动类型</param>
    public static Tween DoScale(Transform target, Vector3 endValue, float duration, EaseType easeType = EaseType.Linear)
    {
        return new Tween(target, endValue, duration, easeType, TweenType.Scale);
    }

    /// <summary>
    /// 每帧更新动画状态
    /// </summary>
    /// <param name="deltaTime"></param>
    /// <returns>是否在播放</returns>
    public bool Update(float deltaTime)
    {
        //如果动画已被终止,则跳过更新
        if (!_isPlaying) return false;

        _elapsedTime += deltaTime;
        float progress = Mathf.Clamp01(_elapsedTime / _duration);//计算进度
        float easedProgress = EaseEvaluator.Evaluate(_easeType, progress);//计算缓动进度
        CurrentValue = Vector3.LerpUnclamped(_startValue, _endValue, easedProgress);//使用LerpUnclamped，因为easedProgress可超出(0,1)范围
        _onUpdate?.Invoke(this);

        //检查动画是否完成
        if (_elapsedTime >= _duration)
        {
            Complete();
            return false;
        }
        return true;
    }

    /// <summary>
    /// 完成动画
    /// </summary>
    private void Complete()
    {
        _isPlaying = false;
        _onComplete?.Invoke();
    }

    /// <summary>
    /// 终止动画
    /// </summary>
    public void Kill()
    {
        _isPlaying = false;
    }

    /// <summary>
    /// 设置动画完成回调
    /// </summary>
    /// <param name="action">回调方法</param>
    /// <returns></returns>
    public Tween OnComplete(Action action)
    {
        _onComplete = action;
        return this;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tween����������
/// </summary>
public class Tween
{
    private Transform _target;//Ŀ��
    private Vector3 _startValue;//������ʼֵ
    private Vector3 _endValue;//��������ֵ
    private float _duration;//��������ʱ��(��)
    private float _elapsedTime;//�ѹ�ȥ��ʱ��
    private bool _isPlaying;//�Ƿ����ڲ���
    private Action<Tween> _onUpdate;//ÿ֡���»ص�
    private Action _onComplete;//��ɻص�
    private EaseType _easeType;//��������

    /// <summary>
    /// �����Ƿ��ڻ״̬
    /// </summary>
    public bool IsActive
    {
        get { return _isPlaying; }
    }

    /// <summary>
    /// ��ǰ����ֵ
    /// </summary>
    public Vector3 CurrentValue { get; private set; }

    /// <summary>
    /// Tween���캯��
    /// </summary>
    /// <param name="target">Ŀ��</param>
    /// <param name="endValue">��������ֵ</param>
    /// <param name="duration">��������ʱ��(��)</param>
    /// <param name="easeType">��������</param>
    /// <param name="tweenType">Tween����</param>
    /// <exception cref="ArgumentException"></exception>
    private Tween(Transform target, Vector3 endValue, float duration, EaseType easeType, TweenType tweenType)
    {
        _target = target;
        _endValue = endValue;
        _duration = duration;
        _easeType = easeType;
        _elapsedTime = 0f;
        _isPlaying = true;

        //���ݶ������ͳ�ʼ��ֵ�͸��º���
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
                Debug.LogError("��֧�ֵ�Tween���ͣ�");
                break;
        }
    }

    /// <summary>
    /// �����ƶ�����
    /// </summary>
    /// <param name="target">Ŀ��Transform</param>
    /// <param name="endValue">Ŀ��λ��</param>
    /// <param name="duration">����ʱ��</param>
    /// <param name="easeType">��������</param>
    public static Tween DoMove(Transform target, Vector3 endValue, float duration, EaseType easeType = EaseType.Linear)
    {
        return new Tween(target, endValue, duration, easeType, TweenType.Move);
    }

    /// <summary>
    /// �������Ŷ���
    /// </summary>
    /// <param name="target">Ŀ��Transform</param>
    /// <param name="endValue">Ŀ������</param>
    /// <param name="duration">����ʱ��</param>
    /// <param name="easeType">��������</param>
    public static Tween DoScale(Transform target, Vector3 endValue, float duration, EaseType easeType = EaseType.Linear)
    {
        return new Tween(target, endValue, duration, easeType, TweenType.Scale);
    }

    /// <summary>
    /// ÿ֡���¶���״̬
    /// </summary>
    /// <param name="deltaTime"></param>
    /// <returns>�Ƿ��ڲ���</returns>
    public bool Update(float deltaTime)
    {
        //��������ѱ���ֹ,����������
        if (!_isPlaying) return false;

        _elapsedTime += deltaTime;
        float progress = Mathf.Clamp01(_elapsedTime / _duration);//�������
        float easedProgress = EaseEvaluator.Evaluate(_easeType, progress);//���㻺������
        CurrentValue = Vector3.LerpUnclamped(_startValue, _endValue, easedProgress);//ʹ��LerpUnclamped����ΪeasedProgress�ɳ���(0,1)��Χ
        _onUpdate?.Invoke(this);

        //��鶯���Ƿ����
        if (_elapsedTime >= _duration)
        {
            Complete();
            return false;
        }
        return true;
    }

    /// <summary>
    /// ��ɶ���
    /// </summary>
    private void Complete()
    {
        _isPlaying = false;
        _onComplete?.Invoke();
    }

    /// <summary>
    /// ��ֹ����
    /// </summary>
    public void Kill()
    {
        _isPlaying = false;
    }

    /// <summary>
    /// ���ö�����ɻص�
    /// </summary>
    /// <param name="action">�ص�����</param>
    /// <returns></returns>
    public Tween OnComplete(Action action)
    {
        _onComplete = action;
        return this;
    }
}

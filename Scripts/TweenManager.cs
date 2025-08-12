using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tween������
/// </summary>
public class TweenManager : MonoBehaviour
{
    private static TweenManager _instance;//����������
    private readonly List<Tween> _activeTweens = new List<Tween>();//������л�е�Tween

    /// <summary>
    /// �������ʵ�
    /// </summary>
    public static TweenManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TweenManager>();//�ڳ����в�������ʵ��
                if (_instance == null)
                {
                    GameObject go = new GameObject("TweenManager");//�Զ���������������
                    _instance = go.AddComponent<TweenManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    /// <summary>
    /// ÿ֡�������л�е�Tween
    /// </summary>
    private void Update()
    {
        //�Ӻ���ǰ��������ɾ��ʱ����������
        for (int i = _activeTweens.Count - 1; i >= 0; i--)
        {
            if (!_activeTweens[i].Update(Time.deltaTime))
            {
                _activeTweens.RemoveAt(i);//�Ƴ�����ɺͱ�ɱ���Ķ���
            }
        }
    }

    /// <summary>
    /// ����µ�Tween���б�
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

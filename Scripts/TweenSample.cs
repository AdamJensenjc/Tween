using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenSample : MonoBehaviour
{
    void Start()
    {
        //�ƶ�ʾ��
        transform.DoMove(new Vector3(5, 0, 0), 2f, EaseType.EaseInOutBack)
                .OnComplete(() => Debug.Log("�ƶ���ɣ�"));

        ////����ʾ��
        //transform.DoScale(Vector3.one * 2, 1f, EaseType.EaseOutBack)
        //        .OnComplete(() => Debug.Log("������ɣ�"));

        ////��ֹʾ��
        //Tween myTween = transform.DoMove(new Vector3(5, 0, 0), 2f, EaseType.EaseInOutSine);
        //myTween.Kill();
        //Debug.Log("��ֹ������");
    }
}

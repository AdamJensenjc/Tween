using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenSample : MonoBehaviour
{
    void Start()
    {
        //移动示例
        transform.DoMove(new Vector3(5, 0, 0), 2f, EaseType.EaseInOutBack)
                .OnComplete(() => Debug.Log("移动完成！"));

        ////缩放示例
        //transform.DoScale(Vector3.one * 2, 1f, EaseType.EaseOutBack)
        //        .OnComplete(() => Debug.Log("缩放完成！"));

        ////终止示例
        //Tween myTween = transform.DoMove(new Vector3(5, 0, 0), 2f, EaseType.EaseInOutSine);
        //myTween.Kill();
        //Debug.Log("终止动画！");
    }
}

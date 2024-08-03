using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOTweenHover : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private float hoverSpeed;

    void Start()
    {
        transform.DOLocalMoveY(distance, hoverSpeed).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }
}

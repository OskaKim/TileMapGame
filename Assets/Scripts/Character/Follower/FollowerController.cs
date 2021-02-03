using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class FollowerController : CharacterControllerBase
{
    [SerializeField] private CharacterModelBase targetModel;
    [SerializeField] private uint startFollowDist = 128;
    [SerializeField] private uint maxCapacity = 512;

    private Transform targetTransform;
    private Vector2 targetPrevPos;
    private Vector2 targetOldestPos;
    private readonly Queue<Vector2> targetPosLogQueue = new Queue<Vector2>();
    private bool isHeadingToTarget = false;

    protected override void Init()
    {
        targetTransform = targetModel.transform;
        targetModel.MoveSpeed = model.MoveSpeed;
        targetOldestPos = transform.localPosition;
    }

    protected override void UpdateInputHandler()
    {
        // 타겟 이동 로그 기록
        if (targetPrevPos != (Vector2)targetTransform.localPosition)
        {
            RecordTargetPos();
        }

        var relativeVec = targetOldestPos - (Vector2)transform.localPosition;

        // 도착. 같은 프레임에서 이동 개시도 처리할 수 있도록 이동 개시보다 위에 와야함
        if (relativeVec.sqrMagnitude < 0.1f)
        {
            ArrivedAtTarget();
        }

        // 이동 개시
        if (targetPosLogQueue.Count > startFollowDist && !isHeadingToTarget)
        {
            StartToMove();
        }

        var priorityVec = Mathf.Abs(relativeVec.x) > Mathf.Abs(relativeVec.y) ? MathUtility.VecType.x : MathUtility.VecType.y;

        model.Dir = MathUtility.GetDirOnlyOneSide(relativeVec, priorityVec);
        model.IsMoving = isHeadingToTarget;

        // 타겟으로부터 거리가 너무 많이 떨어지면 가장 마지막 위치로 순간이동
        if (targetPosLogQueue.Count > maxCapacity)
        {
            TeleportFollower();
        }
    }

    private void RecordTargetPos()
    {
        targetPrevPos = targetTransform.localPosition;
        targetPosLogQueue.Enqueue(targetPrevPos);
    }

    private void StartToMove()
    {
        targetOldestPos = targetPosLogQueue.Dequeue();
        isHeadingToTarget = true;
    }

    private void ArrivedAtTarget()
    {
        isHeadingToTarget = false;
    }

    private void TeleportFollower()
    {
        targetPosLogQueue.Clear();
        transform.localPosition = targetPrevPos;
        isHeadingToTarget = false;
        model.IsMoving = false;
    }
}
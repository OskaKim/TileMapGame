using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterViewBase : MonoBehaviour
{
    [SerializeField] protected CharacterModelBase model;
    [SerializeField] private Animator animator;

    public void LateUpdate()
    {
        UpdateAnimator();
    }

    protected virtual void UpdateAnimator()
    {
        var moveDir = model.MoveDir;
        animator.SetFloat("Horizontal", moveDir.x);
        animator.SetFloat("Vertical", moveDir.y);

        animator.SetBool("IsMoving", !(moveDir.x == 0 && moveDir.y == 0));
    }
}

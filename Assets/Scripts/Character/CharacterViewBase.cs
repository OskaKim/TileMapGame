using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterViewBase : MonoBehaviour
{
    [SerializeField] protected CharacterModelBase model;
    [SerializeField] protected Animator animator;

    private void Awake()
    {
        if (model == null) model = GetComponent<CharacterModelBase>();
        if (animator == null) animator = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        UpdateAnimator();
    }

    protected virtual void UpdateAnimator()
    {
        var dir = model.Dir;
        animator.SetFloat("Horizontal", dir.x);
        animator.SetFloat("Vertical", dir.y);
        animator.SetBool("IsMoving", model.IsMoving);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerBase : MonoBehaviour
{
    [SerializeField] protected CharacterModelBase model;

    protected virtual void Awake() { }

    public virtual void Update()
    {
        UpdateInputHandler();
    }

    protected virtual void UpdateInputHandler(){}

}

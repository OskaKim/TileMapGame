using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerBase : MonoBehaviour
{
    [SerializeField] protected CharacterModelBase model;

    public virtual void Update()
    {
        UpdateInputHandler();
    }

    protected virtual void UpdateInputHandler(){}

}

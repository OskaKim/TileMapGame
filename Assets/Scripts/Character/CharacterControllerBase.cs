using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterControllerBase : MonoBehaviour
{
    [SerializeField] protected CharacterModelBase model;

    private void Awake()
    {
        if(model == null) model = GetComponent<CharacterModelBase>();
        Init();
    }

    private void Update()
    {
        UpdateInputHandler();
    }
    
    // 초기화 정의
    protected abstract void Init();

    // 입력 정의
    protected abstract void UpdateInputHandler();

}

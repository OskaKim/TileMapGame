using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModelBase : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 1;

    // 각 축을 기준으로 -1,0,1의 값.
    public Vector2 Dir { get; set; }
    public bool IsMoving { get; set; }

    public void Update()
    {
        if (IsMoving)
        {
            transform.Translate(Dir * moveSpeed * Time.deltaTime);
        }
    }
}

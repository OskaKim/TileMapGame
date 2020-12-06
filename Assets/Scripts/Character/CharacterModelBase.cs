using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModelBase : MonoBehaviour
{
    // 각 축을 기준으로 -1,0,1의 값. 0이면 멈춘상태
    public Vector2 MoveDir { get; set; }
}

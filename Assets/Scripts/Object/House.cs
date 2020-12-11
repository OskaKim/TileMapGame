using UnityEngine;

// TODO : 상호작용 가능한 배치형 오브젝트는 전부 이걸로 제어가 되게 한 뒤, rename
[RequireComponent(typeof(SpriteRenderer))]
public class House : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite OpenSprite;
    [SerializeField] private Sprite CloseSprite;
    [SerializeField] private bool isOpen = false;

    // 오브젝트 상태제어
    public bool IsOpen
    {
        get { return isOpen; }
        set
        {
            isOpen = value;
            UpdateState();
        }
    }
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        IsOpen = isOpen;
    }

    private void UpdateState()
    {
        spriteRenderer.sprite = isOpen ? OpenSprite : CloseSprite;
    }

#if UNITY_EDITOR
    // 이 방법을 통해 spriteRender의 스프라이트를 변경하면 warning메세지가 나오지만, 에디터에서 테스트용으로 인스펙터를 수정할 때만 해당하므로 무시
    private void OnValidate()
    {
        if (spriteRenderer == null) return;
        IsOpen = isOpen;
    }
#endif
}

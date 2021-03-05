using UnityEngine;
using UnityEngine.EventSystems;

public class JoysticController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 _basePosition;
    private Vector2 _startPos;
    private Vector2 _endPos;

    public void Awake()
    {
        _basePosition = (transform as RectTransform).anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPos = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        var rt = (transform as RectTransform);
        Vector2 offset = new Vector2(Screen.width / 2, rt.sizeDelta.y / 2);
        Vector2 newPos = eventData.position - offset;
        if (Vector2.Distance(_basePosition, newPos) < 300)
        {
            rt.anchoredPosition = newPos;
        }
        else
        {
            Vector2 dir = _basePosition - newPos;
            dir.Normalize();
            rt.anchoredPosition = _basePosition + (new Vector2(300, 300) * -dir);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _endPos = eventData.position;
        (transform as RectTransform).anchoredPosition = _basePosition;
        Vector2 dir = _endPos - _startPos;
        dir.Normalize();
        dir *= (Vector3.Distance(_endPos, _startPos) / 20);
        OnGotDirection(dir);
    }

    public delegate void OnGetDir(Vector2 direction);
    public static event OnGetDir OnGotDirection;
}

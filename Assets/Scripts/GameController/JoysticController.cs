using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Test.GameController
{
    public class JoysticController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Vector2 _basePosition;
        private Vector2 _startPos;
        private Vector2 _endPos;
        public Image _arrow;

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
            RectTransform parent = transform.parent as RectTransform;
            Vector2 offset = new Vector2((parent.sizeDelta.x * parent.lossyScale.x) / 2, 0);
            Vector2 newPos = eventData.position - offset;
            Vector2 arrowDir = newPos - _startPos;
            float distance = Vector2.Distance(_basePosition, newPos);
            _arrow.rectTransform.localScale = new Vector2(1, 1 + (distance > 300 ? 300 : distance) / 300);
            float difY = (eventData.position.y - _startPos.y);
            difY = difY == 0 ? 1 : difY;
            float difX = (eventData.position.x - _startPos.x);
            float targetRotation = Mathf.Atan(difX / difY);
            targetRotation = Mathf.Rad2Deg * targetRotation;
            targetRotation = eventData.position.y > _startPos.y ? (targetRotation * -1) - 180 : -1 * targetRotation;
            Quaternion data = Quaternion.Euler(0, 0, targetRotation);
            rt.rotation = data;
            if (distance < 300)
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
            (transform as RectTransform).rotation = Quaternion.identity;
            _arrow.rectTransform.localScale = Vector3.one;
            (transform as RectTransform).anchoredPosition = _basePosition;
            Vector2 dir = _endPos - _startPos;
            dir.Normalize();
            dir *= (Vector3.Distance(_endPos, _startPos) / 20);
            OnGotDirection(dir);
        }

        public delegate void OnGetDir(Vector2 direction);
        public static event OnGetDir OnGotDirection;
    }
}
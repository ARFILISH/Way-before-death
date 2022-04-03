using DG.Tweening;
using UnityEngine;

namespace _Scripts
{
    public class PanelWindow : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;
        private RectTransform _transform;

        [SerializeField] private bool _shown = false;

        private void Start()
        {
            _canvasGroup.alpha = _shown ? 1 : 0;
            _transform.localScale = _shown ? new Vector2(1f, 1f) : new Vector2(0.75f, 0.75f);
            _canvasGroup.interactable = _shown;
        }
        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _transform = GetComponent<RectTransform>();
        }

        public void Show()
        {
            if (!_shown)
            {
                _shown = true;
                _canvasGroup.interactable = true;
                _canvasGroup.DOFade(1f, 0.2f);
                _transform.DOScale(new Vector2(1, 1), 0.2f);
            }
        }
        public void Hide()
        {
            if (_shown)
            {
                _canvasGroup.interactable = false;
                _canvasGroup.DOFade(0f, 0.2f);
                _transform.DOScale(new Vector2(0.75f, 0.75f), 0.2f);   
            }
        }
    }
}

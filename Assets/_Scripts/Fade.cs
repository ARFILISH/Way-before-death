using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts
{
    [RequireComponent(typeof(Canvas))]
    public class Fade : MonoBehaviour
    {
        private Image _image;

        [SerializeField] private Color _colorToFade;
        
        private void Awake()
        {
            _image = GetComponentInChildren<Image>();
        }

        public void StartFadeIn(float duration)
        {
            _image.color = new Color(0, 0, 0, 0);
            _image.DOColor(_colorToFade, 2f);
        }
        public void StartFadeOut(float duration)
        {
            _image.color = _colorToFade;
            _image.DOColor(new Color(0, 0, 0, 0), 2f);
        }
    }
}
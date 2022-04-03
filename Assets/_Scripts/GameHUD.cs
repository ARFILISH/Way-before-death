using TMPro;
using UnityEngine;

namespace _Scripts
{
    public class GameHUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private TextMeshProUGUI _hintText;
        [SerializeField] private PanelWindow _controlsWindow;
        [SerializeField] private PanelWindow _winWindow;
        [SerializeField] private PanelWindow _looseWindow;

        public void ShowWinWindow()
        {
            _winWindow.Show();
        }
        public void ShowLooseWindow()
        {
            _looseWindow.Show();
        }
        
        public void ShowControls()
        {
            _controlsWindow.Show();
        }

        public void HideControls()
        {
            _controlsWindow.Hide();
        }

        public void ShowTimer()
        {
            _timerText.enabled = true;
        }
        public void HideTimer()
        {
            _timerText.enabled = false;
        }
        
        public void UpdateTimer(int seconds)
        {
            _timerText.text = "Blant will die in <color=green>" + seconds.ToString() + "</color> seconds";
            if (seconds < 10)
                _timerText.color = Color.red;
            else _timerText.color = Color.white;
        }

        public void ShowHint(string hintText)
        {
            _hintText.enabled = true;
            _hintText.text = hintText;
        }
        public void HideHint()
        {
            _hintText.enabled = false;
            _hintText.text = "";
        }
    }
}

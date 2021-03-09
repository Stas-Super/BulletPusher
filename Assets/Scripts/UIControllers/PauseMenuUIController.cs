using UnityEngine;
using UnityEngine.SceneManagement;

namespace Test.UIController
{
    public class PauseMenuUIController : MonoBehaviour
    {
        public RectTransform _menuPanel;

        public void OpenMenu()
        {
            bool active = _menuPanel.gameObject.activeSelf;
            Time.timeScale = active ? 1 : 0;
            _menuPanel.gameObject.SetActive(!active);
        }

        public void BackToMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
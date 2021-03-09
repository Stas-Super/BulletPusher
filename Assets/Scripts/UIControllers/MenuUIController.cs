using UnityEngine;
using UnityEngine.SceneManagement;

namespace Test.UIController
{
    public class MenuUIController : MonoBehaviour
    {
        public void PlayButton()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Game");
        }

        public void QuitButton()
        {
            Application.Quit();
        }
    }
}
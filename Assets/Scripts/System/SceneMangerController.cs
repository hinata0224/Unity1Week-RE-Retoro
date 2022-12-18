using UnityEngine;
using UnityEngine.SceneManagement;

namespace _SceneManager
{
    public class SceneMangerController : MonoBehaviour
    {
        public void GoTitle()
        {
            Time.timeScale = 1;
            SceneManager.LoadSceneAsync("Title", LoadSceneMode.Single);
        }

        public void GoTetrisStart()
        {
            Time.timeScale = 1;
            SceneManager.LoadSceneAsync("Tetris", LoadSceneMode.Single);
        }

        public void GoPacMan()
        {
            Time.timeScale = 1;
            SceneManager.LoadSceneAsync("PacMan",LoadSceneMode.Single);
        }
    }
}
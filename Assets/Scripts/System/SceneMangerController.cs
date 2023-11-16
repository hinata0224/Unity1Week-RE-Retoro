using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneController
{
    public class SceneMangerController : SingletonMonoBehaviour<SceneMangerController>
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
            SceneManager.LoadSceneAsync("PacMan", LoadSceneMode.Single);
        }
    }
}

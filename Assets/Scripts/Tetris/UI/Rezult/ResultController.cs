using UnityEngine;
using TMPro;

namespace Tetris_UI
{
    public class ResultController : MonoBehaviour
    {
        [SerializeField, Header("���U���g�̐e�I�u�W�F�N�g")]
        private GameObject rezult;

        [SerializeField, Header("���U���g�X�R�A")]
        private TextMeshProUGUI scoreText;

        private ScoreModel model = new();

        private void Awake()
        {
            rezult.SetActive(false);
        }

        public void GameOver()
        {
            rezult.SetActive(true);
            scoreText.text = "����̃X�R�A��" + model.GetScorePoint() + "�ł��B";
        }
    }
}
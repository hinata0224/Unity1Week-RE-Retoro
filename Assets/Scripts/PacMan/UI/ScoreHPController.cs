using UnityEngine;
using TMPro;

namespace PackMan_UI
{
    public class ScoreHPController : MonoBehaviour
    {
        [SerializeField, Header("�X�R�A�̃e�L�X�g")]
        private TextMeshProUGUI scoreText;

        [SerializeField, Header("HP�̃e�L�X�g")]
        private TextMeshProUGUI hpText;

        public void SetScore(int now)
        {
            scoreText.text = "�c��" + now;
        }

        public void SetHp(int hp)
        {
            hpText.text = "�c�@ : " + hp;
        }
    }
}
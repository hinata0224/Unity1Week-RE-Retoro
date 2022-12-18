using UnityEngine;
using TMPro;

namespace PackMan_UI
{
    public class ScoreHPController : MonoBehaviour
    {
        [SerializeField, Header("スコアのテキスト")]
        private TextMeshProUGUI scoreText;

        [SerializeField, Header("HPのテキスト")]
        private TextMeshProUGUI hpText;

        public void SetScore(int now)
        {
            scoreText.text = "残り" + now;
        }

        public void SetHp(int hp)
        {
            hpText.text = "残機 : " + hp;
        }
    }
}
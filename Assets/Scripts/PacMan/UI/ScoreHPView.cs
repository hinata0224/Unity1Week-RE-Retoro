using UnityEngine;
using TMPro;

namespace PackMan_UI
{
    public class ScoreHPView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI scoreText;

        [SerializeField]
        private TextMeshProUGUI hpText;

        /// <summary>
        /// 残り個数を表示
        /// </summary>
        /// <param name="now">値</param>
        public void SetScore(int now)
        {
            scoreText.text = "残り" + now;
        }

        /// <summary>
        /// 残り残機を表示
        /// </summary>
        /// <param name="hp">値</param>
        public void SetHp(int hp)
        {
            hpText.text = "残機 : " + hp;
        }
    }
}

using UniRx;

namespace Tetris_UI
{
    public class ScoreModel
    {
        private static ReactiveProperty<int> _score = new(0);
        public IReadOnlyReactiveProperty<int> Score => _score;
        private static int point = 100;

        public static void ResetScore()
        {
            _score.Value = 0;
        }

        public static void AddScore()
        {
            _score.Value += point;
        }

        public int GetScorePoint()
        {
            return _score.Value;
        }
    }
}

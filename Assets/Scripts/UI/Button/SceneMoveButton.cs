using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Constants;
using SceneController;

[RequireComponent(typeof(Button))]
public class SceneMoveButton : MonoBehaviour
{
    [SerializeField] private EMoveScene _moveScene;

    private Button _button;
    private SceneMangerController _sceneManger;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _sceneManger = SceneMangerController.Instance;
    }

    void Start()
    {
        _button.OnButtonObservable()
            .Subscribe(_ => ChangeScene())
            .AddTo(gameObject);
    }

    private void ChangeScene()
    {
        switch (_moveScene)
        {
            case EMoveScene.Title:
                _sceneManger.GoTitle();
                break;
            case EMoveScene.Tetris:
                _sceneManger.GoTetrisStart();
                break;
            case EMoveScene.PacMan:
                _sceneManger.GoPacMan();
                break;
        }
    }
}

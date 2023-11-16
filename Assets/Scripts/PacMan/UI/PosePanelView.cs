using UnityEngine;
using UnityEngine.UI;
using UniRx;
using SceneController;
using UnityEngine.SceneManagement;

public class PosePanelView : MonoBehaviour
{

    [SerializeField] private GameObject _posePanel;
    [SerializeField] private Button _poseButton;
    [SerializeField] private Button _titleButton;
    [SerializeField] private Button _moreButton;

    private bool _isPose = false;
    private SceneMangerController sceneManagerController;

    private void Awake()
    {
        sceneManagerController = SceneMangerController.Instance;
    }

    private void Start()
    {
        _posePanel.SetActive(false);
        _poseButton.OnButtonObservable()
            .Subscribe(_ => DisplayPanel())
            .AddTo(gameObject);

        _titleButton.OnButtonObservable()
            .Subscribe(_ => sceneManagerController.GoTitle())
            .AddTo(gameObject);

        _moreButton.OnButtonObservable()
            .Subscribe(_ => sceneManagerController.GoPacMan())
            .AddTo(gameObject);
    }

    private void DisplayPanel()
    {
        _isPose = !_isPose;
        if (_isPose)
        {
            Time.timeScale = 0;
            _posePanel.SetActive(_isPose);
        }
        else
        {
            Time.timeScale = 1;
            _posePanel.SetActive(_isPose);
        }
    }
}

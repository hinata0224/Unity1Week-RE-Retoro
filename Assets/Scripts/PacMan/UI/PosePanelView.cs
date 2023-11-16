using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class PosePanelView : MonoBehaviour
{

    [SerializeField] private GameObject _posePanel;
    [SerializeField] private Button _poseButton;

    private bool _isPose = false;

    private void Start()
    {
        _posePanel.SetActive(false);
        _poseButton.OnButtonObservable()
            .Subscribe(_ => DisplayPanel())
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

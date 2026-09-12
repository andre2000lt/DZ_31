using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _MiniGame
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private Button _startButton;

        private void Awake()
        {
            _startButton.onClick.AddListener(OnStartButtonClick);
        }

        private void OnStartButtonClick()
        {
            SceneManager.LoadScene((int)SceneType.Game);
        }
    }
}
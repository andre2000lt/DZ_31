using System.Collections;
using TMPro;
using UnityEngine;

namespace _MiniGame
{
    public class ConfirmWindow : MonoBehaviour
    {
        [SerializeField] private TMP_Text _messageOutput;

        public void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }

        public IEnumerator ConfirmProcess(KeyCode keyCodeToConfirm)
        {
            _messageOutput.text = $"Press '{keyCodeToConfirm}' to start";

            yield return new WaitUntil(() => Input.GetKeyDown(keyCodeToConfirm));

            Hide();
        }
    }
}
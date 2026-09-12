using System.Collections;
using TMPro;
using UnityEngine;

namespace _MiniGame
{
    public class EndGameWindow : MonoBehaviour
    {
        private const int SecondsToShow = 5;

        [SerializeField] private TMP_Text _counterOutput;

        public IEnumerator Show()
        {
            gameObject.SetActive(true);

            int i = SecondsToShow;

            while (i > 0)
            {
                _counterOutput.text = i.ToString();
                i--;

                yield return new WaitForSeconds(1f);
            }

            Hide();
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
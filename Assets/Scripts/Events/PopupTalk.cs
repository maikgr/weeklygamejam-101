using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Events {
    public class PopupTalk : MonoBehaviour {

        [SerializeField]
        private TextMeshProUGUI textMesh;
        [SerializeField]
        private float duration;

        private float timeLeft;

        public void OnEnable () {
            timeLeft = duration;
        }

        public void Update () {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0) {
                gameObject.SetActive(false);
            }
        }

        public void Display(string text) {
            textMesh.text = text;
            gameObject.SetActive(true);
        }
    }
}

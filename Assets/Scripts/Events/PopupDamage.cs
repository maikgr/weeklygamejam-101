using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts {
    public class PopupDamage : MonoBehaviour {
        [SerializeField]
        private float floatSpeed;
        [SerializeField]
        private float fadeSpeed;
        [SerializeField]
        private float floatTime;
        [SerializeField]
        private TextMeshProUGUI textMesh;

        private RectTransform rect;
        private Vector2 initialPos;
        private float currentFloatTime;

        private void Awake () {
            rect = gameObject.GetComponent<RectTransform>();
            initialPos = rect.anchoredPosition;
        }

        private void OnEnable () {
            rect.anchoredPosition = initialPos;
            currentFloatTime = floatTime;
            textMesh.alpha = 1f;
        }

        private void Update () {
            rect.anchoredPosition += new Vector2(0, floatSpeed) * Time.deltaTime;
            currentFloatTime -= Time.deltaTime;

            if (currentFloatTime < 0) {
                textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, textMesh.color.a - (fadeSpeed * Time.deltaTime));

                if (textMesh.color.a < 0) {
                    gameObject.SetActive(false);
                }
            }
        }

        public void Hit(int damage) {
            this.textMesh.text = damage.ToString();
            this.textMesh.color = Color.yellow;
            this.textMesh.fontSize = 20;
            this.gameObject.SetActive(true);
        }

        public void Crit(int damage) {
            this.textMesh.text = damage.ToString() + "!";
            this.textMesh.color = Color.red;
            this.textMesh.fontSize = 30;
            this.gameObject.SetActive(true);
        }

        public void Miss() {
            this.textMesh.text = "Miss";
            this.textMesh.color = Color.white;
            this.textMesh.fontSize = 20;
            this.gameObject.SetActive(true);
        }
    }
}

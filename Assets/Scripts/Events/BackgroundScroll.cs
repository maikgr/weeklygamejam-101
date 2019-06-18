using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Events {
    public class BackgroundScroll : MonoBehaviour {
        [SerializeField]
        private RectTransform bgTransform;
        [SerializeField]
        private float speed;
        [SerializeField]
        private float overlapXPos;

        private Vector2 initialPos;

        private void Awake() {
            initialPos = bgTransform.anchoredPosition;
        }

        private void Update() {
            float newXPos = bgTransform.anchoredPosition.x - (speed * Time.deltaTime);
            bgTransform.anchoredPosition = new Vector2(newXPos, bgTransform.anchoredPosition.y);
            if (newXPos < overlapXPos) {
                bgTransform.anchoredPosition = initialPos;
            }
        }
    }
}

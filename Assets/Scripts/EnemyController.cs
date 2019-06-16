using Assets.Scripts.Entities;
using Assets.Scripts.Repositories;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts {
    public class EnemyController : MonoBehaviour {
        [SerializeField]
        private Image sprite;
        [SerializeField]
        private RectTransform healthBar;
        [SerializeField]
        private TextMeshProUGUI healthPoints;
        [SerializeField]
        private TextMeshProUGUI displayName;
        [SerializeField]
        private PopupDamage popupDamage;

        public int CurrentHealth { get; private set; }

        private RectTransform rect;
        private Enemy enemy;
        private TalkType talkType;

        public void Awake () {
            rect = GetComponent<RectTransform>();
        }

        public void SetEnemy (Enemy enemy, Trait trait) {
            this.enemy = enemy;
            this.talkType = trait.CorrectTalk;

            this.displayName.text = trait.Name + " " + enemy.name;
            this.CurrentHealth = enemy.stats.health;
            this.sprite.sprite = enemy.sprite.sprite;
            this.rect.sizeDelta = enemy.sprite.size;
            this.rect.anchoredPosition = enemy.sprite.offset;
            UpdateHealth();

            this.gameObject.SetActive(true);
        }

        public void UpdateHealth () { 
            healthBar.localScale = new Vector3 ((float) CurrentHealth / enemy.stats.health, 1f, 1f);
            healthPoints.text = $"{CurrentHealth}/{enemy.stats.health}";
        }

        public void ReceiveDamage (int damage, bool isCrit = false) {
            if (isCrit) {
                popupDamage.Crit(damage);
            }
            else {
                popupDamage.Hit(damage);
            }
            CurrentHealth = Mathf.Max(0, CurrentHealth - damage);
            UpdateHealth();
            CheckAlive();
        }

        public void MissDamage() {
            popupDamage.Miss();
        }

        public Stats Stats {
            get {
                return enemy.stats;
            }
        }

        public TalkType GetTalkType () {
            return talkType;
        }

        public bool IsAlive {
            get {
                return CurrentHealth > 0;
            }
        }

        private void CheckAlive () {
            if (CurrentHealth.Equals(0)) {
                gameObject.SetActive(false);
            }
        }
    }
}

using Assets.Scripts.Entities;
using Assets.Scripts.Events;
using Assets.Scripts.Repositories;
using System;
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
        [SerializeField]
        private Animator animator;

        public int CurrentHealth { get; private set; }

        private RectTransform rect;
        private Enemy enemy;
        private IList<Action> onDeadEvents;

        public void Awake () {
            rect = GetComponent<RectTransform>();
            onDeadEvents = new List<Action>();
        }

        public void SetEnemy (Enemy enemy, Trait trait) {
            this.enemy = enemy;

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
            animator.SetTrigger("hurt");
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

        public void Attack() {
            animator.SetTrigger("attack");
        }

        public Stats Stats {
            get {
                return enemy.stats;
            }
        }

        public bool IsAlive {
            get {
                return CurrentHealth > 0;
            }
        }

        public void OnDead(Action action) {
            onDeadEvents.Add(action);
        }

        private void CheckAlive () {
            if (CurrentHealth.Equals(0)) {
                StartCoroutine(DeadCoroutine());
            }
        }

        private IEnumerator DeadCoroutine() {
            animator.SetTrigger("die");
            yield return new WaitForSeconds(1);
            foreach(Action action in onDeadEvents) {
                action.Invoke();
            }
            gameObject.SetActive(false);
        }
    }
}

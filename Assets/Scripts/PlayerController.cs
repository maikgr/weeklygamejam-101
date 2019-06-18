using Assets.Scripts.Entities;
using Assets.Scripts.Events;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts {
    public class PlayerController : MonoBehaviour {
        public int maxStamina;

        [SerializeField]
        private Stats stats;
        [SerializeField]
        private RectTransform healthBar;
        [SerializeField]
        private TextMeshProUGUI healthPoints;
        [SerializeField]
        private RectTransform staminaBar;
        [SerializeField]
        private TextMeshProUGUI staminaPoints;
        [SerializeField]
        private PopupDamage popupDamage;
        [SerializeField]
        private Animator animator;

        private int currentHealth;
        private int currentStamina;

        private void Awake () {
            currentHealth = stats.health;
        }

        private void Start () {
            UpdateHealth();
        }

        private void UpdateHealth () {
            healthBar.localScale = new Vector3((float)currentHealth / stats.health, 1, 1);
            healthPoints.text = $"{currentHealth}/{stats.health}";
        }

        public Stats Stats {
            get {
                return stats;
            }
        }

        public void ReceiveDamage (int damage, bool isCrit = false) {
            animator.SetTrigger("hurt");
            if (isCrit) {
                popupDamage.Crit(damage);
            }
            else {
                popupDamage.Hit(damage);
            }
            currentHealth = Mathf.Max(0, currentHealth - damage);
            UpdateHealth();
        }

        public void Attack() {
            animator.SetTrigger("attack");
        }

        public void BattleMode(bool active) {
            animator.SetBool("battle", active);
        }

        public void ExploreMode(bool active) {
            animator.SetBool("explore", active);
        }

        public void MissDamage () {
            popupDamage.Miss();
        }

        public void ReduceStamina () {
            currentStamina -= 1;
            staminaBar.localScale = new Vector3(currentStamina / maxStamina, 1, 1);
            staminaPoints.text = $"{currentStamina}/{maxStamina}";
        }
    }
}
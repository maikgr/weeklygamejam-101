using Assets.Scripts.Entities;
using Assets.Scripts.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts {
    public class BattleSystemController : MonoBehaviour {
        [SerializeField]
        private PlayerController playerControl;
        [SerializeField]
        private EnemyController enemyControl;
        [SerializeField]
        private EnemyRepository enemyRepository;
        [SerializeField]
        private PopupTalk playerTalk;
        [SerializeField]
        private PopupTalk enemyTalk;

        public Enemy Enemy { get; private set; }
        public Trait Trait { get; private set; }
        private TraitRepository traitRepository;
        private TalkRepository talkRepository;
        private IList<Action> animatingEvents;
        private IList<Action> animatedEvents;

        private void Awake () {
            this.traitRepository = new TraitRepository();
            this.talkRepository = new TalkRepository();
            this.animatingEvents = new List<Action>();
            this.animatedEvents = new List<Action>();
        }

        public void SetEnemy () {
            Trait = traitRepository.Random();
            Enemy = enemyRepository.GetEnemy();

            this.enemyControl.SetEnemy(Enemy, Trait);
        }

        public void Fight () {
            StartCoroutine(AttackCoroutine());
        }

        public bool Talk (string pickedOption) {
            int missingHealth = Enemy.stats.health - enemyControl.CurrentHealth;
            bool isChance = DiceRoll(Mathf.Max(missingHealth / (Enemy.stats.health - 1) * 100, 10));
            bool isCorrect = talkRepository.CheckCorrectTalk(Trait.CorrectTalk, pickedOption);
            bool result = isCorrect && isChance;
            StartCoroutine(TalkCoroutine(result));
            return result;
        }

        public void OnAnimating(Action action) {
            animatingEvents.Add(action);
        }

        public void OnAnimated(Action action) {
            animatedEvents.Add(action);
        }

        private void ExecuteActions(IEnumerable<Action> actions) {
            foreach(Action action in actions) {
                action.Invoke();
            }
        }

        private IEnumerator AttackCoroutine () {
            ExecuteActions(animatingEvents);
            AttackEnemy();
            yield return new WaitForSeconds(1f);
            if (enemyControl.IsAlive) {
                AttackPlayer();
                yield return new WaitForSeconds(1f);
            }
            ExecuteActions(animatedEvents);
        }

        private IEnumerator TalkCoroutine (bool isCorrect) {
            ExecuteActions(animatingEvents);
            playerTalk.Display("···");
            yield return new WaitForSeconds(1);
            enemyTalk.Display("···");
            enemyTalk.gameObject.SetActive(true);
            yield return new WaitForSeconds(1);
            if (isCorrect) {
                enemyTalk.Display(":)");
            }
            else {
                enemyTalk.Display(">:(");
                yield return new WaitForSeconds(1);
                AttackPlayer();
            }
            yield return new WaitForSeconds(1);
            ExecuteActions(animatedEvents);
        }

        private void AttackEnemy () {
            Stats player = playerControl.Stats;
            bool isHit = DiceRoll(player.accuracy);
            bool isCrit = DiceRoll(player.critRate);

            if (isHit && isCrit) {
                enemyControl.ReceiveDamage((int)(player.damage * 1.5f), true);
            }
            else if (isHit && !isCrit) {
                enemyControl.ReceiveDamage(player.damage);
            }
            else {
                enemyControl.MissDamage();
            }
        }

        private void AttackPlayer () {
            Stats enemy = enemyControl.Stats;
            bool isHit = DiceRoll(enemy.accuracy);
            bool isCrit = DiceRoll(enemy.critRate);

            if (isHit && isCrit) {
                playerControl.ReceiveDamage((int)(enemy.damage * 1.5f), true);
            }
            else if (isHit && !isCrit) {
                playerControl.ReceiveDamage(enemy.damage);
            }
            else {
                playerControl.MissDamage();
            }
        }

        private bool DiceRoll(int rate) {
            return UnityEngine.Random.Range(0, 100) < rate;
        }
    }
}
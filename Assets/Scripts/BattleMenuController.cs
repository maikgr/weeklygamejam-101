using Assets.Scripts.Entities;
using Assets.Scripts.Repositories;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts {
    public class BattleMenuController : MonoBehaviour {
        [SerializeField]
        private RectTransform actionMenu;
        [SerializeField]
        private RectTransform talkMenu;
        [SerializeField]
        private BattleSystemController battleSystem;

        private TalkRepository talkRepository;
        private Button fightButton;
        private Button talkButton;
        private Button talkBackButton;
        private Button talkOption1;
        private Button talkOption2;
        private Button talkOption3;
        private Button talkOption4;

        private void Awake () {
            this.talkRepository = new TalkRepository();
            fightButton = actionMenu.Find("Fight").GetComponent<Button>();

            talkButton = actionMenu.Find("Talk").GetComponent<Button>();
            talkButton.onClick.AddListener(() => ShowTalkMenu(true));

            talkBackButton = talkMenu.Find("Back").GetComponent<Button>();
            talkBackButton.onClick.AddListener(() => ShowTalkMenu(false));

            talkOption1 = talkMenu.Find("Option 1").GetComponent<Button>();
            talkOption2 = talkMenu.Find("Option 2").GetComponent<Button>();
            talkOption3 = talkMenu.Find("Option 3").GetComponent<Button>();
            talkOption4 = talkMenu.Find("Option 4").GetComponent<Button>();

            battleSystem.OnAnimating(() => {
                actionMenu.gameObject.SetActive(false);
                talkMenu.gameObject.SetActive(false);
            });

            battleSystem.OnAnimated(() => {
                actionMenu.gameObject.SetActive(true);
                talkMenu.gameObject.SetActive(false);
            });
        }

        private void OnEnable () {
            ShowTalkMenu(false);
            battleSystem.SetEnemy();
            ConfigureFight();
            ConfigureTalk();
        }

        public void ShowTalkMenu (bool show) {
            actionMenu.gameObject.SetActive(!show);
            talkMenu.gameObject.SetActive(show);
        }

        private void ConfigureFight() {
            fightButton.onClick.AddListener(() => battleSystem.Fight());
        }

        private void ConfigureTalk () {
            int optionNumber = 4;
            string[] talks = talkRepository.GetTalkOptions(battleSystem.Trait.CorrectTalk, optionNumber).ToArray();
            talkOption1.GetComponentInChildren<TextMeshProUGUI>().text = talks[0];
            BindTalkEvent(talkOption1, talks[0]);

            talkOption2.GetComponentInChildren<TextMeshProUGUI>().text = talks[1];
            BindTalkEvent(talkOption2, talks[1]);

            talkOption3.GetComponentInChildren<TextMeshProUGUI>().text = talks[2];
            BindTalkEvent(talkOption3, talks[2]);

            talkOption4.GetComponentInChildren<TextMeshProUGUI>().text = talks[3];
            BindTalkEvent(talkOption4, talks[3]);
        }

        private void BindTalkEvent (Button button, string option) {
            button.onClick.AddListener(() => {
                battleSystem.Talk(option);
            });
        }
    }
}
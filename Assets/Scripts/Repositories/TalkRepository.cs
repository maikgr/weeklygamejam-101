using Assets.Scripts.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Repositories {
    public class TalkRepository {
        private readonly IDictionary<TalkType, IList<string>> talkMap;
        private readonly TalkType[] types;
        public TalkRepository() {
            talkMap = new Dictionary<TalkType, IList<string>>() {
                { TalkType.Anger, GetAngerTalk() },
                { TalkType.Arrogant, GetArrogantTalk() },
                { TalkType.Comedic, GetComedicTalk() },
                { TalkType.Kind, GetKindTalk() },
                { TalkType.Vague, GetVagueTalk() },
            };

            types = new TalkType[] {
                TalkType.Anger,
                TalkType.Arrogant,
                TalkType.Comedic,
                TalkType.Kind,
                TalkType.Vague
            };
        }

        public bool CheckCorrectTalk(TalkType target, string talk) {
            return talkMap[target].Contains(talk);
        }

        public IEnumerable<string> GetTalkOptions(TalkType targetType, int number) {
            IList<string> options = new List<string> {
                talkMap[targetType].Random()
            };

            IList<string> otherTalks = GetOtherTalks(targetType);
            otherTalks.Randomize();
            for (int i = 0; i < number - 1; ++i) {
                options.Add(otherTalks[i]);
            }

            options.Randomize();

            return options;
        }

        private IList<string> GetOtherTalks(TalkType exclude) {
            IList<string> otherTalks = new List<string>();
            foreach (TalkType type in types) {
                if (type != exclude) {
                    foreach (string talk in talkMap[type]) {
                        otherTalks.Add(talk);
                    }
                }
            }

            return otherTalks;
        }

        private IList<string> GetAngerTalk() {
            return new string[] {
                "Complain about attacks",
                "Scream in frustration",
                "Swear at the enemy",
                "Threaten menacingly"
            };
        }

        private IList<string> GetKindTalk() {
            return new string[] {
                "Ask about injury",
                "Reassure constructively",
                "Offer help",
                "Support enemy opinion"
            };
        }

        private IList<string> GetComedicTalk() {
            return new string[] {
                "Tell a bad pun",
                "Shows a magic trick",
                "Make funny faces",
                "Say something funny"
            };
        }

        private IList<string> GetArrogantTalk() {
            return new string[] {
                "Belittle enemy skills",
                "Show off your skills",
                "Talk about achievements",
                "Point out enemy flaws"
            };
        }

        private IList<string> GetVagueTalk() {
            return new string[] {
                "Keep quiet",
                "Say ok",
                "Approach awkwardly",
                "Mumbles"
            };
        }
    }
}

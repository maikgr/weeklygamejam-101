using Assets.Scripts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities {
    public class Trait {
        public string Name { get; set; }
        public TalkType CorrectTalk { get; set; }

        public Trait (string name, TalkType correctTalk) {
            Name = name;
            CorrectTalk = correctTalk;
        }
    }
}

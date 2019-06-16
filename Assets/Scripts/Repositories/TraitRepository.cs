using Assets.Scripts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Repositories {
    public class TraitRepository {
        public TraitRepository() {

        }

        public Trait Random() {
            return new Trait("Envious", TalkType.Arrogant);
        }
    }
}

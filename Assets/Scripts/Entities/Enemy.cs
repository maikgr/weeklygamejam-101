using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities {
    [Serializable]
    public class Enemy {
        public string name;
        public Stats stats;
        public EnemySprite sprite;
    }
}

using Assets.Scripts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Repositories {
    public class EnemyRepository : MonoBehaviour{
        [SerializeField]
        private List<Enemy> enemies;

        public Enemy GetEnemy() {
            int randIndex = UnityEngine.Random.Range(0, enemies.Count);
            return enemies[randIndex];
        }
    }
}

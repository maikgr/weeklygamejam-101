using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts {
    public class ExploreSystemController : MonoBehaviour {
        [SerializeField]
        PlayerController playerControl;

        void Start () {
            playerControl.ExploreMode(true);
        }

        // Update is called once per frame
        void Update () {

        }
    }
}
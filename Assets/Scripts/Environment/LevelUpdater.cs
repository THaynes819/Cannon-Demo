using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Demo.Control;


namespace Demo.Environment
{
    public class LevelUpdater : MonoBehaviour, ILevelCompleter
    {
        private Level[] levels;

        // Start is called before the first frame update
        void Start()
        {
            levels = Resources.LoadAll<Level>("");
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void CompleteLevel()
        {
            
        }

        public void HalfWayBonus()
        {
            
        }

        public void StageLevel()
        {
            
        }
    }
}


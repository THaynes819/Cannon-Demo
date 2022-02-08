using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Demo.PlayerSpace;
using UnityEngine;

namespace Demo.Environment
{
    public class GameBoard : MonoBehaviour, ILevelCompleter
    {
        [Header("Board Coniguration")]
        [SerializeField] int rowTotal = 4;
        [SerializeField] int targetsPerRow = 4;
        private int targetTotal;

        [Tooltip("Objects that must reset between levels")]
        [SerializeField] GameObject[] restageObjects = null;

        [SerializeField] float destroyDelay = 2.0f;


        void Start()
        {
            targetTotal = rowTotal * targetsPerRow;
        }

        void Update()
        {
            
        }

        

        public void RemoveTarget(int value)
        {
            targetTotal -= value;
            
            if (targetTotal <= 0)
            {
                ReportTargetsCompleted();
            }
        }

        public void ReportTargetsCompleted()
        {
            foreach (var completer in restageObjects)
            {
                if (completer.GetComponent<ILevelCompleter>() != null)
                {
                    completer.GetComponent<ILevelCompleter>().CompleteLevel();
                }
            }
        }


        public void CompleteLevel()
        {
            // Does Game Board need to do anything here?
            Debug.Log("Targets complete");
        }

        public void StageLevel()
        {
            // Game Board could regenerate/Instantiate Targets here for the level
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Demo.Control;
using UnityEngine;

namespace Demo.Environment
{
    public class GameBoard : MonoBehaviour, ILevelCompleter
    {
        
        [Tooltip("Objects that reset between levels")]
        [SerializeField] GameObject[] restageObjects = null;
        [SerializeField] float destroyDelay = 2.0f;

        //maybe remove this variable...
        private int levelNumber = 1;
        private int _startingTotal;
        private int _targetTotal;
        private int _halfTotal;
        private int _rowTotal = 4;
        private int _targetsPerRow = 4;
        private ScoreKeeper _scorekeeper;


        void Start()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            _scorekeeper = player.GetComponent<ScoreKeeper>();
            _startingTotal = _rowTotal * _targetsPerRow;
            _targetTotal = _startingTotal;
            _halfTotal = _startingTotal / 2;
        }

        void Update()
        {

        }

        

        public void RemoveTarget(int value)
        {
            _targetTotal -= value;

            if (_targetTotal == _halfTotal)
            {
                //Debug.Log("Game Board should Report Half"); Working 2/11/2022
                ReportHalfCompleted();
            }
            
            if (_targetTotal <= 0)
            {
                ReportTargetsCompleted();
            }
        }

        public void ReportHalfCompleted()
        {
            foreach (var completer in restageObjects)
            {
                if (completer.GetComponent<ILevelCompleter>() != null)
                {
                    completer.GetComponent<ILevelCompleter>().HalfWayBonus();
                }
            }

            //for each loop wasn't working with the player. May Remove completely. communicating with the scoreKeeper directly
            _scorekeeper.HalfWayBonus();
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

            //for each loop wasn't working with the player. May Remove completely. communicating with the scoreKeeper directly
            _scorekeeper.CompleteLevel();
        }


        public void CompleteLevel()
        {
            // Does Game Board need to do anything here?
            
        }

        public void StageLevel()
        {
            // Game Board could regenerate/Instantiate Targets here for the level
        }

        public void HalfWayBonus()
        {
            
        }
    }

}

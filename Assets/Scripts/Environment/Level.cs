using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Demo.SceneManagement;
using Demo.Control;


namespace Demo.Environment
{
    [CreateAssetMenu (menuName = ("CannonDemo/New Level"))]
    public class Level : ScriptableObject
    {
        [System.Serializable]
        public class BoardConfig
        {
            [Header("Level Configuration")]
            [SerializeField] int _level = 1;
            [SerializeField] int _levelScene = 1;
            [SerializeField] int _nextLevelScene = 1;

            [Header("Game Board Configuration")]
            [SerializeField] int _rows = 4;
            [SerializeField] int _targetsPerRow = 4;

            public int Level {get {return _level;}
                            set {_level = value;}}
            public int LevelScene {get {return _levelScene;}
                                set {_levelScene = value;}}
            public int NextLevelScene {get {return _nextLevelScene;}
                                        set {_nextLevelScene= value;}}
            public int Rows {get { return _rows;}
                            set {_rows = value;}}
            public int Target {get {return _targetsPerRow;}
                            set {_targetsPerRow = value;}}
        }

        [SerializeField] BoardConfig boardConfig;
        private SavingWrapper _savingWrapper;
        private bool _shouldStage = true;
        private bool _isComplete = false;


        public void LoadLevel(int value)
        {
            _shouldStage = true;
            _savingWrapper = FindObjectOfType<SavingWrapper>();
            //boardConfig = new BoardConfig();
            Debug.Log("Board Config: " + boardConfig.Level);
        }

        public BoardConfig GetBoardConfig()
        {
            return boardConfig;
        }

        public int GetLevelByNumber(int value)
        {
            foreach (Level level in Resources.LoadAll<Level>(""))
            {
                if (value == boardConfig.Level)
                {
                    return boardConfig.Level;
                }
                else
                {
                    return 0;
                }
            }
            return 0;
        }

        public bool GetIsComplete()
        {
            return _isComplete;
        }

        public bool ShouldStage()
        {
            if (boardConfig.LevelScene != boardConfig.NextLevelScene)
            {
                _shouldStage = false;
                //_savingWrapper.LoadNextScene(boardConfig.NextLevelScene);
            }

            if (boardConfig.LevelScene == boardConfig.NextLevelScene)
            {
                _shouldStage = true;
            }
            return _shouldStage;
        }


        public void CompleteLevel() //currently going too much
        {
            
        }

    }

}


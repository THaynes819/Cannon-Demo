using UnityEngine;

namespace Demo.Core
{
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObjectPrefab;

        static bool hasSpawned = false;

        private void Awake() 
        {
            if (hasSpawned)
            {
                return;
            }
            SpawnPersistentObject();
            hasSpawned = true;
        }

        private void SpawnPersistentObject()
        {            
            GameObject persistentObject = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPooler : MonoBehaviour
{
    [Serializable]
    public class EnemyClass
    {
        public string tag;
        public GameObject prefab;

        public int size;
    }

    public List<EnemyClass> enemies;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    public ScriptableObject scriptableObject;
    Transform enemiesParent;

    void Start()
    {
        enemiesParent = GameObject.FindGameObjectWithTag("EnemiesParent").transform;
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (EnemyClass enemy in enemies) 
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < enemy.size; i++)
            {
                GameObject obj = Instantiate(enemy.prefab,enemiesParent);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(enemy.tag, objectPool);
        }
        StartCoroutine(SpawnEnemies());
    }

    public IEnumerator SpawnEnemies() {

        for(int i = 0; i < 5; i++) 
        {
            GameObject aux = Spawn("Treant_Melee", new Vector3(0, 0, 0));
            yield return new WaitForSeconds(1);
        }
        for (int i = 0; i < 10; i++)
        {
            GameObject aux = Spawn("Treant", new Vector3(0, 0, 0));
            yield return new WaitForSeconds(1);
        }

    }

    public GameObject Spawn(string tag, Vector3 position) 
    {
        if (!poolDictionary.ContainsKey(tag)) 
            return null;

        GameObject objToSpawn = poolDictionary[tag].Dequeue();

        objToSpawn.SetActive(true);
        objToSpawn.transform.position = position;

        poolDictionary[tag].Enqueue(objToSpawn);

        return objToSpawn;
    }

}

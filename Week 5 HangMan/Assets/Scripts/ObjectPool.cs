using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private ButtonScrt buttonPrefab;
    [SerializeField]
    public static ObjectPool Instance;

    public Dictionary<string, Queue<GameObject>> poolDicionary =new Dictionary<string, Queue<GameObject>>();

    [System.Serializable]
    public class Pool
    {
        public string Tag;
        public GameObject Prefab;
        public int Size;
        public Transform SpawnParent;
    }
    public List<Pool> pools;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        poolDicionary = new Dictionary<string, Queue<GameObject>>();
        InstantiateKeyButtonsPool();

    }
    public void InstantiateKeyButtonsPool()
    {
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.Size; i++)
            {
                var obj = Instantiate(pool.Prefab, pool.SpawnParent);
                obj.gameObject.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDicionary.Add(pool.Tag, objectPool);
        }
    }
    public GameObject GetKeyButton(string tag)
    {
        if (!poolDicionary.ContainsKey(tag))
        {
            return null;
        }
        GameObject objectToSend = poolDicionary[tag].Dequeue();
        objectToSend.gameObject.SetActive(true);
        poolDicionary[tag].Enqueue(objectToSend);
        return objectToSend;
    }

    public void DeactivatePoolObj(string tag)
    {
        GameObject objToDesable = poolDicionary[tag].Dequeue();
        objToDesable.gameObject.SetActive(false);
        poolDicionary[tag].Enqueue(objToDesable);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NoWhaling
{
    public class PoolHandler : MonoBehaviour
    {
        public int size;
        public bool willGrow;
        public GameObject PooledObject;

        List<GameObject> pool;
    
        private void Start()
        {
            pool = new List<GameObject>();
            for(int i = 0; i < size; i++)
            {
                GameObject obj = Instantiate(PooledObject, transform);
                obj.SetActive(false);
                pool.Add(obj);
            }
        }

        public GameObject GetPooledObject()
        {
            for(int i = 0; i < pool.Count; i++)
            {
                if(!pool[i].activeInHierarchy)
                    return pool[i];
            }

            if (willGrow)
            {
                GameObject blt = Instantiate(PooledObject, transform);
                pool.Add(blt);
                blt.SetActive(false);
                return blt;
            }

            return null;
        }
    }
}
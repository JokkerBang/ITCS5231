using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject[] prefabs;

    public void Spawn()
    {
        for (int i = 0; i < prefabs.Length; i++)
        {
            Instantiate(prefabs[i], new Vector3(3, -2, 60+20*i), Quaternion.identity);
        }
    }
}

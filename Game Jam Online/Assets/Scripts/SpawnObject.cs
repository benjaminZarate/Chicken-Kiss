using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] GameObject obj;

    private void Start()
    {
        obj.SetActive(false);
    }

    private void OnDestroy()
    {
        obj.SetActive(true);
    }
}

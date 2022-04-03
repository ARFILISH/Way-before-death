using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Teleport : MonoBehaviour
{
    [SerializeField] private List<GameObject> _allowedObjects;
    public GameObject teleportPoint;
    public UnityEvent teleported;

    private void OnTriggerEnter(Collider other)
    {
        if (_allowedObjects.Contains(other.gameObject))
        {
            other.gameObject.transform.position = teleportPoint.transform.position;
            teleported.Invoke();
        }
    }
}

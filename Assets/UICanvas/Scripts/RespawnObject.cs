using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnObject : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;
    
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    private Rigidbody _rigidBody;

    private void OnEnable()  
    {
        _initialPosition = objectPrefab.transform.position;
        _initialRotation = objectPrefab.transform.rotation;
        _rigidBody = objectPrefab.transform.GetComponent<Rigidbody>();
    }

    public void Respawn()
    {
        objectPrefab.transform.position = _initialPosition;
        objectPrefab.transform.rotation = _initialRotation;

        _rigidBody.linearVelocity = Vector3.zero;
        _rigidBody.angularVelocity = Vector3.zero;
    }

}

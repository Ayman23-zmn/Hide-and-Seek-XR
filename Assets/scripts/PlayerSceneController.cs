using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSceneController : MonoBehaviour
{

    [SerializeField] private GameObject MonsterRadar;


    // Start is called before the first frame update
    void Start()
    {
        MonsterRadar.SetActive(false);
    }

}

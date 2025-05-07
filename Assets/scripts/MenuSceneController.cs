using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Meta.XR.MRUtilityKit;
using UnityEngine;

public class MenuSceneController : MonoBehaviour
{
    [SerializeField] private GameObject PlayerGameInfo;
    [SerializeField] private GameObject SetupUI;


    private void Start()
    {
        //Deactivate player info UI 
        if (PlayerGameInfo != null)
        {
            PlayerGameInfo.SetActive(false);
            Debug.Log("Player info not in scene.");
        }
        else
        {
            Debug.LogWarning("Player info not assigned in the Inspector.");
        }
    }

    public void ActivatePlayerInfo()
    {
            PlayerGameInfo.SetActive(true);   
            SetupUI.SetActive(false);    
    }

    public void ActivateSetupUI()
    {
        SetupUI.SetActive(true);
        PlayerGameInfo.SetActive(false);
    }
}

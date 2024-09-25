using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseTimer : MonoBehaviour
{ 
    MultiplayerManager multiplayer;
    [SerializeField] Image timeImage;
  

    private void Start()
    {
        multiplayer = GetComponentInParent<MultiplayerManager>();
    }
    private void Update()
    {
        timeImage.fillAmount = multiplayer.CurrentTime / 300;
    }    
}

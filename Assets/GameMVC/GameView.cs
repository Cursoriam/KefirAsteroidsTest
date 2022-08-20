using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameView : MonoBehaviour
{
    public Action GameUpdateAction;
    
    // Update is called once per frame
    public void GameUpdate()
    {
        GameUpdateAction?.Invoke();
    }
}

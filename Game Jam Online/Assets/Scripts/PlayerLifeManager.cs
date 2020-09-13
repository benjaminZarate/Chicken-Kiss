using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeManager : MonoBehaviour
{
    public static int hp = 5;

    private void Start()
    {
        hp = 5;
    }
    void Lose() {
        RestartLevel.Restart();
    }

}

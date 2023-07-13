using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCoroutine
{
    public TowerAwaitSetupCoroutine towerAwaitSetupCoroutine;

    public TowerCoroutine(Tower tower)
    {
        towerAwaitSetupCoroutine = new TowerAwaitSetupCoroutine(tower);
    }
}

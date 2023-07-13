using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerManagerCoroutine : MonoBehaviour
{
    public ServerManagerPlayerCoroutine serverManagerPlayerStatsCoroutine;

    public ServerManagerCoroutine()
    {
        serverManagerPlayerStatsCoroutine = new ServerManagerPlayerCoroutine();
    }
}

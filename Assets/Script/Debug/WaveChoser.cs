using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveChoser : MonoBehaviour
{
    public int ChosenWaveCount = 0;

    private void Start()
    {
        WaveManager.Instance.WaveCount = ChosenWaveCount;
    }



}

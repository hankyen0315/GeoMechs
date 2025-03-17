using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    public Dictionary<int, List<GameObject>> EnemysTypeWavePair = new Dictionary<int, List<GameObject>>();
    [SerializeField]
    private WaveManager waveManager;

    public List<GameObject> GetEnemyList()
    {
        int currentSearchWave = waveManager.WaveCount;
        if (EnemysTypeWavePair.ContainsKey(currentSearchWave))
        {
            return EnemysTypeWavePair[currentSearchWave];
        }
        int currentCheckPoint = currentSearchWave;
        EnemysTypeWavePair[currentCheckPoint] = new List<GameObject>();
        currentSearchWave += 1;
        while (waveManager.waveData[currentSearchWave].Type != WaveType.Prepare)
        {
            foreach (EnemyInfo info in waveManager.waveData[currentSearchWave].EnemyInfos)
            {
                if (EnemysTypeWavePair[currentCheckPoint].Contains(info.prefab)) continue;
                EnemysTypeWavePair[currentCheckPoint].Add(info.prefab);
            }
            currentSearchWave += 1;
        }
        return EnemysTypeWavePair[currentCheckPoint];
    }


}

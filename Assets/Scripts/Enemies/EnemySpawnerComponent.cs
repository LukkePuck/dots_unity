using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace Enemies
{
    
public struct EnemySpawnerComponent : IComponentData
{
    public Entity EnemyPrefab;

    public float EntitiesPerWave;
    public float TimeUntilNextWave;
    public float TimeUntilNextWave2;
}
}

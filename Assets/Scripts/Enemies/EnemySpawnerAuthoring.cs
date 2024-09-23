using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace Enemies
{
    public class EnemySpawnerAuthoring : MonoBehaviour
    {
        public GameObject EnemyPrefab;

        public float EntitiesPerWave = 10;
        public float TimeUntilNextWave = 10;
        public float TimeUntilNextWave2 = 10;

        public class EnemySpawnerBaker : Baker<EnemySpawnerAuthoring>
        {
            public override void Bake(EnemySpawnerAuthoring authoring)
            {
                Entity enemySpawnerEntity = GetEntity(TransformUsageFlags.None);

                AddComponent(enemySpawnerEntity, new EnemySpawnerComponent
                {
                    EnemyPrefab = GetEntity(authoring.EnemyPrefab, TransformUsageFlags.None),
                    EntitiesPerWave = authoring.EntitiesPerWave,
                    TimeUntilNextWave = authoring.TimeUntilNextWave,
                    TimeUntilNextWave2 = authoring.TimeUntilNextWave2,
                });
            }
        }
    }
}

using Unity.Burst;
using Unity.Mathematics;
using Unity.Entities;
using Unity.Collections;
using Unity.Transforms;
using UnityEngine;

namespace Enemies
{
   [BurstCompile]
   public partial struct EnemySpawnerSystem : ISystem
   {
      private EntityManager entityManager;

      private Entity enemySpawnerEntity;
      private EnemySpawnerComponent enemySpawnerComponent;

      private Entity playerEntity;
      private Unity.Mathematics.Random random;
      public void OnCreate(ref SystemState state)
      {
         random = Unity.Mathematics.Random.CreateFromIndex((uint)enemySpawnerComponent.GetHashCode());
      }

      public void OnUpdate(ref SystemState state)
      {
         entityManager = state.EntityManager;
         enemySpawnerEntity = SystemAPI.GetSingletonEntity<EnemySpawnerComponent>();
         enemySpawnerComponent = entityManager.GetComponentData<EnemySpawnerComponent>(enemySpawnerEntity);

         SpawnEnemies(ref state);

      }
      [BurstCompile]
      private void SpawnEnemies(ref SystemState state)
      {
         enemySpawnerComponent.TimeUntilNextWave -= SystemAPI.Time.DeltaTime;
         if (enemySpawnerComponent.TimeUntilNextWave <= 0f)
         {
            for (int i = 0; i < enemySpawnerComponent.EntitiesPerWave; i++)
            {
               EntityCommandBuffer ECB = new EntityCommandBuffer(Allocator.Temp);
               Entity enemyEntity = entityManager.Instantiate(enemySpawnerComponent.EnemyPrefab);
               
               LocalTransform enemyTransform = entityManager.GetComponentData<LocalTransform>(enemyEntity);
               float spawnX = UnityEngine.Random.Range(0, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.width)).x);

               enemyTransform.Position = new float3(spawnX,0f,0f);
               ECB.Playback(entityManager);
               ECB.Dispose();
            }

            enemySpawnerComponent.TimeUntilNextWave = enemySpawnerComponent.TimeUntilNextWave2;
         }

         entityManager.SetComponentData(enemySpawnerEntity, enemySpawnerComponent);
      }
   }
}
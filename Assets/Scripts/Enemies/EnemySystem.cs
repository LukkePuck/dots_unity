using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine.SocialPlatforms;

[BurstCompile]
public partial struct EnemySystem : ISystem
{
    private EntityManager entityManager;

    private Entity playerEntity;
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        entityManager = state.EntityManager;

        playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();

        LocalTransform playerTransform = entityManager.GetComponentData<LocalTransform>(playerEntity);
        NativeArray<Entity> allEntities = entityManager.GetAllEntities();

        foreach (Entity entity in allEntities)
        {
                Debug.Log(entityManager.HasComponent<EnemyComponent>(entity));
            if (entityManager.HasComponent<EnemyComponent>(entity))
            {
                LocalTransform enemyTransform = entityManager.GetComponentData<LocalTransform>(entity);
                EnemyComponent enemyComponent = entityManager.GetComponentData<EnemyComponent>(entity);


                float3 moveDirection = math.normalize(playerTransform.Position - enemyTransform.Position);
                enemyTransform.Position += enemyComponent.EnemySpeed*SystemAPI.Time.DeltaTime*moveDirection;
                
                entityManager.SetComponentData(entity, enemyTransform);
            }
        }
    }
}

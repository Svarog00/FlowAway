﻿using Assets.Scripts.Infrastructure.Services;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(GameObject position);
        void CreateHud();
        GameObject CreateHookInstance(Transform casterPosition);
    }
}
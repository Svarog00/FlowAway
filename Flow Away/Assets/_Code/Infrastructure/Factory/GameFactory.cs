using Assets.Scripts.Infrastructure.AssetManagment;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;

        public GameFactory(IAssetProvider assetProvider)
        {
            _assets = assetProvider;
        }

        public GameObject CreateHero()
        {
            return _assets.Instantiate(AssetPaths.PlayerPath, position: Vector2.zero);
        }

        public GameObject CreateHero(GameObject initialPoint)
        {
            return _assets.Instantiate(AssetPaths.PlayerPath, position: initialPoint.transform.position);
        }

        public void CreateHud()
        {
            _assets.Instantiate(AssetPaths.UIPath);
        }
    }
}

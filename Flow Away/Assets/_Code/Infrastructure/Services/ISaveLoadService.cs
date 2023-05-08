using Assets.Scripts.Infrastructure.Services;

namespace Assets.Scripts.Infrastructure
{
    public interface ISaveLoadService : IService
    {
        public void SaveData(string name, WorldData worldData);
        public WorldData LoadData(string name);
        public WorldData LoadHandleSave();
        public void ClearSaves();
    }
}

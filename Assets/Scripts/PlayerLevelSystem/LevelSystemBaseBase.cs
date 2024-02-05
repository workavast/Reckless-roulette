using SomeStorages;

namespace PlayerLevelSystem
{
    public class LevelSystemBaseBase
    {
        protected SomeStorageFloat _experience;
        protected SomeStorageInt _levelsCounter;

        public IReadOnlySomeStorage<int> LevelsCounter => _levelsCounter;
        public IReadOnlySomeStorage<float> ExperienceCounter => _experience;
    }
}
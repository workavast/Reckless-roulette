using UnityEngine;

public class PlayerHeroSaver : MonoBehaviour
{
    public static PlayerHeroSaver Instance { get; private set; }

    private PlayerHeroParamsSaves _playerHeroParamsSaves;
        
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);
            
        _playerHeroParamsSaves = new PlayerHeroParamsSaves();
    }

    public void SaveParams(PlayerHero playerHero)
    {
        _playerHeroParamsSaves = new PlayerHeroParamsSaves(playerHero);
    }

    public void LoadParams(PlayerHero playerHero)
    {
        playerHero.LoadParams(_playerHeroParamsSaves);
    }

    public void Dispose()
    {
        Instance = null;
        Destroy(gameObject);
    }
}

public class PlayerHeroParamsSaves
{
    public int ArmorLevel { get; private set; }
    public int DamageLevel { get; private set; }
    public int HealthLevel { get; private set; }
    
    public float ArmorExp { get; private set; }
    public float DamageExp { get; private set; }
    public float HealthExp { get; private set; }
    
    public float CurrentHealth { get; private set; }

    public PlayerHeroParamsSaves(
        int armorLevel = 0, int damageLevel = 0, int healthLevel = 0, 
        float armorExp = 0, float damageExp = 0, float healthExp = 0, 
        float currentHealth = float.MaxValue)
    {
        ArmorLevel = armorLevel;
        DamageLevel = damageLevel;
        HealthLevel = healthLevel;
        
        ArmorExp = armorExp;
        DamageExp = damageExp;
        HealthExp = healthExp;
        
        CurrentHealth = currentHealth;
    }
            
    public PlayerHeroParamsSaves(PlayerHero playerHero)
    {
        ArmorLevel = playerHero.ArmorLevelSystem.LevelsCounter.CurrentValue;
        DamageLevel = playerHero.DamageLevelSystem.LevelsCounter.CurrentValue;
        HealthLevel = playerHero.HealthPointsLevelSystem.LevelsCounter.CurrentValue;
        
        ArmorExp = playerHero.ArmorLevelSystem.ExperienceCounter.CurrentValue;
        DamageExp = playerHero.DamageLevelSystem.ExperienceCounter.CurrentValue;
        HealthExp = playerHero.HealthPointsLevelSystem.ExperienceCounter.CurrentValue;
        
        CurrentHealth = playerHero.HealthPoints.CurrentValue;
    }
}
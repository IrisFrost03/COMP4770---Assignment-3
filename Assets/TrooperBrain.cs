using KaijuSolutions.Agents.Exercises.CTF;
using KaijuSolutions.Agents.Extensions;
using KaijuSolutions.Agents.Utility;

public class TrooperBrain : KaijuUtilityBrain {
    private Trooper trooper;

    protected new void Awake() {
        base.Awake();
        trooper = GetComponent<Trooper>();
    }

    protected override void UpdateBlackboard() {
        if (trooper != null) {
            SetFloat("Health", trooper.Health);
            SetFloat("Ammo", trooper.Ammo);
            SetBool("HasAmmo", trooper.HasAmmo);
            SetBool("CanAttack", trooper.CanAttack);
            SetBool("TeamOne", trooper.TeamOne);
            SetBool("HasFlag", trooper.FlagPosition.childCount > 0);  // If we are carrying the enemy flag.
            SetBool("FlagAtBase", Flag.Base(trooper.TeamOne) == (trooper.TeamOne ? Flag.TeamOneFlag.Position : Flag.TeamTwoFlag.Position));  // If our flag is at our base.
            
            // Nearest health detect.
            HealthVisionSensor healthSensor = Agent.GetSensor<HealthVisionSensor>();
            if (healthSensor != null && healthSensor.Observed.Count > 0) {
                HealthPickup nearestHealth = Position.Nearest(healthSensor.Observed, out float distance);
                Set("NearestHealth", nearestHealth);
                SetFloat("HealthDistance", distance);
                Set("HealthVisible", true);
            }
            else {
                Set("NearestHealth", null);
                SetFloat("HealthHealthDistance", float.MaxValue);
                Set("HealthVisible", false);
            }

            // Nearest ammo detect.
            AmmoVisionSensor ammoSensor = Agent.GetSensor<AmmoVisionSensor>();
            if (ammoSensor != null && ammoSensor.Observed.Count > 0) {
                AmmoPickup nearestAmmo = Position.Nearest(ammoSensor.Observed, out float distance);
                Set("NearestAmmo", nearestAmmo);
                SetFloat("AmmoDistance", distance);
                Set("AmmoVisible", true);
            }
            else {
                Set("NearestAmmo", null);
                SetFloat("AmmoDistance", float.MaxValue);
                Set("AmmoVisible", false);
            }
            
            // Nearest enemy detect.
            TrooperEnemyVisionSensor enemySensor = Agent.GetSensor<TrooperEnemyVisionSensor>();
            if (enemySensor != null && enemySensor.Observed.Count > 0) {
                Trooper nearestEnemy = Position.Nearest(enemySensor.Observed, out float distance);
                Set("NearestEnemy", nearestEnemy);
                SetFloat("EnemyDistance", distance);
                SetBool("EnemyVisible", true);
            }
            else {
                Set("NearestEnemy", null);
                SetFloat("EnemyDistance", float.MaxValue);
                SetBool("EnemyVisible", false);
            }
        }
    }
}
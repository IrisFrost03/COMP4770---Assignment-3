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
            Set("Trooper", trooper);  // Save the trooper itself for easy lookup in actions.
            SetFloat("Health", trooper.Health);  // Our current health. Use this when considering how badly we want a health pack.
            SetFloat("Ammo", trooper.Ammo);  // Our current health. Use this when considering how badly we want to pick up ammo.
            SetBool("HasAmmo", trooper.HasAmmo); // If we currently have ammo. Can use this when seeing if we can consider attacking.
            SetBool("CanAttack", trooper.CanAttack);  // If we are currently able to attack, meaning the weapon is not on cooldown. Can use this when seeing if we can consider attacking.
            SetBool("TeamOne", trooper.TeamOne);  // Store what team we are on.
            Set("EnemyFlag", trooper.TeamOne ? Flag.TeamTwoFlag : Flag.TeamOneFlag);  // The enemy flag. Can potentially try to navigate to this to pick it up, in which case we navigate towards the enemy flag.
            Set("TeamFlag", trooper.TeamOne ? Flag.TeamOneFlag : Flag.TeamTwoFlag);  // Our team's flag. Can potentially try to navigate to this to recover it, in which case we navigate towards our flag.
            SetBool("HasFlag", trooper.FlagPosition.childCount > 0);  // If we are carrying the enemy flag. Can use this to help consider if we should head back to our base with the enemy's flag.
            SetBool("FlagAtBase", Flag.Base(trooper.TeamOne) == (trooper.TeamOne ? Flag.TeamOneFlag.Position : Flag.TeamTwoFlag.Position));  // If our flag is at our base. Can use this to potentially make a high priority to chase down the enemy which captured our flag, in which case we navigate towards our flag.
            SetBool("TeamCapturingFlag", (trooper.TeamOne ? Flag.TeamTwoFlag : Flag.TeamOneFlag).Parent != null);  // Boolean to see if someone on our team is currently carrying the enemy's flag. Could maybe use this to see if our team already is collecting it and thus we can focus on something else.
            SetBool("EnemyCapturingFlag", (trooper.TeamOne ? Flag.TeamOneFlag : Flag.TeamTwoFlag).Parent != null);  // Boolean to see if someone on the enemy team is currently carrying our team's flag. Can use this to potentially make a high priority to chase down the enemy which captured our flag, in which case we navigate towards our flag.
            SetFloat("TeamFlagDistance", Position.Distance(trooper.TeamOne ?  Flag.TeamOneFlag.Position : Flag.TeamTwoFlag.Position));  // The distance from us to our team's flag. Could maybe be useful in knowing if we should try to recover it.
            SetFloat("EnemyFlagDistance", Position.Distance(trooper.TeamOne ?  Flag.TeamTwoFlag.Position : Flag.TeamOneFlag.Position));  // The distance from us to the enemy team's flag. Could be useful in deciding if we should try to capture the enemy's flag.
            Set("BasePosition", Flag.Base(trooper.TeamOne));  // The location of our base. If we have the flag, we should have a high desire to move here.
            
            // Nearest health detect.
            HealthVisionSensor healthSensor = Agent.GetSensor<HealthVisionSensor>();
            if (healthSensor != null && healthSensor.Observed.Count > 0) {
                HealthPickup nearestHealth = Position.Nearest(healthSensor.Observed, out float distance);
                Set("HealthVisible", true);  // If we even see a health pickup, and if we don't there is no use in looking at the other variables.
                SetFloat("HealthDistance", distance);  // The distance to the health pickup.
                Set("NearestHealth", nearestHealth);  // The nearest health pickup itself which we could maybe navigate to.
            }
            else {
                Set("HealthVisible", false);
                SetFloat("HealthHealthDistance", float.MaxValue);
                Set("NearestHealth", null);
            }

            // Nearest ammo detect.
            AmmoVisionSensor ammoSensor = Agent.GetSensor<AmmoVisionSensor>();
            if (ammoSensor != null && ammoSensor.Observed.Count > 0) {
                AmmoPickup nearestAmmo = Position.Nearest(ammoSensor.Observed, out float distance);
                Set("AmmoVisible", true);  // If we even see an ammo pickup, and if we don't there is no use in looking at the other variables.
                SetFloat("AmmoDistance", distance);  // The distance to the ammo pickup.
                Set("NearestAmmo", nearestAmmo);  // The nearest ammo health itself which we could maybe navigate to.
            }
            else {
                Set("AmmoVisible", false);
                SetFloat("AmmoDistance", float.MaxValue);
                Set("NearestAmmo", null);
            }
            
            // Nearest enemy detect.
            TrooperEnemyVisionSensor enemySensor = Agent.GetSensor<TrooperEnemyVisionSensor>();
            if (enemySensor != null && enemySensor.Observed.Count > 0) {
                Trooper nearestEnemy = Position.Nearest(enemySensor.Observed, out float distance);
                SetBool("EnemyVisible", true);  // If we even see an enemy, and if we don't there is no use in looking at the other variables.
                SetFloat("EnemyDistance", distance);  // The distance to the enemy.
                Set("NearestEnemy", nearestEnemy);  // The nearest enemy itself which we could maybe attack it.
            }
            else {
                SetBool("EnemyVisible", false);
                SetFloat("EnemyDistance", float.MaxValue);
                Set("NearestEnemy", null);
            }
        }
    }
}
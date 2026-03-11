using KaijuSolutions.Agents;
using KaijuSolutions.Agents.Exercises.CTF;
using KaijuSolutions.Agents.Extensions;
using KaijuSolutions.Agents.Utility;
using UnityEngine;

public class MicrobeBrain : KaijuUtilityBrain {
    private Trooper trooper;

    protected new void Awake() {
        base.Awake();
        trooper = GetComponent<Trooper>();
    }

    protected override void UpdateBlackboard() {
        if (trooper != null) {
            // Existing code
            SetFloat("Health", trooper.Health);
            SetFloat("Ammo", trooper.Ammo);
            SetBool("HasAmmo", trooper.HasAmmo);
            SetBool("CanAttack", trooper.CanAttack);
            SetBool("TeamOne", trooper.TeamOne);

            // NEW: Calculate distance to nearest health pickup
            HealthVisionSensor healthSensor = Agent.GetSensor<HealthVisionSensor>();
            if (healthSensor != null && healthSensor.Observed.Count > 0) {
                Transform nearestHealth = Position.Nearest(healthSensor.Observed, out float distance);
                SetFloat("HealthPickupDistance", distance);
            }
            else {
                SetFloat("HealthPickupDistance", 9999f);  // No health visible
            }

            // Distance to nearest enemy
            TrooperEnemyVisionSensor enemySensor = Agent.GetSensor<TrooperEnemyVisionSensor>();
            if (enemySensor != null && enemySensor.Observed.Count > 0) {
                Transform nearestEnemy = Position.Nearest(enemySensor.Observed, out float distance);
                SetFloat("EnemyDistance", distance);
                SetBool("EnemyVisible", true);
            }
            else {
                SetFloat("EnemyDistance", 9999f);
                SetBool("EnemyVisible", false);
            }


        }
    }
}
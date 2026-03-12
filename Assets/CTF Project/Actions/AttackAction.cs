using System.Diagnostics.CodeAnalysis;
using KaijuSolutions.Agents.Exercises.CTF;
using KaijuSolutions.Agents.Extensions;
using KaijuSolutions.Agents.Utility;
using UnityEngine;

/// <summary>
/// Action to look at and then attack a component.
/// </summary>
[Icon("Packages/com.kaijusolutions.agents.utility/Editor/Icon.png")]
[HelpURL("https://utility.kaijusolutions.ca")]
[CreateAssetMenu(menuName = "Capture the Flag/Attack", fileName = "Attack Action")]
public class AttackAction : KaijuUtilityAction
{
    /// <summary>
    /// The name of the trooper variable so we can actually fire its weapon.
    /// </summary>
    [Tooltip("The name of the trooper variable so we can actually fire its weapon.")]
    [SerializeField]
    private string trooper;
    
    /// <summary>
    /// The name of the key to aim at to attack.
    /// </summary>
    [Tooltip("The name of the key to aim at to attack.")]
    [SerializeField]
    private string enemy;
    
    /// <summary>
    /// The angle to fire at the enemy when we are within facing them.
    /// </summary>
    [Range(0f, 360f)]
    [Tooltip("The angle to fire at the enemy when we are within facing them.")]
    [SerializeField]
    private float angle = 1f;
    
    /// <summary>
    /// Called when this action is run for the first time.
    /// </summary>
    /// <param name="brain">The <see cref="KaijuUtilityBrain"/> this is for.</param>
    public override void Enter([NotNull] KaijuUtilityBrain brain)
    {
        // Get the trooper.
        Trooper t = brain.Get<Trooper>(trooper);
        if (!t)
        {
            return;
        }
        
        // Get the enemy.
        Trooper e = brain.Get<Trooper>(enemy);
        if (!e)
        {
            // Nothing to look at.
            t.Agent.LookTransform = null;
            return;
        }
        
        // Look at the enemy.
        t.Agent.LookTransform = e.transform;
        
        // If we are facing them, attack.
        if (t.InView((Component)e, angle))
        {
            t.Attack();
        }
    }
}
using System.Diagnostics.CodeAnalysis;
using KaijuSolutions.Agents.Utility;
using UnityEngine;

/// <summary>
/// Action to wander around.
/// </summary>
[Icon("Packages/com.kaijusolutions.agents.utility/Editor/Icon.png")]
[HelpURL("https://utility.kaijusolutions.ca")]
[CreateAssetMenu(menuName = "Capture the Flag/Wander", fileName = "Wander Action")]
public class WanderAction : KaijuUtilityAction
{
    /// <summary>
    /// Called when this action is run for the first time.
    /// </summary>
    /// <param name="brain">The <see cref="KaijuUtilityBrain"/> this is for.</param>
    public override void Enter([NotNull] KaijuUtilityBrain brain)
    {
        brain.Agent.Wander(clear: true);
        brain.Agent.ObstacleAvoidance(clear: false);
        brain.Agent.Separation(clear: false);
    }
}
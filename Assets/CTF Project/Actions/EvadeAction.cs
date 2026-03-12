using System.Diagnostics.CodeAnalysis;
using KaijuSolutions.Agents.Utility;
using UnityEngine;

/// <summary>
/// Action to evade from a component.
/// </summary>
[Icon("Packages/com.kaijusolutions.agents.utility/Editor/Icon.png")]
[HelpURL("https://utility.kaijusolutions.ca")]
[CreateAssetMenu(menuName = "Capture the Flag/Evade", fileName = "Evade Action")]
public class EvadeAction : KaijuUtilityAction
{
    /// <summary>
    /// The name of the key to evade from.
    /// </summary>
    [Tooltip("The name of the key to evade from.")]
    [SerializeField]
    private string target;
    
    /// <summary>
    /// Called when this action is run for the first time.
    /// </summary>
    /// <param name="brain">The <see cref="KaijuUtilityBrain"/> this is for.</param>
    public override void Enter([NotNull] KaijuUtilityBrain brain)
    {
        Component c = brain.Get<Component>(target);
        if (c)
        {
            brain.Agent.Evade(c, clear: true);
        }
    }
}
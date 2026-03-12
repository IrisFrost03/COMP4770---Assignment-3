using System.Diagnostics.CodeAnalysis;
using KaijuSolutions.Agents.Utility;
using UnityEngine;

/// <summary>
/// Action to seek to a component.
/// </summary>
[Icon("Packages/com.kaijusolutions.agents.utility/Editor/Icon.png")]
[HelpURL("https://utility.kaijusolutions.ca")]
[CreateAssetMenu(menuName = "Capture the Flag/Seek", fileName = "Seek Action")]
public class SeekAction : KaijuUtilityAction
{
    /// <summary>
    /// The name of the key to seek to.
    /// </summary>
    [Tooltip("The name of the key to seek to.")]
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
            brain.Agent.Seek(c, clear: true);
        }
    }
}
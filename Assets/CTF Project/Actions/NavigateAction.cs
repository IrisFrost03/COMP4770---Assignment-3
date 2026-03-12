using System.Diagnostics.CodeAnalysis;
using KaijuSolutions.Agents.Utility;
using UnityEngine;

/// <summary>
/// Action to navigate to a component.
/// </summary>
[Icon("Packages/com.kaijusolutions.agents.utility/Editor/Icon.png")]
[HelpURL("https://utility.kaijusolutions.ca")]
[CreateAssetMenu(menuName = "Capture the Flag/Navigate", fileName = "Navigate Action")]
public class NavigateAction : KaijuUtilityAction
{
    /// <summary>
    /// The name of the key to navigate to.
    /// </summary>
    [Tooltip("The name of the key to navigate to.")]
    [SerializeField]
    private string target;
    
    /// <summary>
    /// Called when this action is run for the first time.
    /// </summary>
    /// <param name="brain">The <see cref="KaijuUtilityBrain"/> this is for.</param>
    public override void Enter([NotNull] KaijuUtilityBrain brain)
    {
        try
        {
            Component c = brain.Get<Component>(target);
            if (c)
            {
                brain.Agent.PathFollow(c, clear: true);
            }
            return;
        }
        catch {
            // Ignored.
        }
        
        try
        {
            Vector2 t = brain.Get<Vector2>(target);
            brain.Agent.PathFollow(t, clear: true);
            return;
        }
        catch {
            // Ignored.
        }
        
        try
        {
            Vector3 t = brain.Get<Vector3>(target);
            brain.Agent.PathFollow(t, clear: true);
        }
        catch {
            // Ignored.
        }
    }
}
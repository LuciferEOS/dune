using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._Dune.Shield;

// "just make a separate comp instead of a generic one"
// mfw we are doing bloat
[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class EnergyGlovesComponent : Component
{
    [DataField, AutoNetworkedField]
    public bool IsShieldActive = false;

    [DataField]
    public EntProtoId ToggleAction = "DuneActionToggleShield";

    [DataField, AutoNetworkedField]
    public EntityUid? ToggleActionEntity;
}

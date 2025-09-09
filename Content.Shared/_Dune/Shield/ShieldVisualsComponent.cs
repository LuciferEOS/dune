using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype;

namespace Content.Shared._Dune.Shield;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState(raiseAfterAutoHandleState: true)] // holy fucking shit
public sealed partial class ShieldVisualsComponent : Component
{
    [DataField("prototype", customTypeSerializer:typeof(PrototypeIdSerializer<EntityPrototype>))]
    public string Prototype { get; private set; } = "DuneShieldVisual";

    // the size of shield
    [DataField]
    public float Scale = 1.1f;

    // This field is now properly handled on the client side
    [ViewVariables, AutoNetworkedField]
    public ShieldState State = ShieldState.Off;

    [ViewVariables, AutoNetworkedField]
    public float StateTimer;

    [ViewVariables, AutoNetworkedField]
    public float CurrentOpacity = 0.4f;

    [ViewVariables, AutoNetworkedField]
    public bool WasActiveLastFrame = false;

    [DataField, AutoNetworkedField]
    public Color ActiveColor = new Color(3, 94, 252, 85); // #035efc55

    [DataField, AutoNetworkedField]
    public Color PassiveColor = new Color(3, 94, 252, 40); // #035efc28

    [DataField, AutoNetworkedField]
    public Color WeakColor = new Color(252, 3, 3, 85); // #fc030355

    [DataField, AutoNetworkedField]
    public Color OffColor = new Color(0, 0, 0, 0);
}

public enum ShieldState
{
    Active,
    Passive,
    Weak,
    Off
}

[Serializable, NetSerializable]
public enum ShieldVisualsKeys : byte
{
    State
}

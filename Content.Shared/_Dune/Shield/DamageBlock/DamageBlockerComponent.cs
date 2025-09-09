namespace Content.Shared._Dune.Shield.DamageBlock;

[RegisterComponent]
public sealed partial class DamageBlockerComponent : Component
{
    [DataField]
    public float MaxHealth = 15f;

    [DataField]
    public float RegenRate = 1f;

    // if the health drops below zero, it would take 10 seconds for shield to start working again
    [DataField]
    public float RechargeTime = 10f;

    // below this, if the entity has ShieldVisualsComponent, will trigger the weak state
    [DataField]
    public float WeakThreshold = 5f;

    [ViewVariables]
    public float CurrentHealth;

    [ViewVariables]
    public bool IsBroken = false;

    [ViewVariables]
    public float RechargeTimer;
}

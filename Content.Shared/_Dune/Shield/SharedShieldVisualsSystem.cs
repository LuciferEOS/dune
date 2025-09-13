using Content.Shared._Dune.Shield.DamageBlock;
using Content.Shared.Actions;
using Content.Shared.Clothing;

namespace Content.Shared._Dune.Shield;

public sealed class DuneShieldSystem : EntitySystem
{
    [Dependency] private readonly SharedActionsSystem _actions = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<EnergyGlovesComponent, MapInitEvent>(OnMapInit);
        SubscribeLocalEvent<EnergyGlovesComponent, ComponentRemove>(OnComponentRemove);
        SubscribeLocalEvent<EnergyGlovesComponent, ClothingGotEquippedEvent>(OnEquipped);
        SubscribeLocalEvent<EnergyGlovesComponent, ClothingGotUnequippedEvent>(OnUnequipped);
        SubscribeLocalEvent<EnergyGlovesComponent, DuneToggleShieldActionEvent>(OnToggleShield);
    }

    private void OnMapInit(EntityUid uid, EnergyGlovesComponent component, MapInitEvent args)
    {
        component.ToggleActionEntity = Spawn(component.ToggleAction);
        Dirty(uid, component);
    }

    private void OnComponentRemove(EntityUid uid, EnergyGlovesComponent component, ComponentRemove args)
    {
        if (component.ToggleActionEntity != null)
            Del(component.ToggleActionEntity);
    }

    private void OnEquipped(EntityUid uid, EnergyGlovesComponent component, ClothingGotEquippedEvent args)
    {
        if (component.ToggleActionEntity is not { } action)
            return;

        _actions.AddAction(args.Wearer, action, uid);
    }

    private void OnUnequipped(EntityUid uid, EnergyGlovesComponent component, ClothingGotUnequippedEvent args)
    {
        if (component.ToggleActionEntity is not { } action)
            return;

        _actions.RemoveAction(args.Wearer, action);

        if (component.IsShieldActive)
            ToggleShield(uid, args.Wearer, component);
    }

    private void OnToggleShield(EntityUid uid, EnergyGlovesComponent component, DuneToggleShieldActionEvent args)
    {
        if (args.Handled)
            return;

        ToggleShield(uid, args.Performer, component);
        args.Handled = true;
    }

    private void ToggleShield(EntityUid uid, EntityUid wearer, EnergyGlovesComponent component)
    {
        component.IsShieldActive = !component.IsShieldActive;
        Dirty(uid, component);

        if (component.IsShieldActive)
        {
            var visuals = EnsureComp<ShieldVisualsComponent>(wearer);
            var blocker = EnsureComp<DamageBlockerComponent>(wearer);
            Dirty(wearer, visuals);
            Dirty(wearer, blocker);
        }
        else
        {
            RemComp<ShieldVisualsComponent>(wearer);
            RemComp<DamageBlockerComponent>(wearer);
        }
    }
}

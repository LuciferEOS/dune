using Content.Shared.Damage;

namespace Content.Shared._Dune.Shield.DamageBlock;

public sealed class DamageBlockerSystem : EntitySystem
{ // there is a bug that makes the shield grow to the infinity
    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<DamageBlockerComponent, DamageModifyEvent>(OnDamageModify);
    }

    private void OnDamageModify(EntityUid uid, DamageBlockerComponent comp, ref DamageModifyEvent args)
    {
        if (comp.IsBroken)
            return;

        var totalDamage = args.Damage.GetTotal();
        // i hate this
        var damageToBlock = Math.Min(comp.CurrentHealth, (float)totalDamage);
        comp.CurrentHealth -= damageToBlock;

        var newDamage = new DamageSpecifier();
        foreach (var (type, value) in args.Damage.DamageDict)
        {
            if (value != null)
            {
                var reducedValue = value * ((totalDamage - damageToBlock) / totalDamage);
                newDamage.DamageDict.Add(type, reducedValue);
            }
        }
        args.Damage = newDamage;

        if (comp.CurrentHealth <= 0)
        {
            comp.IsBroken = true;
            comp.RechargeTimer = comp.RechargeTime;

            if (TryComp<ShieldVisualsComponent>(uid, out var shieldVisuals))
            {
                shieldVisuals.State = ShieldState.Off;
            }
        }
        else
        {
            UpdateShieldState(uid, comp);
        }
    }

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        var query = EntityQueryEnumerator<DamageBlockerComponent>();
        while (query.MoveNext(out var uid, out var comp))
        {
            if (comp.IsBroken)
            {
                comp.RechargeTimer -= frameTime;
                if (comp.RechargeTimer <= 0)
                {
                    comp.IsBroken = false;
                    comp.CurrentHealth = 1f;
                    UpdateShieldState(uid, comp);
                }
            }
            else if (comp.CurrentHealth < comp.MaxHealth)
            {
                comp.CurrentHealth += comp.RegenRate * frameTime;
                comp.CurrentHealth = Math.Min(comp.CurrentHealth, comp.MaxHealth);

                UpdateShieldState(uid, comp);
            }
        }
    }

    private void UpdateShieldState(EntityUid uid, DamageBlockerComponent comp)
    {
        if (!TryComp<ShieldVisualsComponent>(uid, out var shield))
            return;

        var newState = ShieldState.Off;

        if (!comp.IsBroken)
        {
            newState = comp.CurrentHealth < comp.WeakThreshold
                ? ShieldState.Weak
                : ShieldState.Active;
        }

        if (shield.State != newState)
        {
            shield.State = newState;
            shield.StateTimer = 0f;
            Dirty(uid, shield);
        }
    }
}

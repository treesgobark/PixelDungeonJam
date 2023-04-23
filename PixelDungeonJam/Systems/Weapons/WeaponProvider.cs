using PixelDungeonJam.Entities;
using System;
using System.Collections.Generic;

namespace PixelDungeonJam.Systems.Weapons;

public static class WeaponProvider
{
    private static Dictionary<string, IWeapon> _weapons = new();

    public static void LoadAllWeapons()
    {
        _weapons.Clear();

        _weapons[Unarmed.Id] = new Unarmed();

        foreach (var sword in Player.SwordDesign.Values)
        {
            _weapons[sword.Id] = new MeleeWeapon(sword);
        }
    }

    public static IWeapon Get(string weaponId)
    {
        if (_weapons.ContainsKey(weaponId))
        {
            return _weapons[weaponId];
        }

        if (Player.SwordDesign.ContainsKey(weaponId))
        {
            var sword = Player.SwordDesign[weaponId];
;           return _weapons[weaponId] = new MeleeWeapon(sword);
        }

        throw new InvalidOperationException($"Can't find weapon id {weaponId}. Available Weapon Ids: {string.Join(", ", _weapons.Keys)}");
    }
    
}

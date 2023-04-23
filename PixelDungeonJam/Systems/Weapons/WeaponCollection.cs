using ANLG.Utilities.FlatRedBall.NonStaticUtilities;

namespace PixelDungeonJam.Systems.Weapons;

public class WeaponCollection : CyclableList<IWeapon>
{
    public IWeapon LoadWeapon(string id)
    {
        var weapon = WeaponProvider.Load(id);
        Add(weapon);
        if (Count == 1)
        {
            SetCurrentItem(0);
        }
        return weapon;
    }

    public void ReloadAll()
    {
        for (int i = 0; i < Count; i++)
        {
            string id = this[i].Id;
            this[i] = WeaponProvider.ForceLoad(id);
        }
    }
}

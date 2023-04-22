using FlatRedBall.Forms.MVVM;
using PixelDungeonJam.Models.Design;

namespace PixelDungeonJam.Models.View;

public class WeaponView : ViewModel
{
    public string Id { get => Get<string>(); set => Set(value); }
    public string Name { get => Get<string>(); set => Set(value); }
    public string Description { get => Get<string>(); set => Set(value); }

    public void SetFrom(IWeaponModel other)
    {
        Id = other.Id;
        Name = other.Name;
        Description = other.Description;
    }
}

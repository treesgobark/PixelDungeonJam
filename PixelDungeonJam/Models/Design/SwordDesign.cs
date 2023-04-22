using PixelDungeonJam.Models.Design;

namespace PixelDungeonJam.DataTypes;

public partial class SwordDesign : IWeaponModel
{
    string IWeaponModel.Id => Id;
    string IWeaponModel.Name => Name;
    string IWeaponModel.Description => Description;
}

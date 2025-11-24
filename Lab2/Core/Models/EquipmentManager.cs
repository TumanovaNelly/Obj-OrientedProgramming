using System.Collections.ObjectModel;
using Lab2.Core.Enums;
using Lab2.Core.Interfaces;

namespace Lab2.Core.Models;

public class EquipmentManager : IEquipmentManager
{
    public IReadOnlyDictionary<EquipmentSlot, IEquippableItem?> Equipped => 
        new ReadOnlyDictionary<EquipmentSlot, IEquippableItem?>(_equipped);
    
    private readonly Dictionary<EquipmentSlot, IEquippableItem?> _equipped = Enum.GetValues<EquipmentSlot>()
        .ToDictionary(slot => slot, IEquippableItem? (_) => null);
    
    public IEquippableItem? Equip(EquipmentSlot slot, IEquippableItem item)
    {
        var oldItem = UnEquip(slot);
        _equipped[slot] = item;
        return oldItem;
    }

    public IEquippableItem? UnEquip(EquipmentSlot slot)
    {
        var oldItem = _equipped[slot];
        _equipped[slot] = null;
        return oldItem;
    }

    public IEquippableItem? Get(EquipmentSlot slot) => _equipped[slot];
}
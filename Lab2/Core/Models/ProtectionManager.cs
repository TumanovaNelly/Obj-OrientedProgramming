using System.Collections.ObjectModel;
using Lab2.Core.Enums;
using Lab2.Core.Interfaces;

namespace Lab2.Core.Models;

public class ProtectionManager : IProtectionManager
{
    public int TotalProtection => _equipped.Values.Sum(protection => protection?.Protect ?? 0);

    
    private readonly Dictionary<ProtectionSlot, IProtection?> _equipped = Enum.GetValues<ProtectionSlot>()
        .ToDictionary(slot => slot, IProtection? (_) => null);
    
    public void EquipProtection(IProtection protection, out IProtection? oldProtection)
    {
        oldProtection = _equipped[protection.EquipSlot];
        _equipped[protection.EquipSlot] = protection;
    }

    public void UnEquipProtection(ProtectionSlot slot, out IProtection? oldProtection)
    {
        oldProtection = _equipped[slot];
        _equipped[slot] = null;
    }
}
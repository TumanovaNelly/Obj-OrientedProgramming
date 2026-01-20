using Lab2.Core.Enums;

namespace Lab2.Core.Interfaces;

public interface IProtectionManager
{
    public int TotalProtection { get; }
    
    public void EquipProtection(IProtection protection, out IProtection? oldProtection);
    public void UnEquipProtection(ProtectionSlot slot, out IProtection? oldProtection);
}
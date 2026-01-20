using Lab2.Core.Enums;
using Lab2.Core.Interfaces;

namespace Lab2.Core.Models;

public class Shield(string name, int protect) : AProtection(name, ProtectionSlot.OffHand, protect);
public class Helmet(string name, int protect) : AProtection(name, ProtectionSlot.Head, protect);
public class Armor(string name, int protect) : AProtection(name, ProtectionSlot.Body, protect);
public class Pants(string name, int protect) : AProtection(name, ProtectionSlot.Legs, protect);
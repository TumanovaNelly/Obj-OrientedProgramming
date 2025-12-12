using Lab2.Core.Models;

namespace Lab2.Core.Interfaces;

public interface IUsableByPlayer
{
    public bool UseByPlayer(ICharacter character, out IItem? droppedItem);
}
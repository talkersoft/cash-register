using cashregister.Models;

namespace cashregister.Machine.Chamber
{
    public interface ICoinChamber
    {
        ICoin Coin { get; set; }

        int Units { get; set; }

        decimal TotalValue { get; }

        void DispenseChange(ChangeDue changeTray);

        void DispenseChangeRandom(ChangeDue changeTray);
    }
}

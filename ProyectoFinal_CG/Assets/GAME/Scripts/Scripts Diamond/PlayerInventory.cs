using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int NumberOfDiamonds { get; private set; }

    public void DiamondCollected()
    {
        NumberOfDiamonds++;
    }

}

using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DelegateEventData", menuName = "ScriptableObjects/Delegate/DelegateEventScriptableObject", order = 1)]

public class DelegateEventScriptableObject : ScriptableObject
{
    public delegate void TransferPlayer(PlayerMove Player);
    public TransferPlayer player_transfer;

    public delegate void AddNum(int Num);
    public AddNum Add_Count;
}

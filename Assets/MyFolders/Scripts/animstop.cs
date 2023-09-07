using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animstop : MonoBehaviour
{

    public void endAnimation()
    {
        Gameplay.instance.skipAnimation();

    }
    
}

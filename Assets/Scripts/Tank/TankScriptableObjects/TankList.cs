using System.Collections.Generic;
using UnityEngine;

namespace TankGame.Tanks
{
    [CreateAssetMenu(fileName = "TankList", menuName = "ScriptableObject/TankList")]
    public class TankList : ScriptableObject
    {
        public List<TankScriptableObject> tankSOList;
    }
}

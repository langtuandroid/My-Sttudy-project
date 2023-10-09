using System;
using System.Collections.Generic;
using UnityEngine;

namespace Programming_Study
{
    public abstract class Ability : MonoBehaviour
    {
        public abstract string Name { get; }
        public abstract void Process();
    }

    public class StartFireAbility : Ability
    {
        public override string Name => "Fire";

        public override void Process()
        {
            Debug.Log("In Complete Start Fire Ability");
        }
    }

    public class StartReloadAbility : Ability
    {
        public override string Name => "Reload";

        public override void Process()
        {
            Debug.Log("In Complete Start Reload Ability");
        }
    }

    public class AbilityFactory
    {
        private Dictionary<string, Type> abilitiesByName;

        public AbilityFactory()
        {
            
        }
        
    }
}
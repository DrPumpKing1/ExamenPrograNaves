using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Features
{
    public class AttackLink : Link //Other Link channels
    {

        //States
        //Properties

        public AttackLink(Controller actor, Controller reactor, Settings settings) : base(actor, reactor, settings)
		{
            //Link Features
            CombatEntity attacker = actor as CombatEntity;

            reactor.CallFeature<Life>(new Setting("attackDamage", attacker != null ? attacker.attack : 0f, Setting.ValueType.Float));

            Unlink();
        }
    }
}
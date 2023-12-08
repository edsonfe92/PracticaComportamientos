using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.SistemasUtilidad.Core;
using Scripts.SistemasUtilidad.UtilityAI;
using UnityEngine.AI;

namespace Scripts.SistemasUtilidad.UtilityAI.Actions
{
    public class Recargar : Action
    {
        public override void Execute(NPCController npc)
        {
            //npc.Reload(6-bulletsLeft);
            Debug.Log("Reload");
        }
    }
}
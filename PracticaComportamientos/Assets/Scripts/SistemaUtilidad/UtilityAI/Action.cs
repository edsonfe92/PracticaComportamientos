using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.SistemasUtilidad.Core;

namespace Scripts.SistemasUtilidad
{
    public abstract class Action : ScriptableObject
    {
        public string Name;
        private float _score;
        public float score
        {
            get { return _score; }
            set
            {
                this._score = Mathf.Clamp01(value);
            }
        }

        public Considerations[] considerations;

        public virtual void Awake()
        {
            score = 0;
        }

        public abstract void Execute(NPCController npc);
    }
}

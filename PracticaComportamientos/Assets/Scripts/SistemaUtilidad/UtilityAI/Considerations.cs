using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.SistemasUtilidad
{
    public abstract class Considerations : ScriptableObject
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

        public virtual void Awake()
        {
            score = 0;
        }

        public abstract float ScoreConsideration();
    }
}

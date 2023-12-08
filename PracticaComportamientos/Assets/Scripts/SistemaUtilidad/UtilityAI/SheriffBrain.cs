using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.SistemasUtilidad
{
    public class SheriffBrain : MonoBehaviour
    {

        public Action bestAction { get; set; }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        //Bucle para buscar la mejor acción (la de mayor urgencia/score) entre la lista de acciones disponibles
        public void DecideBestAction(Action[] actionsAvailable)
        {
            float score = 0f;
            int nextBestActionIndex = 0;
            for (int i = 0; i < actionsAvailable.Length; i++)
            {
                if (ScoreAction(actionsAvailable[i]) > score)       //compara las urgencias y guarda el score para compararlo con el resto y encontrar el más alto
                {
                    nextBestActionIndex = i;
                    score = actionsAvailable[i].score;
                }
            }

            bestAction = actionsAvailable[nextBestActionIndex];
        }

        public float ScoreAction(Action action)
        {
            float score = 1f;
            for (int i =0; i < action.considerations.Length; i++)
            {
                float considerationScore = action.considerations[i].ScoreConsideration();
                score *= considerationScore;

                if (score == 0)
                {
                    action.score = 0;
                    return action.score;
                }
            }

            //Reescalado para mantener el score entre 0 y 1 (Average scheme from Dave Mark)
            float originalScore = score;
            float modFactor = 1 - (1 / action.considerations.Length);
            float makeupValue = (1 - originalScore) * modFactor;
            action.score = originalScore + (makeupValue * originalScore); //Mantiene el mismo significado

            return action.score;
        }
    }
}

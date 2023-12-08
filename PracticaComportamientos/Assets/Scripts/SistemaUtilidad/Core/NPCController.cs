using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.SistemasUtilidad.Core
{
    public class NPCController : MonoBehaviour
    {
        public MoveController mover { get; set; }
        public SheriffBrain aiBrain { get; set; }
        public Action[] actionsAvailable;

        // Start is called before the first frame update
        void Start()
        {
            mover = GetComponent<MoveController>();
            aiBrain = GetComponent<SheriffBrain>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        #region Corutinas
        public void Reload(int time)
        {
            StartCoroutine(ReloadCoroutine(time));
        }

        IEnumerator ReloadCoroutine(int time)
        {
            int counter = time;
            while (counter > 0)
            {
                yield return new WaitForSeconds(1);
                counter--;
            }

            Debug.Log("I just reloaded");
            //Implementar logica
        }
        #endregion
    }
}
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Test.GameController
{
    public class BlocksController : MonoBehaviour
    {
        public List<BlockObject> _blocks;
        public delegate void StartNewSession();
        public static event StartNewSession OnStartNewSession;

        public void OnEnable()
        {
            _blocks = GetComponentsInChildren<BlockObject>().ToList();
            int c = _blocks.Count;
            for (int i = 0; i < c; i++)
            {
                _blocks[i].OnDestroy += CountDestroy;
            }
        }

        private void CountDestroy()
        {
            int c = _blocks.Count;
            for (int i = 0; i < c; i++)
            {
                if ((_blocks[i] as MonoBehaviour).gameObject.activeInHierarchy)
                {
                    return;
                }
            }
            RestartGame();
        }

        private void RestartGame()
        {
            OnStartNewSession();
            int c = _blocks.Count;
            for (int i = 0; i < c; i++)
            {
                (_blocks[i] as MonoBehaviour).gameObject.SetActive(true);
            }
        }
    }
}
using UnityEngine;

namespace BlackIceFight
{
    public class PauseBehaviour : MonoBehaviour
    {
        protected bool _pause = true;
        
        public void UnPause()
        {
            _pause = false;
        }
    }
}
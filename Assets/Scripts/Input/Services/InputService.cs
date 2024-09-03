using Input.Interfaces;
using UnityEngine;


namespace Input.Services
{
    public class InputService 
    {
        public IInput CurrentInput { get; private set; }


        public InputService()
        {
            SetInput();
        }


        private void SetInput()
        {
            if (Application.isEditor)
            {
                CurrentInput = new PcInput();
            }
            else if (Application.isMobilePlatform)
            {
                CurrentInput = new MobileInput();
            }
        
        
        }
    }
}
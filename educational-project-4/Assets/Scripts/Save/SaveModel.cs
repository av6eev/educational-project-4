using System;

namespace Save
{
    public class SaveModel
    {
        public event Action OnSave; 

        public void Save()
        {
            OnSave?.Invoke();
        }
    }
}
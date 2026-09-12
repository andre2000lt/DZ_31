using System.Collections.Generic;

namespace _MiniGame
{
    public class ControllersUpdateService
    {
        private Dictionary<Character, Controller> _controllers = new();

        public void Reset()
        {
            foreach (KeyValuePair<Character, Controller> controller in _controllers)
                controller.Value.Disable();

            _controllers.Clear();
        }

        public void Add(Character character, Controller controller)
        {
            _controllers.Add(character, controller);
        }

        public void Disable(Character character)
        {
            _controllers[character].Disable();
        }

        public void Update(float deltaTime)
        {
            foreach (KeyValuePair<Character, Controller> controller in _controllers)
                controller.Value.Update(deltaTime);
        }
    }
}
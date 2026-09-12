using UnityEngine;

public abstract class Controller 
{
    private bool _isEnabled;
    
    public void Enable() => _isEnabled = true;
    
    public void Disable() => _isEnabled = false;
    
    public void Update(float deltaTime)
    {
        if (_isEnabled == false) return;
        
        UpdateLogic(deltaTime);
    }

    protected abstract void UpdateLogic(float deltaTime);
}

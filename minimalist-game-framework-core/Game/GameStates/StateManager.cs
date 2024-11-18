using System;
using System.Collections.Generic;
using System.Text;

class StateManager
{
    private Stack<IGameState> _gameStates;

    private IGameState _currentState;

    public StateManager()
    {
        _gameStates = new Stack<IGameState>();
    }

    // add a new state
    public void PushState(IGameState state)
    {
        _currentState = state;
        _gameStates.Push(state);
        state.Enter(this);
    }

    // remove current state
    public void PopState()
    {
        _gameStates.Pop();
        _currentState = _gameStates.Peek();
        _currentState.Enter(this);
    }

    public void PopStateNoEnter()
    {
        _gameStates.Pop();
        _currentState = _gameStates.Peek();
    }

    public ActiveLevel GetCurrentState()
    {
        if(_currentState is ActiveLevel)
        {
            return (ActiveLevel)_currentState;
        }
        return null;
    }

    public void SetBaseState(IGameState state)
    {
        _gameStates.Clear();
        PushState(state);
    }

    public void Update()
    {
        if (_currentState != null)
        {
            _currentState.Update(this);
            _currentState.Draw();
        }
    }
}


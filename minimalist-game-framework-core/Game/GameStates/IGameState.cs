using System;
using System.Collections.Generic;
using System.Text;

interface IGameState
{
    void Update(StateManager manager);
    void Draw();
    void Enter(StateManager manager);
}

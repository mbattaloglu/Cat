using Cat.Abstracts.Inputs;
using Cat.InputActions;
using UnityEngine;

namespace Cat.Concreates.Inputs
{
    public class ComputerInput : IPlayerInput
    {
        private PlayerInput playerInput;
        public PlayerInput.PlayerActions playerInputActions {get; private set;}

        public ComputerInput()
        {
            playerInput = new PlayerInput();
            playerInputActions = playerInput.Player;

            playerInputActions.Enable();
        }

        public float Horizontal => playerInputActions.Move.ReadValue<Vector2>().x;
        public float Vertical => playerInputActions.Move.ReadValue<Vector2>().y;
    }
}


namespace TankGame.Tanks.PlayerServices
{
    public class PlayerModel
    {
        private PlayerController playerController;

        private TankScriptableObject playerObject;

        public PlayerModel(TankScriptableObject tankScriptableObject)
        {
            this.playerObject = tankScriptableObject;
        }

        public TankScriptableObject GetTankObject()
        {
            return playerObject;
        }
        public void SetTankController(PlayerController _controller)
        {
            playerController = _controller;
        }
    }
}

using Game.Misc;

namespace Game.Model
{
    public interface IModelPacMan
    {
        IEventManager EventManager { get; }

        void Init();
        void Update(eDirection direction);
        void InitGhostA();
        void UpdateGhostA();
        void InitGhostB();
        void UpdateGhostB();
    }
     

    // ##################################################

    public partial class ModelPacMan : ModelBase, IModelPacMan
    {

        eDirection _eDirectionGhostA;
        eDirection _eDirectionGhostB;

        protected override void RegisterEvents(IEventManagerInternal eventManager)
        {
            eventManager.Register<IPacManEvents, IPacManEventsWritable>(new PacManEvents());
        }

        // ============== IModelPacMan =================

        IEventManager IModelPacMan.EventManager => EventManager;


        void IModelPacMan.Init()
        {
            CreateAndExecuteTurn(
                (ITurn turn) =>
                {
                    turn.Push(new CmdCreatePacMan(0, 0));
                });
        }

        void IModelPacMan.Update(eDirection direction)
        {
            CreateAndExecuteTurn(
                (ITurn turn) =>
                {
                    turn.Push(new CmdMovePacMan(direction));
                });
        }

        void IModelPacMan.InitGhostA()
        {
            CreateAndExecuteTurn(
                (ITurn turn) =>
                {
                    ICommand cmdCreateGhostA = new CmdCreateGhostA(15, 11);
                    turn.Push(cmdCreateGhostA);
                });
        }

        void IModelPacMan.UpdateGhostA()
        {
            _eDirectionGhostA = eDirection.DOWN;
            CreateAndExecuteTurn(
                (ITurn turn) =>
                {
                    CmdMoveGhostA cmdMoveGhostA = new CmdMoveGhostA(_eDirectionGhostA);
                    turn.Push(cmdMoveGhostA);
                });
        }
        void IModelPacMan.InitGhostB()
        {
            CreateAndExecuteTurn(
                (ITurn turn) =>
                {
                    ICommand cmdCreateGhostB = new CmdCreateGhostB(8, 7);
                    turn.Push(cmdCreateGhostB);
                });
        }

        void IModelPacMan.UpdateGhostB()
        {
            CreateAndExecuteTurn(
                (ITurn turn) =>
                {
                    CmdMoveGhostB cmdMoveGhostB = new CmdMoveGhostB(_eDirectionGhostB);
                    _eDirectionGhostB = cmdMoveGhostB.getDirection(_eDirectionGhostB, _context);
                    turn.Push(cmdMoveGhostB);
                });
        } 
    }
}
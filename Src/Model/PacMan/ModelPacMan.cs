using Game.Misc;
using UnityEngine;
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
        eDirection _eDirectionGhostB_last;
        eDirection _eDirectionGhostB_current;
        int direction_counter = 0;
        int DIRECTION_MAX = 7;

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
                    CmdMoveGhostB cmdMoveGhostB = new CmdMoveGhostB(_eDirectionGhostB_current);
                    _eDirectionGhostB_last = _eDirectionGhostB_current;
                    _eDirectionGhostB_current = cmdMoveGhostB.getDirection(_eDirectionGhostB_current, _context);
                    if(_eDirectionGhostB_current == _eDirectionGhostB_last)
                    {
                        direction_counter++;
                        //Debug.Log("A  direction_counter = " + direction_counter );
                    } else
                    {
                        direction_counter = 0;
                        //Debug.Log("X  last = " + _eDirectionGhostB_last + ", current = " + _eDirectionGhostB_current);
                    }
                    if (direction_counter == DIRECTION_MAX)
                    {
                        while(_eDirectionGhostB_last == _eDirectionGhostB_current)
                        {
                            Debug.Log("1  last = " + _eDirectionGhostB_last + ", current = " + _eDirectionGhostB_current);
                            cmdMoveGhostB.ChangeDirection();
                            _eDirectionGhostB_current = cmdMoveGhostB.getDirection(_eDirectionGhostB_current, _context);
                            Debug.Log("2  last = " + _eDirectionGhostB_last + ", current = " + _eDirectionGhostB_current);

                        }

                        direction_counter = 0;
                    }
                    turn.Push(cmdMoveGhostB);
                });
        } 
    }
}
using Game.Misc;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Model
{
    public partial class ModelPacMan
    {
        class CmdMoveGhostA : ICommand
        {
            eDirection _direction;

            // ========================================

            public CmdMoveGhostA(eDirection direction) {               
                _direction = direction;
            }

            // ============== ICommand ================

            void ICommand.Exec(IContextWritable context)
            {
                IGhostAWritable ghostA = context.CharactardsContainer.Get<IGhostAWritable>();
                IPacManWritable pacman = context.CharactardsContainer.Get<IPacManWritable>();
                 
                bool isCanMove = context.Field.IsCanMove(ghostA.X, ghostA.Y, _direction);

                if (isCanMove)
                {
                    (int x, int y) nextPositon = Direction.GetNextPosition(ghostA.X, ghostA.Y, _direction);
                    ghostA.UpdatePositionA(nextPositon.x, nextPositon.y);
                    context.EventManager.Get<IPacManEventsWritable>().UpdateGhostAPosition(nextPositon.x, nextPositon.y);
                }

                //bool isCanMove = false;

                //while (!isCanMove)
                //{
                //    _direction = Direction.GetNextDirection(pacman.X, pacman.Y, ghostA.X, ghostA.Y);
                //    isCanMove = context.Field.IsCanMove(ghostA.X, ghostA.Y, _direction);
                //}  

                //(int x, int y) nextPositon = Direction.GetNextPosition(ghostA.X, ghostA.Y, _direction);
                //ghostA.UpdatePositionA(nextPositon.x, nextPositon.y);
                //context.EventManager.Get<IPacManEventsWritable>().UpdateGhostAPosition(nextPositon.x, nextPositon.y);
            }
        }
    }
}
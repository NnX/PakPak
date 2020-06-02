using System.Collections;
using UnityEngine;
using Game.Model;
using Game.Misc;

namespace Game
{ 
    public class GameMediator : MonoBehaviour
    {
        const float ITERATION_TIME = 0.5f;
        eDirection current_direction = eDirection.RIGHT;
        [SerializeField]
        View.VisualManager _visualManager;

        IModelPacMan _model = new ModelPacMan();

        // ====================================

        View.IVisualManager VisualManager => _visualManager;

        // ====================================

        IEnumerator Start()
        {
            VisualManager.Init(_model.EventManager, ITERATION_TIME);

            _model.Init();
            _model.InitGhostA();
            _model.InitGhostB();
            while (true)
            {
                _model.Update(current_direction);
                _model.UpdateGhostA();
                _model.UpdateGhostB();
                yield return new WaitForSeconds(ITERATION_TIME);
 
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                current_direction = eDirection.UP;
                _visualManager.RotatePacMan(90);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                current_direction = eDirection.DOWN;
                _visualManager.RotatePacMan(270);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                current_direction = eDirection.RIGHT;
                _visualManager.RotatePacMan(0);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                current_direction = eDirection.LEFT;
                _visualManager.RotatePacMan(180);
            }
        }
    }
}

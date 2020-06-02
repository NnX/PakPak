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
            bool isRight = (Input.GetAxis("DPadX") < -0.1f) ? true : false;
            bool isLeft = (Input.GetAxis("DPadX") > 0.1f) ? true : false;
            bool isDown = (Input.GetAxis("DPadY") < -0.1f) ? true : false;
            bool isUp = (Input.GetAxis("DPadY") > 0.1f) ? true : false;

            if (Input.GetKeyDown(KeyCode.UpArrow) || isUp)
            {
                current_direction = eDirection.UP;
                _visualManager.RotatePacMan(90);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) || isDown)
            {
                current_direction = eDirection.DOWN;
                _visualManager.RotatePacMan(270);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) || isRight)
            {
                current_direction = eDirection.RIGHT;
                _visualManager.RotatePacMan(0);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) || isLeft)
            {
                current_direction = eDirection.LEFT;
                _visualManager.RotatePacMan(180);
            }
        }
    }
}

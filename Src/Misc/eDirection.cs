namespace Game.Misc
{
    public enum eDirection
    {
        LEFT,
        RIGHT,
        UP,
        DOWN,
    }

    public enum pacmanPosition
    {
        UpUp,
        UpRight,
        RightRight,
        RightDown,
        DownDown,
        DownLeft,
        LeftLeft,
        LeftUp,
    }

    public static class Direction
    {
        public static (int x, int y)GetNextPosition(int x, int y, eDirection direction)
        {
            switch (direction)
            {
                
                case eDirection.LEFT:
                    return (x - 1, y);
                case eDirection.RIGHT:
                    return (x + 1, y);
                case eDirection.UP:
                    return (x, y + 1);
                default:
                //case eDirection.DOWN:
                    return (x, y - 1);
            }
        }

        public static eDirection GetNextDirection(int pacman_x, int pacman_y, int ghost_x, int ghost_y)
        {
            eDirection direction = eDirection.DOWN;
            if(pacman_x == ghost_x) // move up or down if possible
            {

            } else if (pacman_x > ghost_x)    //move right
            {
             

            } else if (pacman_x < ghost_x) // left
            {
                
            }


            if (pacman_y == ghost_y) // move left or right if possible
            {

            } else if (pacman_y > ghost_y)    //move up
            {
             

            } else if (pacman_y < ghost_y) // down
            {
                
            }



            return direction;
        }
    }
}
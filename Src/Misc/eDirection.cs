namespace Game.Misc
{
    public enum eDirection
    {
        LEFT,
        RIGHT,
        UP,
        DOWN,
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
    }
}
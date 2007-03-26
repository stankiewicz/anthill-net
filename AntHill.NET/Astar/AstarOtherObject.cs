using System;
using System.Collections.Generic;
using System.Text;

namespace AntHill.NET
{
    class AstarOtherObject : IAstar
    {

        #region IAstar Members

        int IAstar.GetWeight(int x, int y)
        {
            bool rain = false;
            if (Simulation.simulation.rain != null)
            {
                rain = Simulation.simulation.rain.isRainingAt(x, y);
            }
            switch (Simulation.simulation.GetMap().GetTile(x, y).TileType)
            {
                case TileType.Wall:
                    return int.MaxValue;
                case TileType.Outdoor:
                    return rain ? 1 : int.MaxValue;
                case TileType.Indoor:
                    return 1;
                default:
                    break;
            }
            return 0;

        }

        #endregion
    }
}

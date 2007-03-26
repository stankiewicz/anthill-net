using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace AntHill.NET
{
    
    [TestFixture]
    public class TestClass
    {
        public TestClass()
        {


        }

        [Test]
        
        public void TestFun()
        {
            Tile[,] test_tiles =
            {{new Tile(TileType.Indoor), new Tile(TileType.Outdoor), new Tile(TileType.Wall)},
            {new Tile(TileType.Wall), new Tile(TileType.Indoor), new Tile(TileType.Outdoor)},
            {new Tile(TileType.Outdoor), new Tile(TileType.Indoor), new Tile(TileType.Wall)},
            {new Tile(TileType.Indoor), new Tile(TileType.Wall), new Tile(TileType.Outdoor)}};

            Map test_map = new Map(3, 4, test_tiles);
        
            Assert.AreSame(3, test_map.Width);
            
            Assert.AreSame(1, test_map.Height);
            Assert.AreSame(TileType.Wall, test_map.GetTile(1, 3), "Z³e pole na mapie");
        }
    }
}

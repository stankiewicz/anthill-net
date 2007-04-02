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

            // MAP TEST...

            Tile[,] test_tiles =
            {{new Tile(TileType.Indoor), new Tile(TileType.Outdoor), new Tile(TileType.Wall)},
            {new Tile(TileType.Wall), new Tile(TileType.Indoor), new Tile(TileType.Outdoor)},
            {new Tile(TileType.Outdoor), new Tile(TileType.Indoor), new Tile(TileType.Wall)},
            {new Tile(TileType.Indoor), new Tile(TileType.Wall), new Tile(TileType.Outdoor)}};

            Map test_map = new Map(3, 4, test_tiles);

            Assert.AreEqual(3, test_map.Width, "Bad witdth of map");
            Assert.AreEqual(4, test_map.Height, "Bad height of map");
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 4; j++)
                   Assert.AreEqual(test_tiles[j, i], test_map.GetTile(j, i), "Bad type in tile (" + j.ToString() + "," + i.ToString() + ").");

            // WARRIOR TEST...
            Warrior test_warrior = new Warrior(new System.Drawing.Point(123, 345));
            test_warrior.TurnsToBecomeHungry = 43;
            test_warrior.Direction = Direction.E;
            test_warrior.TurnsWithoutFood = 23;

            Assert.AreEqual(43, test_warrior.TurnsToBecomeHungry, "Warrior.TurnsToBecomeHungry problem");
            Assert.AreEqual(23, test_warrior.TurnsWithoutFood, "Warrior.TurnsWithoutFood problem");
            Assert.AreEqual(Direction.E, test_warrior.Direction, "Warrior.Direction problem");
            Assert.AreEqual(new System.Drawing.Point(123, 345),test_warrior.Position, "Warrior.Position problem");

            // WORKER TEST...
            Worker test_worker = new Worker(new System.Drawing.Point(321, 255));
            test_worker.TurnsToBecomeHungry = 34;
            test_worker.Direction = Direction.W;
            test_worker.TurnsWithoutFood = 35;
            test_worker.FoodQuantity = 17;

            Assert.AreEqual(34, test_worker.TurnsToBecomeHungry, "Worker.TurnsToBecomeHungry problem");
            Assert.AreEqual(35, test_worker.TurnsWithoutFood, "Worker.TurnsWithoutFood problem");
            Assert.AreEqual(17, test_worker.FoodQuantity, "Worker.FoodQuantity problem");
            Assert.AreEqual(Direction.W, test_worker.Direction, "Worker.Direction problem");
            Assert.AreEqual(new System.Drawing.Point(321, 255), test_worker.Position, "Worker.Position problem");

            // RAIN TEST...
            Rain test_rain=new Rain(new System.Drawing.Point(155, 155));
            test_rain.isRainingAt(155, 155);
            Assert.AreEqual(new System.Drawing.Point(155, 155), test_rain.Position, "Rain.Position problem");

        }
    }
}

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
        public void AntTest()
        {

        }
        [Test]
        public void CitizenTest()
        {

        }
        [Test]
        public void CreatureTest()
        {

        }

        [Test]
        public void QueenTest()
        {

        }
        [Test]
        public void SpiderTest()
        {

        }
        [Test]
        public void ElementTest()
        {

        }
        [Test]
        public void FoodTest()
        {

        }
        [Test]
        public void MessageTest()
        {

        }
        [Test]
        public void PointWithIntensityTest()
        {
            Tile test_tile = new Tile(TileType.Wall);
            PointWithIntensity test_pointwithintensity = new PointWithIntensity(test_tile, 23);
            Assert.AreEqual(test_pointwithintensity.Intensity, 23, "PointWithIntensity.Intensity problem");
            Assert.AreEqual(test_pointwithintensity.Tile, test_tile, "PointWithIntensity.Tile problem");
        }

        [Test]
        public void XmlTest()
        {

        }
        

        [Test]
        public void MapTest()
        {
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

        }

        [Test]
        public void TileTest()
        {
            Tile test_tile = new Tile(TileType.Indoor);
            Assert.AreEqual(TileType.Indoor, test_tile.TileType, "Tile.TileType problem");

        }


        [Test]
        public void WarriorTest()
        {

            Warrior test_warrior = new Warrior(new System.Drawing.Point(123, 345));
            test_warrior.TurnsToBecomeHungry = 43;
            test_warrior.Direction = Direction.E;
            test_warrior.TurnsWithoutFood = 23;

            Assert.AreEqual(43, test_warrior.TurnsToBecomeHungry, "Warrior.TurnsToBecomeHungry problem");
            Assert.AreEqual(23, test_warrior.TurnsWithoutFood, "Warrior.TurnsWithoutFood problem");
            Assert.AreEqual(Direction.E, test_warrior.Direction, "Warrior.Direction problem");
            Assert.AreEqual(new System.Drawing.Point(123, 345),test_warrior.Position, "Warrior.Position problem");
        }
        
        [Test]
        public void WorkerTest()
        {
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

        }


        [Test]
        [Ignore("narazie brak zaimplementowanych metod (w klasie Simulation) niezbêdnych do dzia³ania tej klasy")]
        public void RainTest()
        {

            Tile[,] test_tiles =
            {{new Tile(TileType.Indoor), new Tile(TileType.Outdoor), new Tile(TileType.Wall)},
            {new Tile(TileType.Wall), new Tile(TileType.Indoor), new Tile(TileType.Outdoor)},
            {new Tile(TileType.Outdoor), new Tile(TileType.Indoor), new Tile(TileType.Wall)},
            {new Tile(TileType.Indoor), new Tile(TileType.Wall), new Tile(TileType.Outdoor)}};

            Map tmp_map = new Map(3, 4, test_tiles);

            Simulation tmp_isw = new Simulation(tmp_map);

            Rain test_rain=new Rain(new System.Drawing.Point(155, 155));
            test_rain.isRainingAt(155, 155);
            Assert.AreEqual(new System.Drawing.Point(155, 155), test_rain.Position, "Rain.Position problem");
            Assert.IsTrue((test_rain.TimeToLive >= 0) && (test_rain.TimeToLive < AntHillConfig.rainMaxDuration + 1), "Rain.TimeToLive range problem");
            int tmp = test_rain.TimeToLive;

// jak bêd¹ funkcje symulacji to bêdzie dzia³a³o (jak mniemam...)
            test_rain.Maintain((ISimulationWorld)tmp_isw);
            Assert.AreEqual(tmp-1, test_rain.TimeToLive-1,"Rain.Maintain problem");
        }

        [Test]
        public void SimulationTest()
        {
            Tile[,] test_tiles =
            {{new Tile(TileType.Indoor), new Tile(TileType.Outdoor), new Tile(TileType.Wall)},
            {new Tile(TileType.Wall), new Tile(TileType.Indoor), new Tile(TileType.Outdoor)},
            {new Tile(TileType.Outdoor), new Tile(TileType.Indoor), new Tile(TileType.Wall)},
            {new Tile(TileType.Indoor), new Tile(TileType.Wall), new Tile(TileType.Outdoor)}};

            Map test_map = new Map(3, 4, test_tiles);

            Simulation tmp_isw = new Simulation(test_map);
            
            Assert.AreSame(test_map, tmp_isw.GetMap(), "Simulation.GetMap problem");

//tu brakuje doœæ du¿o funkcji i do nich testów ;)

        }



    }
}

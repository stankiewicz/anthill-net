using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Drawing;
using astar;

namespace AntHill.NET
{

    [TestFixture]
    public class TestClass
    {
        public TestClass()
        {

        }

        [Test]
        [Ignore("Ant class is abstract. Ant.TurnsWithoutFood and Ant.TurnsToBecomeHungry test in WorkerTest andWarriorTest...")]
        public void AntTest()
        {

        }

        [Test]
        [Ignore("Citizen class is abstract. NO TEST IN OTHER CLASS (yet?)...")]
        public void CitizenTest()
        {

        }
        [Test]
        [Ignore("Creature class is abstract. Creature.Direction test in WorkerTest and WarriorTest...")]
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
        [Ignore("Element class is abstract. Element.Position test in WorkerTest and WarriorTest...")]
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
            //doda³em Point(3,4) - mo¿e nie dzia³aæ czasem - Kamil
            Tile test_tile = new Tile(TileType.Wall, new Point(3, 4));
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
            /*
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
            */
        }

        [Test]
        public void TileTest()
        {
            Tile test_tile = new Tile(TileType.Indoor, new Point());
            Assert.AreEqual(TileType.Indoor, test_tile.TileType, "Tile.TileType problem");
            
        }


        [Test]
        public void WarriorTest()
        {
            Warrior test_warrior = new Warrior(new System.Drawing.Point(123, 345));
            test_warrior.TurnsToBecomeHungry = 43;
            test_warrior.Direction = Dir.E;
            test_warrior.TurnsWithoutFood = 23;

            Assert.AreEqual(43, test_warrior.TurnsToBecomeHungry, "Warrior.TurnsToBecomeHungry problem");
            Assert.AreEqual(23, test_warrior.TurnsWithoutFood, "Warrior.TurnsWithoutFood problem");
            Assert.AreEqual(Dir.E, test_warrior.Direction, "Warrior.Direction problem");
            Assert.AreEqual(new System.Drawing.Point(123, 345),test_warrior.Position, "Warrior.Position problem");
        }
        
        [Test]
        public void WorkerTest()
        {
            Worker test_worker = new Worker(new System.Drawing.Point(321, 255));
            test_worker.TurnsToBecomeHungry = 34;
            test_worker.Direction = Dir.W;
            test_worker.TurnsWithoutFood = 35;
            test_worker.FoodQuantity = 17;

            Assert.AreEqual(34, test_worker.TurnsToBecomeHungry, "Worker.TurnsToBecomeHungry problem");
            Assert.AreEqual(35, test_worker.TurnsWithoutFood, "Worker.TurnsWithoutFood problem");
            Assert.AreEqual(17, test_worker.FoodQuantity, "Worker.FoodQuantity problem");
            Assert.AreEqual(Dir.W, test_worker.Direction, "Worker.Direction problem");
            Assert.AreEqual(new System.Drawing.Point(321, 255), test_worker.Position, "Worker.Position problem");

        }


        [Test]
        [Ignore("narazie brak zaimplementowanych metod (w klasie Simulation) niezbêdnych do dzia³ania tej klasy")]
        public void RainTest()
        {

            Tile[,] test_tiles =
            {{new Tile(TileType.Indoor, new Point()), new Tile(TileType.Outdoor, new Point()), new Tile(TileType.Wall, new Point())},
            {new Tile(TileType.Wall, new Point()), new Tile(TileType.Indoor, new Point()), new Tile(TileType.Outdoor, new Point())},
            {new Tile(TileType.Outdoor, new Point()), new Tile(TileType.Indoor, new Point()), new Tile(TileType.Wall, new Point())},
            {new Tile(TileType.Indoor, new Point()), new Tile(TileType.Wall, new Point()), new Tile(TileType.Outdoor, new Point())}};

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
            {{new Tile(TileType.Indoor, new Point()), new Tile(TileType.Outdoor, new Point()), new Tile(TileType.Wall, new Point())},
            {new Tile(TileType.Wall, new Point()), new Tile(TileType.Indoor, new Point()), new Tile(TileType.Outdoor, new Point())},
            {new Tile(TileType.Outdoor, new Point()), new Tile(TileType.Indoor, new Point()), new Tile(TileType.Wall, new Point())},
            {new Tile(TileType.Indoor, new Point()), new Tile(TileType.Wall, new Point()), new Tile(TileType.Outdoor, new Point())}};

            Map test_map = new Map(3, 4, test_tiles);

            Simulation tmp_isw = new Simulation(test_map);
            
            Assert.AreSame(test_map, tmp_isw.GetMap(), "Simulation.GetMap problem");

//tu brakuje doœæ du¿o funkcji i do nich testów ;)

        }

        [Test]
        public void AstarTest()
        {

            XmlReaderWriter reader = new XmlReaderWriter();
            reader.ReadMe("..\\..\\Configuration\test-ASTAR-anthill.xml");

            Simulation test_isw = new Simulation(new Map(AntHillConfig.mapColCount, AntHillConfig.mapRowCount, AntHillConfig.tiles));

            Spider test_spider = new Spider(new Point(5, 0));
            Ant test_ant = new Warrior(new Point(5, 10));
            List<KeyValuePair<int, int>> trail = Astar.Search(new KeyValuePair<int, int>(test_spider.Position.X, test_spider.Position.Y), new KeyValuePair<int, int>(test_ant.Position.X, test_ant.Position.Y), new TestAstarObject(test_isw));

//            List<KeyValuePair<int, int>> test_trail = new List<KeyValuePair<int,int>>
//            foreach (KeyValuePair<int, int> key in trail)



        }

        class TestAstarObject: IAstar
        {
            Simulation simm;
            public TestAstarObject(Simulation sim)
            {
                simm = sim;
            }

            #region IAstar Members

            public int GetWeight(int x, int y)
            {
                switch (simm.GetMap().GetTile(x, y).TileType)
                {
                    case TileType.Wall:
                        return int.MaxValue;
                    case TileType.Outdoor:
                        return 1;
                    case TileType.Indoor:
                        return 1;
                    default:
                        break;
                }
                return 0;
            }

            #endregion
        }

//getvisible sth... - narazie nie ma funkcji:(
        [Test]
        public void GetVisibleAnts()
        {

        }

        [Test]
        public void GetVisibleFood()
        {

        }

        [Test]
        public void GetVisibleSpiders()
        {

        }

        [Test]
        public void GetVisibleMessages()
        {

        }



    }
}

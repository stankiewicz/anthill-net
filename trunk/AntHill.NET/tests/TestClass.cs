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
            Spider test_spider = new Spider(new System.Drawing.Point(110, 145));
            test_spider.Health = 10;
            test_spider.Direction = Dir.E;
            Assert.AreEqual(Dir.E, test_spider.Direction, "Spider.Direction problem");
            Assert.AreEqual(10, test_spider.Health, "Spider.Health problem");
            Assert.AreEqual(new System.Drawing.Point(110, 145), test_spider.Position, "spider.Position problem");

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
            //doda�em Point(3,4) - mo�e nie dzia�a� czasem - Kamil
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
        [Ignore("narazie brak zaimplementowanych metod (w klasie Simulation) niezb�dnych do dzia�ania tej klasy")]
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
            test_rain.IsRainOver(155, 155);
            Assert.AreEqual(new System.Drawing.Point(155, 155), test_rain.Position, "Rain.Position problem");
            Assert.IsTrue((test_rain.TimeToLive >= 0) && (test_rain.TimeToLive < AntHillConfig.rainMaxDuration + 1), "Rain.TimeToLive range problem");
            int tmp = test_rain.TimeToLive;

// jak b�d� funkcje symulacji to b�dzie dzia�a�o (jak mniemam...)
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

            Map test_map = new Map(2, 3, test_tiles);

            Simulation tmp_isw = new Simulation(test_map);

            AHGraphics.Init();            

            Assert.AreSame(test_map, tmp_isw.GetMap(), "Simulation.GetMap problem");

//tu brakuje do�� du�o funkcji i do nich test�w ;)

        }

        [Test]
        public void AstarTest()
        {

            XmlReaderWriter reader = new XmlReaderWriter();
            reader.ReadMe("..\\..\\tests\\test-ASTAR-anthill.xml");

            AHGraphics.Init();
            Astar.Init(AntHillConfig.mapColCount, AntHillConfig.mapRowCount);

            Simulation test_isw = new Simulation(new Map(AntHillConfig.mapColCount, AntHillConfig.mapRowCount, AntHillConfig.tiles));

            Spider test_spider = new Spider(new Point(5, 0));
            Ant test_ant1 = new Warrior(new Point(5, 8));
            Ant test_ant2 = new Warrior(new Point(0, 3));
            List<KeyValuePair<int, int>> trail = Astar.Search(new KeyValuePair<int, int>(test_spider.Position.X, test_spider.Position.Y), new KeyValuePair<int, int>(test_ant1.Position.X, test_ant1.Position.Y), new TestAstarObject(test_isw));

            List<KeyValuePair<int, int>> test_trail1 = new List<KeyValuePair<int, int>>();
          
/*
              <Map row="sssssSssss" />
              <Map row="sxooooooxs" />
              <Map row="sxooooooxs" />
              <Map row="Wxooooooxs" />
              <Map row="sxooooooxs" />
              <Map row="sxooooooxs" />
              <Map row="sxoooKooxs" />
              <Map row="sxooooooxs" />
              <Map row="sxoooWooxs" />
              <Map row="ssssssssss" />
*/

            test_trail1.Add(new KeyValuePair<int, int>(5, 0));
            test_trail1.Add(new KeyValuePair<int, int>(5, 1));
            test_trail1.Add(new KeyValuePair<int, int>(5, 2));
            test_trail1.Add(new KeyValuePair<int, int>(5, 3));
            test_trail1.Add(new KeyValuePair<int, int>(5, 4));
            test_trail1.Add(new KeyValuePair<int, int>(5, 5));
            test_trail1.Add(new KeyValuePair<int, int>(5, 6));
            test_trail1.Add(new KeyValuePair<int, int>(5, 7));
            test_trail1.Add(new KeyValuePair<int, int>(5, 8));

            Assert.IsNotNull(trail,"Trail is null");
            Assert.AreEqual(test_trail1.Count, trail.Count, "Trail {0} and trail_test {1} count is not equal",trail.Count,test_trail1.Count);

            for (int i=0; i< test_trail1.Count; i++)
            {
                Assert.AreEqual(test_trail1[i], trail[i], "Astar_path element EQUAL problem - is {0}, should be {1}", trail[i],test_trail1[i]);
            }

            trail = Astar.Search(new KeyValuePair<int, int>(test_spider.Position.X, test_spider.Position.Y), new KeyValuePair<int, int>(test_ant2.Position.X, test_ant2.Position.Y), new TestAstarObject(test_isw));

            List<KeyValuePair<int, int>> test_trail2 = new List<KeyValuePair<int, int>>();

            test_trail2.Add(new KeyValuePair<int, int>(5, 0));
            test_trail2.Add(new KeyValuePair<int, int>(4, 0));
            test_trail2.Add(new KeyValuePair<int, int>(3, 0));
            test_trail2.Add(new KeyValuePair<int, int>(2, 0));
            test_trail2.Add(new KeyValuePair<int, int>(1, 0));
            test_trail2.Add(new KeyValuePair<int, int>(0, 0));
            test_trail2.Add(new KeyValuePair<int, int>(0, 1));
            test_trail2.Add(new KeyValuePair<int, int>(0, 2));
            test_trail2.Add(new KeyValuePair<int, int>(0, 3));

            Assert.IsNotNull(trail, "Trail is null");
            Assert.AreEqual(test_trail2.Count, trail.Count, "Trail {0} and trail_test {1} count is not equal", trail.Count, test_trail2.Count);

            for (int i = 0; i < test_trail2.Count; i++)
            {
                Assert.AreEqual(test_trail2[i], trail[i], "Astar_path element EQUAL problem - is {0}, should be {1}", trail[i], test_trail2[i]);
            }
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

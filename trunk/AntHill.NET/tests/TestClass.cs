using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Drawing;
using astar;

/* TODO:
 * CitizenTest - dopisaæ o sygna³ach i inne...
 * RainTest - to dopracowaæ...
 * XmlReaderTest - testowanie mapy...
 * SimulationTest - dopisaæ to co siê pojawi³o...
 * GetVisibleSthTest - napisaæ... (ant, food, spider, message)
 * Create and Delete sth Test (spider, message, food, ant, ...)
 */

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
             XmlReaderWriter reader = new XmlReaderWriter();
             reader.ReadMe("..\\..\\tests\\test-ASTAR-anthill.xml");

            Simulation test_isw = new Simulation(new Map(AntHillConfig.mapColCount, AntHillConfig.mapRowCount, AntHillConfig.tiles));
            Assert.AreEqual(test_isw.queen.TurnsToBecomeHungry, 3, "Queen has wrong TurnsToBecomeHungry");
            Assert.AreEqual(test_isw.queen.TurnsWithoutFood, 100, "Queen has wrong TurnsWithoutFood");
            Assert.AreEqual(new System.Drawing.Point(5, 6), test_isw.queen.Position, "queen.Position problem");

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
            Food test_food = new Food(new Point(7, 7), 8);
            Assert.AreEqual(8, test_food.GetQuantity, "food.Quentity problem");
            Assert.AreEqual(new System.Drawing.Point(7,7), test_food.Position, "food.Position problem");
            

        }
        [Test]
        public void MessageTest()
        {
/*
            Tile[,] test_tiles =
            {{new Tile(TileType.Indoor, new Point()), new Tile(TileType.Outdoor, new Point()), new Tile(TileType.Wall, new Point())},
            {new Tile(TileType.Wall, new Point()), new Tile(TileType.Indoor, new Point()), new Tile(TileType.Outdoor, new Point())},
            {new Tile(TileType.Outdoor, new Point()), new Tile(TileType.Indoor, new Point()), new Tile(TileType.Wall, new Point())},
            {new Tile(TileType.Indoor, new Point()), new Tile(TileType.Wall, new Point()), new Tile(TileType.Outdoor, new Point())}};

            Assert.IsNotNull(test_tiles, "TTT:{0} {1}", test_tiles.GetLength(0), test_tiles.GetLength(1));

            Map test_map = new Map(4, 3, test_tiles);

            Simulation tmp_isw = new Simulation(test_map);
*/



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
            XmlReaderWriter reader = new XmlReaderWriter();
            reader.ReadMe("..\\..\\tests\\test-XML-anthill.xml");

          
            //ant
            Assert.AreEqual(AntHillConfig.antTurnNumberToBecomeHungry,3, "problems ant TurnsToBecomeHungry");
            Assert.AreEqual(AntHillConfig.antMaxLifeWithoutFood, 100, "problem ant.wrong TurnsWithoutFood");
            Assert.AreEqual(AntHillConfig.antFoodQuantityAfterDeath,1,"problem ant.foodQuantityAfterDeath");
            Assert.AreEqual(AntHillConfig.antMaxLife, 100, "problem antMaxLife");
            Assert.AreEqual(AntHillConfig.antMaxHealth,1,"problem ant.MaxHealth");
            Assert.AreEqual(AntHillConfig.antForgettingTime, 5, "problem ant.forgettingtime");
            Assert.AreEqual(AntHillConfig.antSightRadius, 2, "problem antSightRadius");
            Assert.AreEqual(AntHillConfig.antStrength, 3, "problem ant.Strength");
            
            Assert.AreEqual(AntHillConfig.curMagnitude, 1, "problem curMagnitude");
            Assert.AreEqual(AntHillConfig.eggHatchTime, 10, "problem egg.HatchTime");
            Assert.AreEqual(AntHillConfig.eggHatchWarriorProbability, 0.2, "problem eggHatchWarriorProbability");
            Assert.AreEqual(AntHillConfig.foodProbability, 0.2, "problem foodProbability");
            Assert.AreEqual(AntHillConfig.mapColCount, 10, "problem mapColCount");
            Assert.AreEqual(AntHillConfig.mapRowCount, 10, "problem mapRowCount");
            Assert.AreEqual(AntHillConfig.maxMagnitude, 2, "problem maxMagnitude");
            Assert.AreEqual(AntHillConfig.messageLifeTime, 10, "problem messageLifeTime");
            Assert.AreEqual(AntHillConfig.messageRadius,3,"problem messageRadius");
            Assert.AreEqual(AntHillConfig.queenLayEggProbability, 0, "problem queenLayEggProbability");
            Assert.AreEqual(AntHillConfig.queenXPosition, 5, "problem queenXPosition");
            Assert.AreEqual(AntHillConfig.queenYPosition, 6, "problem queenYPosition");
            Assert.AreEqual(AntHillConfig.rainMaxDuration, 20, "problem rainMaxDuration");
            Assert.AreEqual(AntHillConfig.rainProbability, 0.1, "problem rainProbability");
            Assert.AreEqual(AntHillConfig.rainWidth, 3, "problem rainWidth");
            Assert.AreEqual(AntHillConfig.spiderFoodQuantityAfterDeath, 5, "problem spiderFoodQuantityAfterDeath");
            Assert.AreEqual(AntHillConfig.spiderMaxHealth,10,"problem spiderMaxHealth");
            Assert.AreEqual(AntHillConfig.spiderProbability, 0.5, "problem spiderProbability");
            Assert.AreEqual(AntHillConfig.warriorStartCount, 0, "problem warrior startCount");
            Assert.AreEqual(AntHillConfig.workerStartCount, 0, "problem workerStartCount");
            Assert.AreEqual(AntHillConfig.queenXPosition,5, "queen.Position.X problem");
            Assert.AreEqual(AntHillConfig.queenYPosition, 6, "queen.Position.Y problem");
            for (int i = 0; i < AntHillConfig.mapColCount; i++)
            {
                Assert.AreEqual(AntHillConfig.tiles[i, 0].TileType, TileType.Outdoor, "Map xml problem in row 0");
                Assert.AreEqual(AntHillConfig.tiles[i, 1].TileType, TileType.Wall, "Map xml problem in row 1 ");
                Assert.AreEqual(AntHillConfig.tiles[i, 2].TileType, TileType.Indoor, "Map xml problem in row 2 ");
                Assert.AreEqual(AntHillConfig.tiles[i, 3].TileType, TileType.Indoor, "Map xml problem in row 3 ");
                Assert.AreEqual(AntHillConfig.tiles[i, 4].TileType, TileType.Indoor, "Map xml problem in row 4 ");
                Assert.AreEqual(AntHillConfig.tiles[i, 5].TileType, TileType.Indoor, "Map xml problem in row 5 ");
                Assert.AreEqual(AntHillConfig.tiles[i, 6].TileType, TileType.Indoor, "Map xml problem in row 6 ");
                Assert.AreEqual(AntHillConfig.tiles[i, 7].TileType, TileType.Indoor, "Map xml problem in row 7");
                Assert.AreEqual(AntHillConfig.tiles[i, 8].TileType, TileType.Wall, "Map xml problem in row 8 ");
                Assert.AreEqual(AntHillConfig.tiles[i, 9].TileType, TileType.Outdoor, "Map xml problem in row 9");
            }
        }


        [Test]
        public void MapTest()
        {
            Tile[,] test_tiles =
            {{new Tile(TileType.Indoor, new Point()), new Tile(TileType.Outdoor, new Point()), new Tile(TileType.Wall, new Point())},
            {new Tile(TileType.Wall, new Point()), new Tile(TileType.Indoor, new Point()), new Tile(TileType.Outdoor, new Point())},
            {new Tile(TileType.Outdoor, new Point()), new Tile(TileType.Indoor, new Point()), new Tile(TileType.Wall, new Point())},
            {new Tile(TileType.Indoor, new Point()), new Tile(TileType.Wall, new Point()), new Tile(TileType.Outdoor, new Point())}};

            AHGraphics.Init();  

            Map test_map = new Map(4, 3, test_tiles);

            Assert.AreEqual(4, test_map.Width, "Bad witdth of map");
            Assert.AreEqual(3, test_map.Height, "Bad height of map");
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 3; j++)
                {
                    Assert.AreEqual(test_tiles[i, j].TileType, test_map.GetTile(i,j).TileType, "Bad tile type in test_tile ({0},{1}) is {2} should be {3} ", i, j, test_tiles[i, j].TileType, test_map.GetTile(i, j).TileType);
                }
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
        public void RainTest()
        {

            XmlReaderWriter reader = new XmlReaderWriter();
            reader.ReadMe("..\\..\\tests\\test-RAIN-anthill.xml");
            
            Simulation tmp_isw = new Simulation(new Map(AntHillConfig.mapColCount, AntHillConfig.mapRowCount, AntHillConfig.tiles));
            AHGraphics.Init();

            Assert.IsNotNull(tmp_isw, "Simulation is NULL problem!!!");

            Spider test_spider = new Spider(new Point(61,71));
            tmp_isw.spiders.Add(test_spider);
            Ant test_ant1 = new Warrior(new Point(62, 71));
            Ant test_ant2 = new Worker(new Point(61, 72));
            tmp_isw.ants.Add(test_ant1);
            tmp_isw.ants.Add(test_ant2);

            Rain test_rain=new Rain(new Point(60, 70));
            Assert.IsNotNull(test_rain, "Rain is NULL problem!!!");

            Assert.IsTrue(test_rain.IsRainOver(60, 70),"Rain.IsRainOver problem");
            Assert.AreEqual(new Point(60, 70), test_rain.Position, "Rain.Position problem");
            Assert.IsTrue((test_rain.TimeToLive >= 0) && (test_rain.TimeToLive < AntHillConfig.rainMaxDuration + 1), "Rain.TimeToLive range problem");
            int tmp = test_rain.TimeToLive;

            Assert.AreEqual(tmp, test_rain.TimeToLive, "Rain.TimeToLive problem should be {0}, but is {1}", tmp, test_rain.TimeToLive);

            Assert.IsTrue(tmp_isw.spiders.Contains(test_spider), "Find spider problem");
            Assert.IsTrue(tmp_isw.ants.Contains(test_ant1), "Find warrior problem");
            Assert.IsTrue(tmp_isw.ants.Contains(test_ant2), "Find worker problem");

            Assert.IsNotNull(test_rain, "Rain is NULL problem!!!");

            test_rain.Maintain((ISimulationWorld)tmp_isw);
            Assert.AreEqual(tmp - 1,test_rain.TimeToLive, "Rain.Maintain problem should be {0}, but is {1}", tmp - 1, test_rain.TimeToLive);
            
            Assert.IsFalse(tmp_isw.spiders.Contains(test_spider), "Rain destroy spiders problem");
            Assert.IsFalse(tmp_isw.ants.Contains(test_ant1), "Rain destroy warriors problem");
            Assert.IsFalse(tmp_isw.ants.Contains(test_ant2), "Rain destroy workers problem");
        }

        [Test]
        public void SimulationTest()
        {
            Tile[,] test_tiles =
            {{new Tile(TileType.Indoor, new Point()), new Tile(TileType.Outdoor, new Point()), new Tile(TileType.Wall, new Point())},
            {new Tile(TileType.Wall, new Point()), new Tile(TileType.Indoor, new Point()), new Tile(TileType.Outdoor, new Point())},
            {new Tile(TileType.Outdoor, new Point()), new Tile(TileType.Indoor, new Point()), new Tile(TileType.Wall, new Point())},
            {new Tile(TileType.Indoor, new Point()), new Tile(TileType.Wall, new Point()), new Tile(TileType.Outdoor, new Point())}};

            Assert.IsNotNull(test_tiles, "TTT:{0} {1}", test_tiles.GetLength(0), test_tiles.GetLength(1));

            Map test_map = new Map(4, 3, test_tiles);
          
            Simulation tmp_isw = new Simulation(test_map);

            AHGraphics.Init();  

            Assert.AreSame(test_map, tmp_isw.GetMap(), "Simulation.GetMap problem");

//tu brakuje doœæ du¿o funkcji i do nich testów ;)

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
              <Map row="2xooooooxs" />
              <Map row="sxooooooxs" />
              <Map row="sxooooooxs" />
              <Map row="sxoooKooxs" />
              <Map row="sxooooooxs" />
              <Map row="sxooo1ooxs" />
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
        public void GetVisibleAntsTest()
        {

        }

        [Test]
        public void GetVisibleFoodTest()
        {

        }

        [Test]
        public void GetVisibleSpidersTest()
        {

        }

        [Test]
        public void GetVisibleMessagesTest()
        {

        }

    }
}

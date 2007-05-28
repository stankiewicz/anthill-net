using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Drawing;
using astar;

/* TODO:
 * SimulationTest - dopisaæ to co siê pojawi³o...
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
        public void CitizenTest()
        {
            Worker worker1 = new Worker(new Point(2, 2));
            Worker worker2 = new Worker(new Point(3, 3));
            XmlReaderWriter reader = new XmlReaderWriter();
            reader.ReadMe("..\\..\\tests\\test-RAIN-anthill.xml");

            AHGraphics.Init();
            /*test AddSet() function*/
            Simulation tmp_isw = new Simulation(new Map(AntHillConfig.mapColCount, AntHillConfig.mapRowCount, AntHillConfig.tiles));
            Message message=new Message(new Point(2,2),MessageType.FoodLocalization);
            tmp_isw.ants.Add(worker1);
            tmp_isw.ants.Add(worker2);
            tmp_isw.messages.Add(message);
            worker1.AddToSet(message,2);
            worker1.SpreadSignal(tmp_isw);
            List<Message> list = tmp_isw.GetVisibleMessages(worker2);
            bool check = list.Contains(message);
            Assert.IsTrue(check, "problem with adding messages");

            /*test GetNearestFood()*/
            List<Food> foodList=new List<Food>();
            foodList.Add(new Food(new Point(4,4),1));
            foodList.Add(new Food(new Point(8,8),3));
            foodList.Add(new Food(new Point(2,3),3));
            foodList.Add(new Food(new Point(1,1),3));
            Food nFood=worker1.testGetNearestFood(foodList);
            Assert.AreEqual(new System.Drawing.Point(2, 3), nFood.Position, "finding nearest food problem");

            /*test ReadMessage()*/
            Worker worker3 = new Worker(new Point(4, 4));
            worker3.AddToSet(new Message(new Point(5,5),MessageType.FoodLocalization),1);
            worker3.AddToSet(new Message(new Point(1,1),MessageType.FoodLocalization),3);
            worker3.AddToSet(new Message(new Point(3,3),MessageType.FoodLocalization),2);
            Message nMessage=worker3.testReadMessage(MessageType.FoodLocalization);
            Assert.AreEqual(new System.Drawing.Point(1, 1), nMessage.Position, "ReadMessage function problem in Citizen");
            
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
        public void FoodTest()
        {
            Food test_food = new Food(new Point(7, 7), 8);
            Assert.AreEqual(8, test_food.GetQuantity, "food.Quentity problem");
            Assert.AreEqual(new Point(7,7), test_food.Position, "food.Position problem");
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

            Assert.IsTrue(test_rain.IsRainOver(60, 70), "Rain isn't exist or Rain.IsRainOver problem");
            Assert.IsTrue(test_rain.IsRainOver(62, 72), "Rain is too small or Rain.IsRainOver problem");
            Assert.IsTrue(test_rain.IsRainOver(58, 68), "Rain is too small or Rain.IsRainOver problem");
            Assert.IsFalse(test_rain.IsRainOver(63, 73), "Rain is too big or Rain.IsRainOver problem");
            Assert.IsFalse(test_rain.IsRainOver(57, 67), "Rain is too big or Rain.IsRainOver problem");
            
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
          
/* Laduje ponizsza mape, gdzie uzywam oznaczen:
 * S - spider; Q-Queen; 1,2 - Ants i standardowych...
              <Map row="sssssSssss" />
              <Map row="sxooooooxs" />
              <Map row="sxooooooxs" />
              <Map row="2xooooooxs" />
              <Map row="sxooooooxs" />
              <Map row="sxooooooxs" />
              <Map row="sxoooQooxs" />
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

        [Test]
        public void GetVisibleAntsTest()
        {
            Worker worker1 = new Worker(new Point(2, 2));
            Warrior warrior1 = new Warrior(new Point(3, 3));
            Spider spider1 = new Spider(new Point(2, 3));
            XmlReaderWriter reader = new XmlReaderWriter();
            reader.ReadMe("..\\..\\tests\\test-RAIN-anthill.xml");


            AHGraphics.Init();

            Simulation tmp_isw = new Simulation(new Map(AntHillConfig.mapColCount, AntHillConfig.mapRowCount, AntHillConfig.tiles));
            tmp_isw.ants.Add(worker1);
            tmp_isw.ants.Add(warrior1);
            tmp_isw.spiders.Add(spider1);
            
            Rain test_rain = new Rain(new Point(3, 3));
            tmp_isw.rain = test_rain;

            List<Ant> list1 = tmp_isw.GetVisibleAnts(test_rain);
            List<Ant> list2 = tmp_isw.GetVisibleAnts(warrior1);
            List<Ant> list3 = tmp_isw.GetVisibleAnts(worker1);
            List<Ant> list4 = tmp_isw.GetVisibleAnts(spider1);

            Assert.IsTrue(list1.Contains(worker1), "GetVisibleAntsTest problem to see worker by rain");
            Assert.IsTrue(list1.Contains(warrior1), "GetVisibleAntsTest problem to see warrior by rain");
            Assert.IsTrue(list2.Contains(worker1), "GetVisibleAntsTest problem to see worker by warriror");
            Assert.IsTrue(list3.Contains(warrior1), "GetVisibleAntsTest problem to see warrior by worker");
            Assert.IsTrue(list4.Contains(worker1), "GetVisibleAntsTest problem to see worker by rain");
            Assert.IsTrue(list4.Contains(warrior1), "GetVisibleAntsTest problem to see warrior by rain");
        }

        [Test]
        public void GetVisibleFoodTest()
        {
            Worker worker1 = new Worker(new Point(2, 2));
            Warrior warrior1 = new Warrior(new Point(3, 3));
            Spider spider1 = new Spider(new Point(2, 3));
            Food food1 = new Food(new Point(3, 2), 10);
            XmlReaderWriter reader = new XmlReaderWriter();
            reader.ReadMe("..\\..\\tests\\test-RAIN-anthill.xml");


            AHGraphics.Init();

            Simulation tmp_isw = new Simulation(new Map(AntHillConfig.mapColCount, AntHillConfig.mapRowCount, AntHillConfig.tiles));
            tmp_isw.ants.Add(worker1);
            tmp_isw.ants.Add(warrior1);
            tmp_isw.spiders.Add(spider1);
            tmp_isw.food.Add(food1);

            Rain test_rain = new Rain(new Point(3, 3));
            tmp_isw.rain = test_rain;

            List<Food> list1 = tmp_isw.GetVisibleFood(test_rain);
            List<Food> list2 = tmp_isw.GetVisibleFood(warrior1);
            List<Food> list3 = tmp_isw.GetVisibleFood(worker1);
            List<Food> list4 = tmp_isw.GetVisibleFood(spider1);

            Assert.IsTrue(list1.Contains(food1), "GetVisibleAntsTest problem to see food by rain");
            Assert.IsTrue(list2.Contains(food1), "GetVisibleAntsTest problem to see food by warriror");
            Assert.IsTrue(list3.Contains(food1), "GetVisibleAntsTest problem to see food by worker");
            Assert.IsTrue(list4.Contains(food1), "GetVisibleAntsTest problem to see food by rain");
        }

        [Test]
        public void GetVisibleSpidersTest()
        {
            Worker worker1 = new Worker(new Point(2, 2));
            Warrior warrior1 = new Warrior(new Point(3, 3));
            Spider spider1 = new Spider(new Point(2, 3));
            Spider spider2 = new Spider(new Point(3, 2));
            XmlReaderWriter reader = new XmlReaderWriter();
            reader.ReadMe("..\\..\\tests\\test-RAIN-anthill.xml");


            AHGraphics.Init();

            Simulation tmp_isw = new Simulation(new Map(AntHillConfig.mapColCount, AntHillConfig.mapRowCount, AntHillConfig.tiles));
            tmp_isw.ants.Add(worker1);
            tmp_isw.ants.Add(warrior1);
            tmp_isw.spiders.Add(spider1);
            tmp_isw.spiders.Add(spider2);

            Rain test_rain = new Rain(new Point(3, 3));
            tmp_isw.rain = test_rain;

            List<Spider> list1 = tmp_isw.GetVisibleSpiders(test_rain);
            List<Spider> list2 = tmp_isw.GetVisibleSpiders(warrior1);
            List<Spider> list3 = tmp_isw.GetVisibleSpiders(worker1);
            List<Spider> list4 = tmp_isw.GetVisibleSpiders(spider2);

            Assert.IsTrue(list1.Contains(spider1), "GetVisibleAntsTest problem to see spider by rain");
            Assert.IsTrue(list2.Contains(spider1), "GetVisibleAntsTest problem to see spider by warriror");
            Assert.IsTrue(list3.Contains(spider1), "GetVisibleAntsTest problem to see spider by worker");
            Assert.IsTrue(list4.Contains(spider1), "GetVisibleAntsTest problem to see spider by rain");

        }

        [Test]
        public void GetVisibleMessagesTest()
        {
            Worker worker1 = new Worker(new Point(2, 2));
            Warrior warrior1 = new Warrior(new Point(3, 3));
            XmlReaderWriter reader = new XmlReaderWriter();
            reader.ReadMe("..\\..\\tests\\test-RAIN-anthill.xml");


            AHGraphics.Init();

            Simulation tmp_isw = new Simulation(new Map(AntHillConfig.mapColCount, AntHillConfig.mapRowCount, AntHillConfig.tiles));
            tmp_isw.CreateMessage(new Point(2, 2), MessageType.QueenIsHungry, new Point(2, 2));
            tmp_isw.CreateMessage(new Point(3, 3), MessageType.FoodLocalization, new Point(3, 3));
            Message message1 = (tmp_isw.messages).Find(delegate(Message m) {return m.Position == new Point(2, 2);});
            Message message2 = (tmp_isw.messages).Find(delegate(Message m) { return m.Position == new Point(3, 3); });

            Assert.IsNotNull(message1, "message1 is NULL in GetVisibleMessagesTest");
            Assert.IsNotNull(message2, "message1 is NULL in GetVisibleMessagesTest");

            tmp_isw.ants.Add(worker1);
            tmp_isw.ants.Add(warrior1);
            
            Rain test_rain = new Rain(new Point(3, 3));
            tmp_isw.rain = test_rain;

            List<Message> list1 = tmp_isw.GetVisibleMessages(test_rain);
            List<Message> list2 = tmp_isw.GetVisibleMessages(warrior1);
            List<Message> list3 = tmp_isw.GetVisibleMessages(worker1);

            Assert.IsTrue(list1.Contains(message1), "GetVisibleMessagesTest problem to see message by rain");
            Assert.IsTrue(list2.Contains(message2), "GetVisibleMessagesTest problem to see message by warriror");
            Assert.IsTrue(list3.Contains(message1), "GetVisibleMessagesTest problem to see message by worker");

        }
        [Test]
        public void CreateFoodTest()
        {
            XmlReaderWriter reader = new XmlReaderWriter();
            reader.ReadMe("..\\..\\tests\\test-RAIN-anthill.xml");


            AHGraphics.Init();

            Simulation tmp_isw = new Simulation(new Map(AntHillConfig.mapColCount, AntHillConfig.mapRowCount, AntHillConfig.tiles));
            tmp_isw.CreateFood(new Point(2, 2), 23);
            Food food1 = (tmp_isw.food).Find(delegate(Food f) { return f.Position == new Point(2, 2); });

            Assert.IsNotNull(food1, "CreateFood problem");
            Assert.AreEqual(food1.GetQuantity, 23, "CreateFood Quantity problem");
            Assert.AreEqual(food1.Position, new Point(2, 2), "CreateFood Position problem");
        }

        [Test]
        public void CreateWorkerTest()
        {


        }
        [Test]
        public void CreateWarriorTest()
        {

        }
        [Test]
        public void CreateSpiderTest()
        {

        }
        [Test]
        public void CreateSpider_WITH_POSITION_Test()
        {

        }
        [Test]
        public void CreateMessageTest()
        {

        }
        [Test]
        public void CreateEggTest()
        {

        }
        [Test]
        public void CreateAntTest()
        {

        }
        [Test]
        public void DeleteFoodTest()
        {

        }
        [Test]
        public void DeleteRainTest()
        {

        }
        [Test]
        public void DeleteEggTest()
        {

        }
        [Test]
        public void DeleteAntTest()
        {

        }
        [Test]
        public void DeleteSpiderTest()
        {

        }
        [Test]
        public void FeedQueenTest()
        {

        }
        [Test]
        public void AttackTest()
        {

        }

    }
}

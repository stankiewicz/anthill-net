using System;
using System.Collections.Generic;
using System.Drawing;
using astar;

namespace AntHill.NET
{
    public class Simulation : ISimulationUser, ISimulationWorld
    {
        //public event EventHandler afterTurn = null;

        public static Simulation singletonInstance = null;

        public static bool Init(Map map)
        {
            try
            {
                if (singletonInstance == null)
                    singletonInstance = new Simulation(map);
            }
            catch (Exception exc) //Simulation(map) can throw an exception
            {
                throw exc;
            }

            Astar.Init(AntHillConfig.mapColCount, AntHillConfig.mapRowCount);

            return true;
        }

        public static void DeInit()
        {
            singletonInstance = null;
        }

        public static Simulation simulation
        {
            get { return singletonInstance; }
        }
        int turn = 0;
        private Map map;
        public LIList<Egg> eggs;
        public LIList<Message> messages;
        public LIList<Food> food;
        public LIList<Spider> spiders;
        public LIList<Ant> ants;
        public Rain rain;
        public Queen queen;

        private void Initialize()
        {
            map = null;
            eggs = new LIList<Egg>();
            eggs.Clear();
            messages = new LIList<Message>();
            messages.Clear();
            food = new LIList<Food>();
            food.Clear();
            spiders = new LIList<Spider>();
            spiders.Clear();
            ants = new LIList<Ant>();
            ants.Clear();
            rain = null;
            queen = new Queen(new Point(AntHillConfig.queenXPosition, AntHillConfig.queenYPosition));
        }

	    public Simulation(Map map)
        {
            Initialize();

            if (map.GetIndoorCount == 0)
                throw new Exception(Properties.Resources.noIndoorTilesError);
            if (map.GetOutdoorCount == 0)
                throw new Exception(Properties.Resources.noOutdoorTilesError);

            this.map = map;

            for (int i = AntHillConfig.workerStartCount; i > 0; i--)
                this.CreateWorker(Map.GetRandomTile(TileType.Indoor).Position);

            for (int i = AntHillConfig.warriorStartCount; i > 0; i--)
                this.CreateWarrior(Map.GetRandomTile(TileType.Indoor).Position);
        }

        public Map Map
        {
            get { return map; }
        }

        bool CheckIfExists(TileType tt)
        {
            for (int i = 0; i < Map.Height; i++)
                for (int j = 0; j < Map.Width; j++)
                    if (Map.GetTile(j, i).TileType == tt)
                        return true;

            return false;
        }

        Point GetRandomPoint()
        {
            return new Point(Randomizer.Next(Map.Width), Randomizer.Next(Map.Height));
        }

        Point GetRandomPointForRain()
        {
            return new Point(Randomizer.Next(Map.Width - AntHillConfig.rainWidth),
                             Randomizer.Next(Map.Height - AntHillConfig.rainWidth));
        }

	    #region ISimulation Members

        /// <summary>
        /// This is the most important function - activity diagram
        /// </summary>
        bool ISimulationUser.DoTurn()
        {
            if (queen == null) return false;
            turn++;
            if (Randomizer.NextDouble() <= AntHillConfig.spiderProbability)
                    this.CreateSpider(Map.GetRandomTile(TileType.Outdoor).Position);

            if (Randomizer.NextDouble() <= AntHillConfig.foodProbability)
                this.CreateFood(Map.GetRandomTile(TileType.Outdoor).Position, GetRandomFoodQuantity());

            if ((rain == null) && (Randomizer.NextDouble() <= AntHillConfig.rainProbability))
                this.CreateRain(GetRandomPointForRain());

            if (rain != null)
                rain.Maintain(this);

            LinkedListNode<Message> msg = messages.First;
            LinkedListNode<Message> msgT;
            while (msg != null)
            {
                msg.Value.Maintain(this);
                if (msg.Value.Empty)
                {
                    msgT = msg;
                    msg = msg.Next;
                    messages.Remove(msgT);
                }
                else
                    msg = msg.Next;
            }
            LinkedListNode<Ant> ant = ants.First;
            LinkedListNode<Ant> antTemp;            
            while(ant != null)
            {
                if (!ant.Value.Maintain(this))
                {
                    antTemp = ant;
                    ant = ant.Next;
                    ants.Remove(antTemp);
                }
                else
                    ant = ant.Next;
            }
            LinkedListNode<Spider> spider = spiders.First;
            LinkedListNode<Spider> spiderTemp;
            while (spider != null)
            {
                if (!spider.Value.Maintain(this))
                {
                    spiderTemp = spider;
                    spider = spider.Next;
                    spiders.Remove(spiderTemp);
                }
                else
                    spider = spider.Next;
            }

            LinkedListNode<Egg> egg = eggs.First;
            LinkedListNode<Egg> eggTemp;
            while (egg != null)
            {
                if (!egg.Value.Maintain(this))
                {
                    eggTemp = egg;
                    egg = egg.Next;
                    eggs.Remove(eggTemp);
                }
                else
                    egg = egg.Next;
            }
            
            if (queen!=null && !queen.Maintain(this))
            {
                queen = null;
                return false;
            }
            return true;
        }

        void ISimulationUser.Reset()
        {
            Initialize();
            turn = 0;
        }

        void ISimulationUser.Start()
        {

        }

        void ISimulationUser.Stop()
        {
            
        }

        #endregion

        #region ISimulationWorld Members

        private bool CreateRain(Point point)
        {
            rain = new Rain(point);
            return true;
        }

        public bool CreateFood(Point point, int quantity)
        {
            food.AddLast(new Food(point, quantity));
            return true;
        }

        public bool CreateSpider(Point point)
        {
            spiders.AddLast(new Spider(point));
            return true;
        }


        public bool CreateWarrior(Point pos)
        {
            ants.AddLast(new Warrior(pos));
            return true;
        }

        public bool CreateWorker(Point pos)
        {
            ants.AddLast(new Worker(pos));
            return true;
        }

        public bool CreateSpider()
        {
            spiders.AddLast(new Spider(Map.GetRandomTile(TileType.Outdoor).Position));
            return true;
        }


        //returns true if cD is killed
        public bool Attack(Creature cA, Creature cD)
        {
            if (cA is Spider)
            {
                if (cD is Queen) queen = null;
                else if (cD is Ant)
                    this.DeleteAnt((Ant)cD);

                return true;
            }
            else if (cA is Warrior && cD is Spider)
            {
                try
                {
                    ((Spider)cD).Health -= AntHillConfig.antStrength;
                }
                catch (Exception)
                {
                    this.DeleteSpider((Spider)cD);
                    return true;
                }
            }

            return false;
        }

        private bool IsVisible(Point p1, Point p2)
        {
            //not ready
            return true;
        }

        private bool IsInRect(Point pt, Point pos, int width, int height)
        {
            //not ready
            return true;
        }

        public LIList<Ant> GetVisibleAnts(Element c)
        {
            LIList<Ant> res_ants = new LIList<Ant>();
            LinkedListNode<Ant> antNode = ants.First;
            int radius;
            if (c is Spider || c is Ant) //same radius
            {
                radius = AntHillConfig.antSightRadius; //same as for ant
                while (antNode != null)
                {
                    //as for name - simple, implement Bresenham's alg. in the future
                    if (Math.Abs(antNode.Value.Position.X - c.Position.X) <= radius &&
                        Math.Abs(antNode.Value.Position.Y - c.Position.Y) <= radius)
                        res_ants.AddLast(antNode.Value);
                    antNode = antNode.Next;
                }
            }
            else if (c is Rain)
            {
                while (antNode != null)
                {
                    if (map.GetTile(antNode.Value.Position.X, antNode.Value.Position.Y).TileType == TileType.Outdoor &&
                        ((Rain)c).IsRainOver(antNode.Value.Position.X, antNode.Value.Position.Y))
                        res_ants.AddLast(antNode.Value);
                    antNode = antNode.Next;
                }
            }
                
            return res_ants;
        }

        public LIList<Food> GetVisibleFood(Element c)
        {
            LIList<Food> res_food = new LIList<Food>();
            LinkedListNode<Food> foodNode = food.First;
            int radius;
            if (c is Spider || c is Ant) //same radius
            {
                radius = AntHillConfig.antSightRadius; //same as for ant
                while (foodNode != null)
                {
                    //as for name - simple, implement Bresenham's alg. in the future
                    if (Math.Abs(foodNode.Value.Position.X - c.Position.X) <= radius &&
                        Math.Abs(foodNode.Value.Position.Y - c.Position.Y) <= radius)
                        res_food.AddLast(foodNode.Value);
                    foodNode = foodNode.Next;
                }
            }
            else if (c is Rain)
            {
                while (foodNode != null)
                {
                    if (map.GetTile(foodNode.Value.Position.X, foodNode.Value.Position.Y).TileType == TileType.Outdoor &&
                        ((Rain)c).IsRainOver(foodNode.Value.Position.X, foodNode.Value.Position.Y))
                        res_food.AddLast(foodNode.Value);
                    foodNode = foodNode.Next;
                }
            }
            return res_food;
        }

        public LIList<Spider> GetVisibleSpiders(Element c)
        {
            LIList<Spider> res_spiders = new LIList<Spider>();
            LinkedListNode<Spider> spiderNode = spiders.First;
            int radius;
            if (c is Spider || c is Ant) //same radius
            {
                radius = AntHillConfig.antSightRadius; //same as for ant
                while (spiderNode != null)
                {
                    //as for name - simple, implement Bresenham's alg. in the future
                    if (Math.Abs(spiderNode.Value.Position.X - c.Position.X) <= radius &&
                        Math.Abs(spiderNode.Value.Position.Y - c.Position.Y) <= radius)
                        res_spiders.AddLast(spiderNode.Value);
                    spiderNode = spiderNode.Next;
                }
            }
            else if (c is Rain)
            {
                while (spiderNode != null)
                {
                    if (map.GetTile(spiderNode.Value.Position.X, spiderNode.Value.Position.Y).TileType == TileType.Outdoor &&
                        ((Rain)c).IsRainOver(spiderNode.Value.Position.X, spiderNode.Value.Position.Y))
                        res_spiders.AddLast(spiderNode.Value);
                    spiderNode = spiderNode.Next;
                }
            }

            return res_spiders;
        }

        public LIList<Message> GetVisibleMessages(Element c)
        {
            LIList<Message> res_messages = new LIList<Message>();
            LinkedListNode<Message> messageNode;
            if (c is Ant)
            {
                messageNode = Map.GetTile(c.Position).messages.First;
                while (messageNode != null)
                {
                    res_messages.AddLast(messageNode.Value);
                    messageNode = messageNode.Next;
                }
            }
            else if (c is Rain)
            {
                for (int i = 0; i < AntHillConfig.rainWidth; i++)
                {
                    for (int j = 0; j < AntHillConfig.rainWidth; j++)
                    {
                        messageNode = Map.GetTile(c.Position.X + i, c.Position.Y + j).messages.First;
                        while (messageNode != null)
                        {
                            if (!res_messages.Contains(messageNode.Value))
                                res_messages.AddLast(messageNode.Value);
                            messageNode = messageNode.Next;
                        }
                    }
                }
            }
            return res_messages;
        }

        public bool FeedQueen(Worker w)
        {
            if (w.Position != queen.Position) return false;
            queen.FoodQuantity += w.FoodQuantity;
            w.FoodQuantity = 0;
            return true;
        }

        public bool CreateAnt(System.Drawing.Point position)
        {
            if (Randomizer.NextDouble() < AntHillConfig.eggHatchWarriorProbability)
                ants.AddLast(new Warrior(position));
            else
                ants.AddLast(new Worker(position));

            return true;
        }

        public bool DeleteEgg(Egg egg)
        {
            eggs.Remove(egg);
            return true;
        }

        public bool DeleteRain()
        {
            rain = null;
            return true;
        }

        public bool CreateEgg(Point pos)
        {
            eggs.AddLast(new Egg(pos));
            return true;
        }

        public bool DeleteFood(Food food)
        {
            this.food.Remove(food);
            return true;
        }

        public bool DeleteAnt(Ant ant)
        {
            this.food.AddLast(new Food(ant.Position, AntHillConfig.antFoodQuantityAfterDeath));
            ants.Remove(ant);
            return true;
        }

        public bool DeleteSpider(Spider spider)
        {
            this.food.AddLast(new Food(spider.Position, AntHillConfig.spiderFoodQuantityAfterDeath));
            spiders.Remove(spider);
            return true;
        }

        public Map GetMap()
        {
            return map;
        }

        
        public bool CreateMessage(Point pos, MessageType mt, Point location)
        {
            Message ms = new Message(pos, mt,location);
            /*
            for (int i = -AntHillConfig.messageRadius; i <= AntHillConfig.messageRadius; i++)
            {
                for (int j = -AntHillConfig.messageRadius; j <= AntHillConfig.messageRadius; j++)
                {
                    if (i * i + j * j <= AntHillConfig.messageRadius * AntHillConfig.messageRadius)
                    {
                        if (map.Inside(i+pos.X, j+pos.Y))
                        {
                            if(map.GetTile(i+pos.X,j+pos.Y).TileType!= TileType.Wall)
                            ms.AddPoint(map.GetTile(i + pos.X, j + pos.Y), AntHillConfig.messageLifeTime, map);
                        }
                    }
                }
            }
             */
            this.messages.AddLast(ms);
            ms.Spread(this, pos, AntHillConfig.messageLifeTime);

            return true;
        }

        private int GetRandomFoodQuantity()
        {
            return Randomizer.Next(AntHillConfig.foodRandomMaxQuantity) + 1;
        }

        #endregion

        #region ISimulationUser Members


        public int GetNAnts()
        {
            return (ants!=null)?ants.Count:0;
        }

        public int GetNSignals()
        {
            return (messages!=null)?messages.Count:0;
        }

        public int GetNTurns()
        {
            return turn;
        }

        public int GetNSpiders()
        {
            return (spiders!=null)?spiders.Count:0;
        }

        public bool testCheckIfExists(TileType tt)
        {
            return this.CheckIfExists(tt);
        }

        #endregion
    }
}

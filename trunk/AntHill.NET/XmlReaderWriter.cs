

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml; 
using System.Collections;
using System.Xml.Schema;


namespace AntHill.NET
{

    class XmlReaderWriter
    {
        List<string> Errors = new List<string>();
        int rowCount;
        int colCount;
        List<string> MapsRows = new List<string>();
        int maxLife;
        int maxLifeWithoutFood;
        int turnNumberToBecomeHungry;
        int strength;
        int forgettingTime;
        int sightRadius;
        int startCountWarrior;
        int startCountWorker;
        int layEggProbability;
        int xPosition;
        int yPosition;
        int hatchWarriorProbability;
        int hatchTime;
        int maxHealth;
        int probabilitySpider;
        int foodQuantityAfterDeath;
        int cloudWidth;
        int probabilityRain;
        int maxDuration;
        int probabilityFood;
        int lifeTime;
        int radius;

        bool ValidateMe()
        {

            XmlSchemaSet sc = new XmlSchemaSet();

            // Add the schema to the collection.
            sc.Add(null, "C:\\b.xsd");

            // Set the validation settings.
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas = sc;
            settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);

            // Create the XmlReader object.
            XmlReader reader = XmlReader.Create("c:\\b.xml", settings);

            // Parse the file. 
            while (reader.Read()) ;

            if (Errors.Count == 0) return true;
            else return false;
        }

        
         // Display any validation errors.
        private  void ValidationCallBack(object sender, ValidationEventArgs e)
        {
            Errors.Add(e.Message);
        }

        public void ReadMe()
        {
            XmlTextReader textReader = new XmlTextReader("C:\\b.xml");
            textReader.Read();
            while (textReader.Read())
            {
                if (textReader.HasAttributes)
                    switch (textReader.Name)
                    {
                        case "WorldMap":
                            {
                                rowCount = int.Parse(textReader.GetAttribute("rowCount"));
                                colCount = int.Parse(textReader.GetAttribute("colCount"));
                            } break;
                        case "Map":
                            {
                                MapsRows.Add(textReader.GetAttribute("row"));
                            } break;
                        case "Ant":
                            {
                                maxLife = int.Parse(textReader.GetAttribute("maxLife"));
                                maxLifeWithoutFood = int.Parse(textReader.GetAttribute("maxLifeWithoutFood"));
                                turnNumberToBecomeHungry = int.Parse(textReader.GetAttribute("turnNumberToBecomeHungry"));
                                strength = int.Parse(textReader.GetAttribute("strength"));
                                forgettingTime = int.Parse(textReader.GetAttribute("forgettingTime"));
                                sightRadius = int.Parse(textReader.GetAttribute("sightRadius"));
                            } break;
                        case "Warrior":
                            {
                                startCountWarrior = int.Parse(textReader.GetAttribute("startCount"));
                            } break;
                        case "Worker":
                            {
                                startCountWorker = int.Parse(textReader.GetAttribute("startCount"));
                            } break;
                        case "Queen":
                            {
                                layEggProbability = int.Parse(textReader.GetAttribute("layEggProbability"));
                                xPosition = int.Parse(textReader.GetAttribute("xPosition"));
                                yPosition = int.Parse(textReader.GetAttribute("yPosition"));
                            } break;
                        case "Egg":
                            {
                                hatchWarriorProbability = int.Parse(textReader.GetAttribute("hatchWarriorProbability"));
                                hatchTime = int.Parse(textReader.GetAttribute("hatchTime"));
                            } break;
                        case "Spider":
                            {
                                maxHealth = int.Parse(textReader.GetAttribute("maxHealth"));
                                probabilitySpider = int.Parse(textReader.GetAttribute("probability"));
                                foodQuantityAfterDeath = int.Parse(textReader.GetAttribute("foodQuantityAfterDeath"));
                            } break;
                        case "Rain":
                            {
                                cloudWidth = int.Parse(textReader.GetAttribute("cloudWidth"));
                                probabilityRain = int.Parse(textReader.GetAttribute("probability"));
                                maxDuration = int.Parse(textReader.GetAttribute("maxDuration"));
                            } break;
                        case "Food":
                            {
                                probabilityFood = int.Parse(textReader.GetAttribute("probability"));
                            } break;
                        case "Signal":
                            {
                                lifeTime = int.Parse(textReader.GetAttribute("lifeTime"));
                                radius = int.Parse(textReader.GetAttribute("radius"));
                            } break;

                    }

            }

        }

    }
}

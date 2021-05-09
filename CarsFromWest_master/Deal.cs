using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsFromWest_master
{
    class Deal
    {
        public int id { get; set; }
        private string name;
        private int engine;
        private int odometet;
        private string fuel;
        private string color;
        private string body;
        private string drive;
        private string notes;
        private int price;
        private string _image;

        public string Image
        {
            get
            {
                switch (body)
                {
                    case "Купе":
                        {
                            return $"{Environment.CurrentDirectory}\\image\\купе.jpg";
                        }
                    case "Кабріолет":
                        {
                            return $"{Environment.CurrentDirectory}\\image\\кабріолет.jpg";
                        }
                    case "Кросовер":
                        {
                            return $"{Environment.CurrentDirectory}\\image\\кросовер.jpg";
                        }
                    case "Ліфтбек":
                        {
                            return $"{Environment.CurrentDirectory}\\image\\ліфтбек.jpg";
                        }
                    case "Мінівен":
                        {
                            return $"{Environment.CurrentDirectory}\\image\\мінівен.jpg";
                        }
                    case "Пікап":
                        {
                            return $"{Environment.CurrentDirectory}\\image\\пікап.jpg";
                        }
                    case "Позашляховик":
                        {
                            return $"{Environment.CurrentDirectory}\\image\\позашляховик.jpg";
                        }
                    case "Седан":
                        {
                            return $"{Environment.CurrentDirectory}\\image\\седан.jpg";
                        }
                    case "Універсал":
                        {
                            return $"{Environment.CurrentDirectory}\\image\\універсал.jpg";
                        }
                    case "Фургон":
                        {
                            return $"{Environment.CurrentDirectory}\\image\\фургон.jpg";
                        }
                    case "Хетчбек":
                        {
                            return $"{Environment.CurrentDirectory}\\image\\хетчбек.jpg";
                        }
                    default:
                        return _image;
                }
            }

        }


        public string Name { get { return name; } set { name = value; } }
        public int Engine { get { return engine; } set { engine = value; } }
        public int Odometet { get { return odometet; } set { odometet = value; } }
        public string Fuel { get { return fuel; } set { fuel = value; } }
        public string Color { get { return color; } set { color = value; } }
        public string Drive { get { return drive; } set { drive = value; } }
        public string Notes { get { return notes; } set { notes = value; } }
        public int Price { get { return price; } set { price = value; } }
        public string Body { get { return body; } set { body = value; } }


        public Deal() { }

        public Deal(/*int id,*/ string name, int engine, int odometet, string fuel, string color, string body, string drive, string notes, int price)
        {
            //this.id = id;
            this.name = name;
            this.engine = engine;
            this.odometet = odometet;
            this.fuel = fuel;
            this.color = color;
            this.body = body;
            this.drive = drive;
            this.notes = notes;
            this.price = price;
               
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client_server_app
{
    class GPU
    {
        private int id;
        private string model;
        private int performance;
        private int price;

        public GPU()
        {
            id = 0;
            model = "";
            performance = 0;
            price = 0;
        }

        public GPU(int Id, string Model, int Performance, int Price)
        {
            id = Id;
            model = Model;
            performance = Performance;
            price = Price;
        }

        public int Id {
            set{ id = value; }
            get{ return id; }
        }

        public string Model
        {
            set { model = value; }
            get { return model; }
        }

        public int Performance
        {
            set { performance = value; }
            get { return performance; }
        }

        public int Price
        {
            set { price = value; }
            get { return price; }
        }

        public string GpuToString()
        {
            return "ID:" + id + "model: " + model + ". performance: " + performance + ". price: " + price + ".";
        }
    }
}

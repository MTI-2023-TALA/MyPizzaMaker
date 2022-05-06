using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontPizza.Model
{
    public class MainViewModel
    {
        public int Id { get; set; }

        public MainViewModel(int id)
        {
            this.Id = id;
        }
    }
}

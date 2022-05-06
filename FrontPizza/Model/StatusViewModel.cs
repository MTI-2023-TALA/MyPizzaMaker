using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontPizza.Model
{
    public class StatusViewModel
    {
        public int Id { get; set; }

        public StatusViewModel(int id)
        {
            Id = id;    
        }
    }
}

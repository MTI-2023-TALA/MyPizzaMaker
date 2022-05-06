using backend.Dto;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BackOffice.ViewModal
{
    public class StatsModel : ObservableObject
    {
        public StatService _statService;
        private int daily;
        private int weekly;
        private int monthly;

        public int Daily
        {
            get { return daily; }
            set { SetProperty(ref daily, value); }
        }
        public int Weekly
        {
            get { return weekly; }
            set { SetProperty(ref weekly, value); }
        }
        public int Monthly
        {
            get { return monthly; }
            set { SetProperty(ref monthly, value); }
        }
        public ObservableRangeCollection<IngredientStat> Ingredients { get; set; }

        public async Task LoadStats()
        {
            Daily = await _statService.GetStats("daily");
            Weekly = await _statService.GetStats("weekly");
            Monthly = await _statService.GetStats("monthly");
            var tmp = await _statService.GetIngredientsStats();
            tmp = tmp.OrderByDescending(i => i.count).ToList();
            Ingredients.ReplaceRange(tmp);
        }

        public StatsModel(StatService statService)
        {
            _statService = statService;
            Daily = 5;
            Weekly = 4;
            Monthly = 3;
            Ingredients = new ObservableRangeCollection<IngredientStat>();
        }
    }
}

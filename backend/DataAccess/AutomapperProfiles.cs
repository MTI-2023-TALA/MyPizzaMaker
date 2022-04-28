using AutoMapper;

namespace backend.DataAccess
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<Dbo.Cart, EfModels.Cart>();
            CreateMap<EfModels.Cart, Dbo.Cart>();


            CreateMap<Dbo.Pizza, EfModels.Pizza>();
            CreateMap<EfModels.Pizza, Dbo.Pizza>();

            CreateMap<Dbo.Ingredient, EfModels.Ingredient>();
            CreateMap<EfModels.Ingredient, Dbo.Ingredient>();
            CreateMap<Dto.Ingredient, Dbo.Ingredient>();
            CreateMap<Dbo.Ingredient, Dto.Ingredient>();



            CreateMap<Dbo.CartsPizza, EfModels.CartsPizza>();
            CreateMap<EfModels.CartsPizza, Dbo.CartsPizza>();

            CreateMap<Dbo.PizzasIngredient, EfModels.PizzasIngredient>();
            CreateMap<EfModels.PizzasIngredient, Dbo.PizzasIngredient>();
        }
    }
}

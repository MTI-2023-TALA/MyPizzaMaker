using AutoMapper;

namespace backend.DataAccess
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<Dto.Cart, EfModels.Cart>();
            CreateMap<EfModels.Cart, Dto.Cart>();

            CreateMap<Dto.Pizza, EfModels.Pizza>();
            CreateMap<EfModels.Pizza, Dto.Pizza>();

            CreateMap<Dto.Ingredient, EfModels.Ingredient>();
            CreateMap<EfModels.Ingredient, Dto.Ingredient>();

            CreateMap<Dto.CartsPizza, EfModels.CartsPizza>();
            CreateMap<EfModels.CartsPizza, Dto.CartsPizza>();

            CreateMap<Dto.PizzasIngredient, EfModels.PizzasIngredient>();
            CreateMap<EfModels.PizzasIngredient, Dto.PizzasIngredient>();
        }
    }
}

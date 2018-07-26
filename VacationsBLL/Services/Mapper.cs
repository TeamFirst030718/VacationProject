using AutoMapper;

namespace VacationsBLL.Services
{
    public static class Mapper
    {
        public static TypeToMapTo Map<TypeToMapFrom, TypeToMapTo>(TypeToMapFrom model)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TypeToMapFrom, TypeToMapTo>()).CreateMapper();

            return mapper.Map<TypeToMapFrom, TypeToMapTo>(model);
        }

        public static TypeToMapTo[] MapCollection<TypeToMapFrom, TypeToMapTo>(TypeToMapFrom[] model)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TypeToMapFrom, TypeToMapTo>()).CreateMapper();

            return mapper.Map<TypeToMapFrom[], TypeToMapTo[]>(model);
        }
    }
}

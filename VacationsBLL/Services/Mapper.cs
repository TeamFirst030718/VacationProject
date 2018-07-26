using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationsBLL.Interfaces;

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

        public static IEnumerable<TypeToMapTo> MapCollection<TypeToMapFrom, TypeToMapTo>(IEnumerable<TypeToMapFrom> model)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TypeToMapFrom, TypeToMapTo>()).CreateMapper();

            return mapper.Map<IEnumerable<TypeToMapFrom>, IEnumerable<TypeToMapTo>>(model);
        }
    }
}

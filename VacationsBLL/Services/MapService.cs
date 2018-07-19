using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationsBLL.Interfaces;

namespace VacationsBLL.Services
{
    public class MapService : IMapService
    {
        public TypeToMappTo Map<TypeToMappFrom, TypeToMappTo>(TypeToMappFrom model)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TypeToMappFrom, TypeToMappTo>()).CreateMapper();

            return mapper.Map<TypeToMappFrom, TypeToMappTo>(model);
        }
    }
}

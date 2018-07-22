using System.Collections.Generic;

namespace VacationsBLL.Interfaces
{
    public interface IMapService
    {
        TypeToMapTo Map<TypeToMapFrom, TypeToMapTo>(TypeToMapFrom model);
        IEnumerable<TypeToMapTo> MapCollection<TypeToMapFrom, TypeToMapTo>(IEnumerable<TypeToMapFrom> model);
    }
}
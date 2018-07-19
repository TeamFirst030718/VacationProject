namespace VacationsBLL.Interfaces
{
    public interface IMapService
    {
        TypeToMappTo Map<TypeToMappFrom, TypeToMappTo>(TypeToMappFrom model);
    }
}
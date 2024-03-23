using FormulaOne.Entities.DbSet;
using FormulaOne.Entities.Dtos.Requests.Achievement;
using FormulaOne.Entities.Dtos.Requests.Driver;
using FormulaOne.Entities.Dtos.Responses.Achievement;
using FormulaOne.Entities.Dtos.Responses.Driver;
using Mapster;

namespace FormulaOne.Api.MappingProfiles;

public static class MapsterConfig
{
    public static void RegisterMapsterConfiguration(this IServiceCollection services)
    {
        TypeAdapterConfig<CreateDriverAchievementRequest, Achievement>
            .NewConfig()
            .Map(dest => dest.RaceWin, src => src.Wins)
            .Map(dest => dest.Status, src => 1)
            .Map(dest => dest.AddedAt, src => DateTime.UtcNow)
            .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow);

        TypeAdapterConfig<UpdateDriverAchievementRequest, Achievement>
            .NewConfig()
            .Map(dest => dest.Id, src => src.AchievementId)
            .Map(dest => dest.RaceWin, src => src.Wins)
            .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow);



        TypeAdapterConfig<Achievement, DriverAchievementResponse>
            .NewConfig()
            .Map(dest => dest.AchievementId, src => src.Id)
            .Map(dest => dest.Wins, src => src.RaceWin);

        //////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////

        TypeAdapterConfig<CreateDriverRequest, Driver>
            .NewConfig()
            .Map(dest => dest.Status, src => 1)
            .Map(dest => dest.AddedAt, src => DateTime.UtcNow)
            .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow);

        TypeAdapterConfig<UpdateDriverRequest, Driver>
            .NewConfig()
            .Map(dest => dest.Id, src => src.DriverId)
            .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow);


        TypeAdapterConfig<Driver, DriverResponse>
            .NewConfig()
            .Map(dest => dest.DriverId, src => src.Id)
            .Map(dest => dest.FullName, src => src.FirstName + " " + src.LastName);
    }
}

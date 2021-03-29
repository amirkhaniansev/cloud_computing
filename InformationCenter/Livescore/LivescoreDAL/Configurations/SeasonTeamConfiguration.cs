using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LivescoreDAL.Constants;
using LivescoreDAL.Models;

namespace LivescoreDAL.Configurations
{
    internal class SeasonTeamConfiguration : IEntityTypeConfiguration<SeasonTeam>
    {
        public void Configure(EntityTypeBuilder<SeasonTeam> builder)
        {
            builder.ToTable(Tables.SeasonTeam);
            builder.HasKey(st => new
            {
                st.SeasonId,
                st.TeamId
            });
        }
    }
}
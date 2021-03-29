using System;

namespace LivescoreDAL.Description
{
    public interface IDalFactory : IDisposable
    {
        IMatchDAL GetMatchDAL();

        ISeasonDAL GetSeasonDAL();

        ISportDAL GetSportDAL();

        ITeamDAL GetTeamDAL();
    }
}
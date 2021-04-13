using System;

namespace LivescoreDAL.Description
{
    public interface IDalFactory : IDisposable
    {
        IMatchDAL GetMatchDAL();

        IMatchDAL GetMatchDAL(IBaseDAL parent);

        ISeasonDAL GetSeasonDAL();

        ISeasonDAL GetSeasonDAL(IBaseDAL parent);

        ISportDAL GetSportDAL();

        ISportDAL GetSportDAL(IBaseDAL parent);

        ITeamDAL GetTeamDAL();

        ITeamDAL GetTeamDAL(IBaseDAL parent);
    }
}
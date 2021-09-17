using CartoPrime.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CartoPrime.Data
{
    public class CADatabase
    {
        readonly SQLiteAsyncConnection _database;

        public CADatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<TeamCA>().Wait();
            _database.CreateTableAsync<TeamFull>().Wait();
            _database.CreateTableAsync<AthleteCA>().Wait();
            _database.CreateTableAsync<Market>().Wait();
            _database.CreateTableAsync<UserCA>().Wait();
            _database.CreateTableAsync<TimesBonsDeBico>().Wait();
        }

        #region TeamCA

        public Task<List<TeamCA>> GetTeamCAsAsync()
        {
            return _database.Table<TeamCA>().ToListAsync();
        }

        public Task<TeamCA> GetTeamCAAsync(int id)
        {
            return _database.Table<TeamCA>()
                            .Where(i => i.time_id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveTeamCAAsync(TeamCA TeamCA)
        {
            if (TeamCA.time_id != 0)
            {
                return _database.UpdateAsync(TeamCA);
            }
            else
            {
                return _database.InsertAsync(TeamCA);
            }
        }

        public Task<int> InsertTeamCAAsync(TeamCA TeamCA)
        {
            return _database.InsertOrReplaceAsync(TeamCA);
        }

        public Task<int> DeleteTeamCAAsync(TeamCA TeamCA)
        {
            return _database.DeleteAsync(TeamCA);
        }
        #endregion

        #region TeamFull

        public Task<List<TeamFull>> GetTeamFullsAsync()
        {
            return _database.Table<TeamFull>().ToListAsync();
        }

        public Task<TeamFull> GetTeamFullAsync(int id)
        {
            return _database.Table<TeamFull>()
                            .Where(i => i.time_id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveTeamCAAsync(TeamFull obj)
        {
            if (obj.time_id != 0)
            {
                return _database.UpdateAsync(obj);
            }
            else
            {
                return _database.InsertAsync(obj);
            }
        }

        public Task<int> InsertTeamCAAsync(TeamFull obj)
        {
            return _database.InsertOrReplaceAsync(obj);
        }

        public Task<int> DeleteTeamCAAsync(TeamFull obj)
        {
            return _database.DeleteAsync(obj);
        }
        #endregion

        #region AthleteCA

        public Task<List<AthleteCA>> GetAthleteCAsAsync()
        {
            return _database.Table<AthleteCA>().ToListAsync();
        }
        public Task<List<AthleteCA>> GetAthleteCAByTimeAsync(int idTime)
        {
            var ls = _database.Table<AthleteCA>()
                            .Where(i => i.IdTime == idTime).ToListAsync();
            return ls;
        }

        public Task<AthleteCA> GetAthleteCAAsync(int id)
        {
            return _database.Table<AthleteCA>()
                            .Where(i => i.IdAleta == id)
                            .FirstOrDefaultAsync();
        }


        public Task<int> SaveAthleteCAAsync(AthleteCA AthleteCA)
        {
            if (AthleteCA.IdAleta != 0)
            {
                return _database.UpdateAsync(AthleteCA);
            }
            else
            {
                return _database.InsertAsync(AthleteCA);
            }
        }

        public Task<int> InsertAthleteCAAsync(AthleteCA AthleteCA)
        {
            return _database.InsertOrReplaceAsync(AthleteCA);
        }

        public Task<int> DeleteAthleteCAAsync(AthleteCA AthleteCA)
        {
            return _database.DeleteAsync(AthleteCA);
        }
        public Task<int> DeleteAthleteCAAllAsync()
        {
            return _database.DeleteAllAsync<AthleteCA>();
        }
        #endregion

        #region Market

        public Task<List<Market>> GetMarketsAsync()
        {
            return _database.Table<Market>().ToListAsync();
        }

        public Task<Market> GetMarketAsync(int id)
        {
            return _database.Table<Market>()
                            .Where(i => i.rodada_atual == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveMarketAsync(Market Market)
        {
            if (Market.rodada_atual != 0)
            {
                return _database.UpdateAsync(Market);
            }
            else
            {
                return _database.InsertAsync(Market);
            }
        }

        public Task<int> InsertMarketAsync(Market Market)
        {
            return _database.InsertOrReplaceAsync(Market);
        }

        public Task<int> DeleteMarketAsync(Market Market)
        {
            return _database.DeleteAsync(Market);
        }
        #endregion

        #region User

        public Task<List<UserCA>> GetUsersCAAsync()
        {
            return _database.Table<UserCA>().ToListAsync();
        }

        public Task<UserCA> GetUserCAAsync(int id)
        {
            return _database.Table<UserCA>()
                            .Where(i => i.id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveUserCAAsync(UserCA user)
        {
            if (user.id != 0)
            {
                return _database.UpdateAsync(user);
            }
            else
            {
                return _database.InsertAsync(user);
            }
        }

        public Task<int> InsertUserCAAsync(UserCA user)
        {
            return _database.InsertOrReplaceAsync(user);
        }

        public Task<int> DeleteUserCAAsync(UserCA user)
        {
            return _database.DeleteAsync(user);
        }
        #endregion

        #region TimeBonsDeBico
        public Task<int> InsertTimeBonsDeBico(TimesBonsDeBico _timeSimples)
        {
            return _database.InsertOrReplaceAsync(_timeSimples);
        }

        public Task<int> DeleteTimeBonsDeBico(TimesBonsDeBico _timeSimples)
        {
            return _database.DeleteAsync(_timeSimples);
        }

        public Task<TimesBonsDeBico> ObterTimeBonsDeBicoPorId(int id)
        {
            return _database.Table<TimesBonsDeBico>().Where(c => c.time_id == id).FirstOrDefaultAsync();
        }

        public void UpdateTimeBonsDeBico(TimesBonsDeBico _timeSimples)
        {
            _database.UpdateAsync(_timeSimples);
        }
        public Task<List<TimesBonsDeBico>> GetTimeBonsDeBicoAsync()
        {
            return _database.Table<TimesBonsDeBico>().ToListAsync();
        }
        #endregion
    }
}
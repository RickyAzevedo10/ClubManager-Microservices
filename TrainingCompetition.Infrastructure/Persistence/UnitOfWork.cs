using TrainingCompetition.Domain.Entities;
using TrainingCompetition.Domain.Interfaces.Repositories;
using TrainingCompetition.Domain.Interfaces.Repositories.Identity;
using TrainingCompetition.Infra.Contexts;
using TrainingCompetition.Infrastructure.Repositories;

namespace TrainingCompetition.Infra.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        

        //TrainingCompetition
        public IMatchRepository _matchRepository { get; private set; }
        public IMatchStatisticRepository _matchStatisticRepository { get; private set; }
        public ITrainingAttendanceRepository _trainingAttendanceRepository { get; private set; }
        public ITrainingSessionRepository _trainingSessionRepository { get; private set; }
        public IBaseRepository<Player> _playerRepository { get; private set; }
        public IBaseRepository<Team> _teamRepository { get; private set; }

        public async Task<bool> CommitAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        #region TrainingCompetition
        public IMatchRepository MatchRepository
        {
            get
            {
                if (_matchRepository == null)
                {
                    _matchRepository = new MatchRepository(_context);
                }
                return _matchRepository;
            }
        }

        public IMatchStatisticRepository MatchStatisticRepository
        {
            get
            {
                if (_matchStatisticRepository == null)
                {
                    _matchStatisticRepository = new MatchStatisticRepository(_context);
                }
                return _matchStatisticRepository;
            }
        }

        public ITrainingAttendanceRepository TrainingAttendanceRepository
        {
            get
            {
                if (_trainingAttendanceRepository == null)
                {
                    _trainingAttendanceRepository = new TrainingAttendanceRepository(_context);
                }
                return _trainingAttendanceRepository;
            }
        }

        public ITrainingSessionRepository TrainingSessionRepository
        {
            get
            {
                if (_trainingSessionRepository == null)
                {
                    _trainingSessionRepository = new TrainingSessionRepository(_context);
                }
                return _trainingSessionRepository;
            }
        }

        public IBaseRepository<Player> PlayerRepository
        {
            get
            {
                if (_playerRepository == null)
                {
                    _playerRepository = new BaseRepository<Player>(_context);
                }
                return _playerRepository;
            }
        }

        public IBaseRepository<Team> TeamRepository
        {
            get
            {
                if (_teamRepository == null)
                {
                    _teamRepository = new BaseRepository<Team>(_context);
                }
                return _teamRepository;
            }
        }

        #endregion

    }
}

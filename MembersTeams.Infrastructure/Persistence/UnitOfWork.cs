using MembersTeams.Domain.Entities;
using MembersTeams.Domain.Interfaces.Repositories;
using MembersTeams.Domain.Interfaces.Repositories.Identity;
using MembersTeams.Infra.Contexts;
using MembersTeams.Infrastructure.Repositories;

namespace MembersTeams.Infra.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }


        //MembersTeams
        public IPlayerRepository _playerRepository { get; private set; }
        public IBaseRepository<PlayerCategory> _playerCategoryRepository { get; private set; }
        public IPlayerPerformanceHistoryRepository _playerPerformanceHistoryRepository { get; private set; }
        public IClubMemberRepository _clubMemberRepository { get; private set; }
        public IMinorClubMemberRepository _minorClubMemberRepository { get; private set; }
        public IBaseRepository<PlayerResponsible> _playerResponsibleRepository { get; private set; }
        public IPlayerContractRepository _playerContractRepository { get; private set; }
        public IPlayerTransferRepository _playerTransferRepository { get; private set; }
        public ITeamRepository _teamRepository { get; private set; }
        public IBaseRepository<TeamCategory> _teamCategoryRepository { get; private set; }
        public IBaseRepository<TeamPlayer> _teamPlayerRepository { get; private set; }
        public IBaseRepository<TeamCoach> _teamCoachRepository { get; private set; }
        public IUserClubMemberRepository _userClubMemberRepository { get; private set; }
        public IBaseRepository<User> _userRepository { get; private set; }
        public IBaseRepository<Institution> _institutionRepository { get; private set; }



        public async Task<bool> CommitAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }


        #region MembersTeams
        public IPlayerRepository PlayerRepository
        {
            get
            {
                if (_playerRepository == null)
                {
                    _playerRepository = new PlayerRepository(_context);
                }
                return _playerRepository;
            }
        }

        public IBaseRepository<PlayerCategory> PlayerCategoryRepository
        {
            get
            {
                if (_playerCategoryRepository == null)
                {
                    _playerCategoryRepository = new BaseRepository<PlayerCategory>(_context);
                }
                return _playerCategoryRepository;
            }
        }

        public IPlayerPerformanceHistoryRepository PlayerPerformanceHistoryRepository
        {
            get
            {
                if (_playerPerformanceHistoryRepository == null)
                {
                    _playerPerformanceHistoryRepository = new PlayerPerformanceHistoryRepository(_context);
                }
                return _playerPerformanceHistoryRepository;
            }
        }

        public IClubMemberRepository ClubMemberRepository
        {
            get
            {
                if (_clubMemberRepository == null)
                {
                    _clubMemberRepository = new ClubMemberRepository(_context);
                }
                return _clubMemberRepository;
            }
        }

        public IMinorClubMemberRepository MinorClubMemberRepository
        {
            get
            {
                if (_minorClubMemberRepository == null)
                {
                    _minorClubMemberRepository = new MinorClubMemberRepository(_context);
                }
                return _minorClubMemberRepository;
            }
        }

        public IBaseRepository<PlayerResponsible> PlayerResponsibleRepository
        {
            get
            {
                if (_playerResponsibleRepository == null)
                {
                    _playerResponsibleRepository = new BaseRepository<PlayerResponsible>(_context);
                }
                return _playerResponsibleRepository;
            }
        }

        public IPlayerContractRepository PlayerContractRepository
        {
            get
            {
                if (_playerContractRepository == null)
                {
                    _playerContractRepository = new PlayerContractRepository(_context);
                }
                return _playerContractRepository;
            }
        }

        public IPlayerTransferRepository PlayerTransferRepository
        {
            get
            {
                if (_playerTransferRepository == null)
                {
                    _playerTransferRepository = new PlayerTransferRepository(_context);
                }
                return _playerTransferRepository;
            }
        }

        public ITeamRepository TeamRepository
        {
            get
            {
                if (_teamRepository == null)
                {
                    _teamRepository = new TeamRepository(_context);
                }
                return _teamRepository;
            }
        }

        public IBaseRepository<TeamCategory> TeamCategoryRepository
        {
            get
            {
                if (_teamCategoryRepository == null)
                {
                    _teamCategoryRepository = new BaseRepository<TeamCategory>(_context);
                }
                return _teamCategoryRepository;
            }
        }

        public IBaseRepository<TeamPlayer> TeamPlayerRepository
        {
            get
            {
                if (_teamPlayerRepository == null)
                {
                    _teamPlayerRepository = new BaseRepository<TeamPlayer>(_context);
                }
                return _teamPlayerRepository;
            }
        }

        public IBaseRepository<TeamCoach> TeamCoachRepository
        {
            get
            {
                if (_teamCoachRepository == null)
                {
                    _teamCoachRepository = new BaseRepository<TeamCoach>(_context);
                }
                return _teamCoachRepository;
            }
        }

        public IUserClubMemberRepository UserClubMemberRepository
        {
            get
            {
                if (_userClubMemberRepository == null)
                {
                    _userClubMemberRepository = new UserClubMemberRepository(_context);
                }
                return _userClubMemberRepository;
            }
        }

        public IBaseRepository<User> UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new BaseRepository<User>(_context);
                }
                return _userRepository;
            }
        }

        public IBaseRepository<Institution> InstitutionRepository
        {
            get
            {
                if (_institutionRepository == null)
                {
                    _institutionRepository = new BaseRepository<Institution>(_context);
                }
                return _institutionRepository;
            }
        }
        #endregion


    }
}

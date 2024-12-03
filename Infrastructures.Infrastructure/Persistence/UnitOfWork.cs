using Infrastructures.Domain.Entities;
using Infrastructures.Domain.Interfaces.Repositories;
using Infrastructures.Domain.Interfaces.Repositories.Identity;
using Infrastructures.Infra.Contexts;
using Infrastructures.Infrastructure.Repositories;

namespace Infrastructures.Infra.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        //Infrastructures
        public IBaseRepository<FacilityCategory> _facilityCategoryRepository { get; private set; }
        public IBaseRepository<Facility> _facilityRepository { get; private set; }
        public IBaseRepository<FacilityReservation> _facilityReservationRepository { get; private set; }
        public IBaseRepository<MaintenanceRequest> _maintenanceRequestRepository { get; private set; }
        public IMaintenanceHistoryRepository _maintenanceHistoryRepository { get; private set; }
        public IBaseRepository<User> _userRepository { get; private set; }



        public async Task<bool> CommitAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        #region Infrastructures
        public IBaseRepository<FacilityCategory> FacilityCategoryRepository
        {
            get
            {
                if (_facilityCategoryRepository == null)
                {
                    _facilityCategoryRepository = new BaseRepository<FacilityCategory>(_context);
                }
                return _facilityCategoryRepository;
            }
        }

        public IBaseRepository<Facility> FacilityRepository
        {
            get
            {
                if (_facilityRepository == null)
                {
                    _facilityRepository = new BaseRepository<Facility>(_context);
                }
                return _facilityRepository;
            }
        }

        public IBaseRepository<FacilityReservation> FacilityReservationRepository
        {
            get
            {
                if (_facilityReservationRepository == null)
                {
                    _facilityReservationRepository = new BaseRepository<FacilityReservation>(_context);
                }
                return _facilityReservationRepository;
            }
        }

        public IBaseRepository<MaintenanceRequest> MaintenanceRequestRepository
        {
            get
            {
                if (_maintenanceRequestRepository == null)
                {
                    _maintenanceRequestRepository = new BaseRepository<MaintenanceRequest>(_context);
                }
                return _maintenanceRequestRepository;
            }
        }

        public IMaintenanceHistoryRepository MaintenanceHistoryRepository
        {
            get
            {
                if (_maintenanceHistoryRepository == null)
                {
                    _maintenanceHistoryRepository = new MaintenanceHistoryRepository(_context);
                }
                return _maintenanceHistoryRepository;
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
        #endregion

    }
}

using System.Collections.Generic;
using System.Linq;
using LifeManager.Server.Database;
using LifeManager.Server.Model.Domain;
using LifeManager.Server.Model.Entity;
using LifeManager.Server.Model.Mapper;
using LifeManager.Server.Service.Implementation.Tool;

namespace LifeManager.Server.Service.Implementation {
    public class AppointmentService : IAppointmentService {
        //== attributes =============================================================================================================================
        
        private readonly ILifeManagerRepository _lifeManagerRepository;

        private readonly IModelServiceTools _modelServiceTools;
        
        private readonly AppointmentMapper _appointmentMapper = new AppointmentMapper();
        
        //== init ===================================================================================================================================
        
        public AppointmentService(ILifeManagerRepository lifeManagerRepository, IModelServiceTools modelServiceTools) {
            _lifeManagerRepository = lifeManagerRepository;
            _modelServiceTools = modelServiceTools;
        }
        
        //== methods ================================================================================================================================

        public IEnumerable<Appointment> GetAll() {
            return _modelServiceTools.AllEntitiesForLoggedInUser<AppointmentEntity>().Select(entity => _appointmentMapper.ToDomain(entity));
        }

        public Appointment GetById(long id) {
            AppointmentEntity entity = _lifeManagerRepository.LoadEntity<AppointmentEntity>(id);

            if (entity == null) {
                return null;
            }

            return _appointmentMapper.ToDomain(entity);
        }

        public void Create(Appointment domain) {
            _modelServiceTools.InitialiseNewItem(domain);
            
            _lifeManagerRepository.SaveEntity(_appointmentMapper.ToEntity(domain));
        }

        public void Update(Appointment domain) {
            _modelServiceTools.UpdateProcessing<AppointmentEntity>(domain);
            
            _lifeManagerRepository.SaveEntity(_appointmentMapper.ToEntity(domain));
        }

        public void Remove(long id) {
            _modelServiceTools.RemoveEntity<AppointmentEntity>(id);
        }
    }
}
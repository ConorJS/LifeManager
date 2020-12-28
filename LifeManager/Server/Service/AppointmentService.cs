using System;
using LifeManager.Server.Database;
using LifeManager.Server.Database.Entities;
using LifeManager.Server.Domain;
using LifeManager.Server.Domain.Mapper;

namespace LifeManager.Server.Service {
    public class AppointmentService : IAppointmentService {
        private readonly ILifeManagerRepository _lifeManagerRepository;
        
        public AppointmentService(ILifeManagerRepository lifeManagerRepository) {
            _lifeManagerRepository = lifeManagerRepository;
        }
        
        public Appointment GetById(long id) {
            AppointmentEntity entity = _lifeManagerRepository.LoadAppointment(id);

            if (entity == null) {
                return null;
            }

            return new AppointmentMapper().ToDomain(entity);
        }

        public void Create(Appointment domain) {
            domain.DateTimeCreated = DateTime.Now;
            domain.DateTimeLastModified = DateTime.Now;
            
            _lifeManagerRepository.SaveAppointment(new AppointmentMapper().ToEntity(domain));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using LifeManager.Server.Database;
using LifeManager.Server.Database.Entities;
using LifeManager.Server.Domain;
using LifeManager.Server.Domain.Mapper;

namespace LifeManager.Server.Service {
    public class AppointmentService : IAppointmentService {
        private readonly ILifeManagerRepository _lifeManagerRepository;
        
        private readonly AppointmentMapper _appointmentMapper = new AppointmentMapper();
        
        public AppointmentService(ILifeManagerRepository lifeManagerRepository) {
            _lifeManagerRepository = lifeManagerRepository;
        }

        // TODO: Should be GetAllForUser(userId)
        public IEnumerable<Appointment> GetAll() {
            List<AppointmentEntity> entities = _lifeManagerRepository.LoadAppointments();

            return entities.Select(entity => _appointmentMapper.ToDomain(entity));
        }

        public Appointment GetById(long id) {
            AppointmentEntity entity = _lifeManagerRepository.LoadAppointment(id);

            if (entity == null) {
                return null;
            }

            return _appointmentMapper.ToDomain(entity);
        }

        public void Create(Appointment domain) {
            domain.DateTimeCreated = DateTime.Now;
            domain.DateTimeLastModified = DateTime.Now;

            _lifeManagerRepository.SaveAppointment(_appointmentMapper.ToEntity(domain));
        }

        public void Update(Appointment domain) {
            if (_lifeManagerRepository.LoadAppointment(domain.Id) == null) {
                throw new InvalidOperationException(
                    $"Tried to update a AppointmentEntity (id = {domain.Id}), but the task doesn't exist. " +
                    $"This could indicate a misuse of save resources/service endpoints.");
            }

            domain.DateTimeLastModified = DateTime.Now;
            _lifeManagerRepository.SaveAppointment(_appointmentMapper.ToEntity(domain));
        }

        public void Remove(long id) {
            AppointmentEntity appointmentEntity = _lifeManagerRepository.LoadAppointment(id);
            if (appointmentEntity == null) {
                throw new InvalidOperationException(
                    $"Tried to remove a AppointmentEntity (id = {id}), but the task doesn't exist. " +
                    $"This could indicate a misuse of save resources/service endpoints.");
            }

            _lifeManagerRepository.RemoveAppointment(appointmentEntity);
        }
    }
}
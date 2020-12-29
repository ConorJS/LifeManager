using System.Collections.Generic;
using System.Linq;
using LifeManager.Server.Model;
using LifeManager.Server.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace LifeManager.Server.Database.Implementation {
    public class LifeManagerRepository : ILifeManagerRepository {
        //== general ================================================================================================================================

        private readonly LifeManagerDatabaseContext _dbContext;

        public LifeManagerRepository(LifeManagerDatabaseContext lifeManagerDatabaseContext) {
            _dbContext = lifeManagerDatabaseContext;
        }

        public void Dispose() { }

        private void Detach<T>(T entity) where T : class, IItemEntity {
            _dbContext.Entry(entity).State = EntityState.Detached;
        }

        //== dummy ==================================================================================================================================

        public DummyDataEntity LoadDummyData(long id) {
            return _dbContext.Dummy.Find(id);
        }

        public void SaveDummyData(DummyDataEntity dummyDataEntity) {
            _dbContext.Dummy.Update(dummyDataEntity);
            _dbContext.SaveChanges();
        }

        //== appointment ============================================================================================================================

        // TODO: Should only retrieve for a certain user
        public List<AppointmentEntity> LoadAppointments() {
            List<AppointmentEntity> entities = _dbContext.Appointment.ToList();
            entities.ForEach(Detach);

            return entities;
        }

        public AppointmentEntity LoadAppointment(long id) {
            AppointmentEntity entity = _dbContext.Appointment.Find(id);
            if (entity == null) {
                return null;
            }

            Detach(entity);
            return entity;
        }

        public void SaveAppointment(AppointmentEntity entity) {
            _dbContext.Appointment.Update(entity);
            _dbContext.SaveChanges();
        }

        public void RemoveAppointment(AppointmentEntity entity) {
            _dbContext.Appointment.Remove(entity);
            _dbContext.SaveChanges();
        }

        //== chore ==================================================================================================================================

        // TODO: Should only retrieve for a certain user
        public List<ChoreEntity> LoadChores() {
            List<ChoreEntity> entities = _dbContext.Chore.ToList();
            entities.ForEach(Detach);

            return entities;
        }

        public ChoreEntity LoadChore(long id) {
            ChoreEntity entity = _dbContext.Chore.Find(id);
            if (entity == null) {
                return null;
            }

            Detach(entity);
            return entity;
        }

        public void SaveChore(ChoreEntity entity) {
            _dbContext.Chore.Update(entity);
            _dbContext.SaveChanges();
        }

        public void RemoveChore(ChoreEntity entity) {
            _dbContext.Chore.Remove(entity);
            _dbContext.SaveChanges();
        }

        //== leisure activity========================================================================================================================

        // TODO: Should only retrieve for a certain user
        public List<LeisureActivityEntity> LoadLeisureActivities() {
            List<LeisureActivityEntity> entities = _dbContext.LeisureActivity.ToList();
            entities.ForEach(Detach);

            return entities;
        }

        public LeisureActivityEntity LoadLeisureActivity(long id) {
            LeisureActivityEntity entity = _dbContext.LeisureActivity.Find(id);
            if (entity == null) {
                return null;
            }

            Detach(entity);
            return entity;
        }

        public void SaveLeisureActivity(LeisureActivityEntity entity) {
            _dbContext.LeisureActivity.Update(entity);
            _dbContext.SaveChanges();
        }

        public void RemoveLeisureActivity(LeisureActivityEntity entity) {
            _dbContext.LeisureActivity.Remove(entity);
            _dbContext.SaveChanges();
        }

        //== principle ==============================================================================================================================

        // TODO: Should only retrieve for a certain user
        public List<PrincipleEntity> LoadPrinciples() {
            List<PrincipleEntity> entities = _dbContext.Principle.ToList();
            entities.ForEach(Detach);

            return entities;
        }

        public PrincipleEntity LoadPrinciple(long id) {
            PrincipleEntity entity = _dbContext.Principle.Find(id);
            if (entity == null) {
                return null;
            }

            Detach(entity);
            return entity;
        }

        public void SavePrinciple(PrincipleEntity toDoTaskEntity) {
            _dbContext.Principle.Update(toDoTaskEntity);
            _dbContext.SaveChanges();
        }

        public void RemovePrinciple(PrincipleEntity toDoTaskEntity) {
            _dbContext.Principle.Remove(toDoTaskEntity);
            _dbContext.SaveChanges();
        }

        //== recurring task =========================================================================================================================

        // TODO: Should only retrieve for a certain user
        public List<RecurringTaskEntity> LoadRecurringTasks() {
            List<RecurringTaskEntity> entities = _dbContext.RecurringTask.ToList();
            entities.ForEach(Detach);

            return entities;
        }

        public RecurringTaskEntity LoadRecurringTask(long id) {
            RecurringTaskEntity entity = _dbContext.RecurringTask.Find(id);
            if (entity == null) {
                return null;
            }

            Detach(entity);
            return entity;
        }

        public void SaveRecurringTask(RecurringTaskEntity toDoTaskEntity) {
            _dbContext.RecurringTask.Update(toDoTaskEntity);
            _dbContext.SaveChanges();
        }

        public void RemoveRecurringTask(RecurringTaskEntity toDoTaskEntity) {
            _dbContext.RecurringTask.Remove(toDoTaskEntity);
            _dbContext.SaveChanges();
        }

        //== to do task =============================================================================================================================

        // TODO: Should only retrieve for a certain user
        public List<ToDoTaskEntity> LoadToDoTasks() {
            List<ToDoTaskEntity> entities = _dbContext.ToDoTask.ToList();
            entities.ForEach(Detach);

            return entities;
        }

        public ToDoTaskEntity LoadToDoTask(long id) {
            ToDoTaskEntity entity = _dbContext.ToDoTask.Find(id);
            if (entity == null) {
                return null;
            }

            Detach(entity);
            return entity;
        }

        public void SaveToDoTask(ToDoTaskEntity toDoTaskEntity) {
            _dbContext.ToDoTask.Update(toDoTaskEntity);
            _dbContext.SaveChanges();
        }

        public void RemoveToDoTask(ToDoTaskEntity toDoTaskEntity) {
            _dbContext.ToDoTask.Remove(toDoTaskEntity);
            _dbContext.SaveChanges();
        }
    }
}
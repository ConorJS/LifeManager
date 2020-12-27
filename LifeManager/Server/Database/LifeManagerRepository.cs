using System.Collections.Generic;
using System.Linq;
using LifeManager.Server.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace LifeManager.Server.Database {
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
            _dbContext.Dummy.Add(dummyDataEntity);
            _dbContext.SaveChanges();
        }
        
        //== appointment ============================================================================================================================

        public AppointmentEntity LoadAppointment(long id) {
            return _dbContext.Appointment.Find(id);
        }

        public void SaveAppointment(AppointmentEntity appointmentEntity) {
            _dbContext.Appointment.Add(appointmentEntity);
            _dbContext.SaveChanges();
        }
        
        //== chore ==================================================================================================================================

        public ChoreEntity LoadChore(long id) {
            return _dbContext.Chore.Find(id);
        }

        public void SaveChore(ChoreEntity choreEntity) {
            _dbContext.Chore.Add(choreEntity);
            _dbContext.SaveChanges();
        }
        
        //== leisure activity========================================================================================================================

        public LeisureActivityEntity LoadLeisureActivity(long id) {
            return _dbContext.LeisureActivity.Find(id);
        }

        public void SaveLeisureActivity(LeisureActivityEntity leisureActivityEntity) {
            _dbContext.LeisureActivity.Add(leisureActivityEntity);
            _dbContext.SaveChanges();
        }
        
        //== principle ==============================================================================================================================

        public PrincipleEntity LoadPrinciple(long id) {
            return _dbContext.Principle.Find(id);
        }

        public void SavePrinciple(PrincipleEntity principleEntity) {
            _dbContext.Principle.Add(principleEntity);
            _dbContext.SaveChanges();
        }
        
        //== recurring task =========================================================================================================================

        public RecurringTaskEntity LoadRecurringTask(long id) {
            return _dbContext.RecurringTask.Find(id);
        }

        public void SaveRecurringTask(RecurringTaskEntity recurringTaskEntity) {
            _dbContext.RecurringTask.Add(recurringTaskEntity);
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
using LifeManager.Server.Database.Entities;

namespace LifeManager.Server.Database {
    public class LifeManagerRepository : ILifeManagerRepository {
        //== configuration ==========================================================================================================================
        
        private readonly LifeManagerDatabaseContext _dbContext;

        public LifeManagerRepository(LifeManagerDatabaseContext lifeManagerDatabaseContext) {
            _dbContext = lifeManagerDatabaseContext;
        }

        public void Dispose() { }
        
        //== saves/loads ============================================================================================================================
        
        public DummyDataEntity LoadDummyData(long id) {
            return _dbContext.Dummy.Find(id);
        }

        public void SaveDummyData(DummyDataEntity dummyDataEntity) {
            _dbContext.Dummy.Add(dummyDataEntity);
            _dbContext.SaveChanges();
        }
        
        public AppointmentEntity LoadAppointment(long id) {
            return _dbContext.Appointment.Find(id);
        }

        public void SaveAppointment(AppointmentEntity appointmentEntity) {
            _dbContext.Appointment.Add(appointmentEntity);
            _dbContext.SaveChanges();
        }
        
        public ChoreEntity LoadChore(long id) {
            return _dbContext.Chore.Find(id);
        }

        public void SaveChore(ChoreEntity choreEntity) {
            _dbContext.Chore.Add(choreEntity);
            _dbContext.SaveChanges();
        }

        public LeisureActivityEntity LoadLeisureActivity(long id) {
            return _dbContext.LeisureActivity.Find(id);
        }

        public void SaveLeisureActivity(LeisureActivityEntity leisureActivityEntity) {
            _dbContext.LeisureActivity.Add(leisureActivityEntity);
            _dbContext.SaveChanges();
        }
        
        public PrincipleEntity LoadPrinciple(long id) {
            return _dbContext.Principle.Find(id);
        }

        public void SavePrinciple(PrincipleEntity principleEntity) {
            _dbContext.Principle.Add(principleEntity);
            _dbContext.SaveChanges();
        }
        
        public RecurringTaskEntity LoadRecurringTask(long id) {
            return _dbContext.RecurringTask.Find(id);
        }

        public void SaveRecurringTask(RecurringTaskEntity recurringTaskEntity) {
            _dbContext.RecurringTask.Add(recurringTaskEntity);
            _dbContext.SaveChanges();
        }
        
        public ToDoTaskEntity LoadToDoTask(long id) {
            return _dbContext.ToDoTask.Find(id);
        }

        public void SaveToDoTask(ToDoTaskEntity toDoTaskEntity) {
            _dbContext.ToDoTask.Add(toDoTaskEntity);
            _dbContext.SaveChanges();
        }
    }
}
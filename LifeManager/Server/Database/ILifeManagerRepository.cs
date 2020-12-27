using System;
using System.Collections.Generic;
using LifeManager.Server.Database.Entities;

namespace LifeManager.Server.Database {
    public interface ILifeManagerRepository : IDisposable {
        public DummyDataEntity LoadDummyData(long id);

        public void SaveDummyData(DummyDataEntity dummyDataEntity);

        public AppointmentEntity LoadAppointment(long id);

        public void SaveAppointment(AppointmentEntity appointmentEntity);

        public ChoreEntity LoadChore(long id);

        public void SaveChore(ChoreEntity choreEntity);

        public LeisureActivityEntity LoadLeisureActivity(long id);

        public void SaveLeisureActivity(LeisureActivityEntity leisureActivityEntity);

        public PrincipleEntity LoadPrinciple(long id);

        public void SavePrinciple(PrincipleEntity principleEntity);

        public RecurringTaskEntity LoadRecurringTask(long id);

        public void SaveRecurringTask(RecurringTaskEntity recurringTaskEntity);

        public List<ToDoTaskEntity> LoadToDoTasks();

        public ToDoTaskEntity LoadToDoTask(long id);

        public void SaveToDoTask(ToDoTaskEntity toDoTaskEntity);
        
        public void RemoveToDoTask(ToDoTaskEntity toDoTaskEntity);
    }
}
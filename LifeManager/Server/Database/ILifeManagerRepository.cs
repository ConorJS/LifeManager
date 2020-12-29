using System;
using System.Collections.Generic;
using LifeManager.Server.Model.Entity;

namespace LifeManager.Server.Database {
    public interface ILifeManagerRepository : IDisposable {
        //== dummy ==================================================================================================================================
        
        public DummyDataEntity LoadDummyData(long id);

        public void SaveDummyData(DummyDataEntity dummyDataEntity);
        
        //== appointment ============================================================================================================================

        public List<AppointmentEntity> LoadAppointments();
        
        public AppointmentEntity LoadAppointment(long id);

        public void SaveAppointment(AppointmentEntity appointmentEntity);
        
        public void RemoveAppointment(AppointmentEntity appointmentEntity);
        
        //== chore ==================================================================================================================================

        public List<ChoreEntity> LoadChores();
        
        public ChoreEntity LoadChore(long id);
        
        public void SaveChore(ChoreEntity choreEntity);
        
        public void RemoveChore(ChoreEntity choreEntity);
        
        //== leisure activity =======================================================================================================================

        public List<LeisureActivityEntity> LoadLeisureActivities();
        
        public LeisureActivityEntity LoadLeisureActivity(long id);

        public void SaveLeisureActivity(LeisureActivityEntity leisureActivityEntity);
        
        public void RemoveLeisureActivity(LeisureActivityEntity leisureActivityEntity);

        //== principle ==============================================================================================================================
        
        public List<PrincipleEntity> LoadPrinciples();
        
        public PrincipleEntity LoadPrinciple(long id);

        public void SavePrinciple(PrincipleEntity principleEntity);
        
        public void RemovePrinciple(PrincipleEntity principleEntity);
        
        //== recurring tasks ========================================================================================================================

        public List<RecurringTaskEntity> LoadRecurringTasks();
        
        public RecurringTaskEntity LoadRecurringTask(long id);

        public void SaveRecurringTask(RecurringTaskEntity recurringTaskEntity);
        
        public void RemoveRecurringTask(RecurringTaskEntity recurringTaskEntity);
        
        //== to do tasks ============================================================================================================================

        public List<ToDoTaskEntity> LoadToDoTasks();

        public ToDoTaskEntity LoadToDoTask(long id);

        public void SaveToDoTask(ToDoTaskEntity toDoTaskEntity);

        public void RemoveToDoTask(ToDoTaskEntity toDoTaskEntity);
    }
}
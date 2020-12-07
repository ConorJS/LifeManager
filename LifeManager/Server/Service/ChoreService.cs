using LifeManager.Server.Database;
using LifeManager.Server.Database.Entities;
using LifeManager.Server.Domain;
using LifeManager.Server.Domain.Mapper;

namespace LifeManager.Server.Service {
    public class ChoreService : IChoreService {
        private readonly ILifeManagerRepository _lifeManagerRepository;
        
        public ChoreService(ILifeManagerRepository lifeManagerRepository) {
            _lifeManagerRepository = lifeManagerRepository;
        }

        public Chore GetById(long id) {
            ChoreEntity entity = _lifeManagerRepository.LoadChore(id);

            if (entity == null) {
                return null;
            }

            return new ChoreMapper().ToDomain(entity);
        }

        public void Create(Chore domain) {
            _lifeManagerRepository.SaveChore(new ChoreMapper().ToEntity(domain));
        }
    }
}
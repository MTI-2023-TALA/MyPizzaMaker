using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace backend.DataAccess
{
    public class Repository<DBEntity, ModelEntity> : IRepository<DBEntity, ModelEntity>
        where DBEntity : class, new()
        where ModelEntity : class, Dto.IObjectWithId, new()
    {
        private DbSet<DBEntity> _set;
        protected EfModels.myPizzaMakerContext _context;
        protected ILogger _logger;
        protected readonly IMapper _mapper;

        public Repository(EfModels.myPizzaMakerContext context, ILogger logger, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
            _set = _context.Set<DBEntity>();
        }

        public virtual async Task<ModelEntity> GetOne(long id)
        {
            DBEntity dbEntity = await _set.FindAsync(id);
            if (dbEntity == null)
            {
                return null;
            }

            return _mapper.Map<ModelEntity>(dbEntity);
        }

        public virtual async Task<IEnumerable<ModelEntity>> Get(string includeTables = "")
        {
            try
            {
                List<DBEntity> query = null;
                if (String.IsNullOrEmpty(includeTables))
                {
                    query = await _set.AsNoTracking().ToListAsync();
                } else
                {
                    query = await _set.Include(includeTables).AsNoTracking().ToListAsync();
                }

                return _mapper.Map<ModelEntity[]>(query);
            } catch (Exception e)
            {
                _logger.LogError("Unable to get data from the Datanase", e);
                return null;
            }
        }

        public async Task<ModelEntity> Insert(ModelEntity entity)
        {
            DBEntity dbEntity = _mapper.Map<DBEntity>(entity);
            _set.Add(dbEntity);
            try
            {
                await _context.SaveChangesAsync();
                ModelEntity newEntity = _mapper.Map<ModelEntity>(dbEntity);
                return entity;
            } catch (Exception e)
            {
                _logger.LogError("Unable to insert data to the database", e);
                return null;
            }
        }

        public async Task<ModelEntity> Update(ModelEntity entity)
        {
            DBEntity dbEntity = _set.Find(entity.Id);

            if (dbEntity == null) 
            {
                return null;
            }

            _mapper.Map(entity, dbEntity);
            if(!_context.ChangeTracker.HasChanges())
            {
                return entity;
            }

            try
            {
                await _context.SaveChangesAsync();
                return _mapper.Map<ModelEntity>(dbEntity);
            }
            catch (Exception e)
            {
                _logger.LogError("Unable to update data to database", e);
                return null;
            }
        }

        public async Task<bool> Delete(long id)
        {
            DBEntity dbEntity = _set.Find(id);

            if (dbEntity == null)
            {
                return false;
            }

            _set.Remove(dbEntity);
            try
            {
                await _context.SaveChangesAsync();
                return true;
            } catch (Exception e)
            {
                _logger.LogError("Unable to delete data from database", e);
                return false;
            }
        }
    }
}

namespace Alpha.Models
{
   
        public interface IRepository<TEntity> where TEntity : class
        {

            void Delete(int id);
            TEntity GetById(int id);
            void Update(TEntity entity);
           public void Add(TEntity entity);
            List<TEntity> GetAll();

        
    }

}

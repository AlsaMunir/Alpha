
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;


namespace Alpha.Models
{
    public class GenericRepos<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        private string constring = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Alpha;Integrated Security=True;";
        public void Add(TEntity entity)
        {
            var tableName = typeof(TEntity).Name;
            var properties = typeof(TEntity).GetProperties().Where(prop => prop.Name != "Id" && prop.PropertyType != typeof(IFormFile));
            var colName = string.Join(",", properties.Select(x => x.Name));
            var paramName = string.Join(",", properties.Select(y => "@" + y.Name));
            var query = $"INSERT INTO {tableName} ({colName}) VALUES ({paramName})";

            using (var con = new SqlConnection(constring))
            {
                con.Execute(query, entity);
            }
        }

        public void Delete(int id)
        {
            var tablename=typeof(TEntity).Name;
            var query = $"DELETE FROM {tablename} WHERE Id=@Id";
            using (var con=new SqlConnection(constring))
            {

                con.Execute(query, new { Id = id }); 
            }
        }

        public List<TEntity> GetAll()
        {
            var tablename=typeof(TEntity).Name;
            var query = $"SELECT *FROM {tablename}";
            using (var con=new SqlConnection(constring))
            {
               var result=con.Query<TEntity>(query);
                return result.ToList();
            }
        }

        public TEntity GetById(int id)
        {
            var tablename = typeof(TEntity).Name;
            var query = $"SELECT *FROM {tablename} WHERE Id=@Id";
            using (var con = new SqlConnection(constring))
            {
                var result = con.Query<TEntity>(query, new {Id=id});
                return result.FirstOrDefault();
            }
        }

        public void Update(TEntity entity)
        {
            var tablename=typeof(TEntity).Name;
            var properties = typeof(TEntity).GetProperties().Where(x=> x.Name!="Id" && x.PropertyType != typeof(IFormFile));
            var colName=string.Join(",",properties.Select(x => $"{x.Name}=@{x.Name}"));
            var query = $"UPDATE {tablename} SET {colName} WHERE Id=@Id";
            using (var con=new SqlConnection(constring))
            {
                    con.Execute(query, entity); 
            }

        }
    }
}

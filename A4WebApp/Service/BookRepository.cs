using A4WebApp.Interfaces;
using A4WebApp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;
using System.Data;

namespace A4WebApp.Service
{
    [Route("/")]
    public class BookRepository : IRepository<Book,int>,IDisposable  {
        readonly SqlConnection dbConnection;
        readonly IConfiguration configuration;
        readonly ILogger logger;
        private bool disposed = false;
        public BookRepository(IConfiguration configuration,SqlConnection dbConnection,ILogger logger) 
        {
            dbConnection.Open();
            this.dbConnection = dbConnection;
            this.configuration = configuration;
            this.logger = logger;
        }

        public async Task<Book> Get(int id)
        {
            var res= await GetAll();
            
            return res.FirstOrDefault(x => x.Id == id)??new Book();
             

        }
        public async Task<List<Book>> GetAll()
        {
            List<Book> books = new List<Book>();
            await using var command = dbConnection.CreateCommand();
            command.CommandText = "GetAllBooks";
            command.CommandType = CommandType.StoredProcedure;
            var reader = command.ExecuteReader();
            while (await reader.ReadAsync())
            {
                books.Add(new Book()
                {
                    Id = (int)reader["Id"],
                    Title = (string)reader["Title"],
                    Author = (string)reader["Author"],
                    YearPublished = (int)reader["YearPublished"],
                    Content = (string)reader["Content"]
                });
            }
            return books;
        }

        public async Task Add(Book entity)
        {
            try
            {
                await using var command = dbConnection.CreateCommand();
                command.CommandText = "InsertBook";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("Title", entity.Title));
                command.Parameters.Add(new SqlParameter("Author", entity.Author));
                command.Parameters.Add(new SqlParameter("YearPublished", entity.YearPublished));
                command.Parameters.Add(new SqlParameter("Content", entity.Content));
                var reader = await command.ExecuteNonQueryAsync();
            }catch(Exception ex)
            {
                //Тут можно всякое половить и обработать, но это вне тестового.
                logger.LogError(ex.ToString());
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                await using var command = dbConnection.CreateCommand();
                command.CommandText = "DeleteBook";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("Id", id));
                var reader = await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return false;
            }
            return true;
        }

        public async Task Update(Book entity)
        {
            try
            {
                await using var command = dbConnection.CreateCommand();
                command.CommandText = "UpdateBook";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("Id", entity.Id));
                command.Parameters.Add(new SqlParameter("Title", entity.Title));
                command.Parameters.Add(new SqlParameter("Author", entity.Author));
                command.Parameters.Add(new SqlParameter("YearPublished", entity.YearPublished));
                command.Parameters.Add(new SqlParameter("Content", entity.Content));
                var reader = await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                //Тут можно всякое половить и обработать, но это вне тестового.
                logger.LogError(ex.ToString());
            }
        }

        public async Task<bool> Exists(int id)
        {
            var v=await Get(id);
            return v != null;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    dbConnection?.Dispose();
                }

                disposed = true;
            }
        }
    }
}

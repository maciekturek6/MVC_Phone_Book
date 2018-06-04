using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.EntityFrameworkCore;
using Phone_book.Models;

namespace Phone_book.Repositories
{
    public class SourceManager
    {
        
        public List<PersonModel> Get()
        {
            var sql = "SELECT * FROM dbo.People";
            var modelList = new List<PersonModel>();
            using (SqlConnection connection = new SqlConnection("Integrated Security=SSPI;" +
                                                                "Initial Catalog=PhoneBook;" +
                                                                "Data Source=LAPTOP-IGM2F7FV\\SQLEXPRESS;"))
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            var model = new PersonModel();

                            model.ID = reader.GetInt32(0);
                            model.FirstName = reader.GetString(1);
                            model.LastName = reader.GetString(2);
                            model.Phone = reader.GetString(3);
                            model.Email = reader.GetString(4);
                            model.Created = reader.GetDateTime(5);
                            model.Updated = reader.GetDateTime(6);

                            modelList.Add(model);
                        }
                    }

                    return modelList;
                }
                catch (Exception e)
                {
                    throw  new Exception($"Błąd: {e}");
                }
            }
        }

       
        public PersonModel GetByID(int id)
        {
            var model = new PersonModel();
            //return new PersonModel();
            var sql = $"SELECT * FROM dbo.People WHERE Id = {id}";
           
            using (SqlConnection connection = new SqlConnection("Integrated Security=SSPI;" +
                                                                "Initial Catalog=PhoneBook;" +
                                                                "Data Source=LAPTOP-IGM2F7FV\\SQLEXPRESS;"))
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                           

                            model.ID = reader.GetInt32(0);
                            model.FirstName = reader.GetString(1);
                            model.LastName = reader.GetString(2);
                            model.Phone = reader.GetString(3);
                            model.Email = reader.GetString(4);
                            model.Created = reader.GetDateTime(5);
                            model.Updated = reader.GetDateTime(6);

                        }
                    }

                    return model;
                }
                catch (Exception e)
                {
                    throw new Exception($"Błąd: {e}");
                }
            }
        }

        public void Add(PersonModel model)
        {
            var sqlBuilder = new StringBuilder();
            sqlBuilder.Append($"INSERT INTO dbo.People (FirstName, LastName, Phone, Email, Created, Updated)");
            sqlBuilder.Append($"VALUES('{model.FirstName}', '{model.LastName}', '{model.Phone}', '{model.Email}', '{model.Created}', '{model.Updated}')");

            var sql = sqlBuilder.ToString();
            using (SqlConnection connection = new SqlConnection("Integrated Security=SSPI;" +
                                                                "Initial Catalog=PhoneBook;" +
                                                                "Data Source=LAPTOP-IGM2F7FV\\SQLEXPRESS;"))
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();   
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Błąd", e);
                }
            }
        }

        public void Update(PersonModel model)
        {
            var sqlBuilder = new StringBuilder();
            sqlBuilder.Append($"UPDATE dbo.People ");
            sqlBuilder.Append($"SET FirstName = '{model.FirstName}', LastName = '{model.LastName}', Phone = '{model.Phone}', Email = '{model.Email}', Updated = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}' ");
            sqlBuilder.Append($"WHERE Id = '{model.ID}'");

            var sql = sqlBuilder.ToString();
            using (SqlConnection connection = new SqlConnection("Integrated Security=SSPI;" +
                                                                "Initial Catalog=PhoneBook;" +
                                                                "Data Source=LAPTOP-IGM2F7FV\\SQLEXPRESS;"))
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception($"Błąd: {e}");
                }
            }
        }

        public void Remove(int id)
        {
            var sqlBuilder = new StringBuilder();
            sqlBuilder.Append($"DELETE FROM dbo.People where id={id}");
           

            var sql = sqlBuilder.ToString();
            using (SqlConnection connection = new SqlConnection("Integrated Security=SSPI;" +
                                                                "Initial Catalog=PhoneBook;" +
                                                                "Data Source=LAPTOP-IGM2F7FV\\SQLEXPRESS;"))
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception($"Błąd: {e}");
                }
            }
        }
       

    }
}

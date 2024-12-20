using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertUpdateDeleteForm
{
    internal class DB
    {
        IDbConnection connection = null;
        IDataReader reader = null;

        string connectionString = @"Data Source=LAPTOP-5BDQ3HL0;Initial Catalog=ArtistDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False; Database = ArtistDB;";
        public void Update(string text, int id)
        {
            using (connection = new SqlConnection(connectionString))
            {
                // Console.WriteLine("Введите ID ");


                // Console.WriteLine("Введите новое имя ");

                if (text == "")
                    return;
                string FullName = text;

                string sqlQuery = @"update Artists SET ArtistName=@name where Id = @id";

                SqlCommand cmd = new SqlCommand(sqlQuery, (SqlConnection)connection);

                connection.Open();


                SqlParameter NamePar = new SqlParameter("@name", FullName);
                cmd.Parameters.Add(NamePar);
                SqlParameter idPar = new SqlParameter("@id", id);
                cmd.Parameters.Add(idPar);


                int rows = cmd.ExecuteNonQuery();
                MessageBox.Show($"строк обработано : {rows}");


            }
        }
        public void Insert(string text)
        {
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                string insertStr = @$"INSERT INTO Artists (ArtistName) VALUES 
                     (@Name)";

                SqlCommand cmd = new SqlCommand(insertStr, (SqlConnection)connection);

                if (text != "")
                {
                    string FullName = text;


                    SqlParameter NamePar = new SqlParameter("@Name", FullName);
                    cmd.Parameters.Add(NamePar);


                    int rows = cmd.ExecuteNonQuery();
                    MessageBox.Show($"строк добавлено : {rows}");
                    //ExecuteReader() - Select
                    //ExecuteScalar() - AVG() SUM()
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }
        public void Delete(int id)
        {
            using (connection = new SqlConnection(connectionString))
            {

                string sqlQuery = $"delete from Artists  where Id = @id";
                SqlCommand cmd = new SqlCommand(sqlQuery, (SqlConnection)connection);

                connection.Open();

                SqlParameter idParameter = new SqlParameter("@id", id);
                cmd.Parameters.Add(idParameter);

                int number = cmd.ExecuteNonQuery();
                MessageBox.Show($"Delete {number} rows!");

            }
        }

        internal List<Artist>? ReadDB()
        {

            List<Artist> table = new List<Artist>();
            string sqlQuery = $"select * from Artists";
            connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(sqlQuery, (SqlConnection)connection);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                //string col1 = reader.GetName(0);
                //string col2 = reader.GetName(1);

                //string str = $"{col1} \t {col2} \n";
                while (reader.Read())
                {
                    //строки
                    var ID = reader.GetValue(0);

                    var fullName = reader.GetValue(1);

                    Artist tempArtist = new Artist();
                    tempArtist.Id = (int)ID;
                    tempArtist.Name =(string)fullName;

                    table.Add(tempArtist);
                    //  table.Add($"{ID} \t {fullName} \n");
                }
            }
            return table;
        }

    }
}

internal class Artist
{
    public int Id { get; set; }
    public string? Name { get; set; }

}



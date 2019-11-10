using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAuto
{

    class ConnectDB
    {

        string connectString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Works Projects\ProjectAuto\ProjectAuto\Database.mdf;Integrated Security=True"; 

        public List<Automobile> GetAuto()
        {
            List<Automobile> automobiles = new List<Automobile>();
            SqlDataReader reader = null;

            using (SqlConnection connection = new SqlConnection(connectString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM CatalogAutomobile",connection);
                connection.Open();

                try
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Automobile auto = new Automobile()
                        {
                            nameAuto = reader[1].ToString(),
                            linkAuto = reader[2].ToString(),
                            catalogYears = reader[3].ToString(),
                            model = reader[5].ToString(),
                            productInStock = reader[4].ToString()
                        };
                        automobiles.Add(auto);
                    }   
                }
                catch (Exception e)
                {

                    if (reader != null)
                    {
                        reader.Close();
                    }
                    throw e;
                }

                connection.Close();
                return automobiles;
            }

        }

        public void SetAuto(Automobile auto)
        {   

            using (SqlConnection connection = new SqlConnection(connectString))
            {
                SqlCommand command = new SqlCommand("INSERT INTO CatalogAutomobile (name,catalogId,catalogYears,productInStock,model,image)" +
                    "VALUES (@name, @CatalogId, @CatalogYears, @productInStock, @model,@image)", connection);

                command.Parameters.AddWithValue("name", auto.nameAuto.ToString());
                command.Parameters.AddWithValue("CatalogId", 1);
                command.Parameters.AddWithValue("CatalogYears", auto.catalogYears.ToString());
                command.Parameters.AddWithValue("model", auto.model.ToString());
                command.Parameters.AddWithValue("productInStock", auto.productInStock.ToString());
                command.Parameters.AddWithValue("image", auto.img.ToString());

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

            }
        }
     

    }
}

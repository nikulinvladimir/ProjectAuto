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

        // Возвращение из базу данных информацию о авто
        #region GetAuto
        public List<Automobile> GetAuto()
        {
            List<Automobile> automobiles = new List<Automobile>();
            SqlDataReader reader = null;

            using (SqlConnection connection = new SqlConnection(connectString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM CatalogAutomobile", connection);
                connection.Open();

                try
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Automobile auto = new Automobile()
                        {
                            ID = (int)reader[0],
                            img = reader[6].ToString(),
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
        #endregion
        // Возвращение в базу данных информацию о Зап части
        #region GetRepairsParts
        public List<CategoryRepairPart> GetRepairsParts()
        {
            List<CategoryRepairPart> repairParts = new List<CategoryRepairPart>();
            SqlDataReader reader = null;

            using (SqlConnection connection = new SqlConnection(connectString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM CatalogAutomobile", connection);
                connection.Open();

                try
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        CategoryRepairPart auto = new CategoryRepairPart()
                        {
                            id = (int)reader[0],
                            namePart = reader[1].ToString(),
                            parent_id = (int)reader[2],
                        };
                        repairParts.Add(auto);
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
                return repairParts;
            }

        }
        #endregion

        // добовление в базу автомобилей из сайта
        #region SetAuto
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
        #endregion

        // добовление в базу запчастей из сайта
        #region SetRepairsPart

        int i = 1;
        public void SetRepairsPart(List<CategoryRepairPart> parts)
        {


            using (SqlConnection connection = new SqlConnection(connectString))
            {
                connection.Open();

                foreach (var item in parts)
                {
                    SqlCommand command = new SqlCommand("INSERT INTO CatalogRepairsParts (namePart,parent_id) VALUES (@name,@id)", connection);
                    command.Parameters.AddWithValue("name", item.namePart.ToString());
                    command.Parameters.AddWithValue("id", i);
                    command.ExecuteNonQuery();

                    //SqlCommand commandAddId = new SqlCommand("UPDATE CatalogRepairsParts  SET parent_id = CatalogAutomobile.Id FROM CatalogAutomobile", connection);
                    //commandAddId.ExecuteNonQuery();
                }


                connection.Close();

            }
            i++;
        }
        #endregion   

    }
}

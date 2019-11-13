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
                            nameAuto = reader[1].ToString(),
                            catalogId = reader[2].ToString(),
                            catalogYears = reader[3].ToString(),
                            productInStock = reader[4].ToString(),
                            model = reader[5].ToString(),
                            imgPath = reader[6].ToString(),              
                            ImgLink = reader[7].ToString(),
                            autoLink = reader[8].ToString()
                            
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
        #region GetCategoryParts
        public List<string> GetCategoryParts(int n)
        {
            //List<CategoryPart> repairParts = new List<CategoryPart>();
            List<string> nameParts = new List<string>();
            SqlDataReader reader = null;

            using (SqlConnection connection = new SqlConnection(connectString))
            {
                SqlCommand command = new SqlCommand($"SELECT namePart FROM CatalogRepairsParts WHERE autoId = {n}", connection);
                connection.Open();

                try
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        nameParts.Add(reader[0].ToString());
                        //CategoryPart auto = new CategoryPart()
                        //{
                        //    id = (int)reader[0],
                        //    categoryName = reader[1].ToString(),
                        //    autoId = (int)reader[2],
                        //};
                        //repairParts.Add(auto);
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
                return nameParts;
            }

        }
        #endregion


        // добовление в базу автомобилей из сайта
        #region SetAuto
        public void SetAuto(Automobile auto)
        {

            using (SqlConnection connection = new SqlConnection(connectString))
            {
                SqlCommand command = new SqlCommand("INSERT INTO CatalogAutomobile (Id,name,catalogId,catalogYears,productInStock,model,imagePath,imageLink,autoCatalogLink)" +
                    "VALUES (@Id,@name, @CatalogId, @CatalogYears, @productInStock, @model,@imgPath,@imgLink,@autoCatalogLink)", connection);

                command.Parameters.AddWithValue("Id", auto.ID.ToString());
                command.Parameters.AddWithValue("name", auto.nameAuto.ToString());
                command.Parameters.AddWithValue("CatalogId", 1);
                command.Parameters.AddWithValue("CatalogYears", auto.catalogYears.ToString());
                command.Parameters.AddWithValue("model", auto.model.ToString());
                command.Parameters.AddWithValue("productInStock", auto.productInStock.ToString());
                command.Parameters.AddWithValue("imgPath", auto.imgPath.ToString());
                command.Parameters.AddWithValue("imgLink", auto.ImgLink.ToString());
                command.Parameters.AddWithValue("autoCatalogLink", auto.autoLink.ToString());

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

            }
        }
        #endregion

        // добовление в базу запчастей из сайта
        #region SetRepairsPart
        public void SetCatalogPart(List<CategoryPart> parts)
        {
   
            using (SqlConnection connection = new SqlConnection(connectString))
            {
                connection.Open();

                foreach (var item in parts)
                {
                    SqlCommand command = new SqlCommand("INSERT INTO CatalogRepairsParts (Id,namePart,autoId) VALUES (@id,@name,@autoId)", connection);
                    command.Parameters.AddWithValue("id", item.id);
                    command.Parameters.AddWithValue("name", item.categoryName.ToString());
                    command.Parameters.AddWithValue("autoId", item.autoId);
                    command.ExecuteNonQuery();

                    //SqlCommand commandAddId = new SqlCommand("UPDATE CatalogRepairsParts  SET parent_id = CatalogAutomobile.Id FROM CatalogAutomobile", connection);
                    //commandAddId.ExecuteNonQuery();
                }


                connection.Close();

            }
        }
        #endregion

        #region SetRepairsPart
        public void SetSubCategoryPart(List<SubCategoryParts> parts)
        {

            using (SqlConnection connection = new SqlConnection(connectString))
            {
                connection.Open();

                foreach (var item in parts)
                {
                    SqlCommand command = new SqlCommand("INSERT INTO SubCategoryParts (Id,nameSubPart,categoryId,countProductInStock) VALUES (@id,@nameSubPart,@categoryId,@countProductinStock)", connection);
                    command.Parameters.AddWithValue("id", item.id);
                    command.Parameters.AddWithValue("nameSubPart", item.nameSubCategoryPart.ToString());
                    command.Parameters.AddWithValue("categoryId", item.categoryId);
                    command.Parameters.AddWithValue("countProductinStock", item.countProductinStock.ToString());

                    command.ExecuteNonQuery();
                }


                connection.Close();

            }
        }
        #endregion   

    }
}

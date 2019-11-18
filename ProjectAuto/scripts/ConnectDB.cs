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

        // Возвращение из базы данных информацию о авто
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

        // Возвращение из базы данных информацию о категории запчастей
        #region GetCategoryParts
        public List<CategoryPart> GetCategoryParts(int n)
        {
            //List<CategoryPart> repairParts = new List<CategoryPart>();
            List<CategoryPart> categoryPart = new List<CategoryPart>();
            SqlDataReader reader = null;

            using (SqlConnection connection = new SqlConnection(connectString))
            {
                SqlCommand command = new SqlCommand($"SELECT *FROM CatalogRepairsParts WHERE autoId = {n}", connection);
                connection.Open();

                try
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        //nameParts.Add(reader[0].ToString());
                        CategoryPart auto = new CategoryPart()
                        {
                            id = (int)reader[0],
                            categoryName = reader[1].ToString(),
                            //autoId = (int)reader[2],
                        };
                        categoryPart.Add(auto);
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
                return categoryPart;
            }

        }
        #endregion
        //возвращение из базы информацию о под категориях
        #region GetSubCategoryParts
        public List<SubCategoryParts> GetSubCategoryParts()
        {
            //List<CategoryPart> repairParts = new List<CategoryPart>();
            List<SubCategoryParts> listSubParts = new List<SubCategoryParts>();
            SqlDataReader reader = null;

            using (SqlConnection connection = new SqlConnection(connectString))
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM SubCategoryParts", connection);
                connection.Open();

                try
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        //nameSubParts.Add(reader[0].ToString() +" " + reader[1].ToString());
                        SubCategoryParts SubPart = new SubCategoryParts()
                        {
                            id = (int)reader[0],
                            nameSubCategoryPart = reader[1].ToString(),
                            categoryId = (int)reader[2],
                            countProductinStock = reader[3].ToString(),
                        };
                        listSubParts.Add(SubPart);
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
                return listSubParts;
            }

        }
        #endregion
        // возвращение из базы информацию о запчастях
        #region GetRepairsParts

        public List<RepairPart> GetRepairsParts()
        {
            //List<CategoryPart> repairParts = new List<CategoryPart>();
            List<RepairPart> listSubParts = new List<RepairPart>();
            SqlDataReader reader = null;

            using (SqlConnection connection = new SqlConnection(connectString))
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM TableRepairsParts", connection);
                connection.Open();

                try
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        //nameSubParts.Add(reader[0].ToString() +" " + reader[1].ToString());
                        RepairPart RepairPart = new RepairPart()
                        {
                            id = (int)reader[0],
                            nameRepairPart = reader[1].ToString(),
                            subCategoryId = (int)reader[2],
                            countProductinStock = reader[3].ToString(),
                            repairPartLink = reader[4].ToString()
                        };
                        listSubParts.Add(RepairPart);
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
                return listSubParts;
            }

        }
        #endregion

        #region GetDescriptionParts

        public List<Part> GetDescriptionParts()
        {
            //List<CategoryPart> repairParts = new List<CategoryPart>();
            List<Part> listParts = new List<Part>();
            SqlDataReader reader = null;
            using (SqlConnection connection = new SqlConnection(connectString))
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM TablePartDescription", connection);
                connection.Open();

                try
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        //nameSubParts.Add(reader[0].ToString() +" " + reader[1].ToString());
                        Part part = new Part()
                        {
                            id = (int)reader[0],
                            nameGoods = reader[1].ToString(),
                            linkPictureScheme = reader[2].ToString(),
                            articlePart = reader[3].ToString(),
                            countGoods = reader[4].ToString()
                        };
                        listParts.Add(part);
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
                return listParts;
            }

        }

        public List<GoodsPart> GetPartDescriptionInPrice(int n)
        {
            //List<CategoryPart> repairParts = new List<CategoryPart>();
            List<GoodsPart> listgoodsParts = new List<GoodsPart>();
            SqlDataReader reader = null;
            using (SqlConnection connection = new SqlConnection(connectString))
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM TablePartDescriptionInPrice WHERE parentId={n}", connection);
                connection.Open();

                try
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {   
                        GoodsPart part = new GoodsPart()
                        {
                            id = (int)reader[0],
                            namePart = reader[1].ToString(),
                            linkImagePart = reader[2].ToString(),
                            price = reader[3].ToString(),
                            parentId = (int)reader[4]
                        };
                        listgoodsParts.Add(part);
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
                return listgoodsParts;
            }

        }

        public List<MissingPart> GetPartDescriptionNoPrice(int n)
        {
            //List<CategoryPart> repairParts = new List<CategoryPart>();
            List<MissingPart> ListmissingParts = new List<MissingPart>();
            SqlDataReader reader = null;
            using (SqlConnection connection = new SqlConnection(connectString))
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM TablePartDescriptionNoPrice WHERE parentId={n}", connection);
                connection.Open();

                try
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        MissingPart part = new MissingPart()
                        {
                            id = (int)reader[0],
                            namePart = reader[1].ToString(),
                            articlePart = reader[2].ToString(),
                            productInStock = reader[3].ToString(),
                            parentId = (int)reader[4]
                        };
                        ListmissingParts.Add(part);
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
                return ListmissingParts;
            }

        }

        #endregion

        // добовление в базу автомобилей из сайта
        #region SetAuto
        public void SetAuto(List<Automobile> automobiles)
        {

            using (SqlConnection connection = new SqlConnection(connectString))
            {
                connection.Open();
                foreach (var auto in automobiles)
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
                    command.ExecuteNonQuery();
                }

                connection.Close();

            }
        }
        #endregion

        // добовление в базу категорий запчастей с сайта
        #region SetCatalogPart
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
        // добовление в базу под категорий запчастей с сайта
        #region SetSubCategoryPart
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
        // добовление в базу запчастей с сайта
        #region SetRepairPart

        public void SetRepairPart(List<RepairPart> parts)
        {

            using (SqlConnection connection = new SqlConnection(connectString))
            {
                connection.Open();

                foreach (var item in parts)
                {
                    SqlCommand command = new SqlCommand("INSERT INTO TableRepairsParts (Id,nameRepairPart,subCategoryId,countProductinStock,repairPartLink) VALUES (@id,@nameRepairPart,@subCategoryId,@countProductinStock,@repairPartLink)", connection);
                    command.Parameters.AddWithValue("id", item.id);
                    command.Parameters.AddWithValue("nameRepairPart", item.nameRepairPart.ToString());
                    command.Parameters.AddWithValue("subCategoryId", item.subCategoryId);
                    command.Parameters.AddWithValue("countProductinStock", item.countProductinStock.ToString());
                    command.Parameters.AddWithValue("repairPartLink", item.repairPartLink.ToString());
                    command.ExecuteNonQuery();
                }


                connection.Close();

            }
        }
        #endregion
        // добовление в базу описание запчастей с сайта
        #region SetPartDescription


        public void SetPartDescription(List<Part> parts)
        {

            using (SqlConnection connection = new SqlConnection(connectString))
            {
                connection.Open();

                foreach (var item in parts)
                {
                    SqlCommand command = new SqlCommand("INSERT INTO TablePartDescription (name,pictureSchemePath,article,count,Id) VALUES (@name,@pictureSchemePath,@article,@count,@id)", connection);
                    command.Parameters.AddWithValue("id", item.id);
                    command.Parameters.AddWithValue("name", item.nameGoods.ToString());
                    command.Parameters.AddWithValue("pictureSchemePath", item.linkPictureScheme.ToString());
                    command.Parameters.AddWithValue("article", item.articlePart);
                    command.Parameters.AddWithValue("count", item.countGoods.ToString());
                    command.ExecuteNonQuery();
                }


                connection.Close();

            }
        }

        public void SetPartDescriptionInPrice(List<GoodsPart> goodsParts)
        {
            using (SqlConnection connection = new SqlConnection(connectString))
            {
                connection.Open();

                foreach (var part in goodsParts)
                {
                    SqlCommand command = new SqlCommand("INSERT INTO TablePartDescriptionInPrice (Id,name,imagePath,price,parentId) VALUES (@id,@name,@imagePath,@price,@parentId)", connection);

                    command.Parameters.AddWithValue("id", part.id);
                    command.Parameters.AddWithValue("name", part.namePart.ToString());
                    command.Parameters.AddWithValue("imagePath", part.linkImagePart.ToString());
                    command.Parameters.AddWithValue("price", part.price.ToString());
                    command.Parameters.AddWithValue("parentId", part.parentId);
                    command.ExecuteNonQuery();

                }

                connection.Close();
            }
        }

        public void SetPartDescriptionNoPrice(List<MissingPart> missingParts)
        {
            using (SqlConnection connection = new SqlConnection(connectString))
            {
                connection.Open();
            
                foreach (var part in missingParts)
                {
                    SqlCommand command = new SqlCommand("INSERT INTO TablePartDescriptionNoPrice (Id,name,article,countProd,parentId) VALUES (@id,@name,@article,@countProd,@parentId)", connection);
                    command.Parameters.AddWithValue("id", part.id);
                    command.Parameters.AddWithValue("name", part.namePart.ToString());
                    command.Parameters.AddWithValue("article", part.articlePart.ToString());
                    command.Parameters.AddWithValue("countProd", part.productInStock.ToString());
                    command.Parameters.AddWithValue("parentId", part.parentId);
                    command.ExecuteNonQuery();
                }

                

                connection.Close();

            }
        }

        #endregion   
    }
}

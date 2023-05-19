using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

using Alten_test_tech.Back.Domain.Products;
using Alten_test_tech.Back.Domain.Products.Entities;
using Alten_test_tech.Back.Domain.Products.Queries;
using Alten_test_tech.Back.Domain.Products.Commands;
using Alten_test_tech.Back.Domain.Responses;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Alten_test_tech.Back.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private string _connectionString;

        public ProductRepository(IConfiguration configuration)
        {
            this._connectionString = configuration.GetConnectionString("BddAltenProduct");
        }


        /// <summary>
        /// [Requete SQL]
        /// Récupère la liste des produits en base
        /// </summary>
        /// <param name="request"> id du produit </param>
        /// <param name="cancellationToken"></param>
        /// <returns>Liste de products</returns>
        public async Task<IReadOnlyCollection<Product>> GetProductAsync(GetProductQuery request, CancellationToken cancellationToken)
        {

            /*
            * On prépare la requete
            */
            List<Product> products = new List<Product>();
            string query = "SELECT * FROM T_PRODUCT";
            if (request.IdProduct != null)
            {
                query += " WHERE D_ID = " + request.IdProduct;
            }

            await using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    try
                    {
                        /*
                        * On extrait chaque élément de la réponse qu'on formate dans une liste
                        */
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                products.Add(new Product
                                {

                                    Id = int.Parse(sdr["D_ID"].ToString()),
                                    Code = sdr["D_CODE"].ToString(),
                                    Name = sdr["D_NAME"].ToString(),
                                    Description = sdr["D_DESCRIPTION"].ToString(),
                                    Price = float.Parse(sdr["D_PRICE"].ToString()),
                                    Quantity = int.Parse(sdr["D_QUANTITY"].ToString()),
                                    InventoryStatus = sdr["D_INV_STATUS"].ToString(),
                                    Category = sdr["D_CATEG"].ToString(),
                                    Image = sdr["D_IMAGE"].ToString(),
                                    Rating = float.Parse((sdr["D_RATING"] == DBNull.Value) ? "0" : sdr["D_RATING"].ToString())
                            });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        connection.Close();
                        return products;
                    }
                }
                connection.Close();
                return products;
            }
        }


        /// <summary>
        /// [Requete SQL]
        /// Ajout ou modification d'un produit dans la base de données
        /// </summary>
        /// <param name="request"> id + data du produit </param>
        /// <param name="cancellationToken"></param>
        /// <returns> response </returns>
        public async Task<Response> UpdateProductAsync(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            Response response = new Response();

            string query = "";

            if (request.product.Id == null)
            {
                /*
                 * Insert Section
                 */
                query = $"INSERT INTO T_PRODUCT ( D_CODE , D_NAME , D_DESCRIPTION , D_PRICE , D_QUANTITY , D_INV_STATUS, D_CATEG, D_IMAGE, D_RATING ) " +
                    $"VALUES ( @Code , @Name , @Description , @Price , @Quantity, @InventoryStatus, @Category, @Image, @Rating )";

                await using (SqlConnection connection = new SqlConnection(this._connectionString))
                {
                    connection.Open();

                    SqlTransaction transaction = connection.BeginTransaction();

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {

                        /*
                         * On ajoute les paramètres provenant du message à ajouter
                         */
                        cmd.Parameters.AddWithValue("@Code", request.product.Code);
                        cmd.Parameters.AddWithValue("@Name", request.product.Name);
                        cmd.Parameters.AddWithValue("@Description", request.product.Description);
                        cmd.Parameters.AddWithValue("@Price", request.product.Price);
                        cmd.Parameters.AddWithValue("@Quantity", request.product.Quantity);
                        cmd.Parameters.AddWithValue("@InventoryStatus", request.product.InventoryStatus);
                        cmd.Parameters.AddWithValue("@Category", request.product.Category);
                        cmd.Parameters.AddWithValue("@Image", String.IsNullOrEmpty(request.product.Image) ? DBNull.Value : request.product.Image);
                        cmd.Parameters.AddWithValue("@Rating", (request.product.Rating == null) ? DBNull.Value : request.product.Rating);
                        try
                        {
                            cmd.Transaction = transaction;

                            cmd.ExecuteNonQuery();

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            response.Status = "KO";
                            response.Libelle = ex.Message + ex.StackTrace;
                            connection.Close();
                            return response;
                        }
                    }
                    response.Status = "OK";
                    response.Libelle = "Insertion d'une ligne dans la table T_PRODUCT réussie";
                    connection.Close();
                    return response;
                }
            }
            else
            {
                /*
                 * Update Section
                 */
                query = $"UPDATE T_PRODUCT SET D_CODE = @Code , D_NAME = @Name , D_DESCRIPTION = @Description , D_PRICE = @Price , D_QUANTITY = @Quantity , " +
                    $" D_INV_STATUS = @InventoryStatus , D_CATEG = @Category , D_IMAGE = @Image , D_RATING = @Rating WHERE D_ID = @Id";

                await using (SqlConnection connection = new SqlConnection(this._connectionString))
                {
                    connection.Open();

                    SqlTransaction transaction = connection.BeginTransaction();

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {

                        /*
                         * On ajoute les paramètres provenant du message à ajouter
                         */
                        cmd.Parameters.AddWithValue("@Id", request.product.Id);
                        cmd.Parameters.AddWithValue("@Code", request.product.Code);
                        cmd.Parameters.AddWithValue("@Name", request.product.Name);
                        cmd.Parameters.AddWithValue("@Description", request.product.Description);
                        cmd.Parameters.AddWithValue("@Price", request.product.Price);
                        cmd.Parameters.AddWithValue("@Quantity", request.product.Quantity);
                        cmd.Parameters.AddWithValue("@InventoryStatus", request.product.InventoryStatus);
                        cmd.Parameters.AddWithValue("@Category", request.product.Category);
                        cmd.Parameters.AddWithValue("@Image", String.IsNullOrEmpty(request.product.Image) ? DBNull.Value : request.product.Image);
                        cmd.Parameters.AddWithValue("@Rating", (request.product.Rating == null) ? DBNull.Value : request.product.Rating);
                        try
                        {
                            cmd.Transaction = transaction;

                            cmd.ExecuteNonQuery();

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            response.Status = "KO";
                            response.Libelle = ex.Message + ex.StackTrace;
                            connection.Close();
                            return response;
                        }
                    }
                    response.Status = "OK";
                    response.Libelle = "Mise à jour d'une ligne dans la table T_PRODUCT réussie";
                    connection.Close();
                    return response;
                }
            }
        }


        /// <summary>
        /// [Requete SQL]
        /// Suppression d'un produit de la base de données
        /// </summary>
        /// <param name="request"> id du produit </param>
        /// <param name="cancellationToken"></param>
        /// <returns> response </returns>
        public async Task<Response> DeleteProductAsync(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            Response response = new Response();

            await using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                string query = $"DELETE FROM T_PRODUCT WHERE D_ID = @Id ";

                SqlTransaction transaction = connection.BeginTransaction();

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    /*
                     * On ajoute les paramètres
                     */
                    cmd.Parameters.AddWithValue("@Id", request.IdProduct);

                    try
                    {
                        cmd.Transaction = transaction;

                        cmd.ExecuteNonQuery();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        response.Status = "KO";
                        response.Libelle = ex.Message + ex.StackTrace;
                        connection.Close();
                        return response;
                    }
                    response.Status = "OK";
                    response.Libelle = "Suppression d'une ligne dans la table T_PRODUCT réussie";
                    connection.Close();
                    return response;
                }
            }
        }
    }
}

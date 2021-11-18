using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Projektas_10.Models;
using System.Collections.Generic;

namespace Projektas_10.Services
{
    public class FlowerService
    {
        private string _connection;
        public FlowerService()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .Build();

            _connection = config.GetValue<string>("ConnectionStrings:DefaultConnection");
        }

        public List<Flower> GetFlowers()
        {
            MySqlConnection conn = new MySqlConnection(_connection);

            conn.Open();

            var flowers = new List<Flower>();

            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT flowerId, flowerName, flowerKind, " +
                    "flowerStartingPrice, flowerDiscount, flowerDescription, flowerPhoto FROM flowers";

                var reader = cmd.ExecuteReader();

                using(reader)
                {
                    while(reader.Read())
                    {
                        var flower = new Flower(
                            reader.GetString(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetDouble(3),
                            reader.GetDouble(4),
                            reader.GetString(5),
                            reader.GetString(6)
                        );

                        flowers.Add(flower);
                    }
                }
            }

            return flowers;
        }

        public Flower GetFlower(string flowerId)
        {
            MySqlConnection conn = new MySqlConnection(_connection);

            conn.Open();

            using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT flowerId, flowerName, flowerKind, " +
                    "flowerStartingPrice, flowerDiscount, flowerDescription, flowerPhoto " +
                    "FROM flowers WHERE flowerId = @flowerId";

                cmd.Parameters.Add(
                    new MySqlParameter()
                    {
                        ParameterName = "@flowerId",
                        DbType = System.Data.DbType.String,
                        Value = flowerId
                    }
                );

                var reader = cmd.ExecuteReader();

                using(reader)
                {
                    reader.Read();

                    return new Flower(
                        reader.GetString(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetDouble(3),
                        reader.GetDouble(4),
                        reader.GetString(5),
                        reader.GetString(6)
                    );
                }
            }
        }

        public void CreateFlower(Flower flower)
        {
            MySqlConnection conn = new MySqlConnection(_connection);

            conn.Open();

            using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO flowers(flowerId, flowerName, flowerKind, " +
                    "flowerStartingPrice, flowerDiscount, flowerDescription, flowerPhoto) " +
                    "VALUES (@flowerId, @flowerName, @flowerKind, @flowerStartingPrice, " +
                    "@flowerDiscount, @flowerDescription, @flowerPhoto)";

                cmd.Parameters.Add( 
                    new MySqlParameter()
                    {
                        ParameterName = "@flowerId",
                        DbType = System.Data.DbType.String,
                        Value = flower.FlowerId
                    }
                );

                cmd.Parameters.Add(
                    new MySqlParameter()
                    {
                        ParameterName = "@flowerName",
                        DbType = System.Data.DbType.String,
                        Value = flower.FlowerName
                    }
                );

                cmd.Parameters.Add(
                   new MySqlParameter()
                   {
                       ParameterName = "@flowerKind",
                       DbType = System.Data.DbType.String,
                       Value = flower.FlowerKind
                   }
               );

                cmd.Parameters.Add(
                   new MySqlParameter()
                   {
                       ParameterName = "@flowerStartingPrice",
                       DbType = System.Data.DbType.Double,
                       Value = flower.FlowerStartingPrice
                   }
               );

                cmd.Parameters.Add(
                   new MySqlParameter()
                   {
                       ParameterName = "@flowerDiscount",
                       DbType = System.Data.DbType.Double,
                       Value = flower.FlowerDiscount
                   }
               );

                cmd.Parameters.Add(
                   new MySqlParameter()
                   {
                       ParameterName = "@flowerDescription",
                       DbType = System.Data.DbType.String,
                       Value = flower.FlowerDescription
                   }
               );

                cmd.Parameters.Add(
                   new MySqlParameter()
                   {
                       ParameterName = "@flowerPhoto",
                       DbType = System.Data.DbType.String,
                       Value = flower.FlowerPhoto
                   }
               );

                cmd.ExecuteNonQuery();
            }
        }
    }
}

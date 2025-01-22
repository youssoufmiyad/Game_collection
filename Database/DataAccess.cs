using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Microsoft.Data.Sqlite;
using Game_collection.Models;
using System.ComponentModel;
using Game_collection.Views.Controls;
using System.Collections;
using System.Reflection;
using System.Xml.Linq;

namespace Game_collection.Database
{
    public static class DataAccess
    {
        public static void InitializeDatabase()
        {
            using (SqliteConnection db =
               new SqliteConnection($"Filename=collections.db"))
            {
                db.Open();
                Console.WriteLine("DB Opened");
                string tableCollectionsCommand = "CREATE TABLE IF NOT EXISTS " +
                    "Collections (ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
                    "Name VARCHAR(255) NOT NULL" +
                    ");";

                string tableGamesCommand = "CREATE TABLE IF NOT EXISTS " +
                    "Games (ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
                    "Name VARCHAR(255) NOT NULL, " +
                    "Type VARCHAR(255) NULL, " +
                    "Description VARCHAR(255) , " +
                    "Plateform VARCHAR(255) NOT NULL, " +
                    "ReleaseDate DATE NULL, " +
                    "AcquisitionDate DATE NULL, " +
                    "Price DOUBLE NULL, " +
                    "PriceResell DOUBLE NULL, " +
                    "Cover BLOB NULL" +
                    ");";

                string tableCollectionGamesCommand = "CREATE TABLE IF NOT EXISTS " +
                    "CollectionGames (ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
                    "CollectionID INTEGER NOT NULL, " +
                    "GameID INTEGER NOT NULL, " +
                    "FOREIGN KEY(CollectionID) REFERENCES Collections(ID), " +
                    "FOREIGN KEY(GameID) REFERENCES Games(ID)" +
                    ");";

                string addWishlistCommand = "INSERT INTO Collections (Name) VALUES " +
                    "(\"Wishlist\");";

                SqliteCommand createCollectionsTable = new SqliteCommand(tableCollectionsCommand, db);
                SqliteCommand createGamesTable = new SqliteCommand(tableGamesCommand, db);
                SqliteCommand createCollectionGamesTable = new SqliteCommand(tableCollectionGamesCommand, db);
                SqliteCommand addWishlist = new SqliteCommand(addWishlistCommand, db);

                try
                {
                    createCollectionsTable.ExecuteNonQuery();
                    createGamesTable.ExecuteNonQuery();
                    createCollectionGamesTable.ExecuteNonQuery();
                }
                catch
                {
                    Console.WriteLine("Query échoué");
                }



                SqliteCommand collections = new SqliteCommand("SELECT * FROM collections;", db);
                SqliteDataReader count = collections.ExecuteReader();

                if (count.HasRows == false) { addWishlist.ExecuteNonQuery(); }
                Console.WriteLine("initialisation done");
            }
        }

        public static void AddCollection(string name)
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=collections.db"))
            {
                db.Open();

                string insertCommand = "INSERT INTO Collections (Name) VALUES (@Name);";
                SqliteCommand addCollection = new SqliteCommand(insertCommand, db);
                addCollection.Parameters.AddWithValue("@Name", name);

                try
                {
                    addCollection.ExecuteNonQuery();
                    Console.WriteLine($"Collection '{name}' added successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error adding collection: {ex.Message}");
                }
            }
        }

        public static List<string> GetCollections()
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=collections.db"))
            {
                db.Open();

                List<string> collections = new List<string>();
                SqliteCommand getCollectionsCommand = new SqliteCommand("SELECT * FROM Collections;", db);
                SqliteDataReader collectionsReader = getCollectionsCommand.ExecuteReader();

                while (collectionsReader.Read())
                {
                    collections.Add(collectionsReader["Name"].ToString());
                }

                return collections;
            }
        }

        public static string GetCollectionId(string collectionName)
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=collections.db"))
            {
                db.Open();


                SqliteCommand getCollectionIdCommand = new SqliteCommand("SELECT ID FROM Collections WHERE Name = @CollectionName;", db);
                getCollectionIdCommand.Parameters.AddWithValue("@CollectionName", collectionName);
                SqliteDataReader collectionIdReader = getCollectionIdCommand.ExecuteReader();
                collectionIdReader.Read();


                return collectionIdReader["ID"].ToString();
            }
        }

        public static string GetGameId(string gameName)
        {
            StreamWriter writetext = File.AppendText("debug_get_game_Id.txt");

            writetext.WriteLine("Get game ID method starts");
            using (SqliteConnection db = new SqliteConnection($"Filename=collections.db"))
            {
                db.Open();
                writetext.WriteLine($"Game name : {gameName}");

                SqliteCommand getGameIdCommand = new SqliteCommand("SELECT ID FROM Games WHERE Name = @GameName;", db);
                getGameIdCommand.Parameters.AddWithValue("@GameName", gameName);
                try
                {
                    SqliteDataReader gameIdReader = getGameIdCommand.ExecuteReader();
                    gameIdReader.Read();
                    writetext.WriteLine($"Game id : {gameIdReader["ID"].ToString()}");
                    writetext.Close();
                    return gameIdReader["ID"].ToString();
                }
                catch (Exception ex)
                {
                    writetext.WriteLine($"error : {ex}");
                    writetext.Close();
                    return "-1";
                }

            }
        }

        public static void AddGame(Game game)
        {
            StreamWriter writetext = File.AppendText("debug_add_game.txt");

            writetext.WriteLine("Add game method starts");

            using (SqliteConnection db = new SqliteConnection($"Filename=collections.db"))
            {
                db.Open();
                writetext.WriteLine("db opened");
                writetext.WriteLine("game object");

                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(game))
                {
                    string name = descriptor.Name;
                    object value = descriptor.GetValue(game);
                    writetext.WriteLine("{0}={1}", name, value);
                }

                string insertCommand = "INSERT INTO Games (Name, Type, Description, Plateform, ReleaseDate, AcquisitionDate, Price, PriceResell, Cover) " +
                                       "VALUES (@Name, @Genre, @Description, @Platform, @ReleaseDate, @AcquisitionDate, @Price, @PriceResell, @Cover);";
                SqliteCommand addGame = new SqliteCommand(insertCommand, db);
                addGame.Parameters.AddWithValue("@Name", game.Name);
                addGame.Parameters.AddWithValue("@Genre", game.Genre);
                addGame.Parameters.AddWithValue("@Description", game.Description);
                addGame.Parameters.AddWithValue("@Platform", game.Plateform);
                addGame.Parameters.AddWithValue("@ReleaseDate", game.ReleaseDate);
                addGame.Parameters.AddWithValue("@AcquisitionDate", game.AcquisitionDate);
                addGame.Parameters.AddWithValue("@Price", game.Price);
                addGame.Parameters.AddWithValue("@PriceResell", game.PriceResell);
                addGame.Parameters.AddWithValue("@Cover", game.Cover ?? (object)DBNull.Value);

                Console.WriteLine("Parameters binded");

                try
                {
                    addGame.ExecuteNonQuery();
                    writetext.WriteLine($"Game '{game.Name}' added successfully.");
                }
                catch (Exception ex)
                {
                    writetext.WriteLine($"Error adding game: {ex.Message}");
                }
                writetext.Close();
            }
        }

        public static void AddGameToCollection(int collectionId, int gameId)
        {
            StreamWriter writetext = File.AppendText("debug_add_game_to_collection.txt");

            writetext.WriteLine("Add game to collection method starts");

            using (SqliteConnection db = new SqliteConnection($"Filename=collections.db"))
            {
                writetext.WriteLine($"Collection id : {collectionId}, Game id : {gameId}");

                db.Open();

                string insertCommand = "INSERT INTO CollectionGames (CollectionID, GameID) VALUES (@CollectionID, @GameID);";
                SqliteCommand addRelation = new SqliteCommand(insertCommand, db);
                addRelation.Parameters.AddWithValue("@CollectionID", collectionId);
                addRelation.Parameters.AddWithValue("@GameID", gameId);

                try
                {
                    addRelation.ExecuteNonQuery();
                    writetext.WriteLine($"Game ID {gameId} added to Collection ID {collectionId} successfully.");
                }
                catch (Exception ex)
                {
                    writetext.WriteLine($"Error adding game to collection: {ex.Message}");
                }
            }
        }

        public static void AddGameToCollection(string collectionName, Game game)
        {

        }

        public static void AddGameToWishlist(Game game)
        {
            StreamWriter writetext = File.AppendText("debug_add_game_wishlist.txt");

            writetext.WriteLine("writing in text file");

            writetext.WriteLine("Add game to wishlist method starts");

            using (SqliteConnection db = new SqliteConnection("Filename=collections.db"))
            {
                db.Open();

                SqliteCommand gameIdCommand = new SqliteCommand("SELECT ID FROM Games WHERE Name = @GameName;", db);
                gameIdCommand.Parameters.AddWithValue("@GameName", game.Name);
                SqliteDataReader gameID = gameIdCommand.ExecuteReader();
                gameID.Read();


                string insertCommand = "INSERT INTO CollectionGames (CollectionID, GameID) VALUES (1, @GameID);";
                SqliteCommand addRelation = new SqliteCommand(insertCommand, db);

                try
                {
                    addRelation.Parameters.AddWithValue("@GameID", gameID["ID"]);
                    addRelation.ExecuteNonQuery();
                    writetext.WriteLine("Game added to wishlist");
                }
                catch (Exception e)
                {
                    writetext.WriteLine($"error : {e}");
                }
                writetext.Close();
            }
        }

        public static List<Game> BrowseGames()
        {
            using (SqliteConnection db = new SqliteConnection("Filename=collections.db"))
            {
                db.Open();

                SqliteCommand selectGamesCommand = new SqliteCommand("SELECT * FROM Games; ", db);
                SqliteDataReader selectGames = selectGamesCommand.ExecuteReader();
                List<Game> games = new List<Game>();
                while (selectGames.Read())
                {
                    games.Add(new Game(
                        int.Parse(selectGames["ID"].ToString()),
                        selectGames["Name"].ToString(),
                        selectGames["Type"].ToString(),
                        selectGames["Description"].ToString(),
                        selectGames["Plateform"].ToString(),
                        DateTime.Parse(selectGames["ReleaseDate"].ToString()),
                        DateTime.Parse(selectGames["AcquisitionDate"].ToString()),
                        double.Parse(selectGames["Price"].ToString()),
                        double.Parse(selectGames["PriceResell"].ToString()),
                        null));
                }
                return games;
            }
        }

        public static List<Game> BrowseGamesOfCollections(string collectionName)
        {
            using (SqliteConnection db = new SqliteConnection("Filename=collections.db"))
            {
                db.Open();

                SqliteCommand collectionIdCommand = new SqliteCommand("SELECT ID FROM collections WHERE Name = @CollectionName", db);
                collectionIdCommand.Parameters.AddWithValue("@CollectionName", collectionName);
                SqliteDataReader collectionIdReader = collectionIdCommand.ExecuteReader();
                if (!collectionIdReader.Read())
                {
                    throw new Exception($"Collection '{collectionName}' not found.");
                }
                string collectionID = collectionIdReader["ID"].ToString();
                collectionIdReader.Close();


                SqliteCommand selectGamesCommand = new SqliteCommand(@"
                SELECT
                    games.Name AS gameName,
                    games.ID AS gameID,
                    Type, Description, Plateform, ReleaseDate, AcquisitionDate, 
                    Price, PriceResell
                FROM
                    CollectionGames
                INNER JOIN
                    games ON CollectionGames.GameID = games.ID
                INNER JOIN
                    collections ON CollectionGames.CollectionID = collections.ID
                WHERE
                    collections.ID = @CollectionId; ", db);
                selectGamesCommand.Parameters.AddWithValue("@CollectionId", collectionID);
                SqliteDataReader selectGames = selectGamesCommand.ExecuteReader();
                List<Game> games = new List<Game>();
                while (selectGames.Read())
                {
                    games.Add(new Game(
                        int.Parse(selectGames["gameID"].ToString()),
                        selectGames["gameName"].ToString(),
                        selectGames["Type"].ToString(),
                        selectGames["Description"].ToString(),
                        selectGames["Plateform"].ToString(),
                        DateTime.Parse(selectGames["ReleaseDate"].ToString()),
                        DateTime.Parse(selectGames["AcquisitionDate"].ToString()),
                        double.Parse(selectGames["Price"].ToString()),
                        double.Parse(selectGames["PriceResell"].ToString()),
                        null));
                }
                return games;
            }
        }

        internal static void DeleteGame(string gameName)
        {
            using (SqliteConnection db = new SqliteConnection("Filename=collections.db"))
            {
                db.Open();

                SqliteCommand gameIdCommand = new SqliteCommand("SELECT ID FROM Games WHERE Name = @GameName;", db);
                gameIdCommand.Parameters.AddWithValue("@GameName", gameName);
                SqliteDataReader gameID = gameIdCommand.ExecuteReader();
                gameID.Read();


                string deleteCommandRequest = @"
                DELETE FROM Games WHERE ID = @GameID;
                DELETE FROM CollectionGames WHERE GameID = @GameID
                ";
                SqliteCommand deleteCommand = new SqliteCommand(deleteCommandRequest, db);
                deleteCommand.Parameters.AddWithValue("@GameID", gameID["ID"]);

                deleteCommand.ExecuteNonQuery();


            }
        }

        internal static void DeleteGameById(int gameId)
        {
            using (SqliteConnection db = new SqliteConnection("Filename=collections.db"))
            {
                db.Open();

                StreamWriter writetext = File.AppendText("debug_delete_game.txt");

                writetext.WriteLine("delete game start ");

                writetext.WriteLine("game id : ");

                string deleteCommandRequest = @"
                DELETE FROM CollectionGames WHERE GameID = @GameID;
                DELETE FROM Games WHERE ID = @GameID;
                ";
                SqliteCommand deleteCommand = new SqliteCommand(deleteCommandRequest, db);
                deleteCommand.Parameters.AddWithValue("@GameID", gameId);

                try
                {
                    deleteCommand.ExecuteNonQuery();
                    writetext.WriteLine($"game deleted");

                }
                catch (Exception ex)
                {
                    writetext.WriteLine($"error : {ex}");
                }
                writetext.Close();
            }
        }

        internal static void DeleteCollectionById(int collectionId)
        {
            using (SqliteConnection db = new SqliteConnection("Filename=collections.db"))
            {
                db.Open();

                string deleteCommandRequest = @"
                DELETE FROM Collections WHERE ID = @CollectionID;
                DELETE FROM CollectionGames WHERE CollectionID = @CollectionID
                ";
                SqliteCommand deleteCommand = new SqliteCommand(deleteCommandRequest, db);
                deleteCommand.Parameters.AddWithValue("@CollectionID", collectionId);

                deleteCommand.ExecuteNonQuery();

            }
        }

        internal static void MoveGame(string gameId, string newCollectionId, string collectionId)
        {
            using (SqliteConnection db = new SqliteConnection("Filename=collections.db"))
            {
                db.Open();

                string moveCommandRequest = @"
                UPDATE collectionGames 
                SET CollectionID = @NewCollectionId
                WHERE CollectionID = @CollectionId AND gameId = @GameId
                ";
                SqliteCommand moveCommand = new SqliteCommand(moveCommandRequest, db);
                moveCommand.Parameters.AddWithValue("@CollectionID", collectionId);
                moveCommand.Parameters.AddWithValue("@NewCollectionID", newCollectionId);
                moveCommand.Parameters.AddWithValue("@GameId", gameId);

                moveCommand.ExecuteNonQuery();

            }
            throw new NotImplementedException();
        }

        internal static void ChangeGameCollection(int v1, int v2)
        {
            throw new NotImplementedException();
        }

        internal static void ModifyGame(Game game)
        {
            StreamWriter writetext = File.AppendText("debug_modify_game_wishlist.txt");

            writetext.WriteLine("writing in text file");

            writetext.WriteLine("Modify game method starts");

            writetext.WriteLine("game object");

            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(game))
            {
                string name = descriptor.Name;
                object value = descriptor.GetValue(game);
                writetext.WriteLine("{0}={1}", name, value);
            }

            using (SqliteConnection db = new SqliteConnection("Filename=collections.db"))
            {
                db.Open();
                SqliteCommand updateCommand = new SqliteCommand(@"UPDATE Games 
                SET Name = @Name, 
                Type = @Type, 
                Description = @Description, 
                Plateform = @Plateform, 
                ReleaseDate = @ReleaseDate, 
                AcquisitionDate = @AcquisitionDate,
                Price = @Price, 
                PriceResell = @PriceResell, 
                Cover = @PriceResell 
                WHERE ID = @ID;", db);
                updateCommand.Parameters.AddWithValue("@Name", game.Name);
                updateCommand.Parameters.AddWithValue("@Type", game.Genre);
                updateCommand.Parameters.AddWithValue("@Description", game.Description);
                updateCommand.Parameters.AddWithValue("@Plateform", game.Plateform);
                updateCommand.Parameters.AddWithValue("@ReleaseDate", game.ReleaseDate);
                updateCommand.Parameters.AddWithValue("@AcquisitionDate", game.AcquisitionDate);
                updateCommand.Parameters.AddWithValue("@Price", game.Price);
                updateCommand.Parameters.AddWithValue("@PriceResell", game.PriceResell);
                updateCommand.Parameters.AddWithValue("@Cover", game.Cover);
                updateCommand.Parameters.AddWithValue("@ID", game.Id);

                try
                {
                    int rows = updateCommand.ExecuteNonQuery();
                    writetext.WriteLine($"Game modified : {rows}");
                }
                catch (Exception e)
                {
                    writetext.WriteLine($"error : {e}");
                }
                writetext.Close();
            }
        }
    }
}

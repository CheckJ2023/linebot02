// using System;
// using System.Threading.Tasks;
// using linebot02.Models;
// using MySqlConnector;

// namespace linebot02.Services;

// public class RestaurantService {



//     private static readonly MySqlConnectionStringBuilder builder 
//     = new()
//     {
//             Server = RestaurantService.servername,
//             Database = RestaurantService.databasename,
//             UserID = RestaurantService.userId,
//             Password = RestaurantService.password,
//             SslMode = MySqlSslMode.Required,
//     };

//     private const string tablename="restaurant";
//     private const string columnname1="restaurant_id";
//     private const string columnname2="restaurant_name";
//     private const string columnname3="phone_number";
//     private const string columnname4="location";
//     private const string columnname5="description";

//     //獲得資料
//     public async Task<List<RestaurantDAO>?> GetAll()
//     {
//         // Console.WriteLine(builder.ConnectionString);
        
//         try{
//             var restaurantData = new List<RestaurantDAO>();
//             using (var conn = new MySqlConnection(builder.ConnectionString))
//             {
//                 // Console.WriteLine("Opening connection");
//                 // OpenAsync（） 建立與 MySQL 的連線。
//                 await conn.OpenAsync();

//                 // CreateCommand（） 設定 CommandText 屬性。
//                 using (var command = conn.CreateCommand())
//                 {
                    
                    
//                     //MySQL 語法
//                     command.CommandText = @$"SELECT * FROM {tablename} ;";

//                     // ExecuteReaderAsync） 以執行資料庫命令。注意跟插入資料不一樣.
//                     using (var reader = await command.ExecuteReaderAsync())
//                     {
//                         // ReadAsync（） 會前進到結果中的記錄。 然後程式碼會使用 GetLong 和 GetString 來剖析記錄中的值。
//                         while (await reader.ReadAsync())
//                         {
//                             // Console.WriteLine(string.Format(
//                             //     "Reading from table=({0}, {1})",
//                             //     reader.GetInt64(0),
//                             //     reader.GetString(1)));
//                             var rs = new RestaurantDAO();
//                             rs.RestaurantId = reader.GetInt64(0);
//                             rs.RestaurantName = !reader.IsDBNull(1)?
//                              reader.GetString(1):"";
//                             rs.PhoneNumber = !reader.IsDBNull(2)?
//                                  reader.GetInt32(2):0;
//                             rs.Location = !reader.IsDBNull(3)?
//                              reader.GetString(3):"";
//                             rs.Description = !reader.IsDBNull(4)?
//                              reader.GetString(4):"";
                            
//                             restaurantData.Add(rs);
//                         }
//                         foreach (RestaurantDAO r in restaurantData){
//                         // Console.WriteLine($"ID={r.restaurantId},NAME={r.restaurantName}");
//                         }
//                     }
//                 }

//                 // Console.WriteLine("Closing connection");
//             }

//             // Console.WriteLine("Press RETURN to exit");
//             // Console.ReadLine();
//             return restaurantData;
//         }
//         catch (Exception ex)
//         {
//             Console.WriteLine("Error:"+ex.Message);
            
//             return null;
//         }

//     }

//     //獲得指定的資料
//     public async Task<RestaurantDAO?> Get(Int64 id)
//     {
//         // Console.WriteLine(builder.ConnectionString);
//         var rs = new RestaurantDAO();

//         try{
            
//             using (var conn = new MySqlConnection(builder.ConnectionString))
//             {
//                 // Console.WriteLine("Opening connection");
//                 // OpenAsync（） 建立與 MySQL 的連線。
//                 await conn.OpenAsync();

//                 // CreateCommand（） 設定 CommandText 屬性。
//                 using (var command = conn.CreateCommand())
//                 {
                    
//                     //MySQL 語法
//                     command.CommandText = @$"SELECT * FROM {tablename} WHERE {columnname1} = @id1;";
//                     command.Parameters.AddWithValue("@id1", id);

//                     // ExecuteReaderAsync） 以執行資料庫命令。注意跟插入資料不一樣.
//                     using (var reader = await command.ExecuteReaderAsync())
//                     {
//                         // ReadAsync（） 會前進到結果中的記錄。 然後程式碼會使用 GetLong 和 GetString 來剖析記錄中的值。
//                         await reader.ReadAsync();
                        
//                         rs.RestaurantId = reader.GetInt64(0);
//                         rs.RestaurantName = !reader.IsDBNull(1)?
//                              reader.GetString(1):"";
//                             rs.PhoneNumber = !reader.IsDBNull(2)?
//                                  reader.GetInt32(2):0;
//                             rs.Location = !reader.IsDBNull(3)?
//                              reader.GetString(3):"";
//                             rs.Description = !reader.IsDBNull(4)?
//                              reader.GetString(4):"";
   
//                     }
//                 }

//                 // Console.WriteLine("Closing connection");
//             }

//             // Console.WriteLine("Press RETURN to exit");
//             // Console.ReadLine();
//             return rs;
//         }
//         catch (Exception ex)
//         {
//             Console.WriteLine(ex.Message);
  
//             return null;
//         }
//         // return;
//     }

//     //新增資料
//     public async Task<string> Add(RestaurantDAO restaurantData){
//         try{
//             using (var conn = new MySqlConnection(builder.ConnectionString))
//                 {
//                     // Console.WriteLine("Opening connection");

//                     // OpenAsync(): 建立與 MySQL 的連線。
//                     await conn.OpenAsync();

//                     // CreateCommand(): 設定 CommandText 屬性設定 CommandText 屬性
//                     using (var command = conn.CreateCommand())
//                     {
                        
//                         //MySQL 語法
//                         command.CommandText = @$"INSERT INTO {tablename} "+
//                         @$"( {columnname1} , {columnname2}, {columnname3}, {columnname4}, {columnname5} ) "+
//                         @$"VALUES ( NULL, @val1, @val2, @val3, @val4 ) ;";
//                         command.Parameters.AddWithValue("@val1", restaurantData.RestaurantName);
//                         command.Parameters.AddWithValue("@val2", restaurantData.PhoneNumber);
//                         command.Parameters.AddWithValue("@val3", restaurantData.Location);
//                         command.Parameters.AddWithValue("@val4", restaurantData.Description);
                        

//                         //ExecuteNonQueryAsync 執行資料庫命令
//                         int rowCount = await command.ExecuteNonQueryAsync();
//                         // Console.WriteLine(String.Format("Number of rows inserted={0}", rowCount));
//                     }

//                     // connection will be closed by the 'using' block
//                     // Console.WriteLine("Closing connection");
//                 }
//                 return "";
//         }
//         catch (Exception ex)
//         {
//             Console.WriteLine(ex.Message);
            
//             return ex.Message;
//         }
//     }

//     //更新資料
//     public async Task<string> Update(RestaurantDAO restaurantData){
//         try{
//             using (var conn = new MySqlConnection(builder.ConnectionString))
//             {
//                 // Console.WriteLine("Opening connection");

//                 // OpenAsync（） 建立與 MySQL 的連線。
//                 await conn.OpenAsync();

//                 // CreateCommand（） 設定 CommandText 屬性
//                 using (var command = conn.CreateCommand())
//                 {
                    
//                     //MySQL 語法
//                     command.CommandText = $"UPDATE {tablename} "+
//                     $"SET {columnname2} = @val2 "+
//                     $", {columnname3} = @val3 "+
//                     $", {columnname4} = @val4 "+
//                     $", {columnname5} = @val5 "+
//                     $"WHERE {columnname1} = @val1;";
//                     command.Parameters.AddWithValue("@val1", restaurantData.RestaurantId);
//                     command.Parameters.AddWithValue("@val2", restaurantData.RestaurantName);
//                     command.Parameters.AddWithValue("@val3", restaurantData.PhoneNumber);
//                     command.Parameters.AddWithValue("@val4", restaurantData.Location);
//                     command.Parameters.AddWithValue("@val5", restaurantData.Description);

//                     //ExecuteNonQueryAsync（） 以執行資料庫命令。 
//                     int rowCount = await command.ExecuteNonQueryAsync();
//                     // Console.WriteLine(String.Format("Number of rows updated={0}", rowCount));
//                 }

//                 // Console.WriteLine("Closing connection");
//             }
//             return "";
//         }
//         catch(Exception ex){
//             Console.WriteLine(ex.Message);
            
//             return ex.Message;
//         }

//     }
//      //刪除資料
//     public async Task<string> Delete(Int64 id){
//         try{
//             using (var conn = new MySqlConnection(builder.ConnectionString))
//             {
//                 // Console.WriteLine("Opening connection");

//                 // OpenAsync（） 建立與 MySQL 的連線。
//                 await conn.OpenAsync();

//                 // CreateCommand（） 設定 CommandText 屬性。
//                 using (var command = conn.CreateCommand())
//                 {
 
//                     //MySQL 語法
//                     command.CommandText = $"DELETE FROM {tablename} "+
//                     $"WHERE {columnname1} = @val1;";
//                     command.Parameters.AddWithValue("@val1", id);

//                     // ExecuteNonQueryAsync（） 以執行資料庫命令。
//                     int rowCount = await command.ExecuteNonQueryAsync();
//                     // Console.WriteLine(String.Format("Number of rows deleted={0}", rowCount));
//                 }

//                 // Console.WriteLine("Closing connection");
//             }
//             return "";
//         }
//         catch(Exception ex){
//             Console.WriteLine(ex.Message);
            
//             return ex.Message;
//         }
//     }
// }

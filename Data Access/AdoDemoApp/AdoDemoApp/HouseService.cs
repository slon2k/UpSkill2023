using System.Data.SqlClient;
using System.Data;

namespace AdoDemoApp;

public class HouseService
{
    private readonly string connectionString;

    public HouseService(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public IEnumerable<House> GetHouses() 
    {
        string query = "SELECT * FROM House";

        using (var connection = new SqlConnection(connectionString))
        {
            var command = new SqlCommand(query, connection);

            try
            {
                var houses = new List<House>();

                connection.Open();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var id = reader.GetInt32("Id");
                    var name = reader.GetString("Name");
                    houses.Add(new House(id, name));
                }

                return houses;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR :" + ex.Message);
                throw;
            }
        };
    }

    public IEnumerable<HouseModel> GetHousesWithStudents() 
    {
        string query = 
            "SELECT * FROM House;" +
            "SELECT * FROM Student;";

        using (var connection = new SqlConnection(connectionString))
        {

            var command = new SqlCommand(query, connection);

            var da = new SqlDataAdapter(command);

            var ds = new DataSet();

            try
            {
                connection.Open();

                da.Fill(ds);

                ds.Tables[0].TableName = "House";
                ds.Tables[1].TableName = "Student";

                var houses =
                    from row in ds.Tables[0].AsEnumerable()
                    select new House(
                        row.Field<int>("Id"),
                        row.Field<string>("Name") ?? ""
                        );

                var students =
                    from row in ds.Tables[1].AsEnumerable()
                    select new Student(
                        row.Field<int>("Id"),
                        row.Field<string>("FirstName") ?? "",
                        row.Field<string>("LastName") ?? "",
                        row.Field<int>("HouseId")
                        );

                var result =
                    from house in houses
                    join student in students on house.Id equals student.HouseId into houseStudents
                    select new HouseModel(
                        house.Id,
                        house.Name,
                        houseStudents.Select(s => new StudentModel(
                            s.Id,
                            s.FirstName + " " + s.LastName,
                            house.Name)));

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR :" + ex.Message);
                throw;
            }
        }
    }
}

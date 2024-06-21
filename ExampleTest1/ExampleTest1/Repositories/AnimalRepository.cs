using ExampleTest1.Models.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace ExampleTest1.Repositories;

public class AnimalRepository:IAnimalRepository
{
    private readonly IConfiguration _configuration;
    
    public AnimalRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> doesAnimalExists(int id)
    {
        var sqlQuery = "SELECT 1 FROM Animal WHERE @ID=Animal.ID";
        
        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand();
        
        command.Connection = connection;
        command.CommandText = sqlQuery;
        command.Parameters.AddWithValue("@ID", id);

        await connection.OpenAsync();

        var res = await command.ExecuteScalarAsync();
        
        return res is not null;
    }

    public async Task<AnimalClassDTO> GetAnimalClass(int id)
    {
        var sqlQuery = @"select Animal_Class.Name from Animal_Class join Animal on Animal_Class.ID = Animal.ID WHERE @ID = Animal.ID";
        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = sqlQuery;
        command.Parameters.AddWithValue("@ID", id);
	    
        await connection.OpenAsync();

        var reader = await command.ExecuteReaderAsync();

        int classNameOrdinal = reader.GetOrdinal("Name");

        AnimalClassDTO animalClassDto = new AnimalClassDTO()
        {
            Name = reader.GetString(classNameOrdinal)
        };

        return animalClassDto;
    }

    public async Task<List<OwnerDTO>> getOwners(int id)
    {
        var sqlQuery = @"select Owner.ID,Owner.FirstName,Owner.LastName from Owner
join Animal on Animal.ID = Owner.ID WHERE @ID = Animal.ID";
        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = sqlQuery;
        command.Parameters.AddWithValue("@ID", id);
	    
        await connection.OpenAsync();

        var reader = await command.ExecuteReaderAsync();

        var owners = new List<OwnerDTO>();

        var nameOwneOrdinal = reader.GetOrdinal("FirstName");
        var lastNameOwneOrdinal = reader.GetOrdinal("LastName");
        var idOwneOrdinal = reader.GetOrdinal("ID");

        while (await reader.ReadAsync())
        {
            owners.Add(new OwnerDTO()
            {
                ID = reader.GetInt32(idOwneOrdinal),
                FirstName = reader.GetString(nameOwneOrdinal),
                LastName = reader.GetString(lastNameOwneOrdinal)
            });
        }

        return owners;
    }

    public async Task<GetAnimalWithOwnerDTO> GetAnimalWithOwner(int id)
    {
        var SQLQuery = @"SELECT Animal.ID,Animal.Name,Animal.AdmissionDate FROM Animal WHERE Animal.ID = @ID";
        
        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand();
        
        command.Connection = connection;
        command.CommandText = SQLQuery;
        command.Parameters.AddWithValue("@ID", id);
        
        await connection.OpenAsync();
        
        var reader = await command.ExecuteReaderAsync();

        GetAnimalWithOwnerDTO getAnimalWithOwnerDto = null;

        var idOrdinal = reader.GetOrdinal("ID");
        var nameOrdinal = reader.GetOrdinal("Name");
        var admissonDateOrdinal = reader.GetOrdinal("AdmissionDate");
        var animalclass = GetAnimalClass(id).Result.Name;
        
        while (await reader.ReadAsync())
        {
            if (getAnimalWithOwnerDto == null)
            {
                getAnimalWithOwnerDto= new GetAnimalWithOwnerDTO()
                {
                    ID = reader.GetInt32(idOrdinal),
                    Name = reader.GetString(nameOrdinal),
                    animalClass = animalclass,
                    AdmissionDate = reader.GetDateTime(admissonDateOrdinal),
                    owners = getOwners(id).Result
                    // Genres = GetBookGenres(id).Result.ConvertAll(input => input.Genre)
                };
            }
        }
        
        if (getAnimalWithOwnerDto == null)
        {
            throw new Exception("GetBookWithGenresDto instance is null");
        }

        return getAnimalWithOwnerDto;
    }

    // public async Task<GetAnimalWithOwnerDTO> createNewAnimal(CreateNewAnimalDTO createNewAnimal)
    // {
    //     var sqlQuery = @"INSERT INTO Animal VALUES(@Name, @AdmissionDate, @OwnerId);
				// 	   SELECT @@IDENTITY AS ID;";
    //     
    //     await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
    //     await using SqlCommand command = new SqlCommand();
	   //  
    //     command.Connection = connection;
    //     command.CommandText = sqlQuery;
    //
    //     command.Parameters.AddWithValue("@Name", CreateNewAnimalDTO.Name);
    //     command.Parameters.AddWithValue("@AnimalClass", CreateNewAnimalDTO.animalClassName);
    //     command.Parameters.AddWithValue("@AdmissionDate", CreateNewAnimalDTO.AdmissionDate);
    //     command.Parameters.AddWithValue("@OwnerId", CreateNewAnimalDTO.OwnerID);
    //     
    //     await connection.OpenAsync();
    //     
    //     var transaction = await connection.BeginTransactionAsync();
    //     command.Transaction = transaction as SqlTransaction;
    //     
    //     try
    //     {
    //         var id = await command.ExecuteScalarAsync();
    //
    //         foreach (var procedure in CreateNewAnimalDTO.Procedures)
    //         {
    //             command.Parameters.Clear();
    //             command.CommandText = "INSERT INTO Procedure_Animal VALUES(@ProcedureId, @Date)";
    //             command.Parameters.AddWithValue("@ProcedureId", procedure.ProcedureId);
    //             command.Parameters.AddWithValue("@Date", procedure.Date);
    //
    //             await command.ExecuteNonQueryAsync();
    //         }
    //
    //         await transaction.CommitAsync();
    //     }
    //     catch (Exception)
    //     {
    //         await transaction.RollbackAsync();
    //         throw;
    //     }
    //     
    // }

}
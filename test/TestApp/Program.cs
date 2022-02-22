using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Service.Core.Client.Models;
using Service.Grpc;
using Service.ServerKeyValue.Client;
using Service.ServerKeyValue.Grpc;
using Service.ServerKeyValue.Grpc.Models;
using GrpcClientFactory = ProtoBuf.Grpc.Client.GrpcClientFactory;

namespace TestApp
{
	internal class Program
	{
		private static async Task Main()
		{
			GrpcClientFactory.AllowUnencryptedHttp2 = true;

			Console.Write("Press enter to start");
			Console.ReadLine();

			ILogger<Program> logger = LoggerFactory.Create(x => x.AddConsole()).CreateLogger<Program>();

			var factory = new ServerKeyValueClientFactory("http://localhost:5001", logger);
			IGrpcServiceProxy<IServerKeyValueService> client = factory.GetServerKeyValueRepository();
			IServerKeyValueService clientService = client.Service;

			var userId1 = Guid.NewGuid();

			Console.WriteLine($"{Environment.NewLine}Put new values to user {userId1}");
			CommonGrpcResponse putResult1 = await clientService.Put(new ItemsPutGrpcRequest
			{
				UserId = userId1,
				Items = new[]
				{
					new KeyValueGrpcModel {Key = "key1", Value = "{\"id\": \"value1\"}"},
					new KeyValueGrpcModel {Key = "key2", Value = "{\"id\": \"value2\"}"},
					new KeyValueGrpcModel {Key = "key3", Value = "{\"id\": \"value3\"}"}
				}
			});
			LogData(putResult1);

			var userId2 = Guid.NewGuid();

			Console.WriteLine($"{Environment.NewLine}Put new values to user {userId2}");
			CommonGrpcResponse putResult2 = await clientService.Put(new ItemsPutGrpcRequest
			{
				UserId = userId2,
				Items = new[]
				{
					new KeyValueGrpcModel {Key = "key1", Value = "{\"id\": \"value4\"}"},
					new KeyValueGrpcModel {Key = "key2", Value = "{\"id\": \"value5\"}"},
					new KeyValueGrpcModel {Key = "key3", Value = "{\"id\": \"value6\"}"}
				}
			});
			LogData(putResult2);

			Console.WriteLine($"{Environment.NewLine}Get all user {userId2} keys");
			KeysGrpcResponse keysGrpcResponse = await clientService.GetKeys(new GetKeysGrpcRequest {UserId = userId2});
			LogData(keysGrpcResponse);

			string[] keys1 = {"key1", "key2", "some_key"};
			Console.WriteLine($"{Environment.NewLine}Get values for user {userId1}, keys {JsonSerializer.Serialize(keys1)}");
			ItemsGrpcResponse getResult1 = await clientService.Get(new ItemsGetGrpcRequest {UserId = userId1, Keys = keys1});
			LogData(getResult1);

			string[] keys2 = {"key2", "key3"};
			Console.WriteLine($"{Environment.NewLine}Get values for user {userId2}, keys {JsonSerializer.Serialize(keys2)}");
			ItemsGrpcResponse getResult2 = await clientService.Get(new ItemsGetGrpcRequest {UserId = userId2, Keys = keys2});
			LogData(getResult2);

			string[] keys3 = {"key1", "key3"};
			Console.WriteLine($"{Environment.NewLine}Delete values for user {userId1}, keys {JsonSerializer.Serialize(keys3)}");
			CommonGrpcResponse deletetResult = await clientService.Delete(new ItemsDeleteGrpcRequest {UserId = userId1, Keys = keys3});
			LogData(deletetResult);

			string[] keys4 = {"key1", "key2", "key3"};
			Console.WriteLine($"{Environment.NewLine}Get values for user {userId1}, keys {JsonSerializer.Serialize(keys4)}");
			ItemsGrpcResponse getResult3 = await clientService.Get(new ItemsGetGrpcRequest {UserId = userId1, Keys = keys4});
			LogData(getResult3);

			Console.WriteLine($"{Environment.NewLine}Get one value for user {userId1}, key: key2");
			ValueGrpcResponse getResult5 = await clientService.GetSingle(new ItemsGetSingleGrpcRequest {UserId = userId1, Key = "key2"});
			LogData(getResult5);

			Console.WriteLine($"{Environment.NewLine}Change values for user {userId2}, keys {JsonSerializer.Serialize(keys2)}");
			CommonGrpcResponse putResult3 = await clientService.Put(new ItemsPutGrpcRequest
			{
				UserId = userId2,
				Items = new[]
				{
					new KeyValueGrpcModel {Key = "key2", Value = "{\"id\": \"new_value4\"}"},
					new KeyValueGrpcModel {Key = "key3", Value = "{\"id\": \"new_value5\"}"}
				}
			});
			LogData(putResult3);

			Console.WriteLine($"{Environment.NewLine}Get values for user {userId2}, keys {JsonSerializer.Serialize(keys2)}");
			ItemsGrpcResponse getResult4 = await clientService.Get(new ItemsGetGrpcRequest {UserId = userId2, Keys = keys2});
			LogData(getResult4);

			Console.WriteLine("End");
			Console.ReadLine();
		}

		private static void LogData(object data) => Console.WriteLine(JsonSerializer.Serialize(data));
	}
}
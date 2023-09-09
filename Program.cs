using System;
using System.Threading;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using System.Text;

using MQTTnet.Protocol;
class Program
{
	static async Task Main(string[] args)
	{
		var factory = new MqttFactory();
		var mqttClient = factory.CreateMqttClient();

		var options = new MqttClientOptionsBuilder()
		.WithClientId("emqx_NTE2O")
		.WithTcpServer("44.215.98.9")
		.Build();



		mqttClient.ConnectAsync(options).Wait();

		if (mqttClient.IsConnected)
		{
			Console.WriteLine("MQTT Client is connected");
		}
		else
		{
			Console.WriteLine("MQTT Client failed to connect");
		}


		var message = new MqttApplicationMessageBuilder()
			   .WithTopic("zigbee2mqtt/Iroda")
			   .WithPayload("Hello World")
			   .WithRetainFlag()
			   .Build();

		mqttClient.PublishAsync(message);
		// #endreg

		// gion Subscribe
		mqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic("zigbee2mqtt/Iroda").Build());
		// dregion



		// gion Incoming Message Handling

		// mqttClient.UseApplicationMessageReceivedHandler(async e =>
		// {
		// 	Console.WriteLine("### RECEIVED APPLICATION MESSAGE ###");
		// 	Console.WriteLine($"+Topic = {e.ApplicationMessage.Topic}");
		// 	Console.WriteLine($"+Payload = {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
		// 	Console.WriteLine($"+QoS = {e.ApplicationMessage.QualityOfServiceLevel}");
		// 	Console.WriteLine($"+ Retain = {e.ApplicationMessage.Retain}");
		// 	Console.WriteLine();

		// 	Task.Run(() => Client.PublishAsync("hello/world"));
		// });



		// mqttClient.UseApplicationMessageReceivedHandler(e =>
		// {
		// try
		// {
		// string topic = e.ApplicationMessage.Topic;
		// 
		// if (string.IsNullOrWhiteSpace(topic) == false)
		// {
		// string payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
		// Console.WriteLine($"Topic: {topic}. Message Received: {payload}");
		// }
		// }
		// catch (Exception ex)
		// {
		// Console.WriteLine(ex.Message, ex);
		// }
		// });

		Console.ReadLine();
	}
}
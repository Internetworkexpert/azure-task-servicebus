using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace INE.Tasks.SBQueue
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connection = "";
            var queue = "tasks";
            var client = new ServiceBusClient(connection);
            var sender = client.CreateSender(queue);
            var receiver = client.CreateReceiver(queue);

            //IQueueClient client = new QueueClient(connection, queue, ReceiveMode.PeekLock);
            foreach(var request in CourseRequest.GetSampleRequests()) {
                var msg = new ServiceBusMessage(request.Serialize());
                await sender.SendMessageAsync(msg);
            }
            foreach(var msg in await receiver.ReceiveMessagesAsync(20)) {
                var request = CourseRequest.Deserialize(msg.Body.ToArray());
                Console.WriteLine(request.ToString());
                await receiver.CompleteMessageAsync(msg);
            }
           

        }

    }
}

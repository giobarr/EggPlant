using System;
using System.Threading.Tasks;
using Apache.NMS;
using Apache.NMS.ActiveMQ;

class Program
{
    static async Task Main()
    {
        // Randomly generate 0 or 1 (egg type A or B)
        Random random = new Random();
        int value = random.Next(0, 2);

        Console.WriteLine($"Balance detected egg type: {value}");

        // Configure AMQ connection (adjust URI, queue name, credentials)
        string brokerUri = "tcp://localhost:61616"; // default ActiveMQ port
        string queueName = "balance.queue";

        IConnectionFactory factory = new ConnectionFactory(brokerUri);

        using IConnection connection = factory.CreateConnection("admin", "admin"); // adjust user/pass
        connection.Start();

        using ISession session = connection.CreateSession(AcknowledgementMode.AutoAcknowledge);
        IDestination destination = session.GetQueue(queueName);

        using IMessageProducer producer = session.CreateProducer(destination);
        producer.DeliveryMode = MsgDeliveryMode.NonPersistent;

        // Send the message
        ITextMessage message = session.CreateTextMessage(value.ToString());
        producer.Send(message);

        Console.WriteLine($"Sent {value} to AMQ queue '{queueName}'");

        // Small delay to ensure message is flushed
        await Task.Delay(500);
    }
}
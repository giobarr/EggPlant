using System;
using Apache.NMS;
using Apache.NMS.ActiveMQ;

class Program
{
    static void Main()
    {
        string brokerUri = "tcp://localhost:61616"; // adjust if needed
        string smartVisionQueue = "quality.queue";
        string balanceQueue = "balance.queue";

        IConnectionFactory factory = new ConnectionFactory(brokerUri);

        using IConnection connection = factory.CreateConnection("admin", "admin"); // adjust credentials
        connection.Start();

        using ISession session = connection.CreateSession(AcknowledgementMode.AutoAcknowledge);
        IDestination smartVisionDest = session.GetQueue(smartVisionQueue);
        IDestination balanceDest = session.GetQueue(balanceQueue);

        using IMessageConsumer smartVisionConsumer = session.CreateConsumer(smartVisionDest);
        using IMessageConsumer balanceConsumer = session.CreateConsumer(balanceDest);

        Console.WriteLine("Sorter is listening for SmartVision and Balance results...");

        while (true)
        {
            // Receive one message from each queue
            ITextMessage smartVisionMsg = smartVisionConsumer.Receive() as ITextMessage;
            ITextMessage balanceMsg = balanceConsumer.Receive() as ITextMessage;

            if (smartVisionMsg != null && balanceMsg != null)
            {
                string smartVisionValue = smartVisionMsg.Text;
                string balanceValue = balanceMsg.Text;

                Console.WriteLine($"Sorter received SmartVision={smartVisionValue}, Balance={balanceValue}");

                string combined = smartVisionValue + balanceValue;

                // Decide which transfer line to open
                switch (combined)
                {
                    case "00":
                        Console.WriteLine("Opening transfer 1 (00)");
                        break;
                    case "01":
                        Console.WriteLine("Opening transfer 2 (01)");
                        break;
                    case "10":
                        Console.WriteLine("Opening transfer 2 (10)");
                        break;
                    case "11":
                        Console.WriteLine("Opening transfer 2 (11)");
                        break;
                    default:
                        Console.WriteLine("No transfer action for this combination.");
                        break;
                }
            }
        }
    }
}
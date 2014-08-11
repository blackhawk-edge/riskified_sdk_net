﻿using Riskified.SDK.Logging;

namespace Riskified.SDK.Sample
{
    static class Program
    {
        static void Main(string[] args)
        {
            #region logger setup [Optional]

            // setting up a logger facade to the system logger using the ILog interface
            // if a logger facade is created it will enable a peek into the logs created by the SDK and will help understand issues easier
            var logger = new SimpleExampleLogger();
            LoggingServices.InitializeLogger(logger);

            #endregion


            # region notification example
            
            NotificationServerExample.ReceiveNotificationsExample();

            #endregion

            #region orders example
            
            OrderTransmissionExample.SendOrdersToRiskifiedExample();

            #endregion


            // make sure to shut down the notifications server when done
            NotificationServerExample.StopNotificationServer();
            
        }
    }
}
